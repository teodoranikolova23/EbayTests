using EbayTests.Pages.Popups;
using OpenQA.Selenium;
using SeleniumTests.Seleinum.Core;

namespace EbayTests.Pages
{
    public class AddToCartPopup : BasePopup
    {
        public AddToCartPopup(IBrowser browser) : base(browser)
        {
        }
        public IElement CartQuantity => Browser.CreateElement(By.XPath("//div[contains(@class,'title')]//span[contains(@class, 'SECONDARY')]"), "Cart Quantity");
        public IElement TotalPrice => Browser.CreateElement(By.XPath("//span[contains(text(), 'Items')]//ancestor::div[@data-testid='ux-labels-values']//div[contains(@class,'values-content')]//span[@class='ux-textspans']"), "Total Price");
    }
}
