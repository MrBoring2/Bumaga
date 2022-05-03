using DemExBumagaProgramm.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemExBumagaProgramm.Model
{
    public class ItemWithTitle<T> where T : BaseEntity
    {
        public ItemWithTitle()
        {
        }

        public ItemWithTitle(T item, string title)
        {
            Item = item;
            Title = title;
        }

        public T Item { get; set; }
        public string Title { get; set; }
    }
}
