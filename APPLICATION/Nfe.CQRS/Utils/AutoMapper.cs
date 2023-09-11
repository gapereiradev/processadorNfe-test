using AutoMapper;
using CORE.Entidades;
using CORE.Integracao;
using CORE.Relatorios;

namespace Nfe.API.Utils
{
    public class AutoMapper : Profile
    {
        public AutoMapper() : this("MyProfile")
        {

        }
        public AutoMapper(string profileName) : base(profileName)
        {
            MapperNfe();
            MapperNfeAlteracoes();
        }

        public void MapperNfe()
        {
            CreateMap<NfeRequestResponse, NfeMongo>()
                .ForMember(dest => dest.nfeProc, opt => opt.MapFrom(src => src.nfeProc))
                .ForMember(dest => dest.idNfe, opt => opt.MapFrom(src => src.id));

            CreateMap<NfeprocNfeRequestResponse, Nfeproc>()
                .ForMember(dest => dest.versao, opt => opt.MapFrom(src => src.versao))
                .ForMember(dest => dest.xmlns, opt => opt.MapFrom(src => src.xmlns))
                .ForMember(dest => dest.NFe, opt => opt.MapFrom(src => src.NFe));

            CreateMap<NfeNfeRequestResponse, CORE.Entidades.Nfe>()
                .ForMember(dest => dest.infNFe, opt => opt.MapFrom(src => src.infNFe));

            CreateMap<InfnfeNfeRequestResponse, Infnfe>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.versao, opt => opt.MapFrom(src => src.versao))
                .ForMember(dest => dest.ide, opt => opt.MapFrom(src => src.ide))
                .ForMember(dest => dest.emit, opt => opt.MapFrom(src => src.emit))
                .ForMember(dest => dest.dest, opt => opt.MapFrom(src => src.dest))
                .ForMember(dest => dest.det, opt => opt.MapFrom(src => src.det));

            CreateMap<IdeNfeRequestResponse, Ide>();

            CreateMap<EmitNfeRequestResponse, Emit>()
                .ForMember(dest => dest.enderEmit, opt => opt.MapFrom(src => src.enderEmit));

            CreateMap<EnderemitNfeRequestResponse, Enderemit>();

            CreateMap<DestNfeRequestResponse, Dest>()
                .ForMember(dest => dest.enderDest, opt => opt.MapFrom(src => src.enderDest));

            CreateMap<EnderdestNfeRequestResponse, Enderdest>();

            CreateMap<DetNfeRequestResponse, Det>()
                .ForMember(dest => dest.nItem, opt => opt.MapFrom(src => src.nItem))
                .ForMember(dest => dest.prod, opt => opt.MapFrom(src => src.prod));

            CreateMap<ProdNfeRequestResponse, Prod>();
        }
        public void MapperNfeAlteracoes()
        {
            CreateMap<ValorAlteradoNfe, ValorAlteradoNfeRelatorioComparativoAlteracoesNfe>()
                .ForMember(v => v.NomeDoCampoJson, opt => opt.MapFrom(src => src.NomeDoCampoJson))
                .ForMember(v => v.ValorAntigo, opt => opt.MapFrom(src => src.ValorAntigo))
                .ForMember(v => v.ValorNovo, opt => opt.MapFrom(src => src.ValorNovo))
                .ForMember(v => v.IdentificadorCampo, opt => opt.MapFrom(src => src.IdentificadorCampo))
                .ForMember(v => v.CampoUsadoParaIdentificadorCampo, opt => opt.MapFrom(src => src.CampoUsadoParaIdentificadorCampo));

            CreateMap<ValorAlteradoNfeRelatorioComparativoAlteracoesNfe, ValorAlteradoNfe>()
                .ForMember(v => v.NomeDoCampoJson, opt => opt.MapFrom(src => src.NomeDoCampoJson))
                .ForMember(v => v.ValorAntigo, opt => opt.MapFrom(src => src.ValorAntigo))
                .ForMember(v => v.ValorNovo, opt => opt.MapFrom(src => src.ValorNovo))
                .ForMember(v => v.IdentificadorCampo, opt => opt.MapFrom(src => src.IdentificadorCampo))
                .ForMember(v => v.CampoUsadoParaIdentificadorCampo, opt => opt.MapFrom(src => src.CampoUsadoParaIdentificadorCampo));
        
        }
    }
}