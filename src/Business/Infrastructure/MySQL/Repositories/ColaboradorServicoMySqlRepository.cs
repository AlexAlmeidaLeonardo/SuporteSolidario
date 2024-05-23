namespace SuporteSolidarioBusiness.Infrastructure.MySQL.Repositories;

using System.Collections.Generic;
using SuporteSolidarioBusiness.Application.DTOs;
using SuporteSolidarioBusiness.Application.Repositories;
using SuporteSolidarioBusiness.Domain.Entities;
using SuporteSolidarioBusiness.Infrastructure.Mappers;

public class ColaboradorServicoMySqlRepository : IColaboradorServicoRepository
{
    private readonly SuporteSolidarioDbContext _context;
    public ColaboradorServicoMySqlRepository(SuporteSolidarioDbContext context)
    {
        _context = context;
    }

    public ColaboradorServicoEntity Ler(long id)
    {
        ColaboradorServicoModel? model = _context.ColaboradorServicos.Where(x => x.Id == id).FirstOrDefault();

        if(model is null)
        {
            throw new NotFoundException($"Serviço do colaborador com id {id} não encontrado");
        }

        ColaboradorServicoEntity entity = ModelToEntity.MapColaboradorServico(model);

        return entity;
    }

    public ColaboradorServicoEntity Adicionar(ColaboradorServicoEntity obj)
    {
        ColaboradorServicoModel model = EntityToModel.MapColaboradorServico(obj);
        model.Colaborador = _context.Colaboradores.Where(x => x.Id == obj.IdColaborador).FirstOrDefault();
        model.Servico = _context.Servicos.Where(x => x.Id == obj.IdServico).FirstOrDefault();

        _context.ColaboradorServicos.Add(model);

        _context.SaveChanges();

        ColaboradorServicoEntity entity = ModelToEntity.MapColaboradorServico(model);

        return entity;
    }

    public IEnumerable<ColaboradorServicoEntity> LerTodos(long idColaborador)
    {
        IEnumerable<ColaboradorServicoModel> lst = _context.ColaboradorServicos.Where(x => x.IdColaborador == idColaborador).ToList();

        IEnumerable<ColaboradorServicoEntity> entities = ModelToEntity.MapListColaboradorServico(lst);

        return entities;
    }

    public IEnumerable<ColaboradorServicoDTO> GetServicosByColaborador(long idColaborador)
    {
        IQueryable<ColaboradorServicoDTO>
            resultado = from CS in _context.ColaboradorServicos
                        join C in _context.Colaboradores on CS.IdColaborador equals C.Id
                        join S in _context.Servicos on CS.IdServico equals S.Id
                        where CS.IdColaborador == idColaborador
                        select new ColaboradorServicoDTO()
                        {
                            Id = CS.Id,
                            IdColaborador = CS.IdColaborador,
                            IdServico = CS.IdServico,
                            NomeColaborador = C.Nome + " " + C.Sobrenome,
                            DescricaoServico = S.Descricao
                        };
        
        return resultado.ToList();
    }

    public IEnumerable<ServicoDTO> GetServicosNaoPrestadosPorColaborador(long idColaborador)
    {
        // Obtenha todos os serviços
        var todosOsServicos = _context.Servicos;

        // Obtenha os serviços prestados pelo colaborador
        var servicosPrestados = from CS in _context.ColaboradorServicos
                                where CS.IdColaborador == idColaborador
                                select CS.IdServico;

        // Obtenha os serviços que o colaborador não presta
        var servicosNaoPrestados = from s in todosOsServicos
                                where !servicosPrestados.Contains(s.Id)
                                select new ServicoDTO()
                                {
                                    Id = s.Id,
                                    Descricao = s.Descricao
                                };

        return servicosNaoPrestados.ToList();
    }

    public void Remover(long id)
    {
        ColaboradorServicoModel colaboradorServico = _context.ColaboradorServicos.Where(x => x.Id == id).FirstOrDefault();

        if(colaboradorServico == null)
            throw new NotFoundException($"Id {id} não encontrado");

        _context.ColaboradorServicos.Remove(colaboradorServico);
        _context.SaveChanges();
    }
}