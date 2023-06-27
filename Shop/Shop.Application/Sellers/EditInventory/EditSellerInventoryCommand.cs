using Common.Application;
using Shop.Domain.SellerAgg.Repository;

namespace Shop.Application.Sellers.EditInventory;

public class EditSellerInventoryCommand : IBaseCommand
{
    public long InventoryId { get; private set; }
    public long SellerId { get; private set; }
    public int Count { get; private set; }
    public int Price { get; private set; }
    public int? DiscountPercentage { get; private set; }

    public EditSellerInventoryCommand(long inventoryId, long sellerId, int count, int price, int? discountPercentage)
    {
        InventoryId = inventoryId;
        SellerId = sellerId;
        Count = count;
        Price = price;
        DiscountPercentage = discountPercentage;
    }
}

public class EditInventoryCommandHandler : IBaseCommandHandler<EditSellerInventoryCommand>
{
    private readonly ISellerRepository _repository;

    public EditInventoryCommandHandler(ISellerRepository repository)
    {
        _repository = repository;
    }
    public async Task<OperationResult> Handle(EditSellerInventoryCommand request, CancellationToken cancellationToken)
    {
        var seller = await _repository.GetTracking(request.SellerId);
        if (seller == null) 
            return OperationResult.NotFound();

        seller.EditInventory(request.InventoryId, request.Count, request.Price, request.DiscountPercentage);
        await _repository.Save();
        return OperationResult.Success();
    }
}