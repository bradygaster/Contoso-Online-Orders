using System;
using Contoso.Data;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(CardFunctions.Startup))]

namespace CardFunctions
{
    class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            string connectionString = 
                Environment.GetEnvironmentVariable("SqlConnectionString");
                
            builder.Services.AddDbContext<ApplicationDbContext>(
                options => SqlServerDbContextOptionsExtensions
                    .UseSqlServer(options, connectionString));
        }
    }
}