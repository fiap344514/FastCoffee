using static GeekBurger.Products.Api.Endpoints.EndpointsHandlers.Products;

namespace GeekBurger.Products.Api.Endpoints;

internal static class EndpointsMapping
{
    internal static void MapProductEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/products", GetAllHandler);
        endpoints.MapGet("/api/products/{productId:guid}", GetByIdHandler);
        endpoints.MapPost("/api/products", AddHandler);
        endpoints.MapPut("/api/products/{productId:guid}", UpdateHandler);
        endpoints.MapDelete("/api/products/{productId:guid}", DeleteHandler);
    }
}
