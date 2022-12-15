﻿
using Infrastructure.EF.DbEntities;

namespace Infrastructure.EF.User;

public interface IUsers
{
    IEnumerable<DbUser> GetAll();
    DbUser Create(string email, string pseudo, string localite, string mdp);
    DbUser FetchById(int id);
    DbUser FetchByPseudo(string pseudo);
    bool Delete(int id);

    public bool Update(String email, String pseudo, String localite, int id);


}