using Infrastructure.EF;
using Infrastructure.EF.Article;
using Infrastructure.EF.DbEntities;
using Infrastructure.EF.User;
using NUnit.Framework;


namespace Nunit.NUnit
{

    [TestFixture]
    public class NUnit
    {
        

        private DbArticle _article;
        private ArticleRepository _articleRepository;
        
        [SetUp]
        public void SetUp()
        {
            _article = new DbArticle();
            
        }

        [Test]
        public void GetArticle_ReturnsArticleList()
        {
            int test = 3;
            
            

        }
        



    }
}
