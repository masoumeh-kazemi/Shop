using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Users.GetByFilter;

public class GetUserByFilterQuery : QueryFilter<UserFilterResult, UserFilterParams>
{
    public GetUserByFilterQuery(UserFilterParams filterParams) : base(filterParams)
    {
    }

}

public class GetUserByFilterQueryHandler : IQueryHandler<GetUserByFilterQuery, UserFilterResult>
{

    private readonly ShopContext _context;
    public GetUserByFilterQueryHandler(ShopContext context)
    {
        _context = context;
    }
    public async Task<UserFilterResult> Handle(GetUserByFilterQuery request, CancellationToken cancellationToken)
    {
        var @params = request.FilterParams;
        var result = _context.Users.OrderByDescending(f => f.Id).AsQueryable();

        if (@params.Id != null)
            result = result.Where(f => f.Id == @params.Id);

        if (!string.IsNullOrWhiteSpace(@params.PhoneNumber))
            result = result.Where(f => f.PhoneNumber.Contains(@params.PhoneNumber));

        if (!string.IsNullOrWhiteSpace(@params.Email))
            result = result.Where(f => f.Email.Contains(@params.Email));

        var skip = (@params.PageId - 1) * @params.Take;
        var model = new UserFilterResult()
        {
            Data = await result.Skip(skip).Take(@params.Take)
                .Select(user => user.MapFilterData()).ToListAsync(cancellationToken),

            FilterParams = @params
        };

        model.GeneratePaging(result, @params.Take, @params.PageId);
        return model;

    }
}