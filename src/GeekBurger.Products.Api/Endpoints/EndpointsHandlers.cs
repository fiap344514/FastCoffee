using Azure.Messaging.ServiceBus;
using GeekBurger.Products.Api.Extensions;
using GeekBurger.Products.Api.Repositories;
using GeekBurger.Products.Contracts.Enums;
using GeekBurger.Products.Contracts.Models;
using Microsoft.AspNetCore.Mvc;

namespace GeekBurger.Products.Api.Endpoints;

internal static class EndpointsHandlers
{
    internal static class Products
    {
        internal static IResult GetAllHandler([FromServices] IProductRepository products)
            => Results.Ok(products.GetAll());

        internal static IResult GetByIdHandler(
            [FromRoute] Guid productId,
            [FromServices] IProductRepository products)
            => products.GetById(productId) switch
            {
                null => Results.NotFound(),
                var product => Results.Ok(product),
            };

        internal static async Task<IResult> AddHandler(
            [FromBody] Product product,
            [FromServices] IProductRepository products,
            [FromServices] ServiceBusSender busSender)
        {
            var newProductId = products.Add(product);

            await busSender
                .SendMessageAsync(product.ToMessage(EventType.Added))
                .ConfigureAwait(false);

            return Results.Created($"/api/products/{newProductId}", null);
        }

        internal static async Task<IResult> UpdateHandler(
            [FromRoute] Guid productId,
            [FromBody] Product product,
            [FromServices] IProductRepository products,
            [FromServices] ServiceBusSender busSender)
        {
            var updatedProduct = products.Update(productId, product);
            if (updatedProduct is null)
            {
                return Results.NotFound();
            }

            await busSender
                .SendMessageAsync(updatedProduct.ToMessage(EventType.Updated))
                .ConfigureAwait(false);

            return Results.Ok(updatedProduct);
        }

        internal static async Task<IResult> DeleteHandler(
            [FromRoute] Guid productId,
            [FromServices] IProductRepository products,
            [FromServices] ServiceBusSender busSender)
        {
            var deletedProduct = products.Delete(productId);
            if (deletedProduct is null)
            {
                return Results.NotFound();
            }

            await busSender
                .SendMessageAsync(deletedProduct.ToMessage(EventType.Deleted))
                .ConfigureAwait(false);

            return Results.NoContent();
        }
    }
}
