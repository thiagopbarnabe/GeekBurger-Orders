using AutoMapper;
using GeekBurger.Orders.Contract;
using GeekBurger.Orders.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekBurger.Orders.Service
{
    public class PaymentService : IPaymentService
    {   
        IOrdersRepository _ordersRepository;
        IMapper _mapper;
        ICreditCardService _creditCardService;
        public PaymentService(IOrdersRepository ordersRepository, IMapper mapper, ICreditCardService creditCardService)
        {   
            _ordersRepository = ordersRepository;
            _mapper = mapper;
            _creditCardService = creditCardService;
        }
        public void Pay(PaymentToUpsert paymentToUpsert)
        {
            var order = _ordersRepository.GetOrderById(paymentToUpsert.OrderId);

            if (_creditCardService.Pay())
                order.State = "Paid";
            else
                order.State = "Canceled";

            _ordersRepository.Save();
        }
    }
}
