namespace SuporteSolidarioBusiness.Application.DTOs;

public class DetalhesSolicitacaoDTO
{
    public long Id { get; set; }
    public string Nome { get; set; }
    public string Categoria { get; set; }
    public string Servico { get; set; }
    public double Distancia { get; set; }
    public DateTime DataServico { get; set; }
    public string Detalhes { get; set; }
}


/*
  select s.id,
         CONCAT(c.nome, ' ', c.sobrenome) as Nome,
         cat.descricao as Categoria,
         servico.descricao as Servico,
         0.0 as Distancia,
         s.data_servico,
         s.detalhes         
         
    from colaborador_servicos cs join solicitacoes s
                                   on cs.id_servico = s.id_servico
                                 join clientes c 
                                   on s.id_cliente = c.id
                                 join servicos servico
                                   on cs.id_servico = servico.id
                                 join categorias cat
                                   on servico.id_categoria = cat.id
   where cs.id_colaborador = 1
*/