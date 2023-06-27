using Shop.Domain.RoleAgg;
using Shop.Domain.RoleAgg.Repository;
using Shop.Infrastructure._Utilities;

namespace Shop.Infrastructure.Persistent.EF.RoleAgg;

public class RoleRepository : BaseRepository<Role> , IRoleRepository
{
    public RoleRepository(ShopContext context) : base(context)
    {
    }
}