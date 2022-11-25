using Infrastructure.EF.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF;

public class TrocContext :DbContext
{
    private readonly IConnectionStringProvider _connectionStringProvider;

    public TrocContext(IConnectionStringProvider connectionStringProvider)
    {
        _connectionStringProvider = connectionStringProvider;
    }

    public DbSet<DbUser> Utilisateurs { get; set; }
    public DbSet<DbArticle> Articles { get; set; }
    
    public DbSet<DbCommentary> Commentary { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(_connectionStringProvider.getConnectionString("Key"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<DbUser>(entity =>
        {
            entity.ToTable("Users");
            entity.HasKey(utilisateur => utilisateur.Id);
            entity.Property(utilisateur => utilisateur.Id).HasColumnName("id_User");
            entity.Property(utilisateur => utilisateur.Email).HasColumnName("email");
            entity.Property(utilisateur => utilisateur.Pseudo).HasColumnName("pseudo");
            entity.Property(utilisateur => utilisateur.Localite).HasColumnName("localite");
            entity.Property(utilisateur => utilisateur.Mdp).HasColumnName("mdp");
        });
        
        modelBuilder.Entity<DbArticle>(entity =>
        {
            entity.ToTable("Articles");
            entity.HasKey(article => article.Id);
            entity.Property(article => article.Id).HasColumnName("id_Article");
            // property ou haskey peut être à modifier!!!!
            entity.Property(article => article.IdUser).HasColumnName("id_User");
            entity.Property(article => article.CategoryName).HasColumnName("nomCategorie");
            entity.Property(article => article.Name).HasColumnName("nom");
            entity.Property(article => article.URLImage).HasColumnName("urlImage");
            entity.Property(article => article.PublicationDate).HasColumnName("datePubli");

        });
        
        modelBuilder.Entity<DbCommentary>(entity =>
        {
            entity.ToTable("Commentaire");
            entity.HasKey(commentary => commentary.Id);
            entity.Property(commentary => commentary.Id).HasColumnName("id_Commentaire");
            // property ou haskey peut être à modifier!!!!
            entity.Property(commentary => commentary.Note).HasColumnName("note");
            entity.Property(commentary => commentary.Nom).HasColumnName("nom");
            entity.Property(commentary => commentary.Message).HasColumnName("message_");
            entity.Property(commentary => commentary.Id_User).HasColumnName("id_User");
            

        });



    }
}