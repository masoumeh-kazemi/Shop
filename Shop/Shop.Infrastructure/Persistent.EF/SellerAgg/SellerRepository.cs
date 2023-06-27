using Common.Domain.Repository;
using Dapper;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Shop.Domain.SellerAgg;
using Shop.Domain.SellerAgg.Repository;
using Shop.Infrastructure._Utilities;
using Shop.Infrastructure.Persistent.Dapper;

namespace Shop.Infrastructure.Persistent.EF.SellerAgg;

public class SellerRepository : BaseRepository<Seller>, ISellerRepository
{
    private readonly DapperContext _dapperContext;
    public SellerRepository(ShopContext context, DapperContext dapperContext) : base(context)
    {
        _dapperContext = dapperContext;
    }

    public async Task<InventoryResult?> GetInventoryById(long id)
    {
        using var connection = _dapperContext.CreateConnection();
        var sql = $"SELECT * from {_dapperContext.Inventories} where Id = @InventoryId";
        return await connection.QueryFirstOrDefaultAsync<InventoryResult>(sql, new {InventoryId = id});

    }
}