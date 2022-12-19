using Infrastructure.EF.DbEntities;

namespace Infrastructure.EF.HistoricTransaction;

public interface IHistoricTransaction
{
    IEnumerable<DbHisotricTransaction> GetAll();
    //si true = echange
    //si false = pas echange
    DbHisotricTransaction ArchiveTransaction(bool echange,int idtransaction);
    IEnumerable<DbHisotricTransaction> fetchByIdUser(int id);
    IEnumerable<DbHisotricTransaction> fetchByIdUserOffer(int id);
    DbHisotricTransaction fetchById(int id);

}