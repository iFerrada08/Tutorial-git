using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDeveloper.Core.Interfaces;
using WebDeveloper.Core.ViewModels;
using WebDeveloper.Infra.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebDeveloper.Mvc.Controllers
{
    public class ReportesController : Controller
    {
        private readonly IChinookContext _context;
        private readonly IReportesService _reportesService;
        public ReportesController(IChinookContext context, IReportesService reportesService)
        {
            _context = context;
            _reportesService = reportesService;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [Route("/reportes/albumes-por-artista")]
        public IActionResult AlbumesPorArtista()
        {
            // Obtener el reporte de BD usando EF
            var model = _reportesService.ObtenerAlbumPorArtista();
            return View(model);
        }

        [Route("/reportes/albumes-por-artista-sp")]
        public async Task<IActionResult> AlbumesPorArtistaSP()
        {
            var items = await _context.ArtistCounts.FromSqlRaw("sp_algo").ToListAsync();

            // Crear el objeto view model que enviaremos a la vista
            var model = new AlbumPorArtistaViewModel
            {
                Title = $"Se tienen {items.Count}",
                Items = items.Select(a => new AlbumPorArtistaItemViewModel
                {
                    ArtistId = a.ArtistId,
                    ArtistName = a.Nombre,
                    CantidadAlbumes = a.Cantidad
                }).ToList()
            };
            return View("AlbumesPorArtista", model);
        }
    }
}
