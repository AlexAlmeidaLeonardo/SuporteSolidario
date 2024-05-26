namespace SuporteSolidario.ViewModel;

public class DetalhesSolicitacaoViewModel: BaseViewModel
{
    public long IdSolicitacao { get; set; }
    public DateTime Data { get; set; }

    public string NomeCliente { get; set; }

    public string DescricaoCategoria { get; set; }
    
    public string DescricaoServico { get; set; }

    public DateTime DataServico { get; set; }

    public double DistanciaEmKm { get; set; }

    public string Detalhes { get; set; }
}