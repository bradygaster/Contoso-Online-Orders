using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Contoso.Data;
using Microsoft.EntityFrameworkCore;
using AdaptiveCards;

namespace Contoso.CardFunctions
{
    public partial class Functions
    {
        public Functions(ApplicationDbContext applicationDbContext)
        {
            ApplicationDbContext = applicationDbContext;
        }

        public ApplicationDbContext ApplicationDbContext { get; }

        
    }
}
