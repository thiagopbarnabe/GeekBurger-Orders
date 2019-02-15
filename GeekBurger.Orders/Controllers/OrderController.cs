using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekBurger.Orders.Contract;
using Microsoft.AspNetCore.Mvc;

namespace GeekBurger.Orders.Controllers
{
    [Route("api/order")]
    public class OrderController : Controller
    {   
        [HttpPost()]
        public IActionResult CreateOrder([FromBody] OrderToUpsert orderToAdd)
        {
            var orderToGet = new OrderToGet();
            orderToGet.OrderId = orderToAdd.OrderId;
            orderToGet.StoreId = orderToAdd.StoreId;
            orderToGet.Total = orderToAdd.Products.Sum(x => x.Price);

            return CreatedAtRoute("Order",
                new { OrderId = orderToGet.OrderId },
                orderToGet);
        }
        
        [HttpGet()]
        public IActionResult GetOrder(Guid OrderId)
        {
            var orderToGet = new OrderToGet();
            orderToGet.OrderId = OrderId;

            return Ok(orderToGet);
        }
    }
}