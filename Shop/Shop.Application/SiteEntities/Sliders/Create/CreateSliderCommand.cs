using Common.Application;
using Common.Application._Utilities;
using Common.Application.FileUtil.Interfaces;
using Microsoft.AspNetCore.Http;
using Shop.Domain.SiteEntities;
using Shop.Domain.SiteEntities.Repositories;

namespace Shop.Application.SiteEntities.Sliders.Create;

public class CreateSliderCommand : IBaseCommand 
{
    public string Link { get;  set; }
    public IFormFile ImageFile { get;  set; }
    public string Title { get;  set; }
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