using SeleniumTests.Seleinum.Core;

namespace SeleniumTests.Pages
{
    public abstract class BasePage
    {
        public IBrowser Browser { get; } 

        protected BasePage(IBrowser browser)
        {
            Browser = browser;
        }
    }
}