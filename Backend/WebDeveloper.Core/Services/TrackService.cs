using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebDeveloper.Core.Entities;
using WebDeveloper.Core.Interfaces;
using WebDeveloper.Core.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace WebDeveloper.Core.Services
{
    public class TrackService : ITrackService
    {
        private readonly IChinookContext _chinookContext;
        public TrackService(IChinookContext chinookContext)
        {
            _chinookContext = chinookContext;
        }

        public GetListViewModel<TrackViewModel> GetList(int? page)
        {
            // Simular que el servicio demora 4 segundos en cargar
            System.Threading.Thread.Sleep(1000);
            // Por ahora el tamanio de pagina sera fijo de 10
            var pageSize = 10;
            if (!page.HasValue || page == 1)
            {
                return new GetListViewModel<TrackViewModel>
                {
                    Items = _chinookContext.Tracks
                            .OrderByDescending(t => t.TrackId)
                            .Take(pageSize)
                            .Select(t => new TrackViewModel
                            {
                                TrackId = t.TrackId,
                                Name = t.Name,
                                AlbumId = t.AlbumId,
                                GenreId = t.GenreId,
                                MediaTypeId = t.MediaTypeId,
                                AlbumName = t.Album.Title,
                                ArtistName = t.Album.Artist.Name,
                                GenreName = t.GenreId.HasValue ? t.Genre.Name : "NA",
                                MediaTypeName = t.MediaType.Name,
                                UnitPrice = t.UnitPrice
                            })
                            .ToList(),
                    NextPage = 2
                };
            }

            if (page > 1)
            {
                // Cuantos registros no debemos considerar
                var skip = (page.Value - 1) * pageSize;
                return new GetListViewModel<TrackViewModel>
                {
                    Items = _chinookContext.Tracks.OrderByDescending(t => t.TrackId).Skip(skip)
                            .Take(pageSize)
                            .Select(t => new TrackViewModel
                            {
                                TrackId = t.TrackId,
                                Name = t.Name,
                                AlbumId = t.AlbumId,
                                GenreId = t.GenreId,
                                MediaTypeId = t.MediaTypeId,
                                AlbumName = t.Album.Title,
                                ArtistName = t.Album.Artist.Name,
                                GenreName = t.GenreId.HasValue ? t.Genre.Name : "NA",
                                MediaTypeName = t.MediaType.Name,
                                UnitPrice = t.UnitPrice
                            })
                            .ToList(),
                    NextPage = page.Value + 1
                };
            }

            return null;
        }

        public async Task<IEnumerable<TrackViewModel>> GetListAll()
        {
            var lista = await _chinookContext.Tracks
                            .Select(t => new TrackViewModel
                            {
                                TrackId = t.TrackId,
                                Name = t.Name,
                                AlbumId = t.AlbumId,
                                GenreId = t.GenreId,
                                MediaTypeId = t.MediaTypeId,
                                AlbumName = t.Album.Title,
                                ArtistName = t.Album.Artist.Name,
                                GenreName = t.GenreId.HasValue ? t.Genre.Name : "NA",
                                MediaTypeName = t.MediaType.Name,
                                UnitPrice = t.UnitPrice
                            })
                            .ToListAsync();
            return lista;
        }
    }
}
