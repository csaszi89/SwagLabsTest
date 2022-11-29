using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SwagLabs.Tests.Models;

namespace SwagLabs.Tests.PageObjects
{
    public class ProductsPage : SwagLabsPageBase
    {
        public ProductsPage(IWebDriver driver) : base(driver)
        {
        }

        public override string Url => $"{BaseUrl}inventory.html";

        private IWebElement ProductsList => _driver.FindElement(By.ClassName("inventory_list"));

        private IWebElement OrderSelect => _driver.FindElement(By.ClassName("product_sort_container"));

        public IEnumerable<Product> GetProducts()
        {
            var elements = ProductsList.FindElements(By.ClassName("inventory_item"));

            foreach (var element in elements)
            {
                yield return new Product(element);
            }
        }

        public void SelectOrder(string value)
        {
            var element = new SelectElement(OrderSelect);
            element.SelectByText(value);
        }
    }
}
