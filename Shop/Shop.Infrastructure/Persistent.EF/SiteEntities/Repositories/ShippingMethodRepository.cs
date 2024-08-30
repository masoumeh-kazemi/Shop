using Shop.Domain.SiteEntities;
using Shop.Domain.SiteEntities.Repositories;
using Shop.Infrastructure._Utilities;

namespace Shop.Infrastructure.Persistent.EF.SiteEntities.Repositories;

public class ShippingMethodRepository : BaseRepository<ShippingMethod>, IShippingMethodRepository
{
    public ShippingMethodRepository(ShopContext context) : base(context)
    {
    }

    public void Delete(ShippingMethod shippingMethod)
    {
        Context.ShippingMethods.Remove(shippingMethod);
    }
}