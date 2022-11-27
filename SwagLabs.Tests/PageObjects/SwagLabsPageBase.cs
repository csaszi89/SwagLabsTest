using OpenQA.Selenium;

namespace SwagLabs.Tests.PageObjects
{
    public abstract class SwagLabsPageBase
    {
        protected const string BaseUrl = "https://www.saucedemo.com/";
        protected readonly IWebDriver _driver;

        public SwagLabsPageBase(IWebDriver driver)
        {
            _driver = driver;
        }

        public abstract string Url { get; }

        public bool IsPresent()
        {
            return _driver.Url == Url;
        }
    }
}
