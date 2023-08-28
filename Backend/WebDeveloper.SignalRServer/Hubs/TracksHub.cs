using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDeveloper.SignalRServer.Hubs
{
    public class TracksHub : Hub
    {
        public async Task NotificarRegistro(int trackId, string name)
        {
            await Clients.Others.SendAsync("mostrarNotificacion", trackId, name);
        }
    }
}
