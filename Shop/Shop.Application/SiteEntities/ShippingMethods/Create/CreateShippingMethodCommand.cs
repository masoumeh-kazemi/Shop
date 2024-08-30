using Common.Application;
using Common.Domain.Repository;
using Shop.Domain.SiteEntities;
using Shop.Domain.SiteEntities.Repositories;

namespace Shop.Application.SiteEntities.ShippingMethods.Create;

public class CreateShippingMethodCommand : IBaseCommand
{
    public string Title { get; set; }
    public int Cost { get; set; }

}

public class CreateShippingMethodCommandHandler : IBaseCommandHandler<CreateShippingMethodCommand>
{
    private readonly IShippingMethodRepository _shippingMethodRepository;

    public CreateShippingMethodCommandHandler(IShippingMethodRepository shippingMethodRepository)
    {
        _shippingMethodRepository = shippingMethodRepository;
    }
    public async Task<OperationResult> Handle(CreateShippingMethodCommand request, CancellationToken cancellationToken)
    {
        var shippingMethod = new ShippingMethod(request.Title, request.Cost);
        _shippingMethodRepository.Add(shippingMethod);
        await _shippingMethodRepository.Save();
        return OperationResult.Success();
    }
}