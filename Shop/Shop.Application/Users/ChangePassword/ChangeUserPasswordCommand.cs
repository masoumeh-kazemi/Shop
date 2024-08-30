using System.Security.Cryptography;
using Common.Application;
using Common.Application.SecurityUtil;
using Common.Application.Validation;
using FluentValidation;
using Shop.Domain.UserAgg.Repository;

namespace Shop.Application.Users.ChangePassword;

public class ChangeUserPasswordCommand : IBaseCommand
{
    public long UserId { get; set; }
    public string Password { get; set; }
    public string CurrentPassword { get; set; }
}

public class ChangeUserPasswordCommandHandler : IBaseCommandHandler<ChangeUserPasswordCommand>
{
    private readonly IUserRepository _repository;

    public ChangeUserPasswordCommandHandler(IUserRepository repository)
    {
        _repository = repository;
    }
    public async Task<OperationResult> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetTracking(request.UserId);
        if (user == null)
         return OperationResult.NotFound("کاربر یافت نشد");
        
        var currentPasswordHash = Sha256Hasher.Hash(request.CurrentPassword);

        if (user.Password != currentPasswordHash)
            return OperationResult.Error("کلمه عبور فعلی نامعتبر است");

        var newPasswordHash = Sha256Hasher.Hash(request.Password);
        user.ChangePassword(newPasswordHash);
        await _repository.Save();
        return OperationResult.Success();
    }
}

public class ChangeUserPasswordCommandValidator : AbstractValidator<ChangeUserPasswordCommand>
{
    public ChangeUserPasswordCommandValidator()
    {
        RuleFor(r => r.CurrentPassword)
            .NotEmpty().WithMessage(ValidationMessages.required("کلمه عبور فعلی"))
            .MinimumLength(5).WithMessage(ValidationMessages.required("کلمه عبور فعلی"));

        RuleFor(r => r.Password)
            .NotEmpty().WithMessage(ValidationMessages.required("کلمه عبور"))
            .MinimumLength(5).WithMessage(ValidationMessages.required("کلمه عبور "));

    }

    
}