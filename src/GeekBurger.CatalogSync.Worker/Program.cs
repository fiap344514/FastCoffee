using Azure.Messaging.ServiceBus;
using GeekBurger.CatalogSync.Worker;
using Microsoft.Extensions.Azure;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((host, services) =>
    {
        services.AddAzureClients(clientBuilder => clientBuilder
            .AddServiceBusClient(host.Configuration["ServiceBus:ConnectionString"]));
        services.AddSingleton(sp =>
        {
            var serviceBusClient = sp.GetRequiredService<ServiceBusClient>();
            return serviceBusClient.CreateReceiver(host.Configuration["ServiceBus:QueueOrTopicName"]);
        });
        services.AddHostedService<Worker>();
    })
    .Build();

await host
    .RunAsync()
    .ConfigureAwait(false);
