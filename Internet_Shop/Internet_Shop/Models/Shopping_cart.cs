using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Internet_Shop.Models
{
    public class Shopping_cart
    {
        public int Id { get; set; }
        public string Address { get; set; }
        
    }
    public class Product_in_cart
    {
        public int Id { get; set; }
        public int Id_cart { get; set; }
        public int Id_product { get; set; }
        public int Quantity { get; set; }
    }

}
