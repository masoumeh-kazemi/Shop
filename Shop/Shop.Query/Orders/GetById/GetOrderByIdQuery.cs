﻿using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Orders.DTOs;

namespace Shop.Query.Orders.GetById;

public class GetOrderByIdQuery : IQuery<OrderDto>
{
    public long Id { get; private set; }

    public GetOrderByIdQuery(long id)
    {
        Id = id;
    }
}

public class GetOrderByIdQueryHandler : IQueryHandler<GetOrderByIdQuery, OrderDto>
{
    private readonly ShopContext _shopContext;
    private readonly DapperContext _dapperContext;

    public GetOrderByIdQueryHandler(ShopContext shopContext, DapperContext dapperContext)
    {
        _shopContext = shopContext;
        _dapperContext = dapperContext;
    }
    public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _shopContext.Orders
            .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken: cancellationToken);
        if (order == null)
            return null;

        var orderDto = order.Map();
        orderDto.UserFullName = await _shopContext.Users.Where(f => f.Id == orderDto.UserId)
            .Select(s => $"{s.Name}{s.Family}").FirstAsync(cancellationToken);

        orderDto.Items = await orderDto.GetOrderItems(_dapperContext);
        return orderDto;
    }
}