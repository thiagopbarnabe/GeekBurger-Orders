using System;
using System.Collections.Generic;
using System.Text;

namespace GeekBurger.Orders.Contract
{
    public class ProductToUpsert
    {
        public Guid ProductId { get; set; }
        public Decimal Price { get; set; }
    }
}
