using Common.Query;
using Dapper;
using Microsoft.Data.SqlClient;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Users.UserTokens.GetByToken;

public record GetUserTokenByJwtTokenQuery(string HashJwtToken) : IQuery<UserTokenDto?>;

public class GetUserByQueryHandler : IQueryHandler<GetUserTokenByJwtTokenQuery, UserTokenDto?>
{
    private readonly DapperContext _dapperContext;

    public GetUserByQueryHandler(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }
    public async Task<UserTokenDto?> Handle(GetUserTokenByJwtTokenQuery request, CancellationToken cancellationToken)
    {
        //var connection = _dapperContext.CreateConnection();
        //var sql = $"SELECT TOP(1) * FROM {_dapperContext.UserTokens} WHERE HashJwtToken=@hashJwtToken";
        //return connection.QueryFirstOrDefaultAsync<UserTokenDto?>(sql, new { hashJwtToken = request.HashJwtToken});

        using var connection = _dapperContext.CreateConnection();
        var sql = $"SELECT TOP(1) * FROM {_dapperContext.UserTokens} Where HasJwtToken=@hashJwtToken";
        var result = await connection.QueryFirstOrDefaultAsync<UserTokenDto>(sql, new {hashJwtToken = request.HashJwtToken});
        return result;
    }
}



