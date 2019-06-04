using GeekBurger.Orders.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekBurger.Orders.Service
{
    public interface IPaymentService
    {
        void Pay(PaymentToUpsert paymentToUpsert);
    }
}
