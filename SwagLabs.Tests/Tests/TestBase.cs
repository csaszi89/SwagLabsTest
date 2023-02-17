using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Remote;
using SwagLabs.Tests.AUT;
using SwagLabs.Tests.Definitions;

namespace SwagLabs.Tests.Tests
{
    [TestFixture(BrowserType.Chrome, "110")]
    [TestFixture(BrowserType.MicrosoftEdge, "110")]
    [TestFixture(BrowserType.Chrome, "109")]
    [TestFixture(BrowserType.MicrosoftEdge, "109")]
    public class TestBase
    {
        private readonly BrowserType _browserType;
        private readonly string _browserVersion;

        public TestBase(BrowserType browserType, string browserVersion)
        {
            _browserType = browserType;
            _browserVersion = browserVersion;
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
                case BrowserType.MicrosoftEdge:
                    options = new EdgeOptions();
                    break;
                default:
                    throw new NotSupportedException($"Browser {browserType} not supported");
            }
            options.BrowserVersion = _browserVersion;
            return new RemoteWebDriver(new Uri("http://localhost:4444/"), options);
        }
    }
}
