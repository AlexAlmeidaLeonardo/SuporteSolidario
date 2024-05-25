using SuporteSolidarioBusiness.Application.DTOs;

namespace SuporteSolidario.ViewModel;

public class IndexClienteViewModel: BaseViewModel
{
    public IEnumerable<SolicitacaoEmAbertoDTO> listaServicosEmAberto;
}