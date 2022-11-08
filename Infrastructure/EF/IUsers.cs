using Domain;

namespace Infrastructure.EF;

public interface IUsers
{
    IEnumerable<Users> GetAll();
    Users Create(string email, string pseudo, string localite, string mdp);
}