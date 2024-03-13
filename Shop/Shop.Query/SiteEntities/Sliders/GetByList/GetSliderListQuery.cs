using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.SiteEntities.DTOs;

namespace Shop.Query.SiteEntities.Sliders.GetByList;

public class GetSliderListQuery : IQuery<List<SliderDto>>
{
    
}

public class GetSliderByListQueryHandler : IQueryHandler<GetSliderListQuery, List<SliderDto>>
{
    private readonly ShopContext _context;

    public GetSliderByListQueryHandler(ShopContext context)
    {
        _context = context;
    }
    public async Task<List<SliderDto>> Handle(GetSliderListQuery request, CancellationToken cancellationToken)
    {
        var slider = await _context.Sliders.Select(slider => new SliderDto()
        {
            CreationDate = slider.CreationDate,
            Id = slider.Id,
            ImageName = slider.ImageName,
            Title = slider.Title,
            Link = slider.Link,
        }).ToListAsync(cancellationToken: cancellationToken);
        return slider;
    }
}