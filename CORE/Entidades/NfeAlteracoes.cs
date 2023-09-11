using MongoDB.Bson.Serialization.Attributes;

namespace CORE.Entidades
{
    public class NfeAlteracoes
    {
        public NfeAlteracoes(string nfeId)
        {
            NfeId = nfeId;
            DataAlteracao = DateTime.Now;
            ValoresAlterados = new List<ValorAlteradoNfe>();
        }

        public NfeAlteracoes()
        {

        }
        [BsonId]
        public string NfeId { get; set; }
        public DateTime DataAlteracao { get; set; }
        public List<ValorAlteradoNfe> ValoresAlterados { get; set; }
    }

    public class ValorAlteradoNfe
    {
        public ValorAlteradoNfe(string nomeDoCampoJson, string valorAntigo, string valorNovo, string identificadorCampo = "", string campoUsadoParaIdentificadorCampo = "")
        {
            NomeDoCampoJson = nomeDoCampoJson;
            ValorAntigo = valorAntigo;
            ValorNovo = valorNovo;
            IdentificadorCampo = identificadorCampo;
            CampoUsadoParaIdentificadorCampo = campoUsadoParaIdentificadorCampo;
        }

        public string NomeDoCampoJson { get; set; }
        public string ValorAntigo { get; set; }
        public string ValorNovo { get; set; }
        public string IdentificadorCampo { get; set; }
        public string CampoUsadoParaIdentificadorCampo { get; set; }
    }
}
