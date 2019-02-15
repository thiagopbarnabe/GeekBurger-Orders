using System;
using System.Collections.Generic;
using System.Text;

namespace GeekBurger.Orders.Contract
{
    public class PaymentToUpsert
    {
        public Guid OrderId { get; set; }
        public Guid StoreId { get; set; }
        public string PayType { get; set; }
        public string CardNumber { get; set; }
        public string CardOwnerName { get; set; }
        public string SecurityCode { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Guid RequesterId { get; set; }
    }
}
