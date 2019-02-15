using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekBurger.Orders.Contract;
using Microsoft.AspNetCore.Mvc;

namespace GeekBurger.Orders.Controllers
{
    [Route("api/pay")]
    public class PaymentController : Controller
    {
        [HttpPost()]
        public IActionResult Pay([FromBody] PaymentToUpsert paymentToAdd)
        {
            return Ok();
        }
    }
}