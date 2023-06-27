using Common.Application;
using MediatR;
using Shop.Application.Categories.AddChild;
using Shop.Application.Categories.Create;
using Shop.Application.Categories.Edit;
using Shop.Application.Categories.Remove;
using Shop.Query.Categories.DTOs;
using Shop.Query.Categories.GetById;
using Shop.Query.Categories.GetByParentId;
using Shop.Query.Categories.GetListByQuery;

namespace Shop.Presentation.Facade.Categories;

public interface ICategoryFacade
{
    Task<OperationResult<long>> CreateCategory(CreateCategoryCommand command);
    Task<OperationResult> EditCategory(EditCategoryCommand command);
    Task<OperationResult<long>> AddChildCategory(AddChildCategoryCommand command);
    //Task<OperationResult> RemoveCategory(RemoveCategoryCommand command);
    Task<OperationResult> RemoveCategory(long categoryId);


    Task<CategoryDto> GetCategoryById(long id);
    Task<List<ChildCategoryDto>> GetCategoriesByParentId(long parentId);
    Task<List<CategoryDto>> GetCategories();
}

public class CategoryFacade : ICategoryFacade
{
    private readonly IMediator _mediator;
    public CategoryFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult<long>> CreateCategory(CreateCategoryCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> EditCategory(EditCategoryCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult<long>> AddChildCategory(AddChildCategoryCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> RemoveCategory(long categoryId)
    {
        //return await _mediator.Send(command);
        return await _mediator.Send(new RemoveCategoryCommand(categoryId));

    }

    public async Task<CategoryDto> GetCategoryById(long id)
    {
        return await _mediator.Send(new GetCategoryByIdQuery(id));
    }

    public async Task<List<ChildCategoryDto>> GetCategoriesByParentId(long parentId)
    {
        return await _mediator.Send(new GetCategoryByParentIdQuery(parentId));
    }

    public async Task<List<CategoryDto>> GetCategories()
    {
        return await _mediator.Send(new GetCategoryListByQuery());
    }
}