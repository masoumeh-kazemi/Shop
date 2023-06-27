using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Users.GetById;

public record GetUserByIdQuery(long UserId) : IQuery<UserDto?>;

public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserDto?>
{
    private readonly ShopContext _context;

    public GetUserByIdQueryHandler(ShopContext context)
    {
        _context = context;
    }
    public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(f => f.Id == request.UserId, cancellationToken);
        if (user == null)
            return null;

        return await user.Map().SetUserRoleTitles(_context);
    }
}