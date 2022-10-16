using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using SwagLabs.Tests.PageObjects;

namespace SwagLabs.Tests.Tests
{
    [TestFixture]
    public class LoginPageTests
    {
        [Test]
        public void Test_LoginPageCanBeOpen()
        {
            var chromeOptions = new ChromeOptions();
            using var driver = new RemoteWebDriver(chromeOptions);
            var loginPage = SwagLabsBase.NavigateTo<LoginPage>(driver);
            Assert.IsTrue(loginPage.IsPresent());
        }

        [Test]
        [TestCase("", "", LoginPage.MissingUsernameErrorMessage)]
        [TestCase("invalid_username", "invalid_password", LoginPage.InvalidLoginDataErrorMessage)]
        [TestCase("invalid_username", "", LoginPage.MissingPasswordErrorMessage)]
        [TestCase("", "invalid_password", LoginPage.MissingUsernameErrorMessage)]
        public void Test_Login_Negative(string username, string password, string error)
        {
            var chromeOptions = new ChromeOptions();
            using var driver = new RemoteWebDriver(chromeOptions);
            var loginPage = SwagLabsBase.NavigateTo<LoginPage>(driver);
            var productPage = loginPage.Login(username, password);
            Assert.IsTrue(loginPage.IsPresent());
            Assert.IsFalse(productPage.IsPresent());
            Assert.That(loginPage.GetErrorMessage(), Is.EqualTo(error));
        }
    }
}
