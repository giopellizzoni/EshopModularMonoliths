using System.Text.Json;

using MassTransit;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Basket.Data.Processors;

public class OutboxProcessor : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IBus _bus;
    private readonly ILogger<OutboxProcessor> _logger;

    public OutboxProcessor(
        IServiceProvider serviceProvider,
        IBus bus,
        ILogger<OutboxProcessor> logger)
    {
        _serviceProvider = serviceProvider;
        _bus = bus;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<BasketDbContext>();
                var outboxMessages = await GetOutboxMessages(stoppingToken, dbContext);

                await PublishMessages(stoppingToken, outboxMessages);
                await dbContext.SaveChangesAsync(stoppingToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error processing outbox messages");

                throw;
            }

            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }

    private async Task PublishMessages(
        CancellationToken stoppingToken,
        List<OutboxMessage> outboxMessages)
    {
        foreach (var message in outboxMessages)
        {
            var eventType = Type.GetType(message.Type);
            if (eventType == null)
            {
                _logger.LogWarning("Event type {Type} not found", message.Type);

                continue;
            }

            var eventMessage = JsonSerializer.Deserialize(message.Content, eventType);
            if (eventMessage == null)
            {
                _logger.LogWarning("Failed to deserialize message {Id}", message.Id);

                continue;
            }

            await _bus.Publish(eventMessage, stoppingToken);
            message.Processed = true;
            message.ProcessedOn = DateTime.UtcNow;
            _logger.LogInformation("Published message {Id}", message.Id);
        }
    }

    private static async Task<List<OutboxMessage>> GetOutboxMessages(
        CancellationToken stoppingToken,
        BasketDbContext dbContext)
    {
        var outboxMessages = await dbContext.OutboxMessages
            .Where(x => !x.Processed)
            .ToListAsync(stoppingToken);

        return outboxMessages;
    }
}
