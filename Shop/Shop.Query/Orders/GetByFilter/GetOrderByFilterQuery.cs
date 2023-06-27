using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Orders.DTOs;

namespace Shop.Query.Orders.GetByFilter;

public class GetOrderByFilterQuery : QueryFilter<OrderFilterResult, OrderFilterParams>
{
    public GetOrderByFilterQuery(OrderFilterParams filterParams) : base(filterParams)
    {
    }
}

public class GetOrderByFilterQueryHandler : IQueryHandler<GetOrderByFilterQuery, OrderFilterResult>
{
    private readonly ShopContext _context;
    public GetOrderByFilterQueryHandler(ShopContext context)
    {
        _context = context;
    }
    public async Task<OrderFilterResult> Handle(GetOrderByFilterQuery request, CancellationToken cancellationToken)
    {
        var @params = request.FilterParams;
        var result = _context.Orders.OrderByDescending(r=>r.Id).AsQueryable();
        if (@params.UserId != null)
            result = result.Where(r => r.UserId == @params.UserId);
        if (@params.Status != null)
            result = result.Where(r => r.Status == @params.Status);
        if (@params.StartDate != null)
            result = result.Where(r => r.CreationDate.Date == @params.StartDate.Value);
        if (@params.EndDate != null)
            result = result.Where(r => r.CreationDate.Date == @params.EndDate.Value);
        var skip = (@params.PageId - 1) * @params.Take;

        var model = new OrderFilterResult()
        {
            Data = await result.Skip(skip).Take(@params.Take)
                .Select(r => r.MapFilterData(_context))
                .ToListAsync(cancellationToken),

            FilterParams = @params
        };
        model.GeneratePaging(result, @params.Take, @params.PageId);
        return model;
    }
}