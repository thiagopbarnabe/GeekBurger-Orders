using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GeekBurger.Orders.Contract;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Polly;
using Polly.Registry;
using SubscriptionClient = Microsoft.Azure.ServiceBus.SubscriptionClient;


namespace Order.Receiver
{
    class Program
    {
        private const string TopicName = "NewOrder";
        private static IConfiguration _configuration;
        private static ServiceBusConfiguration serviceBusConfiguration;
        private static string _storeId;
        private const string SubscriptionName = "Los_Angeles_Pasadena_store";
        

        static void Main(string[] args)
        {
            if (args.Length <= 0)
            {
                Console.WriteLine("Inform storeId and hit enter to get NewOrder messages");
                _storeId = Console.ReadLine();
            }
            else
                _storeId = args[0];

            //https://github.com/Azure-Samples/service-bus-dotnet-manage-publish-subscribe-with-basic-features

            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            serviceBusConfiguration = _configuration.GetSection("serviceBus").Get<ServiceBusConfiguration>();

            var serviceBusNamespace = _configuration.GetServiceBusNamespace();

            var topic = serviceBusNamespace.Topics.GetByName(TopicName);

            topic.Subscriptions.DeleteByName(SubscriptionName);

            if (!topic.Subscriptions.List()
                   .Any(subscription => subscription.Name
                       .Equals(SubscriptionName, StringComparison.InvariantCultureIgnoreCase)))
                topic.Subscriptions
                    .Define(SubscriptionName)
                    .Create();

            ReceiveMessages();

            Console.ReadLine();
        }

        private static async void ReceiveMessages()
        {
            var subscriptionClient = new SubscriptionClient(serviceBusConfiguration.ConnectionString, TopicName, SubscriptionName);

            //by default a 1=1 rule is added when subscription is created, so we need to remove it
            await subscriptionClient.RemoveRuleAsync("$Default");

            await subscriptionClient.AddRuleAsync(new RuleDescription
            {
                Filter = new CorrelationFilter { Label = _storeId },
                Name = "filter-store"
            });

            var mo = new MessageHandlerOptions(ExceptionHandle) { AutoComplete = true };

            subscriptionClient.RegisterMessageHandler(Handle, mo);

            Console.ReadLine();
        }

        private static async Task Handle(Message message, CancellationToken arg2)
        {
            Console.WriteLine($"message Label: {message.Label}");
            Console.WriteLine($"message CorrelationId: {message.CorrelationId}");
            var newOrderString = Encoding.UTF8.GetString(message.Body);
            
            var order = JsonConvert.DeserializeObject<NewOrderMessage>(newOrderString);

            var _orderToUpsert = new OrderToUpsert();
            _orderToUpsert.OrderId = order.OrderId;
            _orderToUpsert.Products = order.Products;
            _orderToUpsert.Productions = order.ProductionIds;
            

            Console.WriteLine("Message Received");
            Console.WriteLine(newOrderString);

            //Thread.Sleep(40000);


            var client = new HttpClient();
            var byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(_orderToUpsert));
            var content = new ByteArrayContent(byteData);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var _policyRegistry = new PolicyRegistry();
            var apiUrl = "/api/order";
            var _baseUri = "http://localhost:5009/";

            var retryPolicy = _policyRegistry.Get<IAsyncPolicy<HttpResponseMessage>>("basic-retry")
                              ?? Policy.NoOpAsync<HttpResponseMessage>();

            var context = new Context($"GetSomeData-{Guid.NewGuid()}", new Dictionary<string, object>
                {
                    { "url", apiUrl }
                });

            var retries = 0;
            // ReSharper disable once AccessToDisposedClosure
            var response = await retryPolicy.ExecuteAsync((ctx) =>
            {
                client.DefaultRequestHeaders.Remove("retries");
                client.DefaultRequestHeaders.Add("retries", new[] { retries++.ToString() });

                var baseUrl = _baseUri;
                var isValid = Uri.IsWellFormedUriString(apiUrl, UriKind.Absolute);

                return client.PostAsync(isValid ? $"{baseUrl}{apiUrl}" : $"{baseUrl}/api/Order", content);
            }, context);

            content.Dispose();

            return;                
        }

        private static Task ExceptionHandle(ExceptionReceivedEventArgs arg)
        {
            Console.WriteLine($"Message handler encountered an exception {arg.Exception}.");
            var context = arg.ExceptionReceivedContext;
            Console.WriteLine($"- Endpoint: {context.Endpoint}, Path: {context.EntityPath}, Action: {context.Action}");
            return Task.CompletedTask;
        }
    }
}