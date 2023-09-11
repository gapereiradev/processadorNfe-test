using CORE.DTOS;
using CORE.MessagesCQRS.Mediator;
using CORE.Relatorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Nfe.CQRS.Command;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProcessadorNfe.Function.Functions.Https
{
    public class Start
    {
        private readonly IMediatrHandler _cqrs;

        public Start(IMediatrHandler cqrs)
        {
            _cqrs = cqrs;
        }



        [FunctionName("Start")]
        public async Task<IActionResult> RunStart(
           [HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = null)] HttpRequest req,
           ILogger log)

        {
            log.LogInformation($"C# Iniciando Function: {DateTime.Now}");


            var processamento = new ProcessarNfseIntegracaoCommand();
            await _cqrs.EnviarComandoGenerico(processamento);
            var retorno = new ResponseModel<string>();
            retorno.AddData("Solicitação de Processamento efetuado com Sucesso");
            return new OkObjectResult(retorno);

        }
    }
}