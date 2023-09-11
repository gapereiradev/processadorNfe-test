using AutoMapper;
using Azure.Core;
using CORE.DTOS;
using CORE.Entidades;
using CORE.IMongoRepository;
using CORE.Integracao;
using CORE.Relatorios;
using CORE.Utils;
using INFRA.CloudServices;
using INFRA.CloudServices.Interface;
using INFRA.MongoRepository;
using Newtonsoft.Json;

namespace Nfe.CQRS.Queries
{
    public class NfeQuery : INfeQuery
    {
        private readonly INfeRepository _nfeRepository;
        private readonly INfeAlteracoesRepository _nfeAlteracoesRepository;
        private readonly IMapper _mapper;
        private readonly IHttpRequests _httpRequests;

        public NfeQuery(INfeAlteracoesRepository nfeAlteracoesRepository, INfeRepository nfeRepository, IMapper mapper, IHttpRequests httpRequests)
        {
            _nfeAlteracoesRepository = nfeAlteracoesRepository;
            _nfeRepository = nfeRepository;
            _mapper = mapper;
            _httpRequests = httpRequests;
        }

        public async Task<ResponseModel<NfeMongo>> ObterPelaNfId(string id)
        {
            var nf = await _nfeRepository.GetByIdAsync(id);
            var retorno = new ResponseModel<NfeMongo>();
            retorno.Data = nf;
            return retorno;
        }

        public Task<ResponseModel<List<string>>> ObterTodos()
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<List<RelatorioApresentacao>>> RelatorioApresentacao()
        {
            var retorno = new ResponseModel<List<RelatorioApresentacao>>();
            var listaApresentacao = new List<RelatorioApresentacao>();

            var relatorio = await _nfeRepository.RelatorioApresentacao();

            retorno.AddData(relatorio);

            return retorno;

        }

        public async Task<ResponseModel<RelatorioComparativoAlteracoesNfe>> RelatorioNfesAlteradas()
        {
            var retorno = new ResponseModel<RelatorioComparativoAlteracoesNfe>();

            try
            {
                var listaAlteracoes = await _nfeAlteracoesRepository.ObterTodos();

                var relatorio = new RelatorioComparativoAlteracoesNfe();
                relatorio.QuantidadeDocumentosAlterados = listaAlteracoes.Count();

                var alteracoes = new List<ValorAlteradoNfeRelatorioComparativoAlteracoesNfe>();
                foreach (var alt in listaAlteracoes)
                {
                    var alteracoesMap = _mapper.Map<List<ValorAlteradoNfeRelatorioComparativoAlteracoesNfe>>(alt.ValoresAlterados);
                    alteracoesMap.ForEach(t => t.NfId = alt.NfeId);
                    alteracoes.AddRange(alteracoesMap);
                }

                relatorio.Alteracoes= alteracoes;
                retorno.AddData(relatorio);
            }
            catch (Exception ex)
            {
                retorno.AddError($"Ocorreu um erro {ex.Message}");
            }


            return retorno;
        }

        public async Task<ResponseModel<NfeParaDownload>> ObterNfeParaBaixarPeloId(string id)
        {
            var retorno = new ResponseModel<NfeParaDownload>();
            var nf = await _nfeRepository.GetByIdAsync(id);

            if(nf != null)
            {
                var nfe = new NfeParaDownload() { IdNfe = nf.idNfe, LinkBlobDownload = nf.LinkNfeBlob, NfeJaBaixada = nf.NfeJaBaixada };
                retorno.AddData(nfe);
            }
            return retorno;

        }
        public async Task<ResponseModel<List<NfeParaDownload>>> ObterNfesNaoBaixadas()
        {
            var retorno = new ResponseModel<List<NfeParaDownload>>();
            var nf = await _nfeRepository.ObterTodosQueNaoForamBaixados();

            retorno.AddData(nf);

            return retorno;

        }

        public async Task<List<NfeRequestResponse>> Teste()
        {
            try
            {
                var retornoNf = await _httpRequests.GetRestRequestCompletaAsync("https://61a170e06c3b400017e69d00.mockapi.io", "DevTest/invoice");

                var nfes = JsonConvert.DeserializeObject<List<NfeRequestResponse>>(retornoNf);

                return nfes;
            }
            catch (Exception ex)
            {
                throw new Exception("error");
            }
        }
    }
}
