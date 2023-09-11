using AutoMapper;
using CORE.DTOS;
using CORE.Entidades;
using CORE.IMongoRepository;
using CORE.Integracao;
using INFRA.CloudServices.Interface;
using MediatR;
using Newtonsoft.Json;
using Nfe.CQRS.Command;


namespace Nfe.CQRS.Event
{
    public class ProcessarNfseIntegracaoCommandHandle : IRequestHandler<ProcessarNfseIntegracaoCommand, ResponseModel<string>>
    {
        private readonly IHttpRequests _httpRequests;
        private readonly INfeRepository _nfeRepository;
        private readonly IMapper _mapper;
        private readonly IServiceBus _serviceBus;

        public ProcessarNfseIntegracaoCommandHandle(IHttpRequests httpRequests, INfeRepository nfeRepository, IMapper mapper, IServiceBus serviceBus)
        {
            _httpRequests = httpRequests;
            _nfeRepository = nfeRepository;
            _mapper = mapper;
            _serviceBus = serviceBus;
        }
        public async Task<ResponseModel<string>> Handle(ProcessarNfseIntegracaoCommand request, CancellationToken cancellationToken)
        {
            ResponseModel<string> ret = new ResponseModel<string>();

            try
            {
                var retornoNf = await _httpRequests.GetRestRequestCompletaAsync("https://61a170e06c3b400017e69d00.mockapi.io", "DevTest/invoice");

                var nfes = JsonConvert.DeserializeObject<List<NfeRequestResponse>>(retornoNf);

                var listaNfes = new List<NfeMongo>();
                var idsAddServiceBus = new List<string>();
                foreach (var nfe in nfes)
                {
                    var nfMongo = await _nfeRepository.GetByIdAsync(nfe.id);
                    if (nfMongo == null)
                    {
                        var nf = _mapper.Map<NfeMongo>(nfe);
                        listaNfes.Add(nf);
                        idsAddServiceBus.Add(nfe.id);
                    }
                }
                
                if(listaNfes.Count() > 0)
                    await _nfeRepository.AddLote(listaNfes);

                foreach (var id in idsAddServiceBus)
                {
                    await _serviceBus.SendAsync(id, "notas-para-processar");
                }
            }
            catch (Exception ex)
            {
                ret.AddError($"Ocorreu um erro: {ex.Message}");
            }

            return ret;
        }
    }
}
