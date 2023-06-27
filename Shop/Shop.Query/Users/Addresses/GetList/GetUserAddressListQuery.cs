using Common.Query;
using Dapper;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Users.Addresses.GetList;

public record GetUserAddressListQuery (long UserId) : IQuery<List<AddressDto>>;


public class GetUserAddressListQueryHandler : IQueryHandler<GetUserAddressListQuery, List<AddressDto>>
{
    private readonly DapperContext _dapperContext;

    public GetUserAddressListQueryHandler(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }
    public async Task<List<AddressDto>> Handle(GetUserAddressListQuery request, CancellationToken cancellationToken)
    {
        var sql = $"Select * from {_dapperContext.UserAddress} Where UserId =@userId";
        using var context = _dapperContext.CreateConnection();
        var result = await context.QueryAsync<AddressDto>(sql, new { userId = request.UserId });
        return result.ToList();

    }
}