using Infrastructure.EF.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF.Transaction;

public class TransactionRepository:ITransaction
{
    private readonly TrocContextProvider _trocContextProvider;
    
    public TransactionRepository(TrocContextProvider trocContextProvider)
    {
        _trocContextProvider = trocContextProvider;
    }
    
    public IEnumerable<DbTransaction> GetAll()
    {
        using var context = _trocContextProvider.NewContext();
        return context.Transactions.ToList();
    }

    public DbTransaction Create(DateTime Datest, int Id_user1, int Id_user2, string Article1, string Article2)
    {
        using var context = _trocContextProvider.NewContext();
        var transaction = new DbTransaction()
        {
            Dates = Datest,
            Id_User1 = Id_user1,
            Id_User2 = Id_user2,
            Article1 = Article1,
            Article2 = Article2
        };
        context.Transactions.Add(transaction);
        context.SaveChanges();
        return transaction;
    }

    public bool Update(DbTransaction dbTransaction)
    {
        using var context = _trocContextProvider.NewContext();

        try
        {
            context.Update(dbTransaction);
            return context.SaveChanges() == 1;

        }
        catch (DbUpdateConcurrencyException e)
        {
            return false;
        }
    }

    public bool Delete(int id)
    {
        using var context = _trocContextProvider.NewContext();
        try
        {
            context.Remove(new DbTransaction() { Id = id });
            return context.SaveChanges() == 1;
        }
        catch (DbUpdateConcurrencyException e)
        {
            return false;
        }
    }

    //j'utilise le idUser2 car il s'agit de celui qui recoit la demande d'echange
    //cette methode permet d'afficher les demandes de requetes sur mon profil
    public IEnumerable<DbTransaction> fetchByIdUser(int id)
    {
        using var context = _trocContextProvider.NewContext();
        var article = context.Transactions.FirstOrDefault(a => a.Id_User2 == id);
        var activeCustomers = context.Transactions.Where(a => a.Id_User2 == id).ToList();

        if (article == null) throw new KeyNotFoundException($"Transaction with idUser2 {id} has not been found");
        return activeCustomers;
    }

    public IEnumerable<DbTransaction> fetchByIdUserOffer(int id)
    {
        using var context = _trocContextProvider.NewContext();
        var article = context.Transactions.FirstOrDefault(a => a.Id_User1 == id);
        var activeCustomers = context.Transactions.Where(a => a.Id_User1 == id).ToList();

        if (article == null) throw new KeyNotFoundException($"Transaction with idUser1 {id} has not been found");
        return activeCustomers;
    }

    public DbTransaction fetchById(int id)
    {
        using var context = _trocContextProvider.NewContext();
        var transaction = context.Transactions.FirstOrDefault(t => t.Id == id);

        if (transaction == null) throw new KeyNotFoundException($"transaction with id {id} has not been found");
        return transaction;
    }
}