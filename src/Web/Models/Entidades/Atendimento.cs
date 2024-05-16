public class Atendimento: BaseModel
{
    public long id_solicitacao { get; set; }
    public long id_colaborador { get; set; }
    public string atendido_em { get; set; }
    public string confirmado_em { get; set; }
    public string finalizado_em { get; set; }
    public double avaliacao { get; set; }
}