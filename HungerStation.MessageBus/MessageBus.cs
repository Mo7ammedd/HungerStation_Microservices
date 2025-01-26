using System.Text;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;

namespace HungerStation.MessageBus;

public class MessageBus : IMessageBus
{
    private readonly string _connectionString;

    public MessageBus(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("AzureServiceBusConnectionString");
    }

    public async Task PublishMessage(object message, string topic_queue_name)
    {
        await using var client = new ServiceBusClient(_connectionString);
        ServiceBusSender sender = client.CreateSender(topic_queue_name);

        var messageBody = JsonSerializer.Serialize(message);
        ServiceBusMessage serviceBusMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(messageBody))
        {
            CorrelationId = Guid.NewGuid().ToString()
        };
        await sender.SendMessageAsync(serviceBusMessage);
        await client.DisposeAsync();
    }
}