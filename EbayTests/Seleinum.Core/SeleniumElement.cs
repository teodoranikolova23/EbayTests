using OpenQA.Selenium;

namespace SeleniumTests.Seleinum.Core
{
    public class SeleniumElement : IElement
    {
        private readonly IWebDriver _driver;
        private readonly By _locator;
        private readonly string _elementName;

        public SeleniumElement(IWebDriver driver, By locator, string elementName)
        {
            _driver = driver;
            _locator = locator;
            _elementName = elementName;
        }

        private IWebElement WebElement => _driver.FindElement(_locator);

        public void Click()
        {
            WebElement.Click();
        }

        public void TypeText(string text)
        {
            WebElement.Clear();
            WebElement.SendKeys(text);
        }

        public void Clear()
        {
            WebElement.Clear();
        }

        public string Text => WebElement.Text;

        public bool Displayed => WebElement.Displayed;

        public bool Enabled => WebElement.Enabled;

        public string ElementName => _elementName;

        public bool IsDisplayed(int? timeOut = null)
        {
            if (timeOut.HasValue)
            {
                try
                {
                    var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(_driver, TimeSpan.FromSeconds(timeOut.Value));
                    return wait.Until(driver => driver.FindElement(_locator).Displayed);
                }
                catch (OpenQA.Selenium.WebDriverTimeoutException)
                {
                    return false;
                }
            }
            else
            {
                try
                {
                    return WebElement.Displayed;
                }
                catch (OpenQA.Selenium.NoSuchElementException)
                {
                    return false;
                }
            }
        }
    }
}