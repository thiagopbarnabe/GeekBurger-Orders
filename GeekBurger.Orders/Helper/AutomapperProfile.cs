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
            CreateMap<Model.Product, ProductToUpsert>();
            CreateMap<ProductToUpsert, Product>().ForAllOtherMembers(x => x.Ignore());
            CreateMap<ProductionToUpsert, Production>();
            CreateMap<OrderToUpsert, Order>();
            CreateMap<PaymentToUpsert, Payment>();
            CreateMap<EntityEntry<Order>, OrderChangedMessage>()
            .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Entity));


        }
    }
}
