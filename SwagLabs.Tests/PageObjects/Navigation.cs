using OpenQA.Selenium;
using SwagLabs.Tests.AUT;

namespace SwagLabs.Tests.PageObjects
{
    public class Navigation : PageBase
    {
        public Navigation(IWebDriver driver) : base(driver)
        {
        }

        public override string Url => SwagLabsWebSite.BaseUrl;

        private IWebElement CartLink => _driver.FindElement(By.ClassName("shopping_cart_link"));

        public int GetCartProductsCount()
        {
            try
            {
                return int.Parse(CartLink.FindElement(By.TagName("span")).Text);
            }
            catch (NoSuchElementException)
            {
                return 0;
            }
        }

        public void OpenCart()
        {
            CartLink.Click();
        }
    }
}
