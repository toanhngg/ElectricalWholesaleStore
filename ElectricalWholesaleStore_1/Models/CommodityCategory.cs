using System;
using System.Collections.Generic;

namespace ElectricalWholesaleStore_1.Models
{
    public partial class CommodityCategory
    {
        public CommodityCategory()
        {
            Commodities = new HashSet<Commodity>();
        }

        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }

        public virtual ICollection<Commodity> Commodities { get; set; }
    }
}
