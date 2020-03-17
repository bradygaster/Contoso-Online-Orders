using System;
using System.Threading.Tasks;
using AdaptiveCards;
using Contoso.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Contoso.CardFunctions
{
    public partial class Functions
    {
        [FunctionName("GetInventoryCardJson")]
        public async Task<IActionResult> GetInventoryCardJson(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("GetInventoryCardJson function processed a request.");
            int orderId = Convert.ToInt32(req.Query["orderId"]);

            var order = await ApplicationDbContext
                                .Order
                                    .Include(x => x.Items)
                                    .FirstAsync(x => x.Id == orderId);

            AdaptiveCard card = new AdaptiveCard(new AdaptiveSchemaVersion(1, 0));

            // add the heading
            card.Body.Add(new AdaptiveTextBlock() 
            {
                Text = $"Order #{orderId} Received",
                Size = AdaptiveTextSize.Large,
                Weight = AdaptiveTextWeight.Bolder
            });

            // add the call to action
            card.Body.Add(new AdaptiveTextBlock() 
            {
                Text = $"Please verify the inventory is in stock",
                Weight = AdaptiveTextWeight.Bolder
            });

            // show the items in the order and their quantities
            var cartItemFactSet = new AdaptiveFactSet();
            foreach (var cartItem in order.Items)
            {
                var product = ApplicationDbContext.Products.Find(cartItem.ProductId);
                cartItemFactSet.Facts.Add(new AdaptiveFact
                {
                    Title = product.Name,
                    Value = cartItem.Quantity.ToString()
                });
            }
            card.Body.Add(cartItemFactSet);

            // add the confirmation
            card.Body.Add(new AdaptiveTextBlock() 
            {
                Text = $"Is this order in inventory?",
                Weight = AdaptiveTextWeight.Bolder
            });

            // add the choices
            var choices = new AdaptiveChoiceSetInput() { Id = "IsInventoryAvailable" };
            choices.Choices.Add(new AdaptiveChoice { Title = "Yes", Value = "true" });
            choices.Choices.Add(new AdaptiveChoice { Title = "No", Value = "false" });
            card.Body.Add(choices);

            // add the submit button
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Id = "InventoryIsOK",
                Title = "OK",
                Data = new {
                    OrderId = order.Id
                }
            });

            // get the json for the card 
            var json = card.ToJson();

            return new OkObjectResult(json);
        }
    }
}