using SuporteSolidarioBusiness.Application.DTOs;

namespace SuporteSolidario.ViewModel;

public class MensagemClienteViewModel: BaseViewModel
{
    public MensagemClienteViewModel()
    {
        ListaMensagens = new List<AtendimentoMensagemDTO>();
    }

    public long IdAtendimento {get; set;}
    
    public string NomeColaborador {get; set;}

    public string Mensagem { get; set; }

    public List<AtendimentoMensagemDTO> ListaMensagens {get; set;}
}