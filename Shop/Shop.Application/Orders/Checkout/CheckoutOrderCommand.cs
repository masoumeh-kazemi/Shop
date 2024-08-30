using Common.Application;
using Shop.Domain.OrderAgg;
using Shop.Domain.OrderAgg.Repository;
using Shop.Domain.OrderAgg.ValueObjects;
using Shop.Domain.SiteEntities.Repositories;

namespace Shop.Application.Orders.Checkout;

public class CheckoutOrderCommand : IBaseCommand
{
    public CheckoutOrderCommand(long userId, string shire, string city, string postalCode, string postalAddress
        , string phoneNumber, string name, string family, string nationalCode, long shippingMethodId)
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
        ShippingMethodId = shippingMethodId;
    }
    public long UserId { get; private set; }
    public string Shire { get; private set; }
    public string City { get; private set; }
    public string PostalCode { get; private set; }
    public string PostalAddress { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Name { get; private set; }
    public string Family { get; private set; }
    public string NationalCode { get; private set; }
    public long ShippingMethodId { get; private set; }
}



public class CheckoutOrderCommandHandler : IBaseCommandHandler<CheckoutOrderCommand>
{
    private readonly IOrderRepository _repository;
    private readonly IShippingMethodRepository _shippingMethodRepository;

    public CheckoutOrderCommandHandler(IOrderRepository repository, IShippingMethodRepository shippingMethodRepository)
    {
        _repository = repository;
        _shippingMethodRepository = shippingMethodRepository;
    }
    public async Task<OperationResult> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
    {
        var currentOrder = await _repository.GetCurrentUserOrder(request.UserId);
        if (currentOrder == null)
            return OperationResult.NotFound();

        var address = new OrderAddress(request.Shire, request.City, request.PostalCode, request.PostalAddress,
            request.PhoneNumber, request.Name, request.Family, request.NationalCode);

        var shippingMethod = await _shippingMethodRepository.GetAsync(request.ShippingMethodId);
        if(shippingMethod == null)
            return OperationResult.Error();

        currentOrder.Checkout(address, new OrderShippingMethod(shippingMethod.Title, shippingMethod.Cost));
        await _repository.Save();
        return OperationResult.Success();
    }
}