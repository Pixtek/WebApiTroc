using Infrastructure.EF.DbEntities;

namespace Infrastructure.EF.Commentary;

public interface ICommentary
{
    IEnumerable<DbCommentary> GetAll();
    
    DbCommentary Create(int note,  string nom, string message, int id_User);
}