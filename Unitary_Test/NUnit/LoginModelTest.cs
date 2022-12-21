using Domain;
using NUnit.Framework;


namespace UnitTests
{
    using NUnit.Framework;

    namespace UnitTests
    {
        public class LoginModelTests
        {
            [Test]
            public void TestConstructor()
            {
                // Arrange
                var email = "user@example.com";
                var mdp = "password";

                // Act
                var loginModel = new LoginModel
                {
                    Email = email,
                    Mdp = mdp
                };

                // Assert
                Assert.AreEqual(email, loginModel.Email);
                Assert.AreEqual(mdp, loginModel.Mdp);
            }

            [Test]
            public void TestIsValid()
            {
                // Arrange
                var validLoginModel = new LoginModel { Email = "user@example.com", Mdp = "password" };
                var invalidLoginModel1 = new LoginModel { Email = "invalid", Mdp = "password" };
                var invalidLoginModel2 = new LoginModel { Email = "user@example.com", Mdp = "" };

                // Act and Assert
                Assert.IsTrue(validLoginModel.IsValid());
                Assert.IsFalse(invalidLoginModel1.IsValid());
                Assert.IsFalse(invalidLoginModel2.IsValid());
            }
        }
    }
}

