using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDeveloper.SignalRServer.Hubs
{
    public class ChatHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var mensaje = "Nuevo usuario conectado";
            var connId = Context.ConnectionId;
            await Clients.Others.SendAsync("NuevaConexion", mensaje, connId);
        }
        /// <summary>
        /// Este es un evento que los clientes van a llamar para re transmitir algo a los demas usuarios
        /// </summary>
        /// <param name="usuario">El usuario que envia el mensaje</param>
        /// <param name="mensaje">El nuevo mensaje del usuario</param>
        /// <returns></returns>
        public async Task EnviarMensaje(string usuario, string mensaje)
        {
            // Clients.All -> transmite el mensaje a todos los clientes conectados (incluyendo el mismo)
            // Clients.Others -> transmite el mensaje a los otros clientes conectados (no incluye a el mismo)
            await Clients.All.SendAsync("RecibirMensaje", usuario, mensaje);
        }
    }
}
