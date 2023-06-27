using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.RoleAgg;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Roles.DTOs;

namespace Shop.Query.Roles.GetById;

public record GetRoleById(long RoleId) : IQuery<RoleDto>;

public class GetRoleByIdQueryHandler : IQueryHandler<GetRoleById, RoleDto>
{
    private readonly ShopContext _context;
    public GetRoleByIdQueryHandler(ShopContext context)
    {
        _context = context;
    }
    public async Task<RoleDto> Handle(GetRoleById request, CancellationToken cancellationToken)
    {
        var role = await _context.Roles
            .FirstOrDefaultAsync(f => f.Id == request.RoleId, cancellationToken: cancellationToken);

        if (role == null)
            return null;
        return new RoleDto()
        {
            Id = role.Id,
            CreationDate = role.CreationDate,
            Permissions = role.Permissions.Select(s => s.Permission).ToList(),
            Title = role.Title
        };
    }


}