using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using CORE.MessagesCQRS.Mediator;
using CORE.IMongoRepository;
using Nfe.CQRS.Command;

namespace ProcessadorNfe.Function.Functions.ServiceBus
{
    public class AtualizarNfesDaFila
    {
        private readonly IMediatrHandler _cqrs;
        private readonly INfeRepository _nfeService;

        public AtualizarNfesDaFila(IMediatrHandler cqrs, INfeRepository nfeService)
        {
            _cqrs = cqrs;
            _nfeService = nfeService;
        }
        [FunctionName("AtualizarNfesDaFila")]
        public async Task Run([ServiceBusTrigger("notas-para-processar", Connection = "serviceBus")] string myQueueItem, ILogger log)
        {
            var nfe = new AtualizarNfeIntegracaoCommand();
            nfe.NfId = myQueueItem.Replace("\"", "");
            await _cqrs.EnviarComandoGenerico(nfe);

        }
    }
}
