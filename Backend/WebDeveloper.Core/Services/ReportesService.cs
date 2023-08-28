using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebDeveloper.Core.Interfaces;
using WebDeveloper.Core.ViewModels;

namespace WebDeveloper.Core.Services
{
    public class ReportesService : IReportesService
    {
        private readonly IChinookContext _context;
        public ReportesService(IChinookContext context)
        {
            _context = context;
        }
        public AlbumPorArtistaViewModel ObtenerAlbumPorArtista()
        {
            var query = _context.Artists.Select(a => new AlbumPorArtistaItemViewModel
            {
                ArtistId = a.ArtistId,
                ArtistName = a.Name,
                CantidadAlbumes = a.Albums.Count()
            });

            return new AlbumPorArtistaViewModel
            {
                Items = query.ToList(),
                Title = $"Se obtuvieron {query.Count()} registros"
            };

        }

        public void ObtenerCancionesMasVendidas()
        {
            throw new NotImplementedException();
        }
    }

    public class ReportesServiceSP : IReportesService
    {
        public AlbumPorArtistaViewModel ObtenerAlbumPorArtista()
        {
            throw new NotImplementedException();
        }

        public void ObtenerCancionesMasVendidas()
        {
            throw new NotImplementedException();
        }
    }
}
