using OpenQA.Selenium;

namespace SwagLabs.Tests.Models
{
    public class Product
    {
        private readonly IWebElement _element;

        public Product(IWebElement element)
        {
            _element = element;
        }

        public string Name => _element.FindElement(By.ClassName("inventory_item_name")).Text;

        public string Price => _element.FindElement(By.ClassName("inventory_item_price")).Text;

        public string Description => _element.FindElement(By.ClassName("inventory_item_desc")).Text;
    }
}
