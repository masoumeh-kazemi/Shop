using Common.Application;
using MediatR;
using Shop.Application.Products.AddImage;
using Shop.Application.Products.Create;
using Shop.Application.Products.Edit;
using Shop.Application.Products.RemoveImage;
using Shop.Query.Products.Dto;
using Shop.Query.Products.GetProductByFilter;
using Shop.Query.Products.GetProductById;
using Shop.Query.Products.GetProductBySlug;

namespace Shop.Presentation.Facade.Products;

public interface IProductFacade
{

    Task<OperationResult> CreateProduct(CreateProductCommand command);
    Task<OperationResult> EditProduct(EditProductCommand command);
    Task<OperationResult> AddImage(AddProductImageCommand command);
    Task<OperationResult> RemoveImage(RemoveProductImageCommand command);

    Task<ProductDto?> GetProductById(long productId);
    Task<ProductDto?> GetProductBySlug(string slug);
    Task<ProductFilterResult> GetProductsByFilter(ProductFilterParams filterParams);
}


public class ProductFacade: IProductFacade
{
    private readonly IMediator _mediator;

    public ProductFacade(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<OperationResult> CreateProduct(CreateProductCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> EditProduct(EditProductCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> AddImage(AddProductImageCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> RemoveImage(RemoveProductImageCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<ProductDto?> GetProductById(long productId)
    {
        return await _mediator.Send(new GetProductByIdQuery(productId));
    }

    public async Task<ProductDto?> GetProductBySlug(string slug)
    {
        return await _mediator.Send(new GetProductBySlugQuery(slug));
    }

    public async Task<ProductFilterResult> GetProductsByFilter(ProductFilterParams filterParams)
    {
        return await _mediator.Send(new GetProductByFilterQuery(filterParams));
    }
}