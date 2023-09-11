using CORE.Entidades;

namespace CORE.IMongoRepository
{
    public interface INfeAlteracoesRepository
    {
        Task AddAsync(NfeAlteracoes nfeAlteracoes);
        Task AddLoteAsync(List<NfeAlteracoes> nfesAlteracoes);
        Task<List<NfeAlteracoes>> ObterTodos();
    }
}
