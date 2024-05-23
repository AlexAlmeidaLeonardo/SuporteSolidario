using SuporteSolidarioBusiness.Application.DTOs;
using SuporteSolidarioBusiness.Application.Repositories;

namespace SuporteSolidarioBusiness.Application.UseCases;

public class BuscarServicosDoColaboradorUseCase
{
    private readonly IColaboradorServicoRepository _colaboradorServicoRepository;
    private readonly long _idColaborador;

    public BuscarServicosDoColaboradorUseCase(IColaboradorServicoRepository colaboradorServicoRepository, long idColaborador)
    {
        _idColaborador = idColaborador;
        _colaboradorServicoRepository = colaboradorServicoRepository;
    }

    public IEnumerable<ColaboradorServicoDTO> Execute()
    {
        IEnumerable<ColaboradorServicoDTO> lst = _colaboradorServicoRepository.GetServicosByColaborador(_idColaborador);
        return lst;
    }
}