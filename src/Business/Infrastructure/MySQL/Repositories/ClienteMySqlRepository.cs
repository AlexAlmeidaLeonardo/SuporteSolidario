namespace SuporteSolidarioBusiness.Infrastructure.MySQL.Repositories;

using SuporteSolidarioBusiness.Domain.Entities;
using SuporteSolidarioBusiness.Application.Repositories;
using SuporteSolidarioBusiness.Infrastructure.Mappers;
using SuporteSolidarioBusiness.Application.DTOs;

public class ClienteMySqlRepository : IClienteRepository
{
    private readonly SuporteSolidarioDbContext _context;
    public ClienteMySqlRepository(SuporteSolidarioDbContext context)
    {
        _context = context;
    }


    public ClienteEntity Ler(long id)
    {
        ClienteModel? model = _context.Clientes.Where(x => x.Id == id).FirstOrDefault();

        if(model is null)
        {
            throw new NotFoundException($"Cliente com id {id} não encontrado");
        }

        ClienteEntity entity = ModelToEntity.MapCliente(model);

        return entity;
    }

    public ClienteEntity Adicionar(ClienteEntity entity)
    {
        ClienteModel model = EntityToModel.MapCliente(entity);
        model.Criacao = DateTime.Now;

        _context.Clientes.Add(model);

        _context.SaveChanges();

        ClienteEntity entityOut = ModelToEntity.MapCliente(model);

        return entityOut;
    }

    public ClienteEntity Atualizar(ClienteEntity entity)
    {
        ClienteModel? entityModel = _context.Clientes.Find(entity.Id);
        if(entityModel is null)
        {
            throw new NotFoundException($"Cliente com id {entity.Id} não encontrado");
        }

        EntityToModel.MapCliente(entity, entityModel);
        entityModel.Alteracao = DateTime.Now;

        _context.Clientes.Update(entityModel);

        _context.SaveChanges();

        ClienteEntity entityOut = ModelToEntity.MapCliente(entityModel);

        return entityOut;
    }

    public bool ExisteLogin(long id)
    {
        bool existe = _context.Clientes
                        .Where(x => x.IdUsuario == id)
                        .Any();

        return existe;
    }

    public ClienteEntity LerByUsuario(long id)
    {
        ClienteModel? model = _context.Clientes.Where(x => x.IdUsuario == id).FirstOrDefault();

        if(model is null)
        {
            return null;
            //throw new NotFoundException($"Cliente com id {id} não encontrado");
        }

        ClienteEntity entity = ModelToEntity.MapCliente(model);

        return entity;
    }

    public ClienteDTO BuscarPorAtendimento(long idAtendimento)
    {
        IQueryable<ClienteDTO> cliente = from C in _context.Clientes
                                          join S in _context.Solicitacoes on C.Id equals S.IdCliente
                                          join A in _context.Atendimentos on S.Id equals A.IdSolicitacao
                                          where A.Id == idAtendimento
                                          select new ClienteDTO
                                          {
                                              Nome = C.Nome,
                                              Sobrenome = C.Sobrenome
                                          };


        return cliente.FirstOrDefault();
    }
}