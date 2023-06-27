using Shop.Domain.CategoryAgg.Services;
using Shop.Domain.CategoryAgg;

namespace Shop.Application.Categories;

public class CategoryDomainService : ICategoryDomainService
{
    private readonly ICategoryRepository _repository;
    public CategoryDomainService(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public bool IsSlugExist(string slug)
    {
        return _repository.Exists(f => f.Slug == slug);
    }
}