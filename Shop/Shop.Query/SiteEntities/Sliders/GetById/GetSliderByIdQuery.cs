using Common.Application;
using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.SiteEntities.DTOs;

namespace Shop.Query.SiteEntities.Sliders.GetById;

public class GetSliderByIdQuery : IQuery<SliderDto>
{
    public long Id { get; private set; }

    public GetSliderByIdQuery(long id)
    {
        Id = id;
    }
}

public class GetSliderByIdQueryHandler : IQueryHandler<GetSliderByIdQuery, SliderDto>
{
    private readonly ShopContext _context;

    public GetSliderByIdQueryHandler(ShopContext context)
    {
        _context = context;
    }
    public async Task<SliderDto> Handle(GetSliderByIdQuery request, CancellationToken cancellationToken)
    {
        var slider = await _context.Sliders
            .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken: cancellationToken);

        if (slider == null)
            return null;
        return new SliderDto()
        {
            CreationDate = slider.CreationDate,
            Id = slider.Id,
            ImageName = slider.ImageName,
            Link = slider.Link,
            Title = slider.Title,
        };
    }
}