using SuporteSolidarioBusiness.Application.DTOs;

namespace SuporteSolidario.ViewModel;

public class MensagemColaboradorViewModel: BaseViewModel
{
    public MensagemColaboradorViewModel()
    {
        ListaMensagens = new List<AtendimentoMensagemDTO>();
    }

    public long IdAtendimento {get; set;}
    
    public string NomeCliente {get; set;}

    public string Mensagem { get; set; }

    public List<AtendimentoMensagemDTO> ListaMensagens {get; set;}
}