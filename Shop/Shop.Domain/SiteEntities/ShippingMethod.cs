using Common.Domain;
using Common.Domain.Exceptions;

namespace Shop.Domain.SiteEntities;

public class ShippingMethod : BaseEntity
{
    public ShippingMethod(string title, int cost)
    {
        NullOrEmptyDomainDataException.CheckString(title,nameof(Title));
        Title = title;
        Cost = cost;
    }

    public void Edit(string title, int cost)
    {
        NullOrEmptyDomainDataException.CheckString(title, nameof(Title));
        Title = title;
        Cost = cost;
    }
    public string Title { get; private set; }
    public int Cost { get; private set; }
}