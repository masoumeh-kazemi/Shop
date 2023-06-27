using Common.Application;
using Common.Application._Utilities;
using Common.Application.FileUtil.Interfaces;
using Common.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;
using Shop.Domain.ProductAgg;
using Shop.Domain.ProductAgg.Repository;
using Shop.Domain.ProductAgg.Services;

namespace Shop.Application.Products.Edit;

public class EditProductCommand : IBaseCommand
{
    public long ProductId { get; private set; }
    public string Title { get; private set; }
    public IFormFile? ImageName { get; private set; }
    public string Description { get; private set; }
    public long CategoryId { get; private set; }
    public long SubCategoryId { get; private set; }
    public long SecondarySubCategoryId { get; private set; }
    public string Slug { get; private set; }
    public SeoData SeoData { get; private set; }
    public Dictionary<string, string> Specifications { get; private set; }

    public EditProductCommand(long productId, string title, IFormFile? imageName, string description, long categoryId, long subCategoryId, long secondarySubCategoryId, string slug, SeoData seoData, Dictionary<string, string> specifications)
    {
        ProductId = productId;
        Title = title;
        ImageName = imageName;
        Description = description;
        CategoryId = categoryId;
        SubCategoryId = subCategoryId;
        SecondarySubCategoryId = secondarySubCategoryId;
        Slug = slug;
        SeoData = seoData;
        Specifications = specifications;
    }
}

public class EditProductCommandHandler : IBaseCommandHandler<EditProductCommand>
{
    private readonly IProductRepository _repository;
    private readonly IProductDomainService _domainService;
    private readonly IFileService _fileService;

    public EditProductCommandHandler(IProductRepository repository, IProductDomainService domainService, IFileService fileService)
    {
        _repository = repository;
        _domainService = domainService;
        _fileService = fileService;
    }
    public async Task<OperationResult> Handle(EditProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetTracking(request.ProductId);
        if (product == null)
            return OperationResult.NotFound();

        product.Edit(request.ProductId, request.Title, request.Description, request.CategoryId, request.SubCategoryId
            , request.SecondarySubCategoryId, _domainService, request.Slug, request.SeoData);
        var oldImage = product.ImageName;
        if (request.ImageName != null)
        {
            var imageName = await _fileService.SaveFileAndGenerateName(request.ImageName,
                Directories.ProductImages);
            product.SetProductImage(imageName);
        }
        var specifications = new List<ProductSpecification>();
        request.Specifications.ToList().ForEach(specification =>
        {
            specifications.Add(new ProductSpecification(specification.Key, specification.Value));
        });
        product.SetSpecification(specifications);
        await _repository.Save();
        RemoveOldImage(request.ImageName, oldImage);
        return OperationResult.Success();
    }

    private void RemoveOldImage(IFormFile imageName, string oldImageName)
    {
        if (imageName != null)
        {
            _fileService.DeleteFile(Directories.ProductImages, oldImageName);
        }

    }
}