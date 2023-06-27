using Common.Application;
using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Categories.DTOs;

namespace Shop.Query.Categories.GetByParentId;

public class GetCategoryByParentIdQuery : IQuery<List<ChildCategoryDto>>
{
    public long ParentId { get; private set; }

    public GetCategoryByParentIdQuery(long parentId)
    {
        ParentId = parentId;
    }
}

public class GetCategoryByParentIdQueryHandler : IQueryHandler<GetCategoryByParentIdQuery, List<ChildCategoryDto>>
{
    private readonly ShopContext _context;

    public GetCategoryByParentIdQueryHandler(ShopContext context)
    {
        _context = context;
    }
    public async Task<List<ChildCategoryDto>> Handle(GetCategoryByParentIdQuery request, CancellationToken cancellationToken)
    {
        var result = await  _context.Categories
                .Include(c=>c.Childs)
                .Where(f=>f.ParentId == request.ParentId).ToListAsync(cancellationToken: cancellationToken);

        return result.MapChildren();


    }
}