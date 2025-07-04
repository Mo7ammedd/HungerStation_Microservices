using System.Text;
using Azure.Messaging.ServiceBus;
using HungerStation.Services.RewardAPI.Services;
using Newtonsoft.Json;

namespace HungerStation.Services.RewardAPI.Messaging;

public class AzureServiceBusConsumer : IAzureServiceBusConsumer
{
    private readonly IConfiguration _configuration;
    private readonly IRewardService _rewardService;
    private readonly string serviceBusConnectionString;
    private readonly string orderCreatedTopicName;
    private readonly string orderCreatedRewardSubscription;
    private ServiceBusProcessor _rewardProcessor;

    public AzureServiceBusConsumer(IConfiguration configuration, IRewardService rewardService)
    {
        _configuration = configuration;
        _rewardService = rewardService;
        serviceBusConnectionString = _configuration["ServiceBusConnectionStrings"]!;
        orderCreatedTopicName = _configuration["TopicAndQueueNames:OrderCreatedTopic"]!;
        orderCreatedRewardSubscription = _configuration["TopicAndQueueNames:OrderCreated_Rewards_Subscription"]!;

        var client = new ServiceBusClient(serviceBusConnectionString);
        _rewardProcessor = client.CreateProcessor(orderCreatedTopicName, orderCreatedRewardSubscription);
    }

    public async Task Start()
    {
        _rewardProcessor.ProcessMessageAsync += OnNewOrderRewardsRequestReceived;
        _rewardProcessor.ProcessErrorAsync += ErrorHandler;
        await _rewardProcessor.StartProcessingAsync();
    }

    public async Task Stop()
    {
        await _rewardProcessor.StopProcessingAsync();
        await _rewardProcessor.DisposeAsync();
    }

    private async Task OnNewOrderRewardsRequestReceived(ProcessMessageEventArgs args)
    {
        var message = args.Message;
        var body = Encoding.UTF8.GetString(message.Body);

        RewardsMessage objMessage = JsonConvert.DeserializeObject<RewardsMessage>(body)!;
        try
        {
            await _rewardService.UpdateRewards(objMessage);
            await args.CompleteMessageAsync(args.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing rewards message: {ex.Message}");
            throw;
        }
    }

    private Task ErrorHandler(ProcessErrorEventArgs args)
    {
        Console.WriteLine($"Service Bus Error: {args.Exception}");
        return Task.CompletedTask;
    }
}
