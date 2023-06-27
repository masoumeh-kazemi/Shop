using Common.Application;
using Common.Application._Utilities;
using Common.Application.FileUtil.Interfaces;
using Common.Domain.Repository;
using Microsoft.AspNetCore.Http;
using Shop.Domain.SiteEntities;
using Shop.Domain.SiteEntities.Repositories;

namespace Shop.Application.SiteEntities.Banners.Edit;

public class EditBannerCommand : IBaseCommand
{
    public EditBannerCommand(long id, string link, IFormFile imageFile, BannerPosition position)
    {
        Id = id;
        Link = link;
        ImageFile = imageFile;
        Position = position;
    }
    public long Id { get; private set; }
    public string Link { get; private set; }
    public IFormFile? ImageFile { get; private set; }
    public BannerPosition Position { get; private set; }
}

public class EditBannerCommandHandler : IBaseCommandHandler<EditBannerCommand>
{
    private readonly IBannerRepository _repository;
    private readonly IFileService _fileService;

    public EditBannerCommandHandler(IBannerRepository repository, IFileService fileService)
    {
        _repository = repository;
        _fileService = fileService;
    }
    public async Task<OperationResult> Handle(EditBannerCommand request, CancellationToken cancellationToken)
    {
        var banner = await _repository.GetTracking(request.Id);
        if (banner == null)
            return OperationResult.NotFound();

        var imageName = banner.ImageName;
        if (request.ImageFile != null)
            imageName = await _fileService.SaveFileAndGenerateName(request.ImageFile, Directories.BannerImages);

        banner.Edit(request.Link, imageName, request.Position);
         await _repository.Save();
         return OperationResult.Success();
    }
}