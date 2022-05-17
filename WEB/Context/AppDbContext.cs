using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WEB.Models;

namespace WEB.Context
{
    public class AppDbContext: IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Arquivo> Arquivos { get; set; }
        public DbSet<ArquivoItem> ArquivoItems { get; set; }
        public DbSet<ArquivoOp> ArquivoOps { get; set; }
        public DbSet<ArquivoChapaDiscartada> ArquivoChapaDiscartadas { get; set; }
        public DbSet<ArquivoHistorico> ArquivoHistoricos { get; set; }
        public DbSet<ArquivoTipo> ArquivoTipos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
    }
}
