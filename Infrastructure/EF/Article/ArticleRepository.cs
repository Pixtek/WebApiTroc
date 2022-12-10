using Infrastructure.EF.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF.Article;

public class ArticleRepository :IArticle
{
    private readonly TrocContextProvider _trocContextProvider;

    public ArticleRepository(TrocContextProvider trocContextProvider)
    {
        _trocContextProvider = trocContextProvider;
    }

    public IEnumerable<DbArticle> GetAll()
    {
        using var context = _trocContextProvider.NewContext();
        return context.Articles.ToList();
    }

    public DbArticle Create(int idUser,  string name, string urlImage, DateTime publicationDate,string nomCat)
    {
        using var context = _trocContextProvider.NewContext();
        var article = new DbArticle()
        {
            IdUser = idUser,
            CategoryName = nomCat,
            Name = name,
            URLImage = urlImage,
            PublicationDate = publicationDate
        };
        context.Articles.Add(article);
        context.SaveChanges();
        return article;
    }
    public DbArticle FetchByName(string name)
    {
        using var context = _trocContextProvider.NewContext();
        var article = context.Articles.FirstOrDefault(a => a.Name == name);

        if (article == null) throw new KeyNotFoundException($"Article with name {name} has not been found");
        return article;
    }

    public DbArticle FetchByCategory(string category)
    {
        using var context = _trocContextProvider.NewContext();
        var article = context.Articles.FirstOrDefault(a => a.CategoryName == category);

        if (article == null) throw new KeyNotFoundException($"Article with category name {category} has not been found");
        return article;
    }

    public DbArticle FetchById(int id)
    {
        using var context = _trocContextProvider.NewContext();
        var article = context.Articles.FirstOrDefault(a => a.Id == id);

        if (article == null) throw new KeyNotFoundException($"Article with name {id} has not been found");
        return article;
    }  
    
    public IEnumerable<DbArticle> FetchById_Users(int id_user)
    {
        using var context = _trocContextProvider.NewContext();
        var article = context.Articles.FirstOrDefault(a => a.IdUser == id_user);
        var activeCustomers = context.Articles.Where(a => a.IdUser == id_user).ToList();

        if (article == null) throw new KeyNotFoundException($"Article with name {id_user} has not been found");
        return activeCustomers;
    }

    public bool Update(DbArticle dbArticle)
    {
        using var context = _trocContextProvider.NewContext();

        try
        {
            context.Update(dbArticle);
            return context.SaveChanges() == 1;

        }
        catch (DbUpdateConcurrencyException e)
        {
            return false;
        }
    }

    public bool Delete(int id)
    {
        using var context = _trocContextProvider.NewContext();
        try
        {
            context.Remove(new DbArticle() { Id = id });
            return context.SaveChanges() == 1;
        }
        catch (DbUpdateConcurrencyException e)
        {
            return false;
        }
    }
}