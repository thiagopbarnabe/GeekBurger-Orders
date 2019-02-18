using GeekBurger.Orders.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekBurger.Orders.Repository
{
    public class OrdersRepository : IOrdersRepository
    {
        private OrdersContext _context;
        public OrdersRepository(OrdersContext context)
        {
            _context = context;
        }

        public Order GetOrderById(Guid orderId)
        {
            return _context.Orders.FirstOrDefault(x => x.OrderId == orderId);
        }
    }
}
