using System;
using System.Threading.Tasks;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Contoso.Online.Orders
{
    public static class OrderReceived
    {
        [FunctionName("OrderReceived")]
        public static async Task Run(
            [QueueTrigger("order-received", Connection = "AzureWebJobsStorage")] string myQueueItem,
            [EventGrid(TopicEndpointUri = "OrderReceivedTopicEndpointUri", TopicKeySetting = "OrderReceivedTopicKeySetting")]IAsyncCollector<EventGridEvent> outputEvents,
            ILogger log)
        {
            var myEvent = new EventGridEvent(myQueueItem, $"Order {myQueueItem} Received", myQueueItem, "orderReceived", DateTime.UtcNow, "1.0");
            await outputEvents.AddAsync(myEvent);
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
