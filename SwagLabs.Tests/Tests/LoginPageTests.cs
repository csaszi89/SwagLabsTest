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
    }
}
