using Domain;
using NUnit.Framework;

namespace UnitTests
{
    public class UsersTests
    {

        [Test]
        public void TestConstructor()
        {
            // Arrange
            var id = 1;
            var email = "test@example.com";
            var pseudo = "testuser";
            var localite = "Paris";
            var mdp = "password";

            // Act
            var user = new Users
            {
                Id = id,
                Email = email,
                Pseudo = pseudo,
                Localite = localite,
                Mdp = mdp
            };

            // Assert
            Assert.AreEqual(id, user.Id);
            Assert.AreEqual(email, user.Email);
            Assert.AreEqual(pseudo, user.Pseudo);
            Assert.AreEqual(localite, user.Localite);
            Assert.AreEqual(mdp, user.Mdp);
        }

        [Test]
        public void TestEmailValidity()
        {
            // Arrange
            var invalidEmails = new[] { "invalid", "invalid@", "@invalid.com" };
            var validEmails = new[] { "valid@example.com", "valid@sub.example.com" };

            // Act and Assert
            foreach (var email in invalidEmails)
            {
                var user = new Users { Email = email };
                Assert.IsFalse(user.IsEmailValid(), $"{email} should be invalid");
            }

            foreach (var email in validEmails)
            {
                var user = new Users { Email = email };
                Assert.IsTrue(user.IsEmailValid(), $"{email} should be valid");
            }
        }


        [Test]
        public void TestPasswordStrength()
        {
            // Arrange
            var weakPasswords = new[] { "password", "123456", "abcdef" };
            var strongPasswords = new[] { "PaSsWoRd1!", "sTr0nGPa5s!", "p@5sW0rD" };

            // Act and Assert
            foreach (var password in weakPasswords)
            {
                var user = new Users { Mdp = password };
                Assert.IsFalse(user.IsPasswordStrong(), $"{password} should be weak");
            }

            foreach (var password in strongPasswords)
            {
                var user = new Users { Mdp = password };
                Assert.IsTrue(user.IsPasswordStrong(), $"{password} should be strong");
            }
        }
    }
}
