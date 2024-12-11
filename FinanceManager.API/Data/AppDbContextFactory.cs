using FinanceManager.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
namespace FinanceManager.API.Data
{

    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            // Carrega a configuração do arquivo 'appsettings.json'
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())  // Define o diretório base, normalmente a raiz do projeto
                .AddJsonFile("appsettings.json")  // Carrega as configurações do arquivo appsettings.json
                .Build();

            // Cria as opções para o DbContext
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            // Configura a string de conexão para o PostgreSQL (UseNpgsql)
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("AppDbContext"));

            // Retorna uma instância do AppDbContext com as opções configuradas
            return new AppDbContext(optionsBuilder.Options);
        }
    
    }
}
