using Common.Application;
using Shop.Domain.SiteEntities.Repositories;

namespace Shop.Application.SiteEntities.ShippingMethods.Edit;

public class EditShippingMethodCommand : IBaseCommand
{
    public long Id { get; set; }
    public string Title { get; set; }
    public int Cost { get; set; }

}

public class EditShippingMethodCommandHandler : IBaseCommandHandler<EditShippingMethodCommand>
{
    private readonly IShippingMethodRepository _shippingMethodRepository;

    public EditShippingMethodCommandHandler(IShippingMethodRepository shippingMethodRepository)
    {
        _shippingMethodRepository = shippingMethodRepository;
    }
    public async Task<OperationResult> Handle(EditShippingMethodCommand request, CancellationToken cancellationToken)
    {
        var shippingMethod = await _shippingMethodRepository.GetTracking(request.Id);
        if (shippingMethod == null) 
            return OperationResult.NotFound();

        shippingMethod.Edit(request.Title, request.Cost);
        await _shippingMethodRepository.Save();
        return OperationResult.Success();

    }
}