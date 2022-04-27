namespace AuthTokenServiceCore
{
    public sealed class WindowsBackgroundService : BackgroundService
    {
        private readonly Service _service;
        private readonly ILogger<WindowsBackgroundService> _logger;

        public WindowsBackgroundService(
            Service jokeService,
            ILogger<WindowsBackgroundService> logger) =>
            (_service, _logger) = (jokeService, logger);

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                string joke = _service.GetJoke();
                _logger.LogWarning(joke);

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}