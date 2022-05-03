using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemExBumagaProgramm.Model
{
    public class SortParam
    {
        public SortParam(string title, string property, bool isAscending)
        {
            Title = title;
            Property = property;
            IsAscending = isAscending;
        }

        public string Title { get; set; }
        public string Property { get; set; }
        public bool IsAscending { get; set; }
    }
}
