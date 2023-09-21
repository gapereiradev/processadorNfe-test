namespace CORE.Relatorios
{
    public class RelatorioComparativoAlteracoesNfe
    {
        public int QuantidadeDocumentosAlterados { get; set; }
        public List<ValorAlteradoNfeRelatorioComparativoAlteracoesNfe> Alteracoes { get; set; }
    }

    public class ValorAlteradoNfeRelatorioComparativoAlteracoesNfe
    {
        public string NfId { get; set; }
        public string NomeDoCampoJson { get; set; }
        public string ValorAntigo { get; set; }
        public string ValorNovo { get; set; }
        public string IdentificadorCampo { get; set; }
        public string CampoUsadoParaIdentificadorCampo { get; set; }
        public DateTime DataHoraAlteracao { get; set; }

    }
}
