// function actualizarBody() {
//     // Manipular el DOM (Document Object Model)
//     // Vamos a crear un elemento div
//     const nuevoElemento = document.createElement("div");

//     // Agregar contenido al elemento div
//     nuevoElemento.innerHTML = "Hola mundo con Webpack 2123213";

//     // Agregar este elemento al body del html
//     document.body.appendChild(nuevoElemento);
// }

// actualizarBody();

import $ from "jquery";
import ROOT_URL, { ROOT_URL_PROD } from "./variables";
import "./styles/main.scss";

$(function() {
    $("#otroBoton").on("click", function(e){
        alert("otro");
    })
    // Manejar el evento click del boton guardar
    $("#formArtista").on("submit", function (e) {
        e.preventDefault();

        // Obtener el nombre de artista a insertar
        const nombre = e.target["nombreArtista"].value;
        
        // Hacer la llamada POST para insertar el artista
        $.ajax({
            method: "POST",
            url: `${ROOT_URL}/artists`,
            data: JSON.stringify({ name: nombre }),
            headers: {
                "Content-Type": "application/json"
            }
        }).done(function(data){
            console.log(data);
        }).fail(function(error){
            console.error(error);
        })
    })
    // invocar al servicio web mediante AJAX
    // GET https://localhost:44307/api/reportes/AlbumPorArtista
    $.ajax({
        method: "GET",
        url: `${ROOT_URL}/reportes/AlbumPorArtista`
    }).done(function (data) {
        console.info("respuesta del servicio", data);

        // Recorrer items y crear las filas
        const htmlArregloFilas = [];
        for (const item of data.items) {
            htmlArregloFilas.push(`
                <tr>
                    <td>${item.artistName}</td>
                    <td>${item.cantidadAlbumes}</td>
                </tr>
            `)
        }

        // Obtener el tbdoy de la tabla e insertar el arreglo de filas
        $("#tabla-reporte tbody").html(htmlArregloFilas);
    });

})