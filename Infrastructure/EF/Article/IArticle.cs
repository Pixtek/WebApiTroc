using Domain;

namespace Infrastructure.EF;

public interface IArticle
{
    IEnumerable<Article> GetAll();

    Article Create(int idUser,  string name, string urlImage, DateTime publicationDate, string nomCat);

}