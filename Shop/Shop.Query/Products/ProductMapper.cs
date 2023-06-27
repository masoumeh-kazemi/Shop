using AngleSharp.Css;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.ProductAgg;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Categories.DTOs;
using Shop.Query.Products.Dto;

namespace Shop.Query.Products;

public static class ProductMapper
{
    public static ProductDto Map(this Product product)
    {
        return new ProductDto()
        {
            Id = product.Id,
            CreationDate = product.CreationDate,
            Title = product.Title,
            ImageName = product.ImageName,
            Description = product.Description,
            Slug = product.Slug,
            SeoData = product.SeoData,
            Specifications = product.Specifications.Select(s => new ProductSpecificationDto()
            {
                Key = s.Key,
                Value = s.Value,
            }).ToList(),
            Images = product.Images.Select(s => new ProductImageDto()
            {
                CreationDate = s.CreationDate,
                Id = s.Id,
                ImageName = s.ImageName,
                ProductId = s.ProductId,
                Sequence = s.Sequence
            }).ToList(),

            Category = new ProductCategoryDto() { Id = product.CategoryId },
            SubCategory = new ProductCategoryDto() { Id = product.SubCategoryId },
            SecondarySubCategory = product.SecondarySubCategoryId != null
                ? new()
                {
                    Id = (long)product.SecondarySubCategoryId
                }
                : null,
        };

    }

    public static ProductFilterData MapListData(this Product product)
    {
        return new ProductFilterData()
        {
            CreationDate = product.CreationDate,
            Id = product.Id,
            ImageName = product.ImageName,
            Slug = product.Title
        };
    }

    public static async Task SetCategories(this ProductDto productDto, ShopContext context)
    {
        var categories = await context.Categories
            .Where(r => r.Id == productDto.Category.Id || r.Id == productDto.SubCategory.Id)
            .Select(s => new ProductCategoryDto()
            {
                Id = s.Id,
                ParentId = s.ParentId,
                Slug = s.Slug,
                SeoData = s.SeoData
            }).ToListAsync();


        if (productDto.SecondarySubCategory != null)
        {
            var secondarySubCategory = await context.Categories
                .Where(f => f.Id == productDto.SecondarySubCategory.Id)
                .Select(s => new ProductCategoryDto()
                {
                    Id = s.Id,
                    ParentId = s.ParentId,
                    Slug = s.Slug,
                    SeoData = s.SeoData
                })
                .FirstOrDefaultAsync();

            if (secondarySubCategory != null)
                productDto.SecondarySubCategory = secondarySubCategory;
        }



        productDto.Category = categories.First(r => r.Id == productDto.Category.Id);
        productDto.SubCategory = categories.First(r => r.Id == productDto.SubCategory.Id);

    }
}


