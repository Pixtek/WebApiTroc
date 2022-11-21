
using Infrastructure.EF.DbEntities;

namespace Infrastructure.EF.User;

public interface IUsers
{
    IEnumerable<DbUser> GetAll();
    DbUser Create(string email, string pseudo, string localite, string mdp);
    DbUser FetchById(int id);
 
}