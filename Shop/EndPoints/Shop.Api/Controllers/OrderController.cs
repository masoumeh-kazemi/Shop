using Common.Application;
using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.Security;
using Shop.Application.Categories.Create;
using Shop.Application.Orders.AddItem;
using Shop.Application.Orders.Checkout;
using Shop.Application.Orders.DecreaseItemCount;
using Shop.Application.Orders.IncreaseItemCount;
using Shop.Application.Orders.RemoveItem;
using Shop.Application.Orders.SendOrder;
using Shop.Domain.OrderAgg;
using Shop.Domain.RoleAgg.Enums;
using Shop.Presentation.Facade.Orders;
using Shop.Query.Categories.DTOs;
using Shop.Query.Orders.DTOs;

namespace Shop.Api.Controllers
{
    [Authorize]
    public class OrderController : ApiController
    {

        private readonly IOrderFacade _orderFacade;

        public OrderController(IOrderFacade orderFacade)
        {
            _orderFacade = orderFacade;
        }

        [PermissionChecker(Permission.Order_Management)]
        [HttpGet]
        public async Task<ApiResult<OrderFilterResult>> GetOrderByFilter([FromQuery] OrderFilterParams filterParams)
        {
            var result = await _orderFacade.GetOrderByFilter(filterParams);
            return QueryResult(result);
        }

        [HttpGet("current/filter")]
        public async Task<ApiResult<OrderFilterResult>> GetOrderByFilter(int pageId=1, int take=10,OrderStatus? status = null)
        {
            var result = await _orderFacade.GetOrderByFilter(new OrderFilterParams()
            {
                PageId = pageId,
                Take = take,
                Status = status,
                EndDate = null,
                StartDate = null,
                UserId = User.GetUserId()
            });
            return QueryResult(result);
        }

        [HttpGet("{orderId}")]
        public async Task<ApiResult<OrderDto?>> GetOrderById(long orderId)
        {
            var result = await _orderFacade.GetOrderById(orderId);
            return QueryResult(result);
        }

        [HttpGet("current")]
        public async Task<ApiResult<OrderDto?>> GetCurrentOrder()
        {
            var result = await _orderFacade.GetCurrentOrder(User.GetUserId());
            var orderResult = QueryResult(result);
            return orderResult;
        }

        [HttpPost]
        public async Task<ApiResult> AddOrderItem(AddOrderItemCommand command)
        {
            var result = await _orderFacade.AddOrderItem(command);
            return CommandResult(result);

        }

        [HttpPost("Checkout")]
        public async Task<ApiResult> CheckoutOrder(CheckoutOrderCommand command)
        {
            var result = await _orderFacade.Checkout(command);
            return CommandResult(result);
        }

        [HttpPut("sendOrder/{orderId}")]
        public async Task<ApiResult> SendOrder(long orderId)
        {
            var result = await _orderFacade.SendOrder(new SendOrderCommand(orderId));
            return CommandResult(result);
        }


        [HttpPut("OrderItem/IncreaseCount")]
        public async Task<ApiResult> IncreaseOrderItemCount(IncreaseOrderItemCountCommand command)
        {
            var result = await _orderFacade.IncreaseOrderItemCount(command);
            return CommandResult(result);
        }

        [HttpPut("OrderItem/DecreaseCount")]
        public async Task<ApiResult> DecreaseOrderItemCount(DecreaseOrderItemCountCommand command)
        {
            var result = await _orderFacade.DecreaseItemCount(command);
            return CommandResult(result);
        }

        [HttpDelete("OrderItem/{itemId}")]
        public async Task<ApiResult> RemoveOrderItem(long itemId)
        {
            var result = await _orderFacade.RemoveOrderItem(new RemoveOrderItemCommand(User.GetUserId(), itemId));
            return CommandResult(result);
        }
    }
}
