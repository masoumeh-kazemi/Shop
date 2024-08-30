using Common.Application;
using MediatR;
using Shop.Application.Orders.AddItem;
using Shop.Application.Orders.Checkout;
using Shop.Application.Orders.DecreaseItemCount;
using Shop.Application.Orders.Finally;
using Shop.Application.Orders.IncreaseItemCount;
using Shop.Application.Orders.RemoveItem;
using Shop.Application.Orders.SendOrder;
using Shop.Query.Orders.DTOs;
using Shop.Query.Orders.GetByFilter;
using Shop.Query.Orders.GetById;
using Shop.Query.Orders.GetCurrent;

namespace Shop.Presentation.Facade.Orders;

public interface IOrderFacade
{
    Task<OperationResult> AddOrderItem(AddOrderItemCommand command);
    Task<OperationResult> RemoveOrderItem(RemoveOrderItemCommand command);
    Task<OperationResult> DecreaseItemCount(DecreaseOrderItemCountCommand command);
    Task<OperationResult> IncreaseOrderItemCount(IncreaseOrderItemCountCommand command);
    Task<OperationResult> Checkout(CheckoutOrderCommand command);
    Task<OperationResult> FinallyOrder(OrderFinallyCommand command);
    Task<OperationResult> SendOrder(SendOrderCommand command);


    Task<OrderDto?> GetOrderById(long id);
    Task<OrderFilterResult> GetOrderByFilter(OrderFilterParams filterParams);
    Task<OrderDto?> GetCurrentOrder(long userId);
}

public class OrderFacade : IOrderFacade
{
    private readonly IMediator _mediator;

    public OrderFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> AddOrderItem(AddOrderItemCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> RemoveOrderItem(RemoveOrderItemCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> DecreaseItemCount(DecreaseOrderItemCountCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> IncreaseOrderItemCount(IncreaseOrderItemCountCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Checkout(CheckoutOrderCommand command)
    {
        return await _mediator.Send(command);
    }

    public Task<OperationResult> FinallyOrder(OrderFinallyCommand command)
    {
        return _mediator.Send(command);
    }

    public Task<OperationResult> SendOrder(SendOrderCommand command)
    {
        return _mediator.Send(command);
    }

    public async Task<OrderDto?> GetOrderById(long id)
    {
        return await _mediator.Send(new GetOrderByIdQuery(id));
    }

    public async Task<OrderFilterResult> GetOrderByFilter(OrderFilterParams filterParams)
    {
        return await _mediator.Send(new GetOrderByFilterQuery(filterParams));
    }

    public async Task<OrderDto?> GetCurrentOrder(long userId)
    {
        return await _mediator.Send(new GetCurrentUserOrderQuery(userId));
    }
}