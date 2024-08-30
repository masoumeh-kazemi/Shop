using Common.Application;
using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.OrderAgg;
using Shop.Domain.OrderAgg.Repository;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Orders.DTOs;

namespace Shop.Query.Orders.GetCurrent;

public record GetCurrentUserOrderQuery(long UserId) : IQuery<OrderDto?>;

public class GetCurrentOrderQueryHandler : IQueryHandler<GetCurrentUserOrderQuery, OrderDto?>
{
    private readonly ShopContext _shopContext;
    private readonly DapperContext _dapperContext;

    public GetCurrentOrderQueryHandler(ShopContext shopContext, DapperContext dapperContext)
    {
        _shopContext = shopContext;
        _dapperContext = dapperContext;
    }
    public async Task<OrderDto?> Handle(GetCurrentUserOrderQuery request, CancellationToken cancellationToken)
    {
        var order = await _shopContext.Orders
            .FirstOrDefaultAsync(f => f.UserId == request.UserId && f.Status == OrderStatus.Pending
            , cancellationToken);
        if (order == null)
            return null; 

        var orderDto = order.Map();
        orderDto.UserFullName = await _shopContext.Users.Where(f => f.Id == request.UserId)
                .Select(s => $"{s.Name}{s.Family}").FirstAsync(cancellationToken);

        orderDto.Items= await orderDto.GetOrderItems(_dapperContext);
        return orderDto;


    }
}


