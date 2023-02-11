using OpenQA.Selenium;
using SwagLabs.Tests.PageObjects;

namespace SwagLabs.Tests.Extensions
{
    public static class IWebDriverExtensions
    {
        public static T GoToPage<T>(this IWebDriver driver) where T : PageBase
        {
            T page = driver.GetPage<T>();
            driver.Navigate().GoToUrl(page.Url);
            return page;
        }

        public static T GetPage<T>(this IWebDriver driver) where T : PageBase
        {
            T page = (T)Activator.CreateInstance(typeof(T), new object[] { driver });
            return page == null ? throw new Exception("Page could not be instantiated") : page;
        }
    }
}
