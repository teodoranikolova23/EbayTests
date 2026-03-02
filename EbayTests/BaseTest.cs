using EbayTests.Pages;
using EbayTests.Pages.Popups;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumTests.Pages;
using SeleniumTests.Seleinum.Core;


namespace Tests
{
    public class BaseTest
    {
        public ChromeDriver driver;
        protected IBrowser Browser { get; private set; }

        protected HomePage HomePage => new HomePage(Browser);
        protected DetailedViewPage DetailedViewPage => new DetailedViewPage(Browser);

        protected AddToCartPopup AddToCartPopup => new AddToCartPopup(Browser);
        protected SeeDetailsPopup SeeDetailsPopup => new SeeDetailsPopup(Browser);

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://ebay.com/");

            Browser = new Browser(driver);
        }

        [TearDown]
        public void TearDown()
        {
            driver?.Dispose();
            Browser?.Dispose();
        }

        protected void OpenFirstItemInNewTab(IElement itemToClick)
        {
            var originalHandle = Browser.NativeDriver.CurrentWindowHandle;

            itemToClick.Click();

            SwitchToNewTab(originalHandle);
        }


        private void SwitchToNewTab(string originalHandle, int timeoutSeconds = 10)
        {
            var driver = HomePage.Browser.NativeDriver;

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
            wait.Until(d => d.WindowHandles.Count > 1);

            var newHandle = driver.WindowHandles.First(h => h != originalHandle);
            driver.SwitchTo().Window(newHandle);
        }
    }
}