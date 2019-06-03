using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekBurger.Orders.Contract;
using Microsoft.AspNetCore.Mvc;

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
    [Route("api/pay")]
    public class PaymentController : Controller
    {

        private IOrdersRepository _ordersRepository;
        private IMapper _mapper;

        public PaymentController(IOrdersRepository ordersRepository, IMapper mapper)
        {
            _ordersRepository = ordersRepository;
            _mapper = mapper;
        }

        [HttpPost()]
        public IActionResult Pay([FromBody] PaymentToUpsert paymentToAdd)
        {
            if (paymentToAdd == null)
                return BadRequest();

            var payment = _mapper.Map<Payment>(paymentToAdd);
            if (payment.StoreId == Guid.Empty)
                return new UnprocessableEntityResult(ModelState);

            _ordersRepository.Add(payment);
            _ordersRepository.Save();

            var orderToGet = _mapper.Map<OrderToGet>(payment);
            return CreatedAtRoute(new { OrderId = orderToGet.OrderId }, orderToGet);
        }
    }
}



