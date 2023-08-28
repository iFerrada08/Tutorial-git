using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebDeveloper.Core.Entities;
using WebDeveloper.Core.Interfaces;
using WebDeveloper.Infra.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebDeveloper.Api.Controllers
{
    /// <summary>
    /// Este controlador sirve para manejar los endpoints de artistas
    /// </summary>
    [Route("api/[controller]")]
    public class ArtistsController : Controller
    {
        private readonly IChinookContext _context;
        private readonly ILogger<ArtistsController> _logger;
        public ArtistsController(IChinookContext context, ILogger<ArtistsController> logger)
        {
            _context = context;
            _logger = logger;
        }
        
        /// <summary>
        /// Obtener los valores de artistas
        /// </summary>
        /// <returns>La lista de artistas</returns>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            // Escribir un mensaje en el log
            _logger.LogInformation("Alguien ha visitado el metodo GET de api/artist");

            foreach(var claim in User.Claims)
            {
                _logger.LogInformation($"{claim.Type} - {claim.Value}");
            }

            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            throw new Exception("Este es un error provocado");
        }

        // POST api/<controller>
        [HttpPost]
        public ActionResult<int> Post([FromBody]Artist artist)
        {
            if (string.IsNullOrEmpty(artist.Name)){
                return BadRequest("Nombre no especificado");
            }
            _context.Artists.Add(artist);
            _context.SaveChanges();
            return Ok(artist.ArtistId);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
