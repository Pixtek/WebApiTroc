using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF;

public class TrocContext :DbContext
{
    private readonly IConnectionStringProvider _connectionStringProvider;

    public TrocContext(IConnectionStringProvider connectionStringProvider)
    {
        _connectionStringProvider = connectionStringProvider;
    }

    public DbSet<Users> Utilisateurs { get; set; }
    public DbSet<Article> Articles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(_connectionStringProvider.getConnectionString("Key"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Users>(entity =>
        {
            entity.ToTable("Users");
            entity.HasKey(utilisateur => utilisateur.Id);
            entity.Property(utilisateur => utilisateur.Id).HasColumnName("id_User");
            entity.Property(utilisateur => utilisateur.Email).HasColumnName("email");
            entity.Property(utilisateur => utilisateur.Pseudo).HasColumnName("pseudo");
            entity.Property(utilisateur => utilisateur.Localite).HasColumnName("localite");
            entity.Property(utilisateur => utilisateur.Mdp).HasColumnName("mdp");
        });
        
        modelBuilder.Entity<Article>(entity =>
        {
            
        });



    }
}