using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.SiteEntities.DTOs;

namespace Shop.Query.SiteEntities.ShippingMethod.GetList;

public class GetShippingMethodByListQuery : IQuery<List<ShippingMethodDto>>
{
    
}

public  class GetShippingMethodByListQueryHandler : IQueryHandler<GetShippingMethodByListQuery, List<ShippingMethodDto>>
{
    private readonly ShopContext _shopContext;

    public GetShippingMethodByListQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }
    public async Task<List<ShippingMethodDto>> Handle(GetShippingMethodByListQuery request, CancellationToken cancellationToken)
    {
        return  await _shopContext.ShippingMethods.Select(shipping => new ShippingMethodDto()
        {
            Title = shipping.Title,
            Cost = shipping.Cost,
            CreationDate = shipping.CreationDate,
            Id = shipping.Id
        }).ToListAsync(cancellationToken: cancellationToken);
    }
}