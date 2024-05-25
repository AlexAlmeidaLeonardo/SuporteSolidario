public class SolicitacaoDistanciaDTO
{
    public long Id { get; set; }

    public DateTime DataDaSolicitacao { get; set; }

    public DateTime DataDoServico { get; set; }
    
    public string DescricaoServico { get; set; }

    public double DistanciaEmKm { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }
}