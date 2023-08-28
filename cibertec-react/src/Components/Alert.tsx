import React, { ReactChildren } from "react";

export interface IProps {
    // El tipo de la alerta segun BS
    type: "success" | "warning" | "info" | "primary" | "danger";
    // El mensaje de la alerta
    message?: string;
    element?: React.ReactNode;
}

// Los parametros de los componentes se denomina properties
export default function CibertecAlert(props: IProps) {
    return <div className={`alert alert-${props.type}`} role="alert">
        {props.message || "Mensaje no asignado"}
        {props.element}
    </div>
}