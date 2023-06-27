using Common.Application;
using Shop.Domain.UserAgg;
using Shop.Domain.UserAgg.Repository;

namespace Shop.Application.Users.AddToken;

public class AddUserTokenCommand : IBaseCommand
{
    public AddUserTokenCommand(long userId, string hasJwtToken, string hashRefreshToken, DateTime tokenExpireDate, DateTime refreshTokenExpireDate, string device)
    {
        UserId = userId;
        HasJwtToken = hasJwtToken;
        HashRefreshToken = hashRefreshToken;
        TokenExpireDate = tokenExpireDate;
        RefreshTokenExpireDate = refreshTokenExpireDate;
        Device = device;
    }
    public long UserId { get; }
    public string HasJwtToken { get; }
    public string HashRefreshToken { get; }
    public DateTime TokenExpireDate { get; }
    public DateTime RefreshTokenExpireDate { get; }
    public string Device { get;  }
}

public class AddUserTokenCommandHandler : IBaseCommandHandler<AddUserTokenCommand>
{
    private readonly IUserRepository _repository;

    public AddUserTokenCommandHandler(IUserRepository repository)
    {
        _repository = repository;
    }
    public async Task<OperationResult> Handle(AddUserTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetTracking(request.UserId);
        if (user == null) 
            return OperationResult.NotFound();

        user.AddToken(request.HasJwtToken,request.HashRefreshToken, request.TokenExpireDate, request.RefreshTokenExpireDate, request.Device);
        await _repository.Save();
        return OperationResult.Success();
    }
}