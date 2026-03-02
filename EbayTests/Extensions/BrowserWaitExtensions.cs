using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumTests.Seleinum.Core;

namespace EbayTests.Extensions
{
    public static class BrowserWaitExtensions
    {
        public static void WaitForHomePageLoaded(this IBrowser browser, string zipCodeText, int timeoutSeconds = 20)
        {
            var wait = new WebDriverWait(browser.NativeDriver, TimeSpan.FromSeconds(timeoutSeconds));

            var by = By.XPath($"//div[contains(@class,'shipping')]//span[normalize-space(.)= '{zipCodeText}']");

            wait.Until(_ =>
            {
                try
                {
                    var el = browser.NativeDriver.FindElement(by);
                    return el.Displayed;
                }
                catch (NoSuchElementException) { return false; }
                catch (StaleElementReferenceException) { return false; }
            });
        }
    }
}
