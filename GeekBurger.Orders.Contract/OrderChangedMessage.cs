using System;
using System.Collections.Generic;
using System.Text;

namespace GeekBurger.Orders.Contract
{
    public class OrderChangedMessage
    {   
        public OrderToGet Order{ get; set; }
        public string State { get; set; }
    }
}
