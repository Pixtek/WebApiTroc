using Infrastructure.EF.DbEntities;

namespace Infrastructure.EF.HistoricArticle;

public interface IHistoricArticle
{
    IEnumerable<DbHistoricArticle> GetAll();

    DbHistoricArticle archiveArticle(int idArticle);
    DbHistoricArticle FetchById(int id);
    

}