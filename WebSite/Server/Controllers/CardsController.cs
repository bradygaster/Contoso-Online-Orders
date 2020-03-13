using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.Data;
using AdaptiveCards;
using System;
using System.Collections.Generic;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CardsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{orderId}/sendCheckInventoryCard")]
        public async Task<ActionResult<string>> SendCheckInventoryCard(int orderId)
        {
            var order = await _context.Order.FindAsync(orderId);

            AdaptiveCard card = new AdaptiveCard(new AdaptiveSchemaVersion(1, 0));

            card.Body.Add(new AdaptiveTextBlock() 
            {
                Text = $"Order #{orderId} Received",
                Size = AdaptiveTextSize.Large,
                Weight = AdaptiveTextWeight.Bolder
            });

            var choices = new AdaptiveChoiceSetInput()
            {
                Id = "IsInventoryAvailable"
            };

            choices.Choices.Add(new AdaptiveChoice
            {
                Title = "Yes",
                Value = "true"
            });

            choices.Choices.Add(new AdaptiveChoice
            {
                Title = "No",
                Value = "false"
            });

            card.Body.Add(choices);

            card.Actions.Add(new AdaptiveSubmitAction
            {
                Id = "InventoryIsOK",
                Title = "OK",
                Data = new {
                    OrderId = order.Id
                }
            });

            var json = card.ToJson();

            return Ok(json);
        }
    }
}