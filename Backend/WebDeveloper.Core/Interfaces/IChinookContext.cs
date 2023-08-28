using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebDeveloper.Core.Entities;

namespace WebDeveloper.Core.Interfaces
{
    public interface IChinookContext
    {
        DbSet<Artist> Artists { get; set; }
        DbSet<Album> Albums { get; set; }
        DbSet<ArtistCount> ArtistCounts { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Track> Tracks { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<MediaType> MediaTypes { get; set; }
        int SaveChanges();
    }
}
