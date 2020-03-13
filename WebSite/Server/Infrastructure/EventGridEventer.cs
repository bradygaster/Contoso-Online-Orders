using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;

namespace Server.Infrastructure
{
    public class EventGridEventer
    {
        public static async Task FireEventAsync(string topicEndpoint,
            string topicKey,
            string eventName,
            string subject,
            object eventData)
        {
            string topicHostname = new Uri(topicEndpoint).Host;
            TopicCredentials topicCredentials = new TopicCredentials(topicKey);
            EventGridClient client = new EventGridClient(topicCredentials);

            await client.PublishEventsAsync(topicHostname, 
                new List<EventGridEvent>(new EventGridEvent[] {
                    new EventGridEvent
                    {
                        Id = Guid.NewGuid().ToString(),
                        EventType = eventName,
                        Data = eventData,
                        EventTime = DateTime.Now,
                        Subject = subject,
                        DataVersion = "2.0"
                    }
                })
            );
        }
    }
}