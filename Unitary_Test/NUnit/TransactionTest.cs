using Domain;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class TransactionTests
    {
        [Test]
        public void TestTransactionProperties()
        {
            var transaction = new Transaction
            {
                Id = 1,
                Dates = new DateTime(2020, 1, 1),
                Id_User1 = 100,
                Id_User2 = 200,
                Article1 = "Article 1",
                Article2 = "Article 2"
            };

            var id = transaction.Id;
            var dates = transaction.Dates;
            var idUser1 = transaction.Id_User1;
            var idUser2 = transaction.Id_User2;
            var article1 = transaction.Article1;
            var article2 = transaction.Article2;
            
            Assert.AreEqual(1, id);
            Assert.AreEqual(new DateTime(2020, 1, 1), dates);
            Assert.AreEqual(100, idUser1);
            Assert.AreEqual(200, idUser2);
            Assert.AreEqual("Article 1", article1);
            Assert.AreEqual("Article 2", article2);
        }
    }
}
