namespace SuporteSolidarioBusiness.Application.DTOs;

public class SolicitacaoEmAndamentoDTO
{
    public long Id { get; set; }

    public long IdSolicitacao { get; set; }

    public long IdAtendimento { get; set; }

    public long IdCliente { get; set; }

    public long IdServico { get; set; }

    public string DescricaoCategoria { get; set; }

    public string DescricaoServico { get; set; }

    public string Colaborador { get; set; }
    public string Cliente { get; set; }

    public DateTime Data { get; set; }

    public DateTime DataServico { get; set; }

    public string Detalhes { get; set; }
}