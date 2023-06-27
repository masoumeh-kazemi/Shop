using System.Security.Cryptography;
using Common.Application;
using Common.Application.SecurityUtil;
using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using FluentValidation;
using Shop.Domain.UserAgg;
using Shop.Domain.UserAgg.Enums;
using Shop.Domain.UserAgg.Repository;
using Shop.Domain.UserAgg.Services;

namespace Shop.Application.Users.Create;

public class CreateUserCommand : IBaseCommand
{
    public CreateUserCommand(string name, string family, string phoneNumber, string email, string password, string avatarName, Gender gender)
    {
        Name = name;
        Family = family;
        PhoneNumber = phoneNumber;
        Email = email;
        Password = password;
        AvatarName = avatarName;
        Gender = gender;
    }
    public string Name { get; private set; }
    public string Family { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public string AvatarName { get; private set; }
    public Gender Gender { get; private set; }
}


internal class CreateUserCommandHandler : IBaseCommandHandler<CreateUserCommand>
{
    private readonly IUserRepository _repository;
    private readonly IUserDomainService _domainService;

    public CreateUserCommandHandler(IUserRepository repository, IUserDomainService domainService)
    {
        _repository = repository;
        _domainService = domainService;
    }
    public async Task<OperationResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var password = Sha256Hasher.Hash(request.Password);
        var user = new User(request.Name, request.Family, request.PhoneNumber, request.Email, password
            , request.Gender, _domainService);

        _repository.Add(user);
        await _repository.Save();
        return OperationResult.Success();
    }
}

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(r => r.PhoneNumber)
            .ValidPhoneNumber();

        RuleFor(r => r.Email)
            .EmailAddress().WithMessage("ایمیل نامعتبر است");

        RuleFor(r => r.Password)
            .NotEmpty().WithMessage(ValidationMessages.required("کلمه عبور"))
            .NotNull()
            .MinimumLength(4).WithMessage("کلمه عبور باید بیشتر از 4 کاراکتر باشد");
    }
}