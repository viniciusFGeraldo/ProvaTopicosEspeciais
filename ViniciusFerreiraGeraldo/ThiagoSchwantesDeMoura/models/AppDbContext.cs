using Microsoft.EntityFrameworkCore;

namespace ThiagoSchwantesDeMoura.models;

public class AppDbContext : DbContext
{
    public DbSet<Folha> Folhas { get; set; }
    public DbSet<Funcionario> Funcionarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Vinicius_ThiagoSchwantes.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Funcionario>()
            .HasMany(func => func.Folhas)
            .WithOne(fol => fol.Funcionario)
            .HasForeignKey(fol => fol.FuncionarioId)
            .HasPrincipalKey(func => func.FuncionarioId)
            .IsRequired();
    }
}