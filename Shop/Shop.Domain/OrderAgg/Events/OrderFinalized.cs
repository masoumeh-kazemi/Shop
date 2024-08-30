using Common.Domain;

namespace Shop.Domain.OrderAgg.Events;

public class OrderFinalized : BaseDomainEvent
{
    public long OrderId { get; private set; }

    public OrderFinalized(long orderId)
    {
        OrderId = orderId;
    }
}