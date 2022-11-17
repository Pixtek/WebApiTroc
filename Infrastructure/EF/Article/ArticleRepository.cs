using Domain;

namespace Infrastructure.EF;

public class ArticleRepository :IArticle
{
    private readonly TrocContextProvider _trocContextProvider;

    public ArticleRepository(TrocContextProvider trocContextProvider)
    {
        _trocContextProvider = trocContextProvider;
    }

    public IEnumerable<Article> GetAll()
    {
        using var context = _trocContextProvider.NewContext();
        return context.Articles.ToList();
    }

    public Article Create(int idUser,  string name, string urlImage, DateTime publicationDate,string nomCat)
    {
        using var context = _trocContextProvider.NewContext();
        var article = new Article()
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
}