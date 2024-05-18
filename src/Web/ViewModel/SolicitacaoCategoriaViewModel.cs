using SuporteSolidarioBusiness.Domain.Entities;
using SuporteSolidarioBusiness.Infrastructure.MySQL;

namespace SuporteSolidario.ViewModel;

public class SolicitacaoCategoriaViewModel: BaseViewModel
{
    private readonly IEnumerable<CategoriaViewModel> _categorias;

    public SolicitacaoCategoriaViewModel(IEnumerable<CategoriaEntity> lst)
    {
        _categorias = new List<CategoriaViewModel>();

        foreach(CategoriaEntity item in lst)
        {
            CategoriaViewModel model = new CategoriaViewModel(item);
            _categorias = _categorias.Append(model);
        }
    }

    public IEnumerable<CategoriaViewModel> Categorias { get => _categorias; }

    public long IdCategoria { get; set; }
}

