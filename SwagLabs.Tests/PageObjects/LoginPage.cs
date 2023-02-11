using OpenQA.Selenium;
using SwagLabs.Tests.AUT;

namespace SwagLabs.Tests.PageObjects
{
    public class LoginPage : PageBase
    {
        public const string InvalidLoginDataErrorMessage = "Epic sadface: Username and password do not match any user in this service";
        public const string MissingUsernameErrorMessage = "Epic sadface: Username is required";
        public const string MissingPasswordErrorMessage = "Epic sadface: Password is required";
        public const string UserLockedErrorMessage = "Epic sadface: Sorry, this user has been locked out.";
        public const string UnauthorizedErrorMessage = "Epic sadface: You can only access '/inventory.html' when you are logged in.";

        public LoginPage(IWebDriver driver) : base(driver)
        {
        }

        private IWebElement UsernameInput => _driver.FindElement(By.Id("user-name"));

        private IWebElement PasswordInput => _driver.FindElement(By.Id("password"));

        private IWebElement LoginButton => _driver.FindElement(By.Id("login-button"));

        private IWebElement ErrorContainer => _driver.FindElement(By.ClassName("error-message-container"));

        private IWebElement ErrorMessage => ErrorContainer.FindElement(By.TagName("h3"));

        public override string Url => SwagLabsWebSite.BaseUrl;

        public void EnterLoginData(string username, string password)
        {
            UsernameInput.SendKeys(username);
            PasswordInput.SendKeys(password);
        }

        public void ClickLoginButton() => LoginButton.Click();

        public void Login(string username, string password)
        {
            EnterLoginData(username, password);
            ClickLoginButton();
        }

        public string GetErrorMessage() => ErrorMessage.Text;
    }
}
