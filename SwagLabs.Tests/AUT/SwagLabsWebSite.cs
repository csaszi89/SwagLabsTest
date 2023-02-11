using OpenQA.Selenium;
using SwagLabs.Tests.PageObjects;

namespace SwagLabs.Tests.AUT
{
    public class SwagLabsWebSite : IDisposable
    {
        public const string BaseUrl = "https://www.saucedemo.com/";
        private readonly IWebDriver _driver;
        private Navigation? _navigation;
        private LoginPage? _loginPage;
        private ProductsPage? _productsPage;
        private bool disposedValue;

        public SwagLabsWebSite(IWebDriver driver)
        {
            _driver = driver;
        }

        public LoginPage LoginPage => _loginPage ??= new LoginPage(_driver);

        public Navigation Navigation => _navigation ??= new Navigation(_driver);

        public ProductsPage ProductsPage => _productsPage ??= new ProductsPage(_driver);

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _driver.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~SwagLabsWebSite()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
