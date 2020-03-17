using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Server.Infrastructure
{
    public class DefaultWebHostNameDocumentFilter: IDocumentFilter
    {
        public DefaultWebHostNameDocumentFilter(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void Apply(OpenApiDocument swaggerDoc, 
            DocumentFilterContext context)
        {
            swaggerDoc.Servers = new List<OpenApiServer>()
            {
                new OpenApiServer()
                {
                    Url = Configuration["SwaggerBaseUrl"]
                }
            };
        }
    }
}