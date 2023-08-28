using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebDeveloper.Core.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebDeveloper.Api.Controllers
{
    [Route("api/[controller]")]
    public class TrackController : Controller
    {
        private readonly ILogger<TrackController> _logger;
        private readonly ITrackService _trackService;
        public TrackController(ITrackService trackService, ILogger<TrackController> logger)
        {
            _trackService = trackService;
            _logger = logger;
        }

        // GET /api/[controller]/
        [HttpGet]
        public IActionResult Get(int? page)
        {
            try
            {
                return Ok(_trackService.GetList(page));
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error interno", ex);
                throw;
            }
        }
    }
}
