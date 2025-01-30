using System.Text;
using Azure.Messaging.ServiceBus;
using HungerStation.Services.EmailAPI.Models.Dtos;
using HungerStation.Services.EmailAPI.Services;
using Newtonsoft.Json;

namespace HungerStation.Services.EmailAPI.Messaging;

public class AzureServiceBusConsumer : IAzureServiceBusConsumer
{
   private readonly IConfiguration _configuration;
   private readonly IEmailService _emailService;
   private readonly string serviceBusConnectionString;
   private readonly string emailCartQueueName;
   private ServiceBusProcessor _emailCartProcessor;
   public AzureServiceBusConsumer(IConfiguration configuration,IEmailService emailService)
   {
      _configuration = configuration;
      _emailService = emailService;
      serviceBusConnectionString = _configuration["ServiceBusConnectionStrings"];
      emailCartQueueName = _configuration["TopicAndQueueNames:EmailShoppingCartQueue"];
      var client = new ServiceBusClient(serviceBusConnectionString);
      _emailCartProcessor = client.CreateProcessor(emailCartQueueName);
      
      
   }


   public async Task Start()
   {
      _emailCartProcessor.ProcessMessageAsync += OnEmailCartRequestReceived;
      _emailCartProcessor.ProcessErrorAsync += ErrorHandler;
      await _emailCartProcessor.StartProcessingAsync();
   }

   public async Task Stop()
   {
      await _emailCartProcessor.StopProcessingAsync();
      await _emailCartProcessor.DisposeAsync();
   }
   private async Task OnEmailCartRequestReceived(ProcessMessageEventArgs arg)
   {
      var message = arg.Message;
      var body = Encoding.UTF8.GetString(message.Body);
      
      CartDto objMessage = JsonConvert .DeserializeObject<CartDto>(body);
      try
      {
         await _emailService.EmailCartAndLogAsync(objMessage);
         await arg.CompleteMessageAsync(arg.Message);

      }
      catch (Exception e)
      {
         Console.WriteLine(e);
         throw;
      }
   }

   private Task ErrorHandler(ProcessErrorEventArgs arg)
   {
      Console.WriteLine(arg.Exception.ToString());
      return Task.CompletedTask;
   }

} 