using Common.Domain.Repository;
using Common.Domain.ValueObjects;

namespace Shop.Domain.CategoryAgg;

public interface ICategoryRepository : IBaseRepository<Category>
{
    Task<bool> DeleteCategory(long categoryId);
}