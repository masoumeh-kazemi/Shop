using Common.Application;
using Common.Application._Utilities;
using Common.Application.FileUtil.Interfaces;
using Shop.Domain.ProductAgg.Repository;

namespace Shop.Application.Products.RemoveImage;

public record RemoveProductImageCommand(long ProductId, long ImageId) : IBaseCommand;

public class RemoveProductImageCommandHandler : IBaseCommandHandler<RemoveProductImageCommand>
{
    private readonly IProductRepository _repository;
    private readonly IFileService _fileService;

    public RemoveProductImageCommandHandler(IProductRepository repository, IFileService fileService)
    {
        _repository = repository;
        _fileService = fileService;
    }
    public async Task<OperationResult> Handle(RemoveProductImageCommand request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetTracking(request.ProductId);
        if (product == null)
            return OperationResult.NotFound();

        //Delete from database
        var imageName = product.RemoveImageAndReturnImageName(request.ImageId);

        await _repository.Save();

        //delete image from files
        _fileService.DeleteFile(Directories.ProductGalleryImage, imageName);
        return OperationResult.Success();
    }
}