using SuporteSolidarioBusiness.Domain.Entities;

namespace SuporteSolidario.ViewModel;

public class CategoriaViewModel
{

    public CategoriaViewModel(CategoriaEntity item)
    {
        this.Id = item.Id;
        this.Descricao = item.Descricao;
    }

    public long Id { get; set; }

    public string Descricao { get; set; }
}