using CORE;
using CORE.DTOS;
using CORE.Entidades;
using CORE.Integracao;
using CORE.Relatorios;
using Nfe.CQRS.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nfe.CQRS.Queries
{
    public interface INfeQuery
    {
        Task<ResponseModel<NfeMongo>> ObterPelaNfId(string id); 
        Task<ResponseModel<List<string>>> ObterTodos(); 
        Task<ResponseModel<List<RelatorioApresentacao>>> RelatorioApresentacao(); 
        Task<ResponseModel<RelatorioComparativoAlteracoesNfe>> RelatorioNfesAlteradas();

        Task<ResponseModel<List<NfeParaDownload>>> ObterNfesNaoBaixadas();
        Task<ResponseModel<NfeParaDownload>> ObterNfeParaBaixarPeloId(string id);

        Task<List<NfeRequestResponse>> Teste();


    }
}
