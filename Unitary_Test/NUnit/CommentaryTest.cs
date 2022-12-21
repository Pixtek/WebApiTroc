using Domain;
using NUnit.Framework;


namespace UnitTests
{
    using NUnit.Framework;

    namespace UnitTests
    {
        public class CommentaryTests
        {
            [Test]
            public void TestConstructor()
            {
                // Arrange
                var id = 1;
                short note = 5;
                var nom = "John Doe";
                var message = "This is a great article!";
                var idUser = 123;

                // Act
                var commentary = new Commentary
                {
                    Id = id,
                    Note = note,
                    Nom = nom,
                    Message = message,
                    Id_User = idUser
                };

                // Assert
                Assert.AreEqual(id, commentary.Id);
                Assert.AreEqual(note, commentary.Note);
                Assert.AreEqual(nom, commentary.Nom);
                Assert.AreEqual(message, commentary.Message);
                Assert.AreEqual(idUser, commentary.Id_User);
            }

            [Test]
            public void TestIsPositive()
            {
                // Arrange
                var positiveCommentary = new Commentary { Note = 5 };
                var negativeCommentary = new Commentary { Note = 3 };

                // Act and Assert
                Assert.IsTrue(positiveCommentary.IsPositive());
                Assert.IsFalse(negativeCommentary.IsPositive());
            }
        }
    }

}
