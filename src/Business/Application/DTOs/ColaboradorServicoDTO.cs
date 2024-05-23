namespace SuporteSolidarioBusiness.Application.DTOs;

public class ColaboradorServicoDTO
{
    public long Id { get; set; }

    public long IdColaborador { get; set; }
    public string NomeColaborador { get; set; }
    
    public long IdServico { get; set; }
    public string DescricaoServico { get; set; }
}