import React, { FormEvent, FormEventHandler } from "react";
import { getToken } from "../Services/authService";
import { IViewProps, IUser } from "./_types";
import "./Login.css";

export interface ILoginProps extends IViewProps {
    actualizarUsuario: (user:IUser) => void;
}

export default function Login(props: ILoginProps) {
    const submit = async (e: any) => {
        e.preventDefault();

        const email = e.currentTarget["email"].value;
        const password = e.currentTarget["password"].value;

        // Obtener el nuevo token
        const token = await getToken(email, password);

        // Decodificar el payload del token
        const splittedToken = token.split(".");

        if(!splittedToken || splittedToken.length !== 3) {
            window.localStorage.removeItem("auth.key");
            console.warn("El token no tiene la estructurar correcta");
            return;
        }

        const payloadPart = splittedToken[1];

        const payload = JSON.parse(Buffer.from(payloadPart, "base64").toString("utf8"));

        // Invocar al evento actualizarUsuario de las props
        props.actualizarUsuario({
            name: payload["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"],
            userId: payload["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"],
            dni: payload.DNI,
            email: payload["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"],
        })

        // Guardar el token en el local storage
        window.localStorage.setItem("auth.key", token);

        // Redireccionar a la vista de home
        props.actualizarVista("home");

    }
    return <div>
        <h1 className="titulo-cibertec">Iniciar Sesion</h1>
        <form onSubmit={submit}>
            <input name="email" type="email"></input>
            <input name="password" type="password"></input>
            <input type="submit" value="Iniciar Sesion" />
        </form>
    </div>
}