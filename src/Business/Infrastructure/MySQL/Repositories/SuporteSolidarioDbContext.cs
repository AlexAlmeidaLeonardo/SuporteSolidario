namespace SuporteSolidarioBusiness.Infrastructure.MySQL.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

public class SuporteSolidarioDbContext: DbContext
{
    public DbSet<UsuarioModel> Usuarios { get; set; }
    public DbSet<ClienteModel> Clientes { get; set; }
    public DbSet<ColaboradorModel> Colaboradores { get; set; }
    public DbSet<CategoriaModel> Categorias { get; set; }
    public DbSet<ServicoModel> Servicos { get; set; }
    public DbSet<ColaboradorServicoModel> ColaboradorServicos { get; set; }
    public DbSet<SolicitacaoModel> Solicitacoes { get; set; }
    public DbSet<AtendimentoModel> Atendimentos { get; set; }
    public DbSet<AtendimentoMensagemModel> AtendimentoMensagens { get; set; }

    public bool DatabaseExists()
    {
        RelationalDatabaseCreator databaseCreator = (RelationalDatabaseCreator) Database.GetService<IDatabaseCreator>();
        return databaseCreator.Exists();
    }

    public bool CreateDatabase()
    {
        try
        {
            Database.Migrate();
            return true;
        }
        catch
        {
            return false;
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = "server=localhost;user=root;password=gambito;database=suporte_solidario";
      //string connectionString = "server=localhost;user=root;password=gambito";
        optionsBuilder.UseMySql
        (
            connectionString
            ,ServerVersion.AutoDetect(connectionString)

        ).UseSnakeCaseNamingConvention();
    }
}