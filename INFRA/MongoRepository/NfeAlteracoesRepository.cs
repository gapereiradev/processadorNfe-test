using CORE.Entidades;
using CORE.IMongoRepository;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace INFRA.MongoRepository
{
    public class NfeAlteracoesRepository : INfeAlteracoesRepository
    {
        private readonly IMongoCollection<NfeAlteracoes> _collection;

        public NfeAlteracoesRepository(IOptions<NfeMongoDataBaseSettings> nfeMongoServices)
        {
            var mongoClient = new MongoClient(nfeMongoServices.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(nfeMongoServices.Value.DatabaseName);

            _collection = mongoDatabase.GetCollection<NfeAlteracoes>
                ("NfeAlteracoes");

        }

        public async Task AddAsync(NfeAlteracoes nfe)
        {
            await _collection.InsertOneAsync(nfe);
        }

        public async Task AddLoteAsync(List<NfeAlteracoes> nfes)
        {
            await _collection.InsertManyAsync(nfes);
        }

        public async Task<List<NfeAlteracoes>> ObterTodos()
        {
            var nfes = await _collection.Find(_ => true).ToListAsync();
            return nfes;
        }
    }
}
