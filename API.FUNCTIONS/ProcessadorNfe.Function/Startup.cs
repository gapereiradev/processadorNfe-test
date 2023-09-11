using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Nfe.API.Utils;
using System;

[assembly: FunctionsStartup(typeof(ProcessadorNfe.Function.Startup))]

namespace ProcessadorNfe.Function
{
    public class Startup : FunctionsStartup
    {
        private readonly IConfiguration _configuration;
        public Startup()
        {
                
        }
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public override void Configure(IFunctionsHostBuilder builder)
        {
            string mongoCS = Environment.GetEnvironmentVariable("mongoDB");
            string serviceBusCS = Environment.GetEnvironmentVariable("serviceBus");
            string blobStorageCS = Environment.GetEnvironmentVariable("blobStorage");

            builder.Services.InjecaodeDependencia(mongoCS, serviceBusCS, blobStorageCS);

        }

    }
}
