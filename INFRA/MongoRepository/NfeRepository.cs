using CORE.DTOS;
using CORE.Entidades;
using CORE.IMongoRepository;
using CORE.Relatorios;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace INFRA.MongoRepository
{
    public class NfeRepository : INfeRepository
    {
        private readonly IMongoCollection<NfeMongo> _collection;

        public NfeRepository(IOptions<NfeMongoDataBaseSettings> nfeMongoServices)
        {
            var mongoClient = new MongoClient(nfeMongoServices.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(nfeMongoServices.Value.DatabaseName);

            _collection = mongoDatabase.GetCollection<NfeMongo>
                ("Nfe");

        }

        public async Task AddAsync(NfeMongo nfe)
        {
            await _collection.InsertOneAsync(nfe);
        }

        public async Task AddLote(List<NfeMongo> nfes)
        {
            await _collection.InsertManyAsync(nfes);
        }

        public async Task<NfeMongo> GetByIdAsync(string id)
        {
            return await _collection.Find(c => c.idNfe == id).SingleOrDefaultAsync();
        }

        public async Task UpdateAsync(NfeMongo nfe)
        {
            await _collection.ReplaceOneAsync(c => c.idNfe == nfe.idNfe, nfe);
        }

        public async Task<List<NfeMongo>> ObterTodos()
        {
            var filtro = Builders<NfeMongo>.Filter.Empty;
            var nfes = await _collection.FindAsync(filtro);

            return await nfes.ToListAsync();

        }
     
        public async Task<List<NfeParaDownload>> ObterTodosQueNaoForamBaixados()
        {
            var filtro = Builders<NfeMongo>.Filter.Eq(x => x.NfeJaBaixada, false);
            var atualizacao = Builders<NfeMongo>.Update.Set(x => x.NfeJaBaixada, true);

            var documentosAtualizados = await _collection.Find(filtro).ToListAsync();
            var nfesParaDownload = new List<NfeParaDownload>();

            foreach (var documentoAtualizado in documentosAtualizados)
            {
                var nfeParaDownload = new NfeParaDownload
                {
                    IdNfe = documentoAtualizado.idNfe,
                    NfeJaBaixada = true, // Atualize o estado aqui
                    LinkBlobDownload = documentoAtualizado.LinkNfeBlob
                };

                nfesParaDownload.Add(nfeParaDownload);

                var filtroAtualizacao = Builders<NfeMongo>.Filter.Eq(x => x.idNfe, documentoAtualizado.idNfe);
                await _collection.UpdateOneAsync(filtroAtualizacao, atualizacao);
            }

            return nfesParaDownload;

        }

        public async Task<List<RelatorioApresentacao>> RelatorioApresentacao()
        {
            var pipeline = new[]
            {
                new BsonDocument
                {
                    { "$unwind", "$nfeProc.NFe.infNFe.det" }
                },
                new BsonDocument
                {
                    { "$project", new BsonDocument
                        {
                            { "marca", "$nfeProc.NFe.infNFe.det.prod.marca" },
                            { "preco", new BsonDocument { { "$toDouble", "$nfeProc.NFe.infNFe.det.prod.preco" } } }, 
                            { "imposto", "$nfeProc.NFe.infNFe.det.prod.imposto" },
                            { "data", "$nfeProc.NFe.infNFe.ide.dhEmi" }
                        }
                    }
                },
                new BsonDocument
                {
                    {
                        "$group", new BsonDocument
                        {
                            { "_id", new BsonDocument { { "marca", "$marca" }, { "mes", new BsonDocument { { "$month", "$data" } } } } },
                            { "totalVendido", new BsonDocument { { "$sum", "$preco" } } },
                            { "totalImposto", new BsonDocument { { "$sum", "$imposto" } } }
                        }
                    }
                },
                new BsonDocument
                {
                    { "$project", new BsonDocument
                        {
                            { "_id", 0 },
                            { "Marca", "$_id.marca" },
                            { "Mes", "$_id.mes" },
                            { "ValorVendido", "$totalVendido" },
                            { "ValorImposto", "$totalImposto" }
                        }
                    }
                }
            };

            var resultado = await _collection.Aggregate<RelatorioApresentacao>(pipeline).ToListAsync();

            return resultado;
        }

    }
}
