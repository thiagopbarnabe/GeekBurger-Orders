using GeekBurger.Orders.Model;
using GeekBurger.Orders.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekBurger.Orders.Repository
{
    public class OrdersRepository : IOrdersRepository
    {
        private OrdersContext _context;
        private IOrderChangedService _orderChangedService;
        public OrdersRepository(OrdersContext context, IOrderChangedService orderChangedService)
        {
            _orderChangedService = orderChangedService;
            _context = context;
        }

        public bool Add(Order order)
        {
            order.OrderId = new Guid();
            _context.Add(order);
            return true;
        }

        public void Delete(Order order)
        {
            _context.Orders.Remove(order);
        }

        public Order GetOrderById(Guid orderId)
        {
            return _context.Orders.FirstOrDefault(x => x.OrderId == orderId);
        }

        public void Save()
        {
            _orderChangedService.AddToMessageList(_context.ChangeTracker.Entries<Order>());

            _context.SaveChanges();

            _orderChangedService.SendMessagesAsync();
        }

        public bool Update(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
