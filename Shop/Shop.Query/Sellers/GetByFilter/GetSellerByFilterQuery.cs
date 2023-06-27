using AngleSharp.Common;
using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Sellers.DTOs;

namespace Shop.Query.Sellers.GetByFilter;

public class GetSellerByFilterQuery : QueryFilter<SellerFilterResult, SellerFilterParams>
{
    public GetSellerByFilterQuery(SellerFilterParams filterParams) : base(filterParams)
    {
    }
}


public class GetSellerByFilterQueryHandler : IQueryHandler<GetSellerByFilterQuery, SellerFilterResult>
{
    private readonly ShopContext _context;

    public GetSellerByFilterQueryHandler(ShopContext context)
    {
        _context = context;
    }
    public async Task<SellerFilterResult> Handle(GetSellerByFilterQuery request, CancellationToken cancellationToken)
    {
        var @params = request.FilterParams;
        var result = _context.Sellers.OrderByDescending(f => f.Id).AsQueryable();

        if (!string.IsNullOrWhiteSpace(@params.NationalCode))
            result = result.Where(f => f.NationalCode.Contains(@params.NationalCode));

        if (!string.IsNullOrWhiteSpace(@params.ShopName))
            result = result.Where(f => f.ShopName.Contains(@params.ShopName));

        var skip = (@params.PageId - 1) * @params.Take;
        var sellerResult = new SellerFilterResult()
        {
            FilterParams = @params,
            Data = await result.Skip(skip).Take(@params.Take).Select(seller => seller.Map())
                .ToListAsync(cancellationToken)
        };

        sellerResult.GeneratePaging(result, @params.Take, @params.PageId);
        return sellerResult;
    }
}