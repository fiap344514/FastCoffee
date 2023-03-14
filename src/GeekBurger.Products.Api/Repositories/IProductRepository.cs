using GeekBurger.Products.Contracts.Models;

namespace GeekBurger.Products.Api.Repositories;

public interface IProductRepository
{
    IEnumerable<Product> GetAll();

    Product? GetById(Guid productId);

    Guid Add(Product product);

    Product? Update(Guid productId, Product product);

    Product? Delete(Guid productId);
}
