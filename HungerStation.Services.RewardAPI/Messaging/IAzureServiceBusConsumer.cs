namespace HungerStation.Services.RewardAPI.Messaging;

public interface IAzureServiceBusConsumer
{
    Task Start();
    Task Stop();
}
