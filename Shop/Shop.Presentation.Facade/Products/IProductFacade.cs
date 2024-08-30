using Common.Application;
using MediatR;
using Shop.Application.Products.AddImage;
using Shop.Application.Products.Create;
using Shop.Application.Products.Edit;
using Shop.Application.Products.RemoveImage;
using Shop.Presentation.Facade.Sellers.Inventories;
using Shop.Query.Products.Dto;
using Shop.Query.Products.GetForShop;
using Shop.Query.Products.GetProductByFilter;
using Shop.Query.Products.GetProductById;
using Shop.Query.Products.GetProductBySlug;
using Shop.Query.Sellers.DTOs;

namespace Shop.Presentation.Facade.Products;

public interface IProductFacade
{

    Task<OperationResult> CreateProduct(CreateProductCommand command);
    Task<OperationResult> EditProduct(EditProductCommand command);
    Task<OperationResult> AddImage(AddProductImageCommand command);
    Task<OperationResult> RemoveImage(RemoveProductImageCommand command);

    Task<ProductDto?> GetProductById(long productId);
    Task<ProductDto?> GetProductBySlug(string slug);
    Task<SingleProductDto?> GetProductBySlugForSinglePage(string slug);
    Task<ProductFilterResult> GetProductsByFilter(ProductFilterParams filterParams);
    Task<ProductShopResult> GetProductForShop(ProductShopFilterParam filterParam);

    public class SingleProductDto
    {
        public ProductDto Product { get; set; }
        public List<InventoryDto> Inventrories { get; set; }
    }
}


public class ProductFacade: IProductFacade
{
    private readonly IMediator _mediator;
    private readonly ISellerInventoryFacade _sellerInventoryFacade;


    public ProductFacade(IMediator mediator, ISellerInventoryFacade sellerInventoryFacade)
    {
        _mediator = mediator;
        _sellerInventoryFacade = sellerInventoryFacade;
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

    public async Task<IProductFacade.SingleProductDto?> GetProductBySlugForSinglePage(string slug)
    {
        var product = await _mediator.Send(new GetProductBySlugQuery(slug));
        if (product == null)
            return null;
        var inventories = await _sellerInventoryFacade.GetByProductId(product.Id);
        var model = new IProductFacade.SingleProductDto()
        {
            Inventrories = inventories,
            Product = product
        };
        return model; 
    }

    public async Task<ProductFilterResult> GetProductsByFilter(ProductFilterParams filterParams)
    {
        return await _mediator.Send(new GetProductByFilterQuery(filterParams));
    }

    public async Task<ProductShopResult> GetProductForShop(ProductShopFilterParam filterParam)
    {
        return await _mediator.Send(new GetProductsForShopQuery(filterParam));
    }
}