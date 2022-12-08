using Infrastructure.EF.DbEntities;

namespace Infrastructure.EF.Article;

public interface IArticle
{
    IEnumerable<DbArticle> GetAll();
    DbArticle Create(int idUser,  string name, string urlImage, DateTime publicationDate, string nomCat);
    DbArticle FetchByName(string name);
    DbArticle FetchById(int id);
    IEnumerable<DbArticle> FetchById_Users(int id_user);
    bool Update(DbArticle dbArticle);
    bool Delete(int id);
}