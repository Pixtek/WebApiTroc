using Infrastructure.EF.DbEntities;

namespace Infrastructure.EF.Category;

public interface ICategory
{
    IEnumerable<DbCategory> GetAll();
    
    DbCategory Create(string nomCategory);
    
    DbCategory FetchByName(string name);

    bool Update(DbCategory dbCategory);
    
    bool Delete(string nom); 
}