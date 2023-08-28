using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebDeveloper.Core.Interfaces;
using WebDeveloper.Core.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebDeveloper.Api.Controllers
{
    [Route("api/[controller]")]
    public class ReportesController : Controller
    {
        private readonly IReportesService _reportesService;
        public ReportesController(IReportesService reportesService)
        {
            _reportesService = reportesService;
        }

        /// <summary>
        /// La descripcion de la funcion
        /// </summary>
        /// <returns>Lo que retorna la funcion</returns>
        [HttpGet]
        [Route("AlbumPorArtista")]
        //[EnableCors("All")]
        public IActionResult Get()
        {
            var reporte = _reportesService.ObtenerAlbumPorArtista();
            return Ok(reporte);
        }

    }
}
