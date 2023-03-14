using GeekBurger.Products.Contracts.Models;

namespace GeekBurger.Products.Api.Repositories.Impl;

internal sealed class InMemoryProductRepository : IProductRepository
{
    private static readonly List<Product> _inMemoryProducts = new(capacity: 0);

    public Guid Add(Product product)
    {
        _inMemoryProducts.Add(product);
        return product.Id;
    }

    public Product? Delete(Guid productId)
    {
        var productToDelete = _inMemoryProducts.Find(p => p.Id.Equals(productId));
        if (productToDelete is null)
        {
            return null;
        }

        _inMemoryProducts.Remove(productToDelete);
        return productToDelete;
    }

    public IEnumerable<Product> GetAll() => _inMemoryProducts;

    public Product? GetById(Guid productId) => _inMemoryProducts.Find(p => p.Id.Equals(productId));

    public Product? Update(Guid productId, Product product)
    {
        var productToUpdateIndex = _inMemoryProducts.FindIndex(p => p.Id.Equals(productId));
        if (productToUpdateIndex is -1)
        {
            return null;
        }

        _inMemoryProducts.RemoveAt(productToUpdateIndex);

        var updatedProduct = product with { Id = productId };
        _inMemoryProducts.Insert(productToUpdateIndex, updatedProduct);

        return updatedProduct;
    }
}
