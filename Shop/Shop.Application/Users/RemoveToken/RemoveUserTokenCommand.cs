using Common.Application;
using Shop.Domain.UserAgg.Repository;

namespace Shop.Application.Users.RemoveToken;

public record RemoveUserTokenCommand(long UserId, long TokenId) : IBaseCommand
{
    
}

internal class RemoveUserTokenCommandHandler : IBaseCommandHandler<RemoveUserTokenCommand>
{
    private readonly IUserRepository _userRepository;

    public RemoveUserTokenCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<OperationResult> Handle(RemoveUserTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetTracking(request.UserId);
        if (user == null)
        {
            return OperationResult.NotFound();
        }
        
        user.RefreshToken(request.TokenId);
        await _userRepository.Save();
        return OperationResult.Success();
    }
}