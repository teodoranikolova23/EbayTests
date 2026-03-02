using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumTests.Seleinum.Core;
using IElement = SeleniumTests.Seleinum.Core.IElement;

namespace SeleniumTests.Pages
{
    public class HomePage : BasePage
    {
        public HomePage(IBrowser browser) : base(browser) { }

        public IElement SearchField => Browser.CreateElement(By.XPath("//input[@title='Search']"), "Search Field");
        public IElement SearchButton => Browser.CreateElement(By.XPath("//button[contains(@value,'Search')]"), "Search Button");
        public IElement CategoryDropdown => Browser.CreateElement(By.XPath("//select[contains(@id, 'gh-cat')]"), "Category Dropdown");
        public IElement MonopolyBoardGameItem => Browser.CreateElement(By.XPath("(//div[contains(@class,'s-card__title') and contains(normalize-space(.),'Monopoly Board Game')])[1]//span[contains(@class,'su-styled-text')]"), "Monopoly Board Game Item");
        public IElement GamePrice => Browser.CreateElement(By.XPath("((//div[contains(@class,'s-card__title') and contains(normalize-space(.),'Monopoly Board Game')])[1]/ancestor::li[1]//span[contains(@class,'s-card__price')])[1]"), "Game Price");
        public IElement ShippingZipCode(string name) => Browser.CreateElement(By.XPath($"//div[contains(@class,'shipping')]//span[normalize-space(.)= '{name}']"), "Shipping Zip Code");

        public void SelectCategory(string name)
        {
            var select = new SelectElement(Browser.NativeDriver.FindElement(By.Id("gh-cat")));
            select.SelectByText(name);
        }

        public void NavigateToSelectedCategoryAndSearchedName(string categoryName, string searchedName)
        {
            CategoryDropdown.Click();
            SelectCategory(categoryName);
            SearchField.TypeText(searchedName);
            SearchButton.Click();
        }
    }
}