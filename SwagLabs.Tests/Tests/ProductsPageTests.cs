using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using SwagLabs.Tests.Definitions;
using SwagLabs.Tests.Extensions;
using SwagLabs.Tests.Models;
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

        [Test]
        public void Test_SortProducts()
        {
            var firstProduct = "Sauce Labs Backpack";
            var lastProduct = "Test.allTheThings() T-Shirt (Red)";
            var lowestPriceProduct = "Sauce Labs Onesie";
            var highestPriceProduct = "Sauce Labs Fleece Jacket";

            using var browser = StartBrowser(_browserType);
            var loginPage = browser.GoToPage<LoginPage>();
            var productsPage = loginPage.Login("standard_user", "secret_sauce");

            // Default
            var products = productsPage.GetProducts().ToList();
            Assert.That(products.First().Name, Is.EqualTo(firstProduct));
            Assert.That(products.Last().Name, Is.EqualTo(lastProduct));

            // Name (Z to A)
            productsPage.SelectOrder("Name (Z to A)");
            products = productsPage.GetProducts().ToList();
            Assert.That(products.First().Name, Is.EqualTo(lastProduct));
            Assert.That(products.Last().Name, Is.EqualTo(firstProduct));

            // Price (low to high)
            productsPage.SelectOrder("Price (low to high)");
            products = productsPage.GetProducts().ToList();
            Assert.That(products.First().Name, Is.EqualTo(lowestPriceProduct));
            Assert.That(products.Last().Name, Is.EqualTo(highestPriceProduct));

            // Price (low to high)
            productsPage.SelectOrder("Price (high to low)");
            products = productsPage.GetProducts().ToList();
            Assert.That(products.First().Name, Is.EqualTo(highestPriceProduct));
            Assert.That(products.Last().Name, Is.EqualTo(lowestPriceProduct));
        }

        [Test]
        public void Test_CartOperations()
        {
            using var browser = StartBrowser(_browserType);
            var loginPage = browser.GoToPage<LoginPage>();
            var productsPage = loginPage.Login("standard_user", "secret_sauce");
            var navigation = browser.GetPage<Navigation>();

            var product1 = productsPage.GetProducts().First(p => p.Name == "Sauce Labs Backpack");
            var product2 = productsPage.GetProducts().First(p => p.Name == "Sauce Labs Bike Light");

            productsPage.AddProductToCart(product1);
            Assert.That(navigation.GetCartProductsCount(), Is.EqualTo(1));

            productsPage.AddProductToCart(product2);
            Assert.That(navigation.GetCartProductsCount(), Is.EqualTo(2));

            productsPage.RemoveProductFromCart(product2);
            Assert.That(navigation.GetCartProductsCount(), Is.EqualTo(1));

            productsPage.RemoveProductFromCart(product1);
            Assert.That(navigation.GetCartProductsCount(), Is.EqualTo(0));
        }
    }
}
