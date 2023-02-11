using OpenQA.Selenium;

namespace SwagLabs.Tests.PageObjects
{
    public abstract class PageBase
    {
        protected readonly IWebDriver _driver;

        public PageBase(IWebDriver driver)
        {
            _driver = driver;
        }

        public abstract string Url { get; }

        public bool IsPresent()
        {
            return _driver.Url == Url;
        }

        public void Navigate()
        {
            _driver.Navigate().GoToUrl(Url);
        }
    }
}
