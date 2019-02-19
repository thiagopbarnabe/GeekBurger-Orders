using System;
using System.Collections.Generic;
using System.Text;

namespace GeekBurger.Orders.Contract
{
    public class NewOrderMessage
    {
        public Guid OrderId { get; set; }
        public Guid StoreId { get; set; }
        public Decimal Total { get; set; }
        public ProductToUpsert[] Products { get; set; }
        public Guid[] ProductionIds { get; set; }
    }
}
