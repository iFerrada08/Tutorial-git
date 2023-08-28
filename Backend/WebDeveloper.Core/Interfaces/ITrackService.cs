using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebDeveloper.Core.Entities;
using WebDeveloper.Core.ViewModels;

namespace WebDeveloper.Core.Interfaces
{
    public interface ITrackService
    {
        GetListViewModel<TrackViewModel> GetList(int? page);
        Task<IEnumerable<TrackViewModel>> GetListAll();
    }
}
