using Common.Application;
using MediatR;
using Shop.Application.SiteEntities.ShippingMethods.Create;
using Shop.Application.SiteEntities.ShippingMethods.Delete;
using Shop.Application.SiteEntities.ShippingMethods.Edit;
using Shop.Query.SiteEntities.DTOs;
using Shop.Query.SiteEntities.ShippingMethod.GetById;
using Shop.Query.SiteEntities.ShippingMethod.GetList;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Shop.Presentation.Facade.SiteEntities.ShippingMethods;

public interface IShippingMethodFacade
{
    Task<OperationResult> Create(CreateShippingMethodCommand command);
    Task<OperationResult> Edit(EditShippingMethodCommand command);
    Task<OperationResult> Delete(long id);

    Task<ShippingMethodDto?> GetById(long id);
    Task<List<ShippingMethodDto>> GetList();

}

public class ShippingMethodFacade : IShippingMethodFacade
{
    private readonly IMediator _mediator;

    public ShippingMethodFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> Create(CreateShippingMethodCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditShippingMethodCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Delete(long id)
    {
        return await _mediator.Send(new DeleteShippingMethodCommand(id));
    }

    public async Task<ShippingMethodDto?> GetById(long id)
    {
        return await _mediator.Send(new GetShippingMethodByIdQuery(id));
    }

    public async Task<List<ShippingMethodDto>> GetList()
    {
        return await _mediator.Send(new GetShippingMethodByListQuery());
    }
}