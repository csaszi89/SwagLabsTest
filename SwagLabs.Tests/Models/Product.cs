using OpenQA.Selenium;

namespace SwagLabs.Tests.Models
{
    public class Product
    {
        private readonly IWebElement _container;

        public Product(IWebElement container)
        {
            _container = container;
        }

        public string Name => _container.FindElement(By.ClassName("inventory_item_name")).Text;

        public string Price => _container.FindElement(By.ClassName("inventory_item_price")).Text;

        public string Description => _container.FindElement(By.ClassName("inventory_item_desc")).Text;

        public IWebElement AddToCartButton => _container.FindElement(By.CssSelector("button[id^='add-to-cart']"));

        public IWebElement RemoveButton => _container.FindElement(By.CssSelector("button[id^='remove']"));
    }
}
