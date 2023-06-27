using Common.Application;
using Common.Application._Utilities;
using Common.Application.FileUtil.Interfaces;
using Microsoft.AspNetCore.Http;
using Shop.Domain.SiteEntities;
using Shop.Domain.SiteEntities.Repositories;

namespace Shop.Application.SiteEntities.Sliders.Create;

public class CreateSliderCommand : IBaseCommand
{
    public CreateSliderCommand(string link, IFormFile imageFile, string title)
    {
        Link = link;
        ImageFile = imageFile;
        Title = title;
    }
    public string Link { get; private set; }
    public IFormFile ImageFile { get; private set; }
    public string Title { get; private set; }
}

public class CreateSliderCommandHandler : IBaseCommandHandler<CreateSliderCommand>
{
    private readonly ISliderRepository _repository;
    private readonly IFileService _fileService;

    public CreateSliderCommandHandler(ISliderRepository repository, IFileService fileService)
    {
        _repository = repository;
        _fileService = fileService;
    }
    public async Task<OperationResult> Handle(CreateSliderCommand request, CancellationToken cancellationToken)
    {
        var imageName = await _fileService
            .SaveFileAndGenerateName(request.ImageFile, Directories.SliderImages);

        var slider = new Slider(request.Title, request.Link,imageName);
        _repository.Add(slider);
        await _repository.Save();
        return OperationResult.Success();
    }
}