using System;
using System.Collections.Generic;
using System.Text;

namespace WebDeveloper.Core.ViewModels
{
    public class GetListViewModel<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int NextPage { get; set; }
    }
}
