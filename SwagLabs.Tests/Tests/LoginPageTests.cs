using SwagLabs.Tests.Definitions;
using SwagLabs.Tests.Extensions;
using SwagLabs.Tests.PageObjects;

namespace SwagLabs.Tests.Tests
{
    [TestFixture(BrowserType.Chrome)]
    [TestFixture(BrowserType.Edge)]
    [Parallelizable(ParallelScope.All)]
    public class LoginPageTests : SwagLabsTestBase
    {
        private readonly BrowserType _browserType;

        public LoginPageTests(BrowserType browserType)
        {
            _browserType = browserType;
        }

        [Test]
        public void Test_LoginPageCanBeOpen()
        {
            using var browser = StartBrowser(_browserType);
            var loginPage = browser.GoToPage<LoginPage>();
            Assert.IsTrue(loginPage.IsPresent());
        }

        [Test]
        [TestCase("", "", LoginPage.MissingUsernameErrorMessage)]
        [TestCase("invalid_username", "invalid_password", LoginPage.InvalidLoginDataErrorMessage)]
        [TestCase("invalid_username", "", LoginPage.MissingPasswordErrorMessage)]
        [TestCase("", "invalid_password", LoginPage.MissingUsernameErrorMessage)]
        [TestCase("locked_out_user", "secret_sauce", LoginPage.UserLockedErrorMessage)]
        public void Test_Login_Negative(string username, string password, string error)
        {
            using var browser = StartBrowser(_browserType);
            var loginPage = browser.GoToPage<LoginPage>();
            var productPage = loginPage.Login(username, password);
            Assert.IsTrue(loginPage.IsPresent());
            Assert.IsFalse(productPage.IsPresent());
            Assert.That(loginPage.GetErrorMessage(), Is.EqualTo(error));
        }

        [Test]
        [TestCase("standard_user", "secret_sauce")]
        public void Test_Login_Positive(string username, string password)
        {
            using var browser = StartBrowser(_browserType);
            var loginPage = browser.GoToPage<LoginPage>();
            var productPage = loginPage.Login(username, password);
            Assert.IsTrue(productPage.IsPresent());
            Assert.IsFalse(loginPage.IsPresent());
        }
    }
}
