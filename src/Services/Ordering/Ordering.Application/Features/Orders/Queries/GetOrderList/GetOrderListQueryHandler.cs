using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistence;

namespace Ordering.Application.Features.Orders.Queries.GetOrderList
{
    public class GetOrderListQueryHandler : IRequestHandler<GetOrderListQuery, List<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrderListQueryHandler(IMapper mapper, IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<List<OrderDto>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetOrderaByUserName(request.UserName);

            var result = _mapper.Map<List<OrderDto>>(orders);

            return result;
        }
    }
}