import React from "react";
import { IViewProps, IUser } from "./_types";

export interface IHomeProps extends IViewProps {
    user?: IUser;
}

export interface IUserProfileProps {
    user: IUser;
}

export function UserProfile(props: IUserProfileProps) {
    return <div>
        <dl className="row">
            <dt className="col-sm-3">Id</dt>
            <dd className="col-sm-9">{props.user.userId}</dd>
            <dt className="col-sm-3">Nombre</dt>
            <dd className="col-sm-9">{props.user.name}</dd>
            <dt className="col-sm-3">Email</dt>
            <dd className="col-sm-9">{props.user.email}</dd>
            <dt className="col-sm-3">DNI</dt>
            <dd className="col-sm-9">{props.user.dni}</dd>
        </dl>
    </div>
}

export default function Home(props: IHomeProps) {
    return <div>
        {props.user && <UserProfile user={props.user}></UserProfile>}
        <button className="btn btn-sm btn-primary" onClick={() => {
            props.actualizarVista("canciones");
        }}>Ver canciones</button>
    </div>
}