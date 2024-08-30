using Common.Application;
using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Products.Dto;

namespace Shop.Query.Products.GetProductBySlug;

public class GetProductBySlugQuery : IQuery<ProductDto?>
{
    public GetProductBySlugQuery(string slug)
    {
        Slug = slug;
    }
    public string Slug { get; private set; }

}


public class GetProductBySlugQueryHandler : IQueryHandler<GetProductBySlugQuery, ProductDto?>
{
    private readonly ShopContext _context;

    public GetProductBySlugQueryHandler(ShopContext context)
    {
        _context = context;
    }
    public async Task<ProductDto?> Handle(GetProductBySlugQuery request, CancellationToken cancellationToken)
    {
        var product = await _context.Products
                .FirstOrDefaultAsync(f => f.Slug == request.Slug, cancellationToken);

        var model = product.Map();
        if (model == null)
            return null;

        await model.SetCategories(_context);
        return model;
    }
}