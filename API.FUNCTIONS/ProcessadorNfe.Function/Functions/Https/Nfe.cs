using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Nfe.CQRS.Queries;

namespace ProcessadorNfe.Function.Functions.Https
{
    public class Nfe
    {
        private readonly INfeQuery _nfeQuery;
        public Nfe(INfeQuery nfeQuery)
        {
            _nfeQuery = nfeQuery;
        }
        [FunctionName("ObterNfesNaoBaixadas")]
        public async Task<IActionResult> RunObterNfesNaoBaixadas([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("ObterNfesNaoBaixadas");


            var nfes = await _nfeQuery.ObterNfesNaoBaixadas();

            return new OkObjectResult(nfes);


        }

        [FunctionName("ObterNfePeloId")]
        public async Task<IActionResult> RunObterNfePeloId([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");


            var id = req.Query["id"];
            var nfe = await _nfeQuery.ObterNfeParaBaixarPeloId(id);

            return new OkObjectResult(nfe);

        }
    }
}
