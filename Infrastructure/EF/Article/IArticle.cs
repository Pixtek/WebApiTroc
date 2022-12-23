using Infrastructure.EF.DbEntities;

namespace Infrastructure.EF.Article;

public interface IArticle
{
    IEnumerable<DbArticle> GetAll();
    DbArticle Create(int idUser,  string name, string urlImage, DateTime publicationDate, string nomCat, string description);
    IEnumerable<DbArticle> FetchByName(string name);
    IEnumerable<DbArticle> FetchByCategory(string name);
    DbArticle FetchById(int id);
    IEnumerable<DbArticle> FetchById_Users(int id_user);
    bool Update(string name, string description, string url,int id);
    bool Delete(int id);
}