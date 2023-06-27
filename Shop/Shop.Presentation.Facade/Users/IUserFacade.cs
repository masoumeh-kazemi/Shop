using Common.Application;
using MediatR;
using Shop.Application.Users.AddToken;
using Shop.Application.Users.Create;
using Shop.Application.Users.Edit;
using Shop.Application.Users.Register;
using Shop.Domain.UserAgg;
using Shop.Query.Users.DTOs;
using Shop.Query.Users.GetByFilter;
using Shop.Query.Users.GetById;
using Shop.Query.Users.GetByPhoneNumber;

namespace Shop.Presentation.Facade.Users;

public interface IUserFacade
{
    Task<OperationResult> RegisterUser(RegisterUserCommand command);
    Task<OperationResult> CreateUser(CreateUserCommand command);
    Task<OperationResult> EditUser(EditUserCommand command);

    Task<OperationResult> AddToken(AddUserTokenCommand command);

    Task<UserDto?> GetUserById(long userId);
    Task<UserDto?> GetUserByPhoneNumber(string phoneNumber);

    Task<UserFilterResult> GetUserByFilter(UserFilterParams filterParams);
}

public class UserFacade : IUserFacade
{
    private readonly IMediator _mediator;

    public UserFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> RegisterUser(RegisterUserCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> CreateUser(CreateUserCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> EditUser(EditUserCommand command)
    {
        return await _mediator.Send(command);

    }

    public async Task<OperationResult> AddToken(AddUserTokenCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<UserDto?> GetUserById(long userId)
    {
        return  await _mediator.Send(new GetUserByIdQuery(userId));
    }

    public async Task<UserDto?> GetUserByPhoneNumber(string phoneNumber)
    {
        return await _mediator.Send(new GetUserByPhoneNumberQuery(phoneNumber));
    }

    public async Task<UserFilterResult> GetUserByFilter(UserFilterParams filterParams)
    {
        return await _mediator.Send(new GetUserByFilterQuery(filterParams));

    }
}