using System;
using System.Collections.Generic;
using System.Text;
using WebDeveloper.Core.ViewModels;

namespace WebDeveloper.Core.Interfaces
{
    public interface IReportesService
    {
        AlbumPorArtistaViewModel ObtenerAlbumPorArtista();
        void ObtenerCancionesMasVendidas();
    }
}
