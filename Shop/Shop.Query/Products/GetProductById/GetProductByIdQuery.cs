using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.ProductAgg.Repository;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Products.Dto;

namespace Shop.Query.Products.GetProductById;

public record GetProductByIdQuery(long ProductId) : IQuery<ProductDto?>;

public class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, ProductDto?>
{
    private readonly ShopContext _context;

    public GetProductByIdQueryHandler(ShopContext context)
    {
        _context = context;
    }
    public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(f => f.Id == request.ProductId, cancellationToken: cancellationToken);

        var model = product.Map();
        if (model == null)
            return null;
        await model.SetCategories(_context);
        return model;
    }
}