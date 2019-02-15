using System;
using System.Collections.Generic;
using System.Text;

namespace GeekBurger.Orders.Contract
{
    public class OrderChangedMessage
    {
        public Guid OrderId { get; set; }
        public Guid StoreId { get; set; }
        public string State { get; set; }
    }
}
