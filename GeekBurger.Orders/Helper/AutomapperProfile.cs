using AutoMapper;
using GeekBurger.Orders.Contract;
using GeekBurger.Orders.Model;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GeekBurger.Orders
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<OrderToUpsert, Order>();
            CreateMap<Product, ProductToUpsert>();
            CreateMap<OrderToUpsert, Order>();
            CreateMap<PaymentToUpsert, Payment>();
            //CreateMap<Item, ItemToGet>();
            //CreateMap<EntityEntry<Product>, ProductChangedMessage>()
            //.ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Entity));
        }
    }
}
