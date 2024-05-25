using SuporteSolidarioBusiness.Application.DTOs;
using SuporteSolidarioBusiness.Application.Repositories;

namespace SuporteSolidarioBusiness.Application.UseCases;

public class BuscarServicosNaoPrestadosUseCase
{
    private readonly IColaboradorServicoRepository _colaboradorServicoRepository;
    private readonly long _idColaborador;
    private readonly string _descricao;
    
    public BuscarServicosNaoPrestadosUseCase(IColaboradorServicoRepository colaboradorServicoRepository, long idColaborador, string descricao)
    {
        _colaboradorServicoRepository = colaboradorServicoRepository;
        _idColaborador = idColaborador;
        _descricao = descricao;
    }

    public IEnumerable<ServicoDTO> Execute()
    {
        IEnumerable<ServicoDTO> lst;

        if(string.IsNullOrEmpty(_descricao))
            lst = _colaboradorServicoRepository.GetServicosNaoPrestadosPorColaborador(_idColaborador);
        else
            lst = _colaboradorServicoRepository.GetServicosNaoPrestadosPorColaborador(_idColaborador, _descricao);
            
        return lst;
    }
}