using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Exceptions;
using Ordering.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public DeleteOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository; 
            _mapper = mapper;   
        }

        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var orderTodelete = await _orderRepository.GetByIdAsync(request.Id);

            if(orderTodelete == null)
            {
                Console.WriteLine("Order does not exist in database.");
                throw new NotFoundException(nameof(Order), request.Id);
            }

            await _orderRepository.DeleteAsync(orderTodelete);

            Console.WriteLine($"Order {orderTodelete.Id} is not successfully deleted.");

            return Unit.Value;
        }
    }
}
