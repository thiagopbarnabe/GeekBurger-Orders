﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GeekBurger.Orders.Contract
{
    public class OrderToGet
    {
        public Guid OrderId { get; set; }
        public Guid StoreId { get; set; }
        public decimal Total { get; set; }
    }
}
