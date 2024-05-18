using SuporteSolidarioBusiness.Domain.Entities;

namespace SuporteSolidario.ViewModel;

public class ServicoViewModel
{

    public ServicoViewModel(ServicoEntity item)
    {
        this.Id = item.Id;
        this.Descricao = item.Descricao;
    }

    public long Id { get; set; }

    public string Descricao { get; set; }
}