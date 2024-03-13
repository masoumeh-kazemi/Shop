using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Categories.DTOs;

namespace Shop.Query.Categories.GetListByQuery;

public class GetCategoryListByQuery : IQuery<List<CategoryDto>>
{
}

public class GetCategoryListByQueryHandler : IQueryHandler<GetCategoryListByQuery, List<CategoryDto>>
{
    private readonly ShopContext _context;
    public GetCategoryListByQueryHandler(ShopContext context)
    {
        _context = context;
    }
    public async Task<List<CategoryDto>> Handle(GetCategoryListByQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Categories
            .Where(r=>r.ParentId == null)
            .Include(c => c.Childs)
            .ThenInclude(c => c.Childs)
            .OrderByDescending(d => d.Id).ToListAsync(cancellationToken);

        return result.Map();
    }
}