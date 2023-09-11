using CORE.Entidades;
using CORE.IMongoRepository;
using CORE.MessagesCQRS.Mediator;
using INFRA;
using INFRA.CloudServices;
using INFRA.CloudServices.Interface;
using INFRA.MongoRepository;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Nfe.CQRS.Command;
using Nfe.CQRS.Queries;

namespace Nfe.API.Utils
{
    public static class InjecaoDependencia
    {
        
        public static IServiceCollection InjecaodeDependencia(this IServiceCollection services, string mongoCS, string serviceBusCS, string blobStorageCS)
        {
            #region Cloud
            services.AddScoped<IHttpRequests, HttpRequests>();
            services.AddScoped<IBlobStorageService, BlobStorageService>();

            services.AddScoped<IBlobStorageService>((t) =>
            {
                return new BlobStorageService(blobStorageCS);
            });
            #endregion

            #region Repository 
            services.AddScoped<INfeRepository, NfeRepository>();
            services.AddScoped<INfeAlteracoesRepository, NfeAlteracoesRepository>();
            #endregion

            #region CQRS
            services.AddMediatR(typeof(ProcessarNfseIntegracaoCommand));

            services.AddScoped<IMediatrHandler, MediatrHandler>();

            services.AddScoped<INfeQuery, NfeQuery>();

            #endregion


           services.AddAutoMapper(typeof(AutoMapper));


            services.AddSingleton<IServiceBus>((T) =>
            {
                return new BusService(serviceBusCS);
            });



            services.Configure<NfeMongoDataBaseSettings>
            (options =>
            {
                options.ConnectionString = mongoCS;
                options.DatabaseName = "ProcessadorNfe";
                options.NfeCollectionName = "";
            });



            return services;
        }
    }
}