using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.RoleAgg.Repository;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Roles.DTOs;

namespace Shop.Query.Roles.GetList;

public class GetRoleListQuery : IQuery<List<RoleDto>> 
{
    
}

public class GetRoleListQueryHandler : IQueryHandler<GetRoleListQuery, List<RoleDto>>
{
    private readonly ShopContext _context;

    public GetRoleListQueryHandler(ShopContext context)
    {
        _context = context;
    }
    public async Task<List<RoleDto>> Handle(GetRoleListQuery request, CancellationToken cancellationToken)
    {
        return await _context.Roles.Select(role => new RoleDto()
        {
            CreationDate = role.CreationDate,
            Id = role.Id,
            Permissions = role.Permissions.Select(s => s.Permission).ToList(),
            Title = role.Title,
        }).ToListAsync(cancellationToken: cancellationToken);

    }
}