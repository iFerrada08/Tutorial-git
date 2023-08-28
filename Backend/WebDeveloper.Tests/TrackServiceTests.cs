using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebDeveloper.Core.Interfaces;
using WebDeveloper.Core.Services;
using WebDeveloper.Infra.Data;
using Xunit;

namespace WebDeveloper.Tests
{
    public class TrackServiceTests
    {
        private readonly ITrackService _trackService;
        public TrackServiceTests()
        {
            // Instanciar el contexto de EF
            var options = new DbContextOptionsBuilder<ChinookContext>()
                .UseSqlServer("server=.;database=Chinook;trusted_connection=true;")
                .Options;
            _trackService = new TrackService(new ChinookContext(options));
        }

        [Fact]
        public void GetListDebeRetornarElResultadoConNextPage2()
        {
            var resultado = _trackService.GetList(null);
            Assert.Equal(2, resultado.NextPage);
            Assert.Equal(10, resultado.Items.Count());

            // Validar el id del primer elemento
            var primerElemento = resultado.Items.First();
            Assert.Equal(1, primerElemento.TrackId);
        }

        [Fact]
        public void GetListDebeRetornarElResultadoConNextPage2CuandoSeEnviaPage1()
        {
            var resultado = _trackService.GetList(1);
            Assert.Equal(2, resultado.NextPage);
            Assert.Equal(10, resultado.Items.Count());

            // Validar el id del primer elemento
            var primerElemento = resultado.Items.First();
            Assert.Equal(1, primerElemento.TrackId);
        }

        [Fact]
        public void GetListDebeRetornarElResultadoConNextPage3()
        {
            var resultado = _trackService.GetList(2);
            Assert.Equal(3, resultado.NextPage);
            Assert.Equal(10, resultado.Items.Count());

            // Validar el id del primer elemento
            var primerElemento = resultado.Items.First();
            Assert.Equal(11, primerElemento.TrackId);
        }
    }
}
