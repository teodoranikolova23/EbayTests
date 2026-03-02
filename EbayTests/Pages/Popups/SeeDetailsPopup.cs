using OpenQA.Selenium;
using SeleniumTests.Seleinum.Core;

namespace EbayTests.Pages.Popups
{
    public class SeeDetailsPopup : BasePopup
    {
        public SeeDetailsPopup(IBrowser browser) : base(browser)
        {
        }
        public IElement ShipsTo => Browser.CreateElement(By.XPath("//span[contains(text(),'Ships to')]/ancestor::div[contains(@class,'ux-labels-values')]//span[@class='ux-expandable-textual-display-block-inline']//child::span[@class='ux-textspans']"), "Ships to");
        public IElement ShowMore => Browser.CreateElement(By.XPath("//div[contains(@class,'shipsto')]//span[@class='details__label' and contains(.,'Show more')]"), "Show More");
        public IElement ReturnsValue => Browser.CreateElement(By.XPath("//div[@data-testid='x-returns-maxview']//span[contains(., 'Returns') and not(contains(., 'shipping'))]//ancestor::div[@data-testid='ux-labels-values']//div[contains(@class, 'values-content')]//span"), "Returns Value");
        public IElement PaymentMethods => Browser.CreateElement(By.XPath("//div[contains(@data-testid, 'region')]//span[.='Payment methods']/ancestor::*[@role='tab']"), "Show More");
    }
}