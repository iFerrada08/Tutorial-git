using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebDeveloper.Core.ViewModels
{
    public class AlbumPorArtistaViewModel
    {
        
        public string Title { get; set; }
        public List<AlbumPorArtistaItemViewModel> Items { get; set; }
    }

    public class AlbumPorArtistaItemViewModel
    {
        public int ArtistId { get; set; }
        [Display(Name = "Nombre del Artista")]
        public string ArtistName { get; set; }
        [Display(Name = "Cantidad de Albumes")]
        public int CantidadAlbumes { get; set; }
    }
}
