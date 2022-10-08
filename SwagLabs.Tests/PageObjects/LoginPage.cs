using OpenQA.Selenium;

namespace SwagLabs.Tests.PageObjects
{
    public class LoginPage : SwagLabsBase
    {
        public LoginPage(IWebDriver driver) : base(driver)
        {
        }

        public override string Url => BaseUrl;
    }
}
