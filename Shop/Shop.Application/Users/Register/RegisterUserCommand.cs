using Common.Application;
using Common.Application.SecurityUtil;
using Shop.Domain.UserAgg;
using Shop.Domain.UserAgg.Repository;
using Shop.Domain.UserAgg.Services;

namespace Shop.Application.Users.Register;

public record RegisterUserCommand(string PhoneNumber, string Password) : IBaseCommand;

public class RegisterUserCommandHandler : IBaseCommandHandler<RegisterUserCommand>
{
    private readonly IUserRepository _repository;
    private readonly IUserDomainService _domainService;

    public RegisterUserCommandHandler(IUserRepository repository, IUserDomainService domainService)
    {
        _repository = repository;
        _domainService = domainService;
    }
    public async Task<OperationResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {

        var user = User.Register(request.PhoneNumber, Sha256Hasher.Hash(request.Password), _domainService);
        if (user == null)
            return OperationResult.NotFound();

        _repository.Add(user);
        await _repository.Save();
        return OperationResult.Success();

    }
}