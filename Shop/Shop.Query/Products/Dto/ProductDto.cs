using Common.Domain.ValueObjects;
using Common.Query;
using Common.Query.Filter;
using Shop.Domain.ProductAgg;

namespace Shop.Query.Products.Dto;

public class ProductDto : BaseDto
{
    public string Title { get;  set; }
    public string ImageName { get;  set; }
    public string Description { get;  set; }
    public ProductCategoryDto Category { get; set; }
    public ProductCategoryDto SubCategory { get; set; }
    public ProductCategoryDto? SecondarySubCategory { get; set; }
    public string Slug { get;  set; }
    public SeoData SeoData { get;  set; }
    public List<ProductImageDto> Images { get;  set; }
    public List<ProductSpecificationDto> Specifications { get;  set; }
}

public class ProductFilterData : BaseDto
{
    public string Title { get; set; }
    public string ImageName { get; set; }
    public string Slug { get; set; }
}
public class ProductFilterParams : BaseFilterParam
{
    public string? Title { get; set; }
    public long? Id { get; set; }
    public string? Slug { get; set; }
}

public class ProductFilterResult : BaseFilter<ProductFilterData, ProductFilterParams>
{

}