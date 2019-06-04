using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekBurger.Orders.Service
{
    public class CreditCardService : ICreditCardService
    {
        private static int count;
        public CreditCardService()
        {
            count = 0;
        }
        public bool Pay()
        {
            count++;
            return !(count % 5 == 0);
        }
    }
}
