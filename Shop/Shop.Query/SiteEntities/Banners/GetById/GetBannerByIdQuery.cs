using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.SiteEntities;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.SiteEntities.DTOs;

namespace Shop.Query.SiteEntities.Banners.GetById;

public record GetBannerByIdQuery (long BannerId) : IQuery<BannerDto>;


public class GetBannerByIdQueryHandler : IQueryHandler<GetBannerByIdQuery, BannerDto>
{
    private readonly ShopContext _context;

    public GetBannerByIdQueryHandler(ShopContext context)
    {
        _context = context;
    }
    public async Task<BannerDto> Handle(GetBannerByIdQuery request, CancellationToken cancellationToken)
    {
        var banner = await _context.Banners
            .FirstOrDefaultAsync(f => f.Id == request.BannerId, cancellationToken: cancellationToken);
        if (banner == null)
            return null;

        return new BannerDto()
        {
            CreationDate = banner.CreationDate,
            Id = banner.Id,
            Link = banner.Link,
            Position = banner.Position,
            ImageName = banner.ImageName,
        };

    }
}