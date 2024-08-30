using Common.Application;
using Common.Application.SecurityUtil;
using MediatR;
using Shop.Application.Users.AddToken;
using Shop.Application.Users.ChangePassword;
using Shop.Application.Users.Create;
using Shop.Application.Users.Edit;
using Shop.Application.Users.Register;
using Shop.Application.Users.RemoveToken;
using Shop.Domain.UserAgg;
using Shop.Query.Users.DTOs;
using Shop.Query.Users.GetByFilter;
using Shop.Query.Users.GetById;
using Shop.Query.Users.GetByPhoneNumber;
using Shop.Query.Users.UserTokens;
using Shop.Query.Users.UserTokens.GetByRefreshToken;
using Shop.Query.Users.UserTokens.GetByToken;

namespace Shop.Presentation.Facade.Users;

public interface IUserFacade
{
    Task<OperationResult> RegisterUser(RegisterUserCommand command);
    Task<OperationResult> CreateUser(CreateUserCommand command);
    Task<OperationResult> EditUser(EditUserCommand command);
    Task<OperationResult> ChangePassword(ChangeUserPasswordCommand command);


    Task<OperationResult> AddToken(AddUserTokenCommand command);
    Task<OperationResult> RemoveToken(RemoveUserTokenCommand  command);

    Task<UserDto?> GetUserById(long userId);
    Task<UserDto?> GetUserByPhoneNumber(string phoneNumber);

    Task<UserFilterResult> GetUserByFilter(UserFilterParams filterParams);
    Task<UserTokenDto?> GetUserTokenByRefreshToken(string refreshToken);
    Task<UserTokenDto?> GetUserTokenByJwtToken(string jwtToken);
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

    public async Task<OperationResult> ChangePassword(ChangeUserPasswordCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> AddToken(AddUserTokenCommand command)
    {
        return await _mediator.Send(command);
    }

    public Task<OperationResult> RemoveToken(RemoveUserTokenCommand command)
    {
        return _mediator.Send(command);
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

    public async Task<UserTokenDto?> GetUserTokenByRefreshToken(string refreshToken)
    {
        var hashRefreshToken = Sha256Hasher.Hash(refreshToken);
        return await _mediator.Send(new GetUserTokenByRefreshTokenQuery(hashRefreshToken));
    }

    public Task<UserTokenDto?> GetUserTokenByJwtToken(string jwtToken)
    {
        var hashJwtToken = Sha256Hasher.Hash(jwtToken);
        return _mediator.Send(new GetUserTokenByJwtTokenQuery(hashJwtToken)); 
    }
}