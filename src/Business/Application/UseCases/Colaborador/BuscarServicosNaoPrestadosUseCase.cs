using SuporteSolidarioBusiness.Application.DTOs;
using SuporteSolidarioBusiness.Application.Repositories;

namespace SuporteSolidarioBusiness.Application.UseCases;

public class BuscarServicosNaoPrestadosUseCase
{
    private readonly IColaboradorServicoRepository _colaboradorServicoRepository;
    private readonly long _idColaborador;
    
    public BuscarServicosNaoPrestadosUseCase(IColaboradorServicoRepository colaboradorServicoRepository, long idColaborador)
    {
        _idColaborador = idColaborador;
        _colaboradorServicoRepository = colaboradorServicoRepository;
    }

    public IEnumerable<ServicoDTO> Execute()
    {
        IEnumerable<ServicoDTO> lst = _colaboradorServicoRepository.GetServicosNaoPrestadosPorColaborador(_idColaborador);
        return lst;
    }
}