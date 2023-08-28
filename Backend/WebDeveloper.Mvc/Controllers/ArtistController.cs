using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebDeveloper.Infra.Data;

namespace WebDeveloper.Mvc.Controllers
{
    public class ArtistController : Controller
    {
        private readonly ChinookContext _context;
        public ArtistController(ChinookContext context)
        {
            _context = context;
        }
        public int Index()
        {
            var cantidad = _context.Artists.Count();
            return cantidad;
        }
    }
}