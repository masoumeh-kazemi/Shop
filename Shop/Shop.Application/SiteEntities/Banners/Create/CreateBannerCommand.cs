using Common.Application;
using Common.Application._Utilities;
using Common.Application.FileUtil.Interfaces;
using Microsoft.AspNetCore.Http;
using Shop.Domain.SiteEntities;
using Shop.Domain.SiteEntities.Repositories;
using static Shop.Domain.SiteEntities.Banner;

namespace Shop.Application.SiteEntities.Banners.Create;

public class CreateBannerCommand : IBaseCommand
{
    public CreateBannerCommand(string link, IFormFile imageFile, BannerPosition position)
    {
        Link = link;

        Position = position;
        ImageFile = imageFile;
    }
    public string Link { get; private set; }
    public IFormFile ImageFile { get; private set; }

    public BannerPosition Position { get; private set; }
}

public class CreateBannerCommandHandler : IBaseCommandHandler<CreateBannerCommand>
{
    private readonly IBannerRepository _repository;
    private readonly IFileService _fileService;

    public CreateBannerCommandHandler(IBannerRepository repository, IFileService fileService)
    {
        _repository = repository;
        _fileService = fileService;
    }
    public async Task<OperationResult> Handle(CreateBannerCommand request, CancellationToken cancellationToken)
    {
        var imageName = await _fileService.SaveFileAndGenerateName(request.ImageFile, Directories.BannerImages);
        var banner = new Banner(request.Link, imageName, request.Position);
        _repository.Add(banner);
        await _repository.Save();
        return OperationResult.Success();
    }
}
