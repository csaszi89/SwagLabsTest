using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Remote;
using SwagLabs.Tests.AUT;
using SwagLabs.Tests.Definitions;

namespace SwagLabs.Tests.Tests
{
    [TestFixture]
    public class TestBase
    {
        private readonly BrowserType _browserType;

        public TestBase(BrowserType browserType)
        {
            _browserType = browserType;
        }

        [SetUp]
        public void SetUp()
        {
            Browser = StartBrowser(_browserType);
            SwagLabsWebSite = new SwagLabsWebSite(Browser);
        }

        [TearDown]
        public void TearDown()
        {
            SwagLabsWebSite.Dispose();
        }

        protected IWebDriver Browser { get; private set; }

        protected SwagLabsWebSite SwagLabsWebSite { get; private set; }

        private IWebDriver StartBrowser(BrowserType browserType)
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
            return new RemoteWebDriver(new Uri("http://localhost:4444/"), options);
        }
    }
}
