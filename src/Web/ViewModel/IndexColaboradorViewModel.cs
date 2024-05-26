using SuporteSolidarioBusiness.Application.DTOs;

namespace SuporteSolidario.ViewModel;

public class IndexColaboradorViewModel: BaseViewModel
{
    public IndexColaboradorViewModel()
    {
        listaServicosEmAberto = new List<SolicitacaoDistanciaDTO>();
        listaServicosEmAndamento = new List<SolicitacaoEmAndamentoDTO>();
    }

    public IEnumerable<SolicitacaoDistanciaDTO> listaServicosEmAberto;

    public IEnumerable<SolicitacaoEmAndamentoDTO> listaServicosEmAndamento;
}