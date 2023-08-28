using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using WebDeveloper.Infra.Data;

namespace WebDeveloper.DbOperations
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            // Instanciar el contexto de EF
            var options = new DbContextOptionsBuilder<ChinookContext>()
                .UseSqlServer("server=.;database=Chinook;trusted_connection=true;")
                .Options;
            var context = new ChinookContext(options);
            var albums = context.Albums.ToList();
            foreach (var album in albums)
            {
                Console.WriteLine($"Nombre del album: {album.Title}");
                Console.WriteLine("Nombre del album: " + album.Title);
            }
        }
    }
}
