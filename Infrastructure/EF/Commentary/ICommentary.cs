using Infrastructure.EF.DbEntities;

namespace Infrastructure.EF.Commentary;

public interface ICommentary
{
    IEnumerable<DbCommentary> GetAll();
    
    DbCommentary Create(short note,  string nom, string message, int id_User);
    
    DbCommentary FetchByName(string name);
    bool Update(DbCommentary dbCommentary);
    bool Delete(int id);
}