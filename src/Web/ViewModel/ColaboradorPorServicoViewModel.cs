using SuporteSolidarioBusiness.Application.DTOs;

namespace SuporteSolidario.ViewModel;

public class ColaboradorPorServicoViewModel: BaseViewModel
{
    public IEnumerable<ColaboradorServicoDTO> listColaboradorServicos;

    public IEnumerable<ServicoDTO> listServicos;

    public string PesquisaDescricao { get; set; }

}