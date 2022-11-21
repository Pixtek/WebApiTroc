using Infrastructure.EF.DbEntities;

namespace Infrastructure.EF.Article;

public interface IArticle
{
    IEnumerable<DbArticle> GetAll();

    DbArticle Create(int idUser,  string name, string urlImage, DateTime publicationDate, string nomCat);

}