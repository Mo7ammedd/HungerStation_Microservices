using HungerStation.Services.RewardAPI.Messaging;

namespace HungerStation.Services.RewardAPI.Extensions;

public static class ApplicationBuilderExtensions
{
    private static IAzureServiceBusConsumer ServiceBusConsumer { get; set; } = null!;

    public static IApplicationBuilder UseAzureServiceBusConsumer(this IApplicationBuilder app)
    {
        ServiceBusConsumer = app.ApplicationServices.GetService<IAzureServiceBusConsumer>()!;
        var hostApplicationLife = app.ApplicationServices.GetService<IHostApplicationLifetime>()!;

        hostApplicationLife.ApplicationStarted.Register(OnStart);
        hostApplicationLife.ApplicationStopping.Register(OnStop);

        return app;
    }

    private static void OnStart()
    {
        ServiceBusConsumer.Start();
    }

    private static void OnStop()
    {
        ServiceBusConsumer.Stop();
    }
}
