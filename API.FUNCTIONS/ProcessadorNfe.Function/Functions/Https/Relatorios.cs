using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Nfe.CQRS.Queries;
using CORE.DTOS;
using CORE.Relatorios;
using CORE.IMongoRepository;
using System.Collections.Generic;
using CORE.MessagesCQRS.Mediator;

namespace ProcessadorNfe.Function.Functions.Https
{
    public class Relatorios
    {
        private readonly INfeQuery _nfeQuery;
        private readonly IMediatrHandler _cqrs;
        public Relatorios(INfeQuery nfeQuery, INfeRepository nfeRepository, IMediatrHandler cqrs)
        {
            _nfeQuery = nfeQuery;
            _cqrs = cqrs;
        }

        [FunctionName("RelatorioApresentacao")]
        public async Task<ResponseModel<List<RelatorioApresentacao>>> RunRelatorioApresentacao(
           [HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = null)] HttpRequest req,
           ILogger log)
        {
            log.LogInformation($"Requisição RelatorioNotasAlteradas {DateTime.Now}");

            var relatorio = await _nfeQuery.RelatorioApresentacao();

            return relatorio;
        }

        [FunctionName("RelatorioAlteracoesNotas")]
        public async Task<ResponseModel<RelatorioComparativoAlteracoesNfe>> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation($"RelatorioAlteracoesNotas: {DateTime.Now}");

            var relatorio = await _nfeQuery.RelatorioNfesAlteradas();

            return relatorio;
        }        


    }
}
