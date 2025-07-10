using ChatbotAi.Core.Services.Abstractions;
using ChatbotAi.Core.Time;
using Cronos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace ChatbotAi.Core.HostedServices;

public class ChatHostedService : BackgroundService
{
    private readonly ILogger<ChatHostedService> _logger;

    private readonly IServiceProvider _serviceProvider;

    public ChatHostedService(IServiceProvider serviceProvider, ILogger<ChatHostedService> logger)
    {
        _serviceProvider = serviceProvider;
         _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("CronHostedService started.");
        var timeProvider = GetTimeProvider();
        while (!stoppingToken.IsCancellationRequested)
        {
            var delay = new Random().Next(300, 1500);
            await Task.Delay(delay, stoppingToken);
            try
            {
                _logger.LogInformation("Executing scheduled task at: {Time}", timeProvider.GetDate());
                await DoWorkAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during scheduled task.");
            }
        }
    }

    private async Task DoWorkAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var replyQueue = GetReplyQueue();
        var chatService = scope.ServiceProvider.GetRequiredService<IChatService>();
        for (int i = 0; i < 10; i++)
        {
            var message = await replyQueue.DequeueMessage(cancellationToken);

            if(message is null)
            {
                return;
            }

            await chatService.GenerateBotReplyAsync(message.Id, cancellationToken);
        }
    }

    private TimeProvider GetTimeProvider()
        =>_serviceProvider.GetRequiredService<TimeProvider>();

    private IChatReplyQueue GetReplyQueue()
       => _serviceProvider.GetRequiredService<IChatReplyQueue>();
}