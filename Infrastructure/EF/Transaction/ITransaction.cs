using Infrastructure.EF.DbEntities;

namespace Infrastructure.EF.Transaction;

public interface ITransaction
{
    IEnumerable<DbTransaction> GetAll();
    DbTransaction Create(DateTime Date, int Id_user1,int Id_user2, string Article1,string Article2);
    bool Update(DbTransaction dbTransaction);

    bool Delete(int id);
    IEnumerable<DbTransaction> fetchByIdUser(int id);
    DbTransaction fetchById(int id);
}