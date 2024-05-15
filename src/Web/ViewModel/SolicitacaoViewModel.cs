public class SolicitacaoViewModel
{

    public SolicitacaoViewModel(SolicitacaoModel item)
    {
        this.Data = item.Data;
        this.DataServico = item.DataServico;
        this.Detalhes = item.Detalhes;
    }

    public DateTime Data { get; set; }

    public DateTime DataServico { get; set; }

    public string Detalhes { get; set; }
}