#nullable disable
namespace GeekBurger.Products.Contracts.Models;

public record Product
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string Name { get; init; }

    public string Image { get; init; }

    public decimal Price { get; init; }

    public string StoreName { get; init; }

    public ProductItem[] Items { get; init; }
}
