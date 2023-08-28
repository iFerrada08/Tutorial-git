using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace WebDeveloper.DbOperations2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var arregloArtistas = new List<Artist>();
            for (int i = 0; i < 100; i++)
            {
                arregloArtistas.Add(new Artist
                {
                    Name = $"Artista Prueba #{i + 1}",
                    Albums = new List<Album> { 
                        new Album { Title = $"Album 1 de Artista {i + 1}" }, 
                        new Album { Title = $"Album 2 de Artista {i + 1}" } 
                    }
                });
            }

            // Instanciar el contexto de EF
            var options = new DbContextOptionsBuilder<ChinookContext>()
                .UseSqlServer("server=.;database=Chinook;trusted_connection=true;")
                .Options;
            var ctx = new ChinookContext(options);
            ctx.Artists.AddRange(arregloArtistas);

            // Confirmar los cambios
            var resultado = ctx.SaveChanges();

            Console.WriteLine($"Cantidad de registros guardados: {resultado}");
        }
    }
}
