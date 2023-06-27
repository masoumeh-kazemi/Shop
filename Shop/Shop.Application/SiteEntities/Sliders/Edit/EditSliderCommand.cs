using Common.Application;
using Common.Application._Utilities;
using Common.Application.FileUtil.Interfaces;
using Microsoft.AspNetCore.Http;
using Shop.Domain.SiteEntities.Repositories;

namespace Shop.Application.SiteEntities.Sliders.Edit;

public class EditSliderCommand : IBaseCommand
{
    public long Id { get; set; }
    public string Link { get; private set; }
    public IFormFile? ImageFile { get; private set; }
    public string Title { get; private set; }

    public EditSliderCommand(long id, string link, IFormFile imageFile, string title)
    {
        Id = id;
        Link = link;
        ImageFile = imageFile;
        Title = title;
    }
}

public class EditSliderCommandHandler : IBaseCommandHandler<EditSliderCommand>
{
    private readonly ISliderRepository _repository;
    private readonly IFileService _fileService;

    public EditSliderCommandHandler(ISliderRepository repository, IFileService fileService)
    {
        _repository = repository;
        _fileService = fileService;
    }
    public  async Task<OperationResult> Handle(EditSliderCommand request, CancellationToken cancellationToken)
    {
        var slider = await _repository.GetTracking(request.Id);
        if (slider == null) 
            return OperationResult.NotFound();

        var imageName = slider.ImageName;
        if(request.ImageFile != null)
            imageName = await _fileService.SaveFileAndGenerateName(request.ImageFile, Directories.SliderImages);

        slider.Edit(request.Title, request.Link, imageName);
        await _repository.Save();
        return OperationResult.Success();
    }
}

