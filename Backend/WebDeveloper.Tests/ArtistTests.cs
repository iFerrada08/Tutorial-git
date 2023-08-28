using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using WebDeveloper.Infra.Data;
using Xunit;

namespace WebDeveloper.Tests
{
    public class ArtistTests
    {
        private readonly ChinookContext _context;
        public ArtistTests()
        {
            // Instanciar el contexto de EF
            var options = new DbContextOptionsBuilder<ChinookContext>()
                .UseSqlServer("server=.;database=Chinook;trusted_connection=true;")
                .Options;
            _context = new ChinookContext(options);
        }

        [Fact]
        public void LaTablaArtistDebeTenerMasDeUnRegistro()
        {
            var count = _context.Artists.Count();
            Assert.True(count > 0);
        }

        [Fact]
        public void ElArtistaConId1DebeLlamarseACDC()
        {
            // Obtener el artista con ID 1
            var artista = _context.Artists.Find(1);
            Assert.Equal("AC/DC", artista.Name);
        }

        [Fact]
        public void ObtenerArtistasYCantidadDeAlbumesDebeFuncionar()
        {
            var resultado = _context.Artists.Select(a => new { Nombre = a.Name, CantidadAlbumes = a.Albums.Count() });
            // Buscar el resultado para Amy Winehouse
            var amy = resultado.FirstOrDefault(r => r.Nombre == "Amy Winehouse");

            Assert.Equal(2, amy.CantidadAlbumes);
        }

        [Fact]
        public void ObtenerArtistasYCantidadDeAlbumesDebeFuncionarSP()
        {
            var resultado = _context.ArtistCounts.FromSqlRaw("sp_algo").ToList();
            // Buscar el resultado para Amy Winehouse
            var amy = resultado.FirstOrDefault(r => r.Nombre == "Amy Winehouse");

            Assert.Equal(2, amy.Cantidad);
        }
    }
}
