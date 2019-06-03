using System;
using System.Collections.Generic;
using System.Text;

namespace GeekBurger.Orders.Contract
{
    public class OrderToUpsert
    {
        public Guid OrderId { get; set; }
        public Guid StoreId { get; set; }
        public ProductToUpsert[] Products { get; set; }
        public Guid[] Productions { get; set; }
    }

    public class ProductToUpsert
    {   
        public Guid ProductId { get; set; }
        public Decimal Price { get; set; }
    }
}
