namespace SuporteSolidario.ViewModel;

public class SolicitacoesViewModel: BaseViewModel
{
    private readonly IEnumerable<SolicitacaoItemViewModel> _solicitacoes;

    public SolicitacoesViewModel(IEnumerable<SolicitacaoModel> lst)
    {
        _solicitacoes = new List<SolicitacaoItemViewModel>();

        foreach(SolicitacaoModel item in lst)
        {
            SolicitacaoItemViewModel model = new SolicitacaoItemViewModel(item);
            _solicitacoes.Append(model);
        }
    }

    public IEnumerable<SolicitacaoItemViewModel> Solicitacoes { get => _solicitacoes; }
}