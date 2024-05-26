namespace SuporteSolidarioBusiness.Application.DTOs;

public class AtendimentoMensagemDTO
{
    public long IdAtendimento { get; set; }
    public DateTime Criacao { get; set; }
    public bool IsCliente { get; set; }
    public string Mensagem { get; set; }
}