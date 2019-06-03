using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekBurger.Orders.Model
{
    public class Product
    {   
        public Guid ProductId { get; set; }
        public Decimal Price { get; set; }

        public Guid OrderId { get; set; }
        public Order Order { get; set; }
    }
}
