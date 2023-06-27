using Shop.Domain.UserAgg;
using Shop.Domain.UserAgg.Repository;
using Shop.Domain.UserAgg.Services;
using Shop.Infrastructure._Utilities;

namespace Shop.Infrastructure.Persistent.EF.UsersAgg;

public class UserRepository : BaseRepository<User> , IUserRepository
{
    public UserRepository(ShopContext context) : base(context)
    {
    }
}