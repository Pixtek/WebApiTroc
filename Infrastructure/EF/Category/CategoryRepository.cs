using Infrastructure.EF.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF.Category;

public class CategoryRepository : ICategory
{
    private readonly TrocContextProvider _trocContextProvider;

    public CategoryRepository(TrocContextProvider trocContextProvider)
    {
        _trocContextProvider = trocContextProvider;
    }

    public IEnumerable<DbCategory> GetAll()
    {
        using var context = _trocContextProvider.NewContext();
        return context.categories.ToList();
    }

    public DbCategory Create(string nomCategory)
    {
        using var context = _trocContextProvider.NewContext();
        var category = new DbCategory()
        {
            NomCategory = nomCategory
        };
        context.categories.Add(category);
        context.SaveChanges();
        return category;
    }

    public DbCategory FetchByName(string name)
    {
        using var context = _trocContextProvider.NewContext();
        var category = context.categories.FirstOrDefault(a => a.NomCategory == name);

        if (category == null) throw new KeyNotFoundException($"category with name {name} has not been found");
        return category;
    }

    public bool Update(DbCategory dbCategory)
    {
        using var context = _trocContextProvider.NewContext();

        try
        {
            context.Update(dbCategory);
            return context.SaveChanges() == 1;

        }
        catch (DbUpdateConcurrencyException e)
        {
            return false;
        }
    }

    public bool Delete(string nom)
    {
        using var context = _trocContextProvider.NewContext();
        try
        {
            context.Remove(new DbCategory() { NomCategory = nom });
            return context.SaveChanges() == 1;
        }
        catch (DbUpdateConcurrencyException e)
        {
            return false;
        }
    }
}