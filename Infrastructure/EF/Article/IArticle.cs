using Domain;

namespace Infrastructure.EF;

public interface IArticle
{
    IEnumerable<Article> GetAll();

}