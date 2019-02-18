using System;
using GeekBurger.Orders.Model;

namespace GeekBurger.Orders.Repository
{
    public interface IOrdersRepository
    {
        Order GetOrderById(Guid orderId);
    }
}