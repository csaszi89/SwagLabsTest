using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Remote;
using SwagLabs.Tests.Definitions;

namespace SwagLabs.Tests.Tests
{
    public class SwagLabsTestBase
    {
        protected IWebDriver StartBrowser(BrowserType browserType)
        {
            DriverOptions options;
            switch (browserType)
            {
                case BrowserType.Chrome:
                    options = new ChromeOptions();
                    break;
                case BrowserType.Edge:
                    options = new EdgeOptions();
                    break;
                default:
                    throw new NotSupportedException($"Browser {browserType} not supported");
            }
            return new RemoteWebDriver(options);
        }
    }
}
