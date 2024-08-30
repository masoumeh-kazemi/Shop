using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.SiteEntities.DTOs;

namespace Shop.Query.SiteEntities.ShippingMethod.GetById;

public class GetShippingMethodByIdQuery : IQuery<ShippingMethodDto?>
{
    public GetShippingMethodByIdQuery(long id)
    {
        Id = id;
    }
    public long Id { get; set; }

}

public class GetShippingMethodByIdQueryHandler : IQueryHandler<GetShippingMethodByIdQuery, ShippingMethodDto?>
{
    private readonly ShopContext _shopContext;

    public GetShippingMethodByIdQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }
    public async Task<ShippingMethodDto?> Handle(GetShippingMethodByIdQuery request, CancellationToken cancellationToken)
    {
        return await _shopContext.ShippingMethods.Select(shipping => new ShippingMethodDto()
        {
            Title = shipping.Title,
            Cost = shipping.Cost,
            CreationDate = shipping.CreationDate,
            Id = shipping.Id,
        }).FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken: cancellationToken);

    }
}