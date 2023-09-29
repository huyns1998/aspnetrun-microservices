using AutoMapper;
using EventBus.Messages.Events;
using Ordering.Application.Features.Commands.CheckoutOrder;

namespace Ordering.API.Profiles
{
    public class OrderingProfile: Profile
    {
        public OrderingProfile()
        {
            // Source --> Target
            CreateMap<BasketCheckoutEvent, CheckoutOrderCommand>();
        }
    }
}
