using OpenQA.Selenium;
using Selenium.Core.Interfaces;

namespace SeleniumTests.Seleinum.Core
{
    public interface IBrowser : IDisposable
    {
        IWebDriver NativeDriver { get; }
        IElement CreateElement(By locator, string elementName);
        INavigation Navigate();
        void GoToUrl(string url);
        void Maximize();
        IElementsList CreateElements(By locator, string? name = null);
    }
}
