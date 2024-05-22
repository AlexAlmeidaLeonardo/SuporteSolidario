using System.ComponentModel.DataAnnotations;

namespace SuporteSolidario.ViewModel;

public class SolicitacaoItemViewModel
{
    public SolicitacaoItemViewModel(SolicitacaoModel model)
    {
        this.Id = model.Id;
        this.IdCliente = model.IdCliente;
        this.IdServico = model.IdServico;
        this.Data = model.Data;
        this.DataServico = model.DataServico;
        this.Detalhes = model.Detalhes;
    }

    public long Id { get; set; }

    public long IdCliente { get; set; }

    public long IdServico { get; set; }

    public string DescricaoCategoria { get; set; }

    public string DescricaoServico { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime Data { get; set; }

    public DateTime DataServico { get; set; }

    public string Detalhes { get; set; }
}