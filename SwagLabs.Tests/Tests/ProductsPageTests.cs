using SwagLabs.Tests.Definitions;
using SwagLabs.Tests.Extensions;
using SwagLabs.Tests.PageObjects;

namespace SwagLabs.Tests.Tests
{
    [TestFixture(BrowserType.Chrome)]
    [TestFixture(BrowserType.Edge)]
    [Parallelizable(ParallelScope.All)]
    public class ProductsPageTests : SwagLabsTestBase
    {
        private readonly BrowserType _browserType;

        public ProductsPageTests(BrowserType browserType)
        {
            _browserType = browserType;
        }

        [Test]
        public void Test_ProductsPageCannotOpenWithoutLogin()
        {
            using var browser = StartBrowser(_browserType);
            var productsPage = browser.GoToPage<ProductsPage>();
            Assert.IsFalse(productsPage.IsPresent());
            var loginPage = browser.GetPage<LoginPage>();
            Assert.IsTrue(loginPage.IsPresent());
            Assert.That(loginPage.GetErrorMessage(), Is.EqualTo(LoginPage.UnauthorizedErrorMessage));
        }

        [Test]
        public void Test_ProductsArePresent()
        {
            using var browser = StartBrowser(_browserType);
            var loginPage = browser.GoToPage<LoginPage>();
            var productsPage = loginPage.Login("standard_user", "secret_sauce");
            var products = productsPage.GetProducts().ToList();
            Assert.IsNotNull(products);
            Assert.That(products.Count, Is.EqualTo(6));
        }
    }
}
