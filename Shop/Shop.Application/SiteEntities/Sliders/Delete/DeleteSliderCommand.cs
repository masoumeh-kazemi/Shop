using Common.Application;
using Shop.Domain.SiteEntities.Repositories;

namespace Shop.Application.SiteEntities.Sliders.Delete;

public class DeleteSliderCommand : IBaseCommand
{
    public DeleteSliderCommand(long sliderId)
    {
        SliderId = sliderId;
    }
    public long SliderId { get; set; }
    

}

public class DeleteSliderCommandHandler : IBaseCommandHandler<DeleteSliderCommand>
{
    private readonly ISliderRepository _sliderRepository;

    public DeleteSliderCommandHandler(ISliderRepository sliderRepository)
    {
        _sliderRepository = sliderRepository;
    }
    public async Task<OperationResult> Handle(DeleteSliderCommand request, CancellationToken cancellationToken)
    {
        var slider = await _sliderRepository.GetTracking(request.SliderId);
        if (slider == null)
        {
            return OperationResult.NotFound();
        }

        _sliderRepository.Delete(slider);
        await _sliderRepository.Save();
        return OperationResult.Success();

    }
}