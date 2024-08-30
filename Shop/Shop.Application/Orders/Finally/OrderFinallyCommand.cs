using Common.Application;
using MediatR;
using Shop.Domain.OrderAgg.Repository;

namespace Shop.Application.Orders.Finally;

public record OrderFinallyCommand(long OrderId) : IBaseCommand;

public class OrderFinallyCommandHandler : IBaseCommandHandler<OrderFinallyCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMediator _mediator;
    public OrderFinallyCommandHandler(IOrderRepository orderRepository, IMediator mediator)
    {
        _orderRepository = orderRepository;
        _mediator = mediator;
    }
    public async Task<OperationResult> Handle(OrderFinallyCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetTracking(request.OrderId);
        if (order==null)
            return OperationResult.NotFound();

        order.Finally();
        await _orderRepository.Save();
        //foreach (var item in order.DomainEvents)
        //{
        //   await _mediator.Publish(item, cancellationToken);
        //}
        return OperationResult.Success();


    }
}