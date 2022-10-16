using OpenQA.Selenium;

namespace SwagLabs.Tests.PageObjects
{
    public class ProductsPage : SwagLabsPageBase
    {
        public ProductsPage(IWebDriver driver) : base(driver)
        {
        }

        public override string Url => $"{BaseUrl}inventory.html";
    }
}
