using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumTests.Seleinum.Core;

namespace EbayTests.Pages.Popups
{
    public abstract class BasePopup
    {
        protected readonly IBrowser Browser;

        protected BasePopup(IBrowser browser)
        {
            Browser = browser;
        }

        protected virtual By PopupBy =>
            By.XPath("//div[contains(@class,'lightbox-dialog__window') and contains(@class,'keyboard-trap--active')]");

        public IElement PopupRoot => Browser.CreateElement(PopupBy, "Popup Root");

        public TPopup WaitToDisplay<TPopup>(int timeoutSeconds = 50) where TPopup : BasePopup
        {
            var wait = new WebDriverWait(Browser.NativeDriver, TimeSpan.FromSeconds(timeoutSeconds));
            wait.Until(_ => Browser.NativeDriver.FindElements(PopupBy).Count > 0);
            wait.Until(_ => PopupRoot.IsDisplayed());
            return (TPopup)this;
        }
    }
}
