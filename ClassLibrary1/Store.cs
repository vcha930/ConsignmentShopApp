using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsignmentShopClasses
{
    public class Store
    {
        public string Name { get; set; }
        public List<Vendor> Vendors = new List<Vendor>();
        public List<Item> Items = new List<Item>();

    }
}
