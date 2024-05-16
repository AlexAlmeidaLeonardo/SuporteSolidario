public class AtendimentoMensagem: BaseModel
{
    public long id_atendimento { get; set; }
    public string criacao { get; set; }
    public string alteracao { get; set; }
    public bool is_cliente { get; set; }
    public string mensagem { get; set; }
    public long id_em_resposta { get; set; }
}