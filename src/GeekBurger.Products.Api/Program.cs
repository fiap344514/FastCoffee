using Azure.Messaging.ServiceBus;
using GeekBurger.Products.Api.Endpoints;
using GeekBurger.Products.Api.Repositories;
using GeekBurger.Products.Api.Repositories.Impl;
using Microsoft.Extensions.Azure;

var builder = WebApplication.CreateBuilder(args);

var serviceBusConfigSection = builder.Configuration.GetSection("ServiceBus");

builder.Services.AddScoped<IProductRepository, InMemoryProductRepository>();
builder.Services.AddAzureClients(clientBuilder => clientBuilder.AddServiceBusClient(serviceBusConfigSection["ConnectionString"]));
builder.Services.AddSingleton(sp =>
{
    var serviceBusClient = sp.GetRequiredService<ServiceBusClient>();
    return serviceBusClient.CreateSender(serviceBusConfigSection["QueueOrTopicName"]);
});

var app = builder.Build();

app.MapProductEndpoints();

await app
    .RunAsync()
    .ConfigureAwait(false);
