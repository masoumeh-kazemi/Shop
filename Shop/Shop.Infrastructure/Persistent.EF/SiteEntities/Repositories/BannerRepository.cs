using Shop.Domain.SiteEntities;
using Shop.Domain.SiteEntities.Repositories;
using Shop.Infrastructure._Utilities;

namespace Shop.Infrastructure.Persistent.EF.SiteEntities.Repositories;

public class BannerRepository : BaseRepository<Banner>, IBannerRepository
{
    public BannerRepository(ShopContext context) : base(context)
    {
    }
}