using GeekBurger.Orders.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekBurger.Orders.Extensions
{
    public static class OrdersContextExtensions
    {
        public static void Seed(this OrdersContext context)
        {
            context.Orders.RemoveRange(context.Orders);
            context.Productions.RemoveRange(context.Productions);
            context.Products.RemoveRange(context.Products);

            context.SaveChanges();
        }
    }
}