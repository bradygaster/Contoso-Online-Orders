using Contoso.Online.Orders.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Contoso.Online.Orders.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private static readonly Product[] Products = new[]
        {
            new Product { Id = 1, InventoryCount = 10, Name = "Cassette Tape" },
            new Product { Id = 2, InventoryCount = 8, Name = "Compact Disk" },
            new Product { Id = 3, InventoryCount = 3, Name = "DVD Disk" },
            new Product { Id = 4, InventoryCount = 3, Name = "Blu-Ray Disk" },
        };

        private readonly ILogger<ProductsController> logger;

        public ProductsController(ILogger<ProductsController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            var rng = new Random();
            return Products.ToArray();
        }
    }
}
