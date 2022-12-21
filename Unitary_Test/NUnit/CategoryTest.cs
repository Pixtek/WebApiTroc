using Domain;
using NUnit.Framework;

namespace UnitTests
{
    using NUnit.Framework;

    namespace UnitTests
    {
        public class CategoryTests
        {
            [Test]
            public void TestConstructor()
            {
                // Arrange
                var nomCategory = "News";

                // Act
                var category = new Category
                {
                    NomCategory = nomCategory
                };

                // Assert
                Assert.AreEqual(nomCategory, category.NomCategory);
            }
            
        }
    }

}
