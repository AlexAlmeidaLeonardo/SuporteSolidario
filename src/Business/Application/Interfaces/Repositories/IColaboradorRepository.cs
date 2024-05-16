namespace SuporteSolidarioBusiness.Application.Repositories;

using SuporteSolidarioBusiness.Domain.Entities;

public interface IColaboradorRepository
{
    ColaboradorEntity Ler(long id);
    ColaboradorEntity LerByUsuario(long id);
    ColaboradorEntity Adicionar(ColaboradorEntity obj);
    ColaboradorEntity Atualizar(ColaboradorEntity input);
    bool ExisteLogin(long id);
    IEnumerable<ColaboradorEntity> Todos();
}
