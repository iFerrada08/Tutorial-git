using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebDeveloper.Core.Entities
{
    public class ArtistCount
    {
        [Key]
        public int ArtistId { get; set; }
        //[Column("STR_NOMBRE")]
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
    }
}
