using ChatbotAi.Core.Services.Abstractions;
using ChatbotAi.Core.Time;
using Cronos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace ChatbotAi.Core.HostedServices;

internal class ChatHostedService : BackgroundService
{
    private readonly CronExpression _cronExpression;
    private readonly TimeZoneInfo _timeZoneInfo;
    private readonly ILogger<ChatHostedService> _logger;

    //private readonly IChatService _chatService;
    //private readonly IChatReplyQueue _replyQueue;
    //private readonly TimeProvider _timeProvider;

    private readonly IServiceProvider _serviceProvider;

    internal ChatHostedService(IServiceProvider serviceProvider, ILogger<ChatHostedService> logger)
    {
        // _chatService = chatService;
        // _replyQueue = replyQueue;
        // _timeProvider = timeProvider;
        _serviceProvider = serviceProvider;
         _logger = logger;
        _cronExpression = CronExpression.Parse("* * * * *"); 
        _timeZoneInfo = TimeZoneInfo.Local;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("CronHostedService started.");
        var timeProvider = GetTimeProvider();
        while (!stoppingToken.IsCancellationRequested)
        {
            var next = _cronExpression.GetNextOccurrence(timeProvider.GetDate(), _timeZoneInfo);
            if (next.HasValue)
            {
                var delay = next.Value - timeProvider.GetDate();
                if (delay.TotalMilliseconds > 0)
                {
                    await Task.Delay(delay, stoppingToken);
                }

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
    }

    private async Task DoWorkAsync(CancellationToken cancellationToken)
    {
        var replyQueue = GetReplyQueue();
        var chatService = GetChatService();
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

    private IChatService GetChatService()
      => _serviceProvider.GetRequiredService<IChatService>();
}