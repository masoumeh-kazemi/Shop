using Common.Application;
using Common.Domain.ValueObjects;
using Shop.Domain.UserAgg;
using Shop.Domain.UserAgg.Repository;

namespace Shop.Application.Users.AddAddress;

public class AddUserAddressCommand : IBaseCommand
{
    public AddUserAddressCommand(long userId, string shire, string city, string postalCode
        , string postalAddress, PhoneNumber phoneNumber, string name, string family, string nationalCode)
    {
        UserId = userId;
        Shire = shire;
        City = city;
        PostalCode = postalCode;
        PostalAddress = postalAddress;
        PhoneNumber = phoneNumber;
        Name = name;
        Family = family;
        NationalCode = nationalCode;
    }
    public long UserId { get; set; }
    public string Shire { get; private set; }
    public string City { get; private set; }
    public string PostalCode { get; private set; }
    public string PostalAddress { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public string Name { get; private set; }
    public string Family { get; private set; }
    public string NationalCode { get; private set; }
}

public class AddUserAddressCommandHandler : IBaseCommandHandler<AddUserAddressCommand>
{
    private readonly IUserRepository _repository;

    public AddUserAddressCommandHandler(IUserRepository repository)
    {
        _repository = repository;
    }
    public async Task<OperationResult> Handle(AddUserAddressCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetTracking(request.UserId);
        if (user == null) 
            return OperationResult.NotFound();
        var address = new UserAddress(request.Shire, request.City, request.PostalCode, request.PostalAddress,
            request.PhoneNumber, request.Name, request.Family, request.NationalCode);

        user.AddAddress(address);
        await _repository.Save();
        return OperationResult.Success();

    }
}