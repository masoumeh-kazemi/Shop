using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.SiteEntities;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.SiteEntities.DTOs;

namespace Shop.Query.SiteEntities.Banners.GetList;

public class GetBannerListQuery : IQuery<List<BannerDto>>
{
    
}

public class GetBannerListQueryHandler : IQueryHandler<GetBannerListQuery, List<BannerDto>>
{
    private readonly ShopContext _context;
    public GetBannerListQueryHandler(ShopContext context)
    {
        _context = context;
    }
    public async Task<List<BannerDto>> Handle(GetBannerListQuery request, CancellationToken cancellationToken)
    {
        var banner = await _context.Banners.Select(banner => new BannerDto()
        {
            ImageName = banner.ImageName,
            Link = banner.Link,
            Position = banner.Position,
            Id = banner.Id,
            CreationDate = banner.CreationDate,
        }).ToListAsync(cancellationToken: cancellationToken);

        return banner;
    }
}