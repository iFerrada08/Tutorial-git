import React from "react";
import { getTrackList, ITrack } from "../Services/trackService";
import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import CibertecAlert from "./Alert";

// Propiedades del componente CardCancion
export interface ICardCancionProps {
    track: ITrack;
}

export function CardCancion(props: ICardCancionProps) {
    const track = props.track;
    return <div className="card" style={{ width: "20%" }}>
        <div className="card-body">
            <h5 className="card-title">{track.name}</h5>
            <div className="card-text">
                <ul>
                    <li>
                        <strong>Artista</strong>
                        {`: ${track.artistName}`}
                    </li>
                    <li>
                        <strong>Album</strong>
                        {`: ${track.albumName}`}
                    </li>
                    <li>
                        <strong>Genero</strong>
                        {`: ${track.genreName}`}
                    </li>
                    <li>
                        <strong>Tipo</strong>
                        {`: ${track.mediaTypeName}`}
                    </li>
                    <li>
                        <strong>Precio</strong>
                        {`: S/ ${track.unitPrice}`}
                    </li>
                </ul>
            </div>
            <a href="#" className="btn btn-primary">Comprar</a>
        </div>
    </div>
}

const TRACKS_HUB_URL = `${process.env.REACT_APP_SIGNALR_URL}/trackshub`;

export interface INewRecrodNotification {
    trackId: number;
    name: string;
}

export default function Canciones() {
    // Crear el state para guardar la conexion a signalR
    const [hubConnection, setHubConnection] = React.useState<HubConnection>();
    const [showAlertNewRecord, setShowAlertNewRecord] = React.useState<boolean>(false);
    const [newRecordNotification, setNewRecordNotification] = React.useState<INewRecrodNotification>();
    // Crear el state para guardar los items y el nextPage
    const [items, setItems] = React.useState<ITrack[]>([]);
    const [nextPage, setNextPage] = React.useState<number | undefined>();
    const [loading, setLoading] = React.useState(true);

    React.useEffect(() => {
        // Crear una nueva instancia de la conexion
        const newConnection = new HubConnectionBuilder().withUrl(TRACKS_HUB_URL).build();

        newConnection.on("mostrarNotificacion", function (trackId: number, name: string) {
            console.info("nuevo registro", { trackId, name });
            setShowAlertNewRecord(true);
            setNewRecordNotification({ trackId, name });
        })

        // Iniciar la conexion
        newConnection.start().catch(e => console.error(e));

        // Guardar la conexion en el state del componente
        setHubConnection(newConnection);
    }, []);

    // Utilizar los effects
    React.useEffect(() => {
        // Cuando es un [] solo se ejecuta la primera vez que se carga el componente
        // Obtener la data del servicio
        async function loadData() {
            const data = await getTrackList(nextPage);
            console.info("data", data);
            // Setear los items
            setItems(data.items);
            setLoading(false);
            setNextPage(data.nextPage);
        }
        loadData();
    }, []);

    if (loading) {
        return <div>
            Cargando la data.....
        </div>
    }

    const refrescarLista = async () => {
        // obtener el nuevo resultado
        const nuevoResultado = await getTrackList();
        // concatenar los nuevos items a los antiguos
        setItems(nuevoResultado.items);
        setNextPage(nuevoResultado.nextPage);
    }
    return <div>
        <h1>Canciones</h1>
        {showAlertNewRecord && newRecordNotification &&
            <CibertecAlert type="info" message={`Nueva cancion: ${newRecordNotification.name} - Nuevo ID: ${newRecordNotification.trackId}`}
                element={<button className="btn btn-sm btn-default ml-2" onClick={refrescarLista}>Refrescar Lista</button>} />}
        <div className="d-flex flex-wrap">
            {
                items.map(t => { return <CardCancion track={t} key={t.trackId} />; })
            }
            <div>
                <button className="btn btn-primary" onClick={async () => {
                    // obtener el nuevo resultado
                    const nuevoResultado = await getTrackList(nextPage);
                    // concatenar los nuevos items a los antiguos
                    setItems([...items, ...nuevoResultado.items]);
                    setNextPage(nuevoResultado.nextPage);
                }}>Ver mas</button>
            </div>
        </div>

    </div>
}
