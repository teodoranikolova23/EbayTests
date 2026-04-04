using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Selenium.Core.Interfaces;

namespace SeleniumTests.Seleinum.Core
{
    public class Browser : IBrowser
    {
        private ChromeDriver _driver;
        public Browser()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
        }

        public Browser(ChromeDriver driver)
        {
            _driver = driver;
        }

        public IElement CreateElement(By locator, string elementName)
        {
            return new SeleniumElement(_driver, locator, elementName);
        }

        public INavigation Navigate()
        {
            return _driver.Navigate();
        }

        public void GoToUrl(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        public void Maximize()
        {
            _driver.Manage().Window.Maximize();
        }

        public void Dispose()
        {
            _driver?.Quit();
            _driver?.Dispose();
        }

        public IWebDriver NativeDriver => _driver;
    }
}