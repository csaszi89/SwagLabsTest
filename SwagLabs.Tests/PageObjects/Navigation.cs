using OpenQA.Selenium;

namespace SwagLabs.Tests.PageObjects
{
    public class Navigation : SwagLabsPageBase
    {
        public Navigation(IWebDriver driver) : base(driver)
        {
        }

        public override string Url => BaseUrl;

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
