using Azure.Messaging.ServiceBus;

namespace GeekBurger.CatalogSync.Worker;

public class Worker : BackgroundService
{
    private readonly ServiceBusReceiver _busReceiver;
    private readonly ILogger<Worker> _logger;

    public Worker(
        ServiceBusReceiver busReceiver,
        ILogger<Worker> logger)
    {
        _busReceiver = busReceiver;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var message in _busReceiver.ReceiveMessagesAsync(stoppingToken).WithCancellation(stoppingToken))
        {
            await OnMessageReceivedHandlerAsync(message, stoppingToken);
        }
    }

    public override Task StopAsync(CancellationToken cancellationToken)
        => Task.WhenAll(
            _busReceiver.CloseAsync(cancellationToken),
            base.StopAsync(cancellationToken));

    private async Task OnMessageReceivedHandlerAsync(
        ServiceBusReceivedMessage message,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Raw message received: {MessageBody}", message.Body);
        await _busReceiver.CompleteMessageAsync(message, cancellationToken);
    }
}
