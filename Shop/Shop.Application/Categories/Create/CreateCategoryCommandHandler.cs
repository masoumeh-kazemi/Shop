using Common.Application;
using Shop.Domain.CategoryAgg;
using Shop.Domain.CategoryAgg.Services;

namespace Shop.Application.Categories.Create;

public class CreateCategoryCommandHandler : IBaseCommandHandler<CreateCategoryCommand, long>
{
    private readonly ICategoryDomainService _domainService;
    private readonly ICategoryRepository _repository;

    public CreateCategoryCommandHandler(ICategoryDomainService domainService, ICategoryRepository repository)
    {
        _domainService = domainService;
        _repository = repository;
    }
    public async Task<OperationResult<long>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category(request.Title, request.Slug, request.SeoData, _domainService);
        if(category == null)
            return OperationResult<long>.NotFound();

        _repository.Add(category);
        await _repository.Save();
        return OperationResult<long>.Success(category.Id);
    }
} 