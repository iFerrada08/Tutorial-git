import React from "react";
import { HubConnectionBuilder, HubConnection } from "@microsoft/signalr";
import CibertecAlert from "./Alert";

const CHAT_HUB_URL = `${process.env.REACT_APP_SIGNALR_URL}/chathub`;

export interface IMensajeChat {
    usuario: string;
    mensaje: string;
}

export default function Chat() {
    // Crear el state para guardar la conexion a signalR
    const [hubConnection, setHubConnection] = React.useState<HubConnection>();
    // Crear otro state que administre los mensajes del chat
    const [mensajes, setMensajes] = React.useState<IMensajeChat[]>([]);
    // Crear otro estado para manejar los mensajes nuevos
    const [nuevoMensaje, setNuevoMensaje] = React.useState<IMensajeChat>();
    // Crear un state para mostrar la alerta de usuario conectado
    const [mostrarAlertaConexion, setMostrarAlertaConexion] = React.useState<boolean>(false);

    // Crear un efecto que se ejecute cuando se cambie el valor de nuevoMensaje
    React.useEffect(() => {
        if (nuevoMensaje) {
            setMensajes([...mensajes, nuevoMensaje]);
        }
    }, [nuevoMensaje])

    // Utilizar un useEffect para inicializar la conexion al Hub cuando carga el componente
    React.useEffect(() => {
        // Crear una nueva instancia de la conexion
        const newConnection = new HubConnectionBuilder().withUrl(CHAT_HUB_URL).build();

        // Configurar los metodos que son enviados por el servidor de SignalR
        newConnection.on("recibirMensaje", function (usuario: string, mensaje: string) {
            console.info("evento recibido", { usuario, mensaje });

            // Agregar el nuevo mensaje (objeto) a la lista de mensajes del state
            // Para ello, usar el nuevoMensaje del state
            setNuevoMensaje({ usuario, mensaje });
        });

        newConnection.on("nuevaConexion", function (mensaje: string, connId: string) {
            console.info("nueva conexion", { mensaje, connId });
            setMostrarAlertaConexion(true);
        })

        // Iniciar la conexion
        newConnection.start().catch(e => console.error(e));

        // Guardar la conexion en el state del componente
        setHubConnection(newConnection);
    }, []);

    const onHandleFormSubmit = (e: any) => {
        e.preventDefault();

        // Obtener el usuario y mensaje
        const usuario = e.target["usuario"].value;
        const mensaje = e.target["mensaje"].value;

        if (hubConnection) {
            hubConnection.send("enviarMensaje", usuario, mensaje)
                .then(() => {
                    console.log("Se envio el mensaje");
                })
        }
    }

    return <div>
        <h2>Bienvenido al Chat de Cibertec</h2>
        {mostrarAlertaConexion && <CibertecAlert type="info" message="Nuevo usuario conectado"></CibertecAlert>}
        <ul>
            {
                mensajes.map((m, index) => {
                    return <li key={index}>
                        <b>{m.usuario}</b>: {m.mensaje}
                    </li>
                })
            }
        </ul>
        <form onSubmit={onHandleFormSubmit}>
            <input type="text" name="usuario" />
            <input type="text" name="mensaje" />
            <button type="submit">Enviar</button>
        </form>
    </div>
}