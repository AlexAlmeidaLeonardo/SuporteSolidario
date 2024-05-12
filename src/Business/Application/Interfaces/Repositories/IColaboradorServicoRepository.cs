using SuporteSolidarioBusiness.Domain.Entities;

namespace SuporteSolidarioBusiness.Application.Repositories;

public interface IColaboradorServicoRepository
{
    ColaboradorServicoEntity Ler(long id);
    ColaboradorServicoEntity Adicionar(ColaboradorServicoEntity obj);
}