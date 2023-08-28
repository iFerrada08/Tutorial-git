using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDeveloper.Core.Entities;
using WebDeveloper.Core.Interfaces;

namespace WebDeveloper.Mvc.Controllers
{
    public class TracksController : Controller
    {
        private readonly IChinookContext _chinookContext;
        private readonly ITrackService _trackService;
        private HubConnection _hubConnection;

        public TracksController(IChinookContext chinookContext, ITrackService trackService)
        {
            _chinookContext = chinookContext;
            _trackService = trackService;

            // Inicializar el hubConnection a trackshub
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5002/trackshub")
                .Build();
        }
        public async Task<IActionResult> Index()
        {
            // Invocar al metodo para obtener el listado de TrackViewModel
            var modelo = await _trackService.GetListAll();
            return View(modelo);
        }

        public async Task<IActionResult> FormPartial(int trackId)
        {
            var track = new Track();
            if(trackId > 0)
            {
                track = await _chinookContext.Tracks.FindAsync(trackId);
            }

            // Que pasa si el track que se busco, no existe
            if(track == null)
            {
                return null;
            }

            var generosItems = await _chinookContext.Genres
                .OrderBy(g => g.Name)
                .Select(g => new SelectListItem { Value = g.GenreId.ToString(), Text = g.Name })
                .ToListAsync();
            ViewData["generosItems"] = generosItems;
            var albumsItems = await _chinookContext.Albums
                .OrderBy(g => g.Title)
                .Select(g => new SelectListItem { Value = g.AlbumId.ToString(), Text = g.Title })
                .ToListAsync();
            ViewData["albumsItems"] = albumsItems;
            var mediaItems = await _chinookContext.MediaTypes
                .OrderBy(g => g.Name)
                .Select(g => new SelectListItem { Value = g.MediaTypeId.ToString(), Text = g.Name })
                .ToListAsync();
            ViewData["mediaItems"] = mediaItems;
            return PartialView("_TrackFormPartial", track);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> SaveAjax(Track newTrack)
        {
            // Validar el modelo
            if(!ModelState.IsValid)
            {
                return new JsonResult(new { success = false });
            }

            try
            {
                // Variable para alamacenar el ID solicitado
                var tempTrackId = newTrack.TrackId;
                // Si el trackId del param newTrack es 0
                if(newTrack.TrackId == 0)
                {
                    // Insertar un nuevo track
                    await _chinookContext.Tracks.AddAsync(newTrack);
                }
                else if(newTrack.TrackId > 0)
                {
                    // Actualizar un track existente
                    // Trata de obtener la entidad de BD
                    var currentTrack = await _chinookContext.Tracks.FindAsync(newTrack.TrackId);
                    if(currentTrack == null)
                    {
                        throw new Exception("No se encontro el track que se queria editar");
                    }

                    // Setear solo los campos que queremos actualizar
                    currentTrack.Name = newTrack.Name;
                    currentTrack.GenreId = newTrack.GenreId;
                    currentTrack.AlbumId = newTrack.AlbumId;
                    currentTrack.MediaTypeId = newTrack.MediaTypeId;
                    currentTrack.UnitPrice = newTrack.UnitPrice;
                }

                // Guardar los cambios
                var saveResult = _chinookContext.SaveChanges();

                if(saveResult <= 0)
                {
                    throw new Exception("Ningun registro fue afectado");
                }

                // Notificar el registro al trackshub
                if(tempTrackId == 0)
                {
                    // Solo notificar cuando se registra una nueva entidad
                    await _hubConnection.StartAsync();
                    await _hubConnection.InvokeAsync("NotificarRegistro", newTrack.TrackId, newTrack.Name);
                }
                
                // Devolver la respuesta satisfactoria
                return new JsonResult(new { success = true });
            }
            catch (Exception ex)
            {
                // loggear el error
                throw;
            }
        }
    }
}
