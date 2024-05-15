public class SolicitacoesViewModel
{
    private readonly IEnumerable<SolicitacaoViewModel> _solicitacoes;

    public SolicitacoesViewModel(IEnumerable<SolicitacaoModel> lst)
    {
        _solicitacoes = new List<SolicitacaoViewModel>();

        foreach(SolicitacaoModel item in lst)
        {
            SolicitacaoViewModel model = new SolicitacaoViewModel(item);
            _solicitacoes.Append(model);
        }
    }

    public IEnumerable<SolicitacaoViewModel> Solicitacoes { get => _solicitacoes; }
}