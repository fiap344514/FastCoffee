using GeekBurger.Products.Contracts.Enums;
using GeekBurger.Products.Contracts.Models;

namespace GeekBurger.Products.Contracts.Events;

public record ProductChangedEvent(ProductState State, Product Product)
{
    public static ProductChangedEvent Deleted(Product product)
        => new(ProductState.Deleted, product);

    public static ProductChangedEvent Updated(Product product)
        => new(ProductState.Modified, product);
}