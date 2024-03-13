using Common.Application;
using MediatR;
using Shop.Application.Sellers.AddInventory;
using Shop.Application.Sellers.Create;
using Shop.Application.Sellers.Edit;
using Shop.Application.Sellers.EditInventory;
using Shop.Query.Sellers.DTOs;
using Shop.Query.Sellers.GetByFilter;
using Shop.Query.Sellers.GetById;
using Shop.Query.Sellers.GetByUserId;

namespace Shop.Presentation.Facade.Sellers;

public interface ISellerFacade
{
    Task<OperationResult> CreateSeller(CreateSellerCommand command);
    Task<OperationResult> AddSellerInventory(AddSellerInventoryCommand command);
    Task<OperationResult> EditSeller(EditSellerCommand command);
    Task<OperationResult> EditSellerInventory(EditSellerInventoryCommand command);

    Task<SellerDto> GetSellerById(long id);
    Task<SellerDto?> GetSellerByUserId(long userId);
    Task<SellerFilterResult> GetSellerByFilter(SellerFilterParams filterParams);
}

public class SellerFacade : ISellerFacade
{
    private readonly IMediator _mediator;

    public SellerFacade(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<OperationResult> CreateSeller(CreateSellerCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> AddSellerInventory(AddSellerInventoryCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> EditSeller(EditSellerCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> EditSellerInventory(EditSellerInventoryCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<SellerDto> GetSellerById(long id)
    {
        return await _mediator.Send(new GetSellerByIdQuery(id));
    }

    public async Task<SellerDto?> GetSellerByUserId(long userId)
    {
        return await _mediator.Send(new GetSellerByUserIdQuery(userId));
    }

    public async Task<SellerFilterResult> GetSellerByFilter(SellerFilterParams filterParams)
    {
        return await _mediator.Send(new GetSellerByFilterQuery(filterParams));
    }
}