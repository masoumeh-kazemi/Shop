using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Sellers.DTOs;

namespace Shop.Query.Sellers.GetByUserId;

public record GetSellerByUserIdQuery(long UserId) : IQuery<SellerDto?>;

public class GetSellerByUserIdQueryHandler : IQueryHandler<GetSellerByUserIdQuery, SellerDto?>
{
    private readonly ShopContext _shopContext;

    public GetSellerByUserIdQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }
    public async Task<SellerDto?> Handle(GetSellerByUserIdQuery request, CancellationToken cancellationToken)
    {
        var seller = await _shopContext.Sellers
            .FirstOrDefaultAsync(f => f.UserId == request.UserId, cancellationToken);

        if (seller == null)
            return null;

        return seller.Map();
    }
}


