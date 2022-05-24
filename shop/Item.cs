using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop
{
    internal class Item
    {

        public int Passport { get; set; }
        public int Item_Id { get; set; }
        public int Price { get; set; }
        public DateTime DateDelivery { get; set; }
        public DateTime DateRevaluation { get; set; }
        public DateTime DateSale { get; set; }
    }
}
