using AuthTokenServiceCore;
using AuthTokenServiceCore.Configuration;

using IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options =>
    {
        options.ServiceName = "AuthTokenService";
    })
    .ConfigureServices((hostContext, services) =>
    {
        hostContext.Configuration.GetSection("ApplicationSettings").Bind(ApplicationSettings.ApplicationOptions);
        services.AddSingleton<Service>();
        services.AddHostedService<WindowsBackgroundService>();
    })
    .Build();

await host.RunAsync();
