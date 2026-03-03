using OpenQA.Selenium;
using SeleniumTests.Pages;
using SeleniumTests.Seleinum.Core;

namespace EbayTests.Pages
{
    public class DetailedViewPage : BasePage
    {
        public DetailedViewPage(IBrowser browser) : base(browser) { }

        public IElement ItemDetailsName => Browser.CreateElement(By.XPath("//div[contains(@data-testid,'title')]//span"), "Item Details Name");
        public IElement ItemDetailsPrice => Browser.CreateElement(By.XPath("//div[contains(@data-testid,'price')]//span"), "Item Details Price");
        public IElement SeeDetails => Browser.CreateElement(By.XPath("//div[contains(@class,'shipping')]//button//span[contains(text(), \"details\")]"), "See Details");
        public IElement QuantityField => Browser.CreateElement(By.XPath("//input[@name='quantity']"), "Quantity Field");
        public IElement AddToCart => Browser.CreateElement(By.XPath("//a[contains(@id, 'atcBtn_btn')]"), "Add To Cart");

        public void FillQuantity(int quantity)
        {
            QuantityField.Clear();
            QuantityField.TypeText(quantity.ToString());
            AddToCart.Click();
        }
    }
}
