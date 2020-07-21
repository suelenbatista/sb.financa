using System;
using Microsoft.EntityFrameworkCore;
using SB.Financa.DAL.Configuration;
using SB.Financa.Model;
using SB.Financa.Generics.Helper;

namespace SB.Financa.DAL
{
    public class FinancaContext : DbContext
    {
        public FinancaContext(DbContextOptions<FinancaContext> options)
            : base(options)
        {
            //irá criar o banco e a estrutura de tabelas necessárias
            this.Database.EnsureCreated();
        }

        public FinancaContext() {}

        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<ContaCartao> ContaCartao { get; set; }
        public DbSet<ContaBancaria> ContaBancaria { get; set; }
        public DbSet<Etiqueta> Etiqueta { get; set; }
        public DbSet<Planejamento> Planejamento { get; set; }
        public DbSet<Movimento> Movimento { get; set; }
        public DbSet<MovimentoBaixa> MovimentoBaixa { get; set; }

        public DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /* Define a configuracao das tabelas  */
            modelBuilder.ApplyConfiguration<Pessoa>(new PessoaConfiguration());
            modelBuilder.ApplyConfiguration<ContaCartao>(new ContaCartaoConfiguration());
            modelBuilder.ApplyConfiguration<ContaBancaria>(new ContaBancariaConfiguration());
            modelBuilder.ApplyConfiguration<Etiqueta>(new EtiquetaConfiguration());
            modelBuilder.ApplyConfiguration<Planejamento>(new PlanejamentoConfiguration());
            modelBuilder.ApplyConfiguration<Movimento>(new MovimentoConfiguration());
            modelBuilder.ApplyConfiguration<MovimentoBaixa>(new MovimentoBaixaConfiguration());
            modelBuilder.ApplyConfiguration<Usuario>(new UsuarioConfiguration());
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FinancaDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            optionsBuilder.EnableSensitiveDataLogging();

            optionsBuilder.UseSqlServer(SqlHelper.ObterConnectionString("FinancaDB"));
            base.OnConfiguring(optionsBuilder);
        }
    }
}
