using System;
using System.Collections.Generic;
using System.Text;

namespace WebDeveloper.Core.ViewModels
{
    public class TrackViewModel
    {
        public int TrackId { get; set; }
        public string Name { get; set; }
        public int? AlbumId { get; set; }
        public int MediaTypeId { get; set; }
        public int? GenreId { get; set; }
        public decimal UnitPrice { get; set; }
        public string AlbumName { get; set; }
        public string GenreName { get; set; }
        public string MediaTypeName { get; set; }
        public string ArtistName { get; set; }
    }
}
