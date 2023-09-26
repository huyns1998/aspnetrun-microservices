using AutoMapper;
using Ordering.Application.Features.Commands.CheckoutOrder;
using Ordering.Application.Features.Commands.UpdateOrder;
using Ordering.Application.Features.Queries.GetOrdersList;
using Ordering.Domain.Entities;

namespace Ordering.Application.Mappings
{
    public class MappingProfile: Profile
    {
        public MappingProfile() 
        {
            // Source-->Target
            CreateMap<Order, OrderVm>().ReverseMap();
            CreateMap<CheckoutOrderCommand, Order>().ReverseMap();  
            CreateMap<UpdateOrderCommand, Order>().ReverseMap();  
        } 
    }
}
