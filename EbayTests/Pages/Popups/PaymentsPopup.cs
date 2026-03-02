using OpenQA.Selenium;
using SeleniumTests.Seleinum.Core;

namespace EbayTests.Pages.Popups
{
    public class PaymentsPopup : BasePopup
    {
        public PaymentsPopup(IBrowser browser) : base(browser)
        {
        }
        public IElement LabelStandard => Browser.CreateElement(By.XPath("//span[contains(@class,'ux-section__title')]//span[contains(text(),'Standard')]"), "Label Standard");
    }
}