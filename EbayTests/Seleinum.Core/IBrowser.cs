using OpenQA.Selenium;

namespace SeleniumTests.Seleinum.Core
{
    public interface IBrowser : IDisposable
    {
        IWebDriver NativeDriver { get; }
        IElement CreateElement(By locator, string elementName);
        INavigation Navigate();
        void GoToUrl(string url);
        void Maximize();
    }
}
