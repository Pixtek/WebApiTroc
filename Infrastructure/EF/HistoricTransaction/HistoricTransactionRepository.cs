using Infrastructure.EF.DbEntities;
using Infrastructure.EF.Transaction;

namespace Infrastructure.EF.HistoricTransaction;

public class HistoricTransaction:IHistoricTransaction
{
    private readonly TrocContextProvider _trocContextProvider;
    private readonly ITransaction _ITransaction;

    public HistoricTransaction(TrocContextProvider trocContextProvider, ITransaction iTransaction)
    {
        _trocContextProvider = trocContextProvider;
        _ITransaction = iTransaction;
    }

    public DbHisotricTransaction ArchiveTransaction(bool echange, int idTransaction)
    {
        using var context = _trocContextProvider.NewContext();
        DbTransaction transaction = _ITransaction.fetchById(idTransaction);
        var historicTransaction = new DbHisotricTransaction()
        {
            Dates = transaction.Dates,
            Id_User1 = transaction.Id_User1,
            Id_User2 = transaction.Id_User2,
            Article1 = transaction.Article1,
            Article2 = transaction.Article2,
            echange = echange
        };
        context.HisotricTransactions.Add(historicTransaction);
        context.SaveChanges();

        _ITransaction.Delete(transaction.Id);
        return historicTransaction;
    }

    public IEnumerable<DbHisotricTransaction> fetchByIdUser(int id)
    {
        using var context = _trocContextProvider.NewContext();
        var article = context.HisotricTransactions.FirstOrDefault(a => a.Id_User2 == id);
        var activeCustomers = context.HisotricTransactions.Where(a => a.Id_User2 == id).ToList();

        if (article == null) throw new KeyNotFoundException($"Transaction with idUser2 {id} has not been found");
        return activeCustomers;
    }

    public IEnumerable<DbHisotricTransaction> fetchByIdUserOffer(int id)
    {
        using var context = _trocContextProvider.NewContext();
        var article = context.HisotricTransactions.FirstOrDefault(a => a.Id_User1 == id);
        var activeCustomers = context.HisotricTransactions.Where(a => a.Id_User1 == id).ToList();

        if (article == null) throw new KeyNotFoundException($"Transaction with idUser1 {id} has not been found");
        return activeCustomers;
    }

    public DbHisotricTransaction fetchById(int id)
    {
        using var context = _trocContextProvider.NewContext();
        var transaction = context.HisotricTransactions.FirstOrDefault(t => t.Id == id);

        if (transaction == null) throw new KeyNotFoundException($"transaction with id {id} has not been found");
        return transaction;
    }
    
    public IEnumerable<DbHisotricTransaction> GetAll()
    {
        using var context = _trocContextProvider.NewContext();
        return context.HisotricTransactions.ToList();
    }
}