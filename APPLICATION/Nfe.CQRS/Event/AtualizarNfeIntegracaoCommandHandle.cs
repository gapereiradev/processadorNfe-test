using CORE.DTOS;
using CORE.Entidades;
using CORE.IMongoRepository;
using CORE.Utils;
using INFRA.CloudServices.Interface;
using MediatR;
using Nfe.CQRS.Command;
using System.Text.Json;

namespace Nfe.CQRS.Event
{
    public class AtualizarNfeIntegracaoCommandHandle : IRequestHandler<AtualizarNfeIntegracaoCommand, ResponseModel<string>>
    {
        private readonly INfeRepository _nfeRepository;
        private readonly INfeAlteracoesRepository _nfeAlteracoesRepository;
        private readonly IBlobStorageService _blobStorageService;
        public AtualizarNfeIntegracaoCommandHandle(INfeRepository nfeRepository, INfeAlteracoesRepository nfeAlteracoesRepository, IBlobStorageService blobStorageService)
        {
            _nfeRepository = nfeRepository;
            _nfeAlteracoesRepository = nfeAlteracoesRepository;
            _blobStorageService = blobStorageService;
        }

        public async Task<ResponseModel<string>> Handle(AtualizarNfeIntegracaoCommand request, CancellationToken cancellationToken)
        {
            ResponseModel<string> ret = new ResponseModel<string>();
            try
            {
                var nfe = await _nfeRepository.GetByIdAsync(request.NfId);

                var alteracoesNotas = new List<NfeAlteracoes>();

                if (nfe != null)
                {
                    var alteracoesNfe = new NfeAlteracoes(nfe.idNfe);
                    foreach (var det in nfe.nfeProc.NFe.infNFe.det)
                    {
                        //REGRA IMPOSTO
                        det.prod.imposto = Helper.CalcularImposto(det.prod.preco);

                        //REGRA ATUALIZAR MARCA
                        var antigaMarca = det.prod.marca;
                        var novaMarca = det.prod.xProd;
                        alteracoesNfe.ValoresAlterados.Add(new ValorAlteradoNfe("nfeProc.NFe.infNFe.det.prod.marca", antigaMarca, novaMarca, det.nItem.ToString(), "@nItem"));
                        det.prod.marca = novaMarca;
                    }

                    #region BLOB
                    string jsonNF = JsonSerializer.Serialize(nfe);

                    string containerName = "nfes-para-download";
                    string blobName = $"{nfe.idNfe}-nfe.json";

                    await _blobStorageService.UploadBlobAsync(containerName, blobName, jsonNF);

                    var linkNfe = _blobStorageService.GenerateBlobDownloadLink(containerName, blobName);
                    #endregion

                    nfe.SetLinkNfeBlob(linkNfe);

                    await _nfeRepository.UpdateAsync(nfe);
                    await _nfeAlteracoesRepository.AddAsync(alteracoesNfe);
                }

            }
            catch (Exception ex)
            {
                
            }
            return ret;

        }
    }
}
