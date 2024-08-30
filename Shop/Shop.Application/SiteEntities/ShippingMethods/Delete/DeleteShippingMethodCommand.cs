using Common.Application;
using Shop.Domain.SiteEntities.Repositories;

namespace Shop.Application.SiteEntities.ShippingMethods.Delete;

public class DeleteShippingMethodCommand : IBaseCommand
{
    public DeleteShippingMethodCommand(long id)
    {
        Id = id;
    }
    public long Id { get; set; }

}

public class DeleteShippingMethodCommandHandler : IBaseCommandHandler<DeleteShippingMethodCommand>
{
    private readonly IShippingMethodRepository _shippingMethodRepository;

    public DeleteShippingMethodCommandHandler(IShippingMethodRepository shippingMethodRepository)
    {
        _shippingMethodRepository = shippingMethodRepository;
    }
    public async Task<OperationResult> Handle(DeleteShippingMethodCommand request, CancellationToken cancellationToken)
    {
        var shippingMethod = await _shippingMethodRepository.GetTracking(request.Id);
        if (shippingMethod == null) 
            return OperationResult.NotFound();

        _shippingMethodRepository.Delete(shippingMethod);
        await _shippingMethodRepository.Save();
        return OperationResult.Success();

    }
}
