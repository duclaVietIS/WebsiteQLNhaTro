namespace WebsiteQLNhaTro.Services
{
    public class UnpaidNotificationBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<UnpaidNotificationBackgroundService> _logger;

        public UnpaidNotificationBackgroundService(
            IServiceProvider serviceProvider,
            ILogger<UnpaidNotificationBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var now = DateTime.Now;
                    
                    // Check if today is the 10th of the month
                    if (now.Day == 10)
                    {
                        _logger.LogInformation("Starting to send unpaid notifications...");
                        
                        using (var scope = _serviceProvider.CreateScope())
                        {
                            var statisticsService = scope.ServiceProvider.GetRequiredService<StatisticsService>();
                            await statisticsService.SendUnpaidNotifications();
                        }
                        
                        _logger.LogInformation("Completed sending unpaid notifications.");
                    }

                    // Wait until tomorrow
                    var tomorrow = DateTime.Today.AddDays(1);
                    var delay = tomorrow - DateTime.Now;
                    await Task.Delay(delay, stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while sending notifications");
                    await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
                }
            }
        }
    }
}