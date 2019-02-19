using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GeekBurger.Orders.Contract;
using GeekBurger.Orders.Helper;
using GeekBurger.Orders.Model;
using GeekBurger.Orders.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GeekBurger.Orders.Controllers
{
    [Route("api/order")]
    public class OrderController : Controller
    {

        private IOrdersRepository _ordersRepository;
        private IMapper _mapper;

        public OrderController(IOrdersRepository ordersRepository, IMapper mapper)
        {
            _ordersRepository = ordersRepository;
            _mapper = mapper;
        }

        [HttpPost()]
        public IActionResult CreateOrder([FromBody] OrderToUpsert orderToAdd)
        {
            if (orderToAdd == null)
                return BadRequest();

            var order = _mapper.Map<Order>(orderToAdd);
            if (order.StoreId == Guid.Empty)
                return new UnprocessableEntityResult(ModelState);

            _ordersRepository.Add(order);
            _ordersRepository.Save();

            var orderToGet = _mapper.Map<OrderToGet>(order);
            return CreatedAtRoute("Order", new { OrderId = orderToGet.OrderId }, orderToGet);
        }

        [HttpGet()]
        public IActionResult GetOrder(Guid OrderId)
        {
            var order = _ordersRepository.GetOrderById(OrderId);
            var orderToGet = Mapper.Map<OrderToGet>(order);
            return Ok(orderToGet);
        }
    }
}