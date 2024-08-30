using Common.Application;
using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.Security;
using Shop.Api.ViewModels.Products;
using Shop.Application.Products.AddImage;
using Shop.Application.Products.Create;
using Shop.Application.Products.Edit;
using Shop.Application.Products.RemoveImage;
using Shop.Domain.RoleAgg.Enums;
using Shop.Presentation.Facade.Products;
using Shop.Query.Products.Dto;
using Shop.Query.Products.GetProductById;

namespace Shop.Api.Controllers
{
    //[PermissionChecker(Permission.CRUD_Product)]
    public class ProductController : ApiController
    {
        private readonly IProductFacade _productFacade;

        public ProductController(IProductFacade productFacade)
        {
            _productFacade = productFacade;
        }


        [HttpPost]
        public async Task<ApiResult> CreateProduct([FromForm] CreateProductViewModel command)
        {
            var result = await _productFacade.CreateProduct(new CreateProductCommand()
            {
                SeoData = command.SeoData.Map(),
                CategoryId = command.CategoryId,
                Description = command.Description,
                ImageFile = command.ImageFile,
                SecondarySubCategoryId = command.SecondarySubCategoryId,
                Slug = command.Slug,
                Specifications = command.GetSpecification(),
                SubCategoryId = command.SubCategoryId,
                Title = command.Title
            });
            return CommandResult(result);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ApiResult<ProductDto?>> GetProductById(long id)
        {
            var result = await _productFacade.GetProductById(id);
            return QueryResult(result);
        }

        [AllowAnonymous]
        [HttpGet("bySlug/{slug}")]
        public async Task<ApiResult<ProductDto?>> GetProductBySlug(string slug)
        {
            var result = await _productFacade.GetProductBySlug(slug);
            return QueryResult(result);
        }

        [AllowAnonymous]
        [HttpGet("single/{slug}")]
        public async Task<ApiResult<IProductFacade.SingleProductDto?>> GetSingleProductBySlug(string slug)
        {
            var result = await _productFacade.GetProductBySlugForSinglePage(slug);
            return QueryResult(result);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ApiResult<ProductFilterResult>> GetProductByFilter([FromQuery] ProductFilterParams filterParams)
        {
            var result = await _productFacade.GetProductsByFilter(filterParams);
            return QueryResult(result);
        }

        [AllowAnonymous]
        [HttpGet("Shop")]
        public async Task<ApiResult<ProductShopResult>> GetProductForShopFilter([FromQuery] ProductShopFilterParam filterParam)
        {
            var result = await _productFacade.GetProductForShop(filterParam);
            return QueryResult(result);
        }


        [HttpPost("images")]
        public async Task<ApiResult> AddImage([FromForm] AddProductImageCommand command)
        {
            var result = await _productFacade.AddImage(command);
            return CommandResult(result);
        }

        [HttpDelete("images")]
        public async Task<ApiResult> RemoveImage(RemoveProductImageCommand command)
        {
            var result = await _productFacade.RemoveImage(command);
            return CommandResult(result);
        }

        [HttpPut]
        public async Task<ApiResult> EditProduct([FromForm] EditProductViewModel command)
        {
            var result = await _productFacade.EditProduct(new EditProductCommand(command.ProductId, command.Title, command.ImageFile,
                command.Description, command.CategoryId, command.SubCategoryId, command.SecondarySubCategoryId, command.Slug, command.SeoData.Map(),
                command.GetSpecification()));

            return CommandResult(result);
        }
    }
}
