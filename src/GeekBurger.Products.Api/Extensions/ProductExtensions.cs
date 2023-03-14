using Azure.Messaging;
using Azure.Messaging.ServiceBus;
using GeekBurger.Products.Contracts.Enums;
using GeekBurger.Products.Contracts.Events;
using GeekBurger.Products.Contracts.Models;

namespace GeekBurger.Products.Api.Extensions;

internal static class ProductExtensions
{
    internal static ServiceBusMessage ToMessage(this Product product, EventType eventType)
    {
        var eventSubject = $"Product{eventType}Event";
        var @event = new CloudEvent(
            source: $"/cloudevents/products/events/{eventSubject}/{product.Id}",
            type: eventSubject,
            jsonSerializableData: eventType switch
            {
                EventType.Added => new ProductAddedEvent(product),
                EventType.Updated => ProductChangedEvent.Updated(product),
                EventType.Deleted => ProductChangedEvent.Deleted(product),
                _ => throw new ArgumentOutOfRangeException(nameof(eventType)),
            })
        {
            DataContentType = "application/json",
            Subject = eventSubject,
        };

        return new ServiceBusMessage(new BinaryData(@event))
        {
            ContentType = "application/cloudevents+json",
            Subject = eventSubject,
        };
    }
}
