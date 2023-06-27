using Common.Domain.Repository;
using Shop.Domain.UserAgg;

namespace Shop.Domain.OrderAgg.Repository;

public interface IOrderRepository : IBaseRepository<Order>
{
    Task<Order?> GetCurrentUserOrder(long userId);
}