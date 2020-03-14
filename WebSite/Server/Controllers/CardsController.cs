using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.Data;
using AdaptiveCards;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CardsController(ApplicationDbContext context, 
            IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        [HttpGet("{orderId}/createCheckInventoryCard")]
        public async Task<ActionResult<string>> CreateCheckInventoryCard(int orderId)
        {
            var order = await _context.Order.Include(x => x.Items).FirstAsync(x => x.Id == orderId);

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
                var product = _context.Products.Find(cartItem.ProductId);
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

            return Ok(json);
        }

        [HttpGet("{orderId}/createSendToShippingCard")]
        public async Task<ActionResult<string>> CreateSendToShippingCard(int orderId)
        {
            var order = await _context.Order.Include(x => x.Items).FirstAsync(x => x.Id == orderId);

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
                Text = $"Inventory verified. Please ship:",
                Weight = AdaptiveTextWeight.Bolder
            });

            // show the items in the order and their quantities
            var cartItemFactSet = new AdaptiveFactSet();
            foreach (var cartItem in order.Items)
            {
                var product = _context.Products.Find(cartItem.ProductId);
                cartItemFactSet.Facts.Add(new AdaptiveFact
                {
                    Title = product.Name,
                    Value = cartItem.Quantity.ToString()
                });
            }
            card.Body.Add(cartItemFactSet);

            // add the choices
            var choices = new AdaptiveChoiceSetInput() { Id = "ShipmentConfirmed" };
            choices.Choices.Add(new AdaptiveChoice { Title = "Yes", Value = "true" });
            choices.Choices.Add(new AdaptiveChoice { Title = "No", Value = "false" });
            card.Body.Add(choices);

            // add the submit button
            card.Actions.Add(new AdaptiveSubmitAction
            {
                Id = "ShipmentConfirmedOkButton",
                Title = "OK",
                Data = new {
                    OrderId = order.Id
                }
            });

            // get the json for the card 
            var json = card.ToJson();

            return Ok(json);
        }
    }
}