using SwagLabs.Tests.AUT;
using SwagLabs.Tests.Definitions;
using SwagLabs.Tests.PageObjects;

namespace SwagLabs.Tests.Tests
{
    [TestFixture(BrowserType.Chrome)]
    [TestFixture(BrowserType.Edge)]
    public class LoginPageTests : TestBase
    {
        public LoginPageTests(BrowserType browserType) : base(browserType)
        {
        }

        [Test]
        public void Test_LoginPageCanBeOpen()
        {
            SwagLabsWebSite.LoginPage.Navigate();
            Assert.IsTrue(SwagLabsWebSite.LoginPage.IsPresent());
        }

        [Test]
        [TestCase("", "", LoginPage.MissingUsernameErrorMessage)]
        [TestCase("invalid_username", "invalid_password", LoginPage.InvalidLoginDataErrorMessage)]
        [TestCase("invalid_username", "", LoginPage.MissingPasswordErrorMessage)]
        [TestCase("", "invalid_password", LoginPage.MissingUsernameErrorMessage)]
        [TestCase("locked_out_user", "secret_sauce", LoginPage.UserLockedErrorMessage)]
        public void Test_Login_Negative(string username, string password, string error)
        {
            SwagLabsWebSite.LoginPage.Navigate();
            SwagLabsWebSite.LoginPage.Login(username, password);
            Assert.IsTrue(SwagLabsWebSite.LoginPage.IsPresent());
            Assert.IsFalse(SwagLabsWebSite.ProductsPage.IsPresent());
            Assert.That(SwagLabsWebSite.LoginPage.GetErrorMessage(), Is.EqualTo(error));
        }

        [Test]
        [TestCase("standard_user", "secret_sauce")]
        public void Test_Login_Positive(string username, string password)
        {
            SwagLabsWebSite.LoginPage.Navigate();
            SwagLabsWebSite.LoginPage.Login(username, password);
            Assert.IsTrue(SwagLabsWebSite.ProductsPage.IsPresent());
            Assert.IsFalse(SwagLabsWebSite.LoginPage.IsPresent());
        }
    }
}
