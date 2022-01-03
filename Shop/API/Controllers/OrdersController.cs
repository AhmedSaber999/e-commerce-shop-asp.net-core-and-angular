using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.DataShape;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService orderService;
        private readonly IMapper mapper;

        public OrdersController(IOrderService orderService, 
            IMapper mapper)
        {
            this.orderService = orderService;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrderAsync(OrderDto orderDto)
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();

            var address = mapper.Map<OrderAddressDto, Address>(orderDto.ShipToAddress);

            var order = await orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address);
        
            if(order == null) return BadRequest(new ApiResponse(400, "Proplem while creating order"));

            return Ok(order);
            
        }

        [HttpGet]
        public ActionResult<IReadOnlyList<OrderDto>> GetOrdersForUser()
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();
            var orders = orderService.GetOrdersForUser(email);

            return Ok(mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderDto>>(orders));
        }

        
        [HttpGet("{id}")]
        public ActionResult<OrderDto> GetOrderById(int id)
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();
            var order = orderService.GetOrderById(id, email);

            if(order == null) return NotFound(new ApiResponse(404));

            return Ok(mapper.Map<Order, OrderDto>(order));
        }

        [HttpGet("deliveryMethods")]
        public ActionResult<IReadOnlyList<DeliveryMethod>> GetDeliveryMethods()
        {
            return Ok(orderService.GetDeliveryMethods());
        }


    }
}