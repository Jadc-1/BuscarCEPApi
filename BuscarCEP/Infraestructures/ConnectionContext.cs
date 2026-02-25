using BuscarCEP.Models;
using Microsoft.EntityFrameworkCore;

namespace BuscarCEP.Infraestructures
{
    public class ConnectionContext : DbContext
    {
        public DbSet<Endereco> Enderecos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=Endereco;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }
    }
}
