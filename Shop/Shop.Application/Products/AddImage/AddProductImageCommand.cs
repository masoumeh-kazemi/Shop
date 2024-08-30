using Common.Application;
using Common.Application._Utilities;
using Common.Application.FileUtil.Interfaces;
using Microsoft.AspNetCore.Http;
using Shop.Domain.ProductAgg;
using Shop.Domain.ProductAgg.Repository;

namespace Shop.Application.Products.AddImage;

public class AddProductImageCommand : IBaseCommand
{
    public IFormFile ImageFile { get;  set; }
    public long ProductId { get; set; }
    public int Sequence { get;  set; }

 
}


public class AddProductImageCommandHandler : IBaseCommandHandler<AddProductImageCommand>
{
    private readonly IProductRepository _repository;
    private readonly IFileService _fileService;

    public AddProductImageCommandHandler(IProductRepository repository, IFileService fileService)
    {
        _repository = repository;
        _fileService = fileService;
    }
    public async Task<OperationResult> Handle(AddProductImageCommand request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetTracking(request.ProductId);
        if (product == null)
            return OperationResult.NotFound();

        var imageName = await _fileService
            .SaveFileAndGenerateName(request.ImageFile, Directories.ProductGalleryImage);

        var image = new ProductImage(imageName, request.Sequence);
        product.AddImage(image);
        await _repository.Save();
        return OperationResult.Success();
    }
}