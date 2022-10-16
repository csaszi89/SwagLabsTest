using OpenQA.Selenium;

namespace SwagLabs.Tests.PageObjects
{
    public class ProductsPage : SwagLabsBase
    {
        public ProductsPage(IWebDriver driver) : base(driver)
        {
        }

        public override string Url => $"{BaseUrl}inventory.html";
    }
}
