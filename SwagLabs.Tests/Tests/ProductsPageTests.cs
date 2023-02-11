using SwagLabs.Tests.AUT;
using SwagLabs.Tests.Definitions;
using SwagLabs.Tests.PageObjects;

namespace SwagLabs.Tests.Tests
{
    [TestFixture(BrowserType.Chrome)]
    [TestFixture(BrowserType.Edge)]
    public class ProductsPageTests : TestBase
    {
        public ProductsPageTests(BrowserType browserType) : base(browserType)
        {
        }

        [Test]
        public void Test_ProductsPageCannotOpenWithoutLogin()
        {
            SwagLabsWebSite.ProductsPage.Navigate();
            Assert.IsFalse(SwagLabsWebSite.ProductsPage.IsPresent());
            Assert.IsTrue(SwagLabsWebSite.LoginPage.IsPresent());
            Assert.That(SwagLabsWebSite.LoginPage.GetErrorMessage(), Is.EqualTo(LoginPage.UnauthorizedErrorMessage));
        }

        [Test]
        public void Test_ProductsArePresent()
        {
            SwagLabsWebSite.LoginPage.Navigate();
            SwagLabsWebSite.LoginPage.Login("standard_user", "secret_sauce");
            var products = SwagLabsWebSite.ProductsPage.GetProducts().ToList();
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

            SwagLabsWebSite.LoginPage.Navigate();
            SwagLabsWebSite.LoginPage.Login("standard_user", "secret_sauce");

            // Default
            var products = SwagLabsWebSite.ProductsPage.GetProducts().ToList();
            Assert.That(products.First().Name, Is.EqualTo(firstProduct));
            Assert.That(products.Last().Name, Is.EqualTo(lastProduct));

            // Name (Z to A)
            SwagLabsWebSite.ProductsPage.SelectOrder("Name (Z to A)");
            products = SwagLabsWebSite.ProductsPage.GetProducts().ToList();
            Assert.That(products.First().Name, Is.EqualTo(lastProduct));
            Assert.That(products.Last().Name, Is.EqualTo(firstProduct));

            // Price (low to high)
            SwagLabsWebSite.ProductsPage.SelectOrder("Price (low to high)");
            products = SwagLabsWebSite.ProductsPage.GetProducts().ToList();
            Assert.That(products.First().Name, Is.EqualTo(lowestPriceProduct));
            Assert.That(products.Last().Name, Is.EqualTo(highestPriceProduct));

            // Price (low to high)
            SwagLabsWebSite.ProductsPage.SelectOrder("Price (high to low)");
            products = SwagLabsWebSite.ProductsPage.GetProducts().ToList();
            Assert.That(products.First().Name, Is.EqualTo(highestPriceProduct));
            Assert.That(products.Last().Name, Is.EqualTo(lowestPriceProduct));
        }

        [Test]
        public void Test_CartOperations()
        {
            SwagLabsWebSite.LoginPage.Navigate();
            SwagLabsWebSite.LoginPage.Login("standard_user", "secret_sauce");

            var product1 = SwagLabsWebSite.ProductsPage.GetProducts().First(p => p.Name == "Sauce Labs Backpack");
            var product2 = SwagLabsWebSite.ProductsPage.GetProducts().First(p => p.Name == "Sauce Labs Bike Light");

            SwagLabsWebSite.ProductsPage.AddProductToCart(product1);
            Assert.That(SwagLabsWebSite.Navigation.GetCartProductsCount(), Is.EqualTo(1));

            SwagLabsWebSite.ProductsPage.AddProductToCart(product2);
            Assert.That(SwagLabsWebSite.Navigation.GetCartProductsCount(), Is.EqualTo(2));

            SwagLabsWebSite.ProductsPage.RemoveProductFromCart(product2);
            Assert.That(SwagLabsWebSite.Navigation.GetCartProductsCount(), Is.EqualTo(1));

            SwagLabsWebSite.ProductsPage.RemoveProductFromCart(product1);
            Assert.That(SwagLabsWebSite.Navigation.GetCartProductsCount(), Is.EqualTo(0));
        }
    }
}
