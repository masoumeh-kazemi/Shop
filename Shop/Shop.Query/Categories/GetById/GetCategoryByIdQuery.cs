using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Categories.DTOs;

namespace Shop.Query.Categories.GetById;

public record GetCategoryByIdQuery(long CategoryId) : IQuery<CategoryDto>;

public class GetCategoryByIdQueryHandler :  IQueryHandler<GetCategoryByIdQuery, CategoryDto>
{
    private readonly ShopContext _context;

    public GetCategoryByIdQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var model = await _context.Categories
            .Include(c => c.Childs)
            .ThenInclude(c => c.Childs)
            .FirstOrDefaultAsync(f => f.Id == request.CategoryId, cancellationToken);

        return model.Map();
    }
}


