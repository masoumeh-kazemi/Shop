using Common.Application;
using Shop.Domain.OrderAgg;
using Shop.Domain.OrderAgg.Repository;
using Shop.Domain.SellerAgg.Repository;

namespace Shop.Application.Orders.DecreaseItemCount;

public record DecreaseOrderItemCountCommand(long UserId, long ItemId, int Count) : IBaseCommand;

public class DecreaseOrderItemCountCommandHandler : IBaseCommandHandler<DecreaseOrderItemCountCommand>
{
    private readonly IOrderRepository _repository;
    private readonly ISellerRepository _sellerRepository;
    public DecreaseOrderItemCountCommandHandler(IOrderRepository repository, ISellerRepository sellerRepository)
    {
        _repository = repository;
        _sellerRepository = sellerRepository;
    }
    public async Task<OperationResult> Handle(DecreaseOrderItemCountCommand request, CancellationToken cancellationToken)
    {
        var currentOrder = await _repository.GetCurrentUserOrder(request.UserId);
        if (currentOrder == null) 
            return OperationResult.NotFound();

        currentOrder.DecreaseItemCount(request.ItemId, request.Count);
        await _repository.Save();
        return OperationResult.Success();

    }
}