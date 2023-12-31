﻿using System.Diagnostics;
using Dapper;
using Shop.Domain.OrderAgg;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Orders.DTOs;

namespace Shop.Query.Orders;

public static class OrderMapper
{
    public static OrderDto Map(this Order order)
    {
        return new OrderDto()
        {
            CreationDate = order.CreationDate,
            Id = order.Id,
            Status = order.Status,
            Address = order.Address,
            Discount = order.Discount,
            Items = new(),
            LastUpdate = order.LastUpdate,
            ShippingMethod = order.ShippingMethod,
            UserFullName = "",
            UserId = order.UserId,
        };
    }

    public static async Task<List<OrderItemDto>> GetOrderItems(this OrderDto orderDto, DapperContext dapperContext)
    {
        using var connection = dapperContext.CreateConnection();
        var sql = @$"SELECT o.InventoryId, s.ShopName from {dapperContext.OrderItems} o 
                  Inner Join {dapperContext.Inventories} i o.InventoryId = i.Id
                  Inner Join {dapperContext.Sellers} s s.Id = i.SellerId
                  Inner Join {dapperContext.Products} p p.Id = i.ProductId
                  where o.OrderId=@OrderId";
        var result = await connection.QueryAsync<OrderItemDto>(sql, new { OrderId = orderDto.Id });
        return  result.ToList();

    }

    public static OrderFilterData MapFilterData(this Order order, ShopContext context)
    {
        var userFullName = context.Users.Where(r => r.Id == order.UserId)
            .Select(r => $"{r.Name}{r.Family}")
            .First();
        return new OrderFilterData()
        {
            Status = order.Status,
            Id = order.Id,
            CreationDate = order.CreationDate,
            City = order.Address?.City,
            ShippingType = order.ShippingMethod?.ShippingType,
            Shire = order.Address?.Shire,
            TotalItemCount = order.ItemCount,
            TotalPrice = order.TotalPrice,
            UserFullName = userFullName,
            UserId = order.UserId
        };
    }
}