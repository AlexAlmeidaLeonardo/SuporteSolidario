using SuporteSolidarioBusiness.Domain.Entities;
using SuporteSolidarioBusiness.Infrastructure.MySQL;

namespace SuporteSolidario.ViewModel;

public class SolicitacaoServicoViewModel: BaseViewModel
{
    private readonly IEnumerable<ServicoViewModel> _servicos;

    public SolicitacaoServicoViewModel(IEnumerable<ServicoEntity> lst)
    {
        _servicos = new List<ServicoViewModel>();

        foreach(ServicoEntity item in lst)
        {
            ServicoViewModel model = new ServicoViewModel(item);
            _servicos = _servicos.Append(model);
        }
    }

    public IEnumerable<ServicoViewModel> Servicos { get => _servicos; }

    public long IdServico { get; set; }
}

