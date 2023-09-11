using CORE.DTOS;
using CORE.Entidades;
using CORE.Relatorios;

namespace CORE.IMongoRepository
{
    public interface INfeRepository
    {
        Task<NfeMongo> GetByIdAsync(string id);
        Task AddLote(List<NfeMongo> nfes);

        Task UpdateAsync(NfeMongo nfe);

        Task<List<RelatorioApresentacao>> RelatorioApresentacao();
        Task<List<NfeMongo>> ObterTodos();
        Task<List<NfeParaDownload>> ObterTodosQueNaoForamBaixados();
    }
}
