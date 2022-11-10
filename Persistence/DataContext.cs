using Doiman;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

// classe que serve para interacao com a base de dados.
namespace Persistence
{
    public class DataContext : IdentityDbContext //PARA TER O ESQUEMA DA BASE DE DADOS VINDO DO IDENTITY
    {
        // a propriedade base refecencia o construtor da classe Dbcontex
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Post { get; set; } //essa variavel vai ser usada para fazer todas operacos da base de dados
    }
}