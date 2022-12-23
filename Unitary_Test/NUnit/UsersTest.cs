using Domain;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class UsersTests
    {
        [Test]
        public void IsEmailValid_ValidEmail_ReturnsTrue()
        {
            // Arrange
            var user = new Users();
            user.Email = "test@example.com";

            // Act
            bool result = user.IsEmailValid();

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsEmailValid_InvalidEmail_ReturnsFalse()
        {
            // Arrange
            var user = new Users();
            user.Email = "invalidemail";

            // Act
            bool result = user.IsEmailValid();

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsPseudoValid_ValidPseudo_ReturnsTrue()
        {
            // Arrange
            var user = new Users();
            user.Pseudo = "validpseudo";

            // Act
            bool result = user.IsPseudoValid();

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsPseudoValid_InvalidPseudoTooShort_ReturnsFalse()
        {
            // Arrange
            var user = new Users();
            user.Pseudo = "a";

            // Act
            bool result = user.IsPseudoValid();

            // Assert
            Assert.IsFalse(result);
        }



        [Test]
        public void IsPasswordStrong_InvalidPasswordTooShort_ReturnsFalse()
        {
            // Arrange
            var user = new Users();
            user.Mdp = "short";

            // Act
            bool result = user.IsPasswordStrong();

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsPasswordStrong_InvalidPasswordTooLong_ReturnsFalse()
        {
            // Arrange
            var user = new Users();
            user.Mdp = "thispasswordiswaytoolongandinvalid";

            // Act
            bool result = user.IsPasswordStrong();

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsPasswordStrong_InvalidPasswordNoUpperCase_ReturnsFalse()
        {
            // Arrange
            var user = new Users();
            user.Mdp = "nouppercase1";

            // Act
            bool result = user.IsPasswordStrong();

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsPasswordStrong_InvalidPasswordNoLowerCase_ReturnsFalse()
        {
            // Arrange
            var user = new Users();
            user.Mdp = "NOLOWERCASE1";

            // Act
            bool result = user.IsPasswordStrong();

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsPasswordStrong_InvalidPasswordNoDigits_ReturnsFalse()
        {
            // Arrange
            var user = new Users();
            user.Mdp = "NoDigits";

            // Act
            bool result = user.IsPasswordStrong();

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsPasswordStrong_InvalidPasswordNoSymbols_ReturnsFalse()
        {
            // Arrange
            var user = new Users();
            user.Mdp = "NoSymbols1";

            // Act
            bool result = user.IsPasswordStrong();

            // Assert
            Assert.IsFalse(result);
        }
    }
}
