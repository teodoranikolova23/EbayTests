using EbayTests.Extensions;
using EbayTests.Pages;
using EbayTests.Pages.Popups;
using SeleniumTests.Pages;
using Tests;

namespace SeleniumTests
{
    public class Tests : BaseTest
    {
        DetailedViewPage ItemDetailsPage => new(Browser);
        HomePage HomePage => new(Browser);
        AddToCartPopup AddToCartPopup => new(Browser);
        SeeDetailsPopup SeeDetailsPopup => new(Browser);
        PaymentsPopup PaymentsPopup => new(Browser);


        [SetUp]
        public void NavigateToItemsByCategoryAndSearchedName()
        {
            HomePage.NavigateToSelectedCategoryAndSearchedName("Toys & Hobbies", "Monopoly Board Game");
            Browser.WaitForHomePageLoaded("Bulgaria");
        }

        /// <summary>
        ///Verify that the page is correct and opened
        /// </summary>
        [Test]
        public void NavigateToEbay_PageIsLoaded()
        {
            StringAssert.Contains("ebay.com", driver.Url);

        }

        /// <summary>
        ///Verify that there is a shipping to : Bulgaria 
        /// </summary>
        [Test]
        public void SearchBYNameMonopoly_ShippingToBulgariaDisplays()
        {
            
            Assert.That(HomePage.ShippingZipCode("Bulgaria").Displayed, Is.True,
                "Expected shipping location label 'Bulgaria' to be displayed.");
        }

        /// <summary>
        /// 1.Verify that the first items has the title: Monopoly Board Game and 2.that there is a price of the item 
        /// </summary>
        [Test]
        public void SearchBYNameMonopolyBoardGame_MonopolyBoardGameWithPriceDisplays()
        {

            StringAssert.Contains("Monopoly Board Game", HomePage.MonopolyBoardGameItem.Text,
                "Expected the first Monopoly item title to contain 'Monopoly Board Game'.");

            Assert.That(HomePage.GamePrice.Displayed, Is.True,
                "Expected the price for the first 'Monopoly Board Game' item to display.");
        }

        /// <summary>
        ///Verify that the title of the item contains “Monopoly” 
        /// </summary>
        [Test]
        public void NavigateToDetailedView_TitleDisplaysMonopoly()
        {

            OpenFirstItemInNewTab(HomePage.MonopolyBoardGameItem);

            StringAssert.Contains("Monopoly", ItemDetailsPage.ItemDetailsName.Text,
                "Expected the item details name to contain 'Monopoly'.");

        }

        /// <summary>
        ///Verify that the price is the same as on the first page
        /// </summary>
        [Test]
        public void NavigateToDetailedView_PriceMatchesValueInHomePage()
        {
            var gamePriceText = HomePage.GamePrice.Text?.Trim();
            Assert.That(gamePriceText, Is.Not.Empty, "GamePrice text was empty on HomePage.");

            var gamePrice = StringExtensions.ParsePrice(gamePriceText);

            OpenFirstItemInNewTab(HomePage.MonopolyBoardGameItem);


            var detailsPriceText = ItemDetailsPage.ItemDetailsPrice.Text?.Trim();
            Assert.That(detailsPriceText, Is.Not.Empty, "ItemDetailsPrice text was empty on DetailedViewPage.");

            var detailsPrice = StringExtensions.ParsePrice(detailsPriceText);

            Assert.That(detailsPrice, Is.EqualTo(gamePrice),
                $"Expected ItemDetailsPrice ({detailsPriceText}) to equal GamePrice ({gamePriceText}).");
        }

        /// <summary>
        ///Verify 1. Returns and 2.that the item can be shipped to Bulgaria on the Shipping
        ///NOTE: In the task Returns >> See details must be clicked on, but currently there is no such a button; so Returns >> Text is verified instead.
        /// </summary>
        [Test]
        public void ClickOnSeeDetails_ShipsToBulgariaDisplays()
        {
            OpenFirstItemInNewTab(HomePage.MonopolyBoardGameItem);

            ItemDetailsPage.SeeDetails.Click();

            SeeDetailsPopup.WaitToDisplay<SeeDetailsPopup>();

            var expectedReturnsText = "With eBay International Shipping, returns accepted within 30 days";
            
            StringAssert.Contains(expectedReturnsText, SeeDetailsPopup.ReturnsValue.Text,
                $"Expected Returns text to contain '{expectedReturnsText}'.");

            SeeDetailsPopup.ShowMore.Click();
            
            StringAssert.Contains("Bulgaria", SeeDetailsPopup.ShipsTo.Text,
                "Expected the item details name to contain 'Bulgaria'.");
        }

        /// <summary>
        ///Verify Payments display
        ///NOTE: In the task Payments is in the popup, but currently a button must be clicked on
        /// </summary>
        [Test]
        public void ClickOnPayments_PaymentsDisplay()
        {
            OpenFirstItemInNewTab(HomePage.MonopolyBoardGameItem);

            ItemDetailsPage.SeeDetails.Click();

            SeeDetailsPopup.WaitToDisplay<SeeDetailsPopup>();

            SeeDetailsPopup.PaymentMethods.Click();
            
            PaymentsPopup.WaitToDisplay<PaymentsPopup>();

            Assert.That(PaymentsPopup.LabelStandard.IsDisplayed(), Is.True,
                "Expected Label 'Standard' to be displayed.");
        }

        /// <summary>
        ///Verify that you are on “https://cart.payments.ebay.com/”
        /// </summary>
        [Test]
        [Ignore("Currently the user is redirected to ebay.com/itm")]
        public void TwoItemsAddedToChart_CartPaymentsPageIsLoaded()
        {
            OpenFirstItemInNewTab(HomePage.MonopolyBoardGameItem);

            ItemDetailsPage.QuantityField.Clear();
            ItemDetailsPage.QuantityField.TypeText("2");

            ItemDetailsPage.AddToCart.Click();

            StringAssert.Contains("cart.payments.ebay.com", driver.Url);
        }

        /// <summary>
        ///1. Verify that in the Qty Drop Down List the quantity is “2” and 2.that the price is displayed for 2 items
        /// </summary>
        [Test]
        public void TwoItemsAddedToChart_QuantityAndPriceAffected()
        {
            var priceString = HomePage.GamePrice.Text?.Trim();
            Assert.That(priceString, Is.Not.Empty, "GamePrice text was empty on HomePage.");

            var gamePrice = StringExtensions.ParsePrice(priceString);

            OpenFirstItemInNewTab(HomePage.MonopolyBoardGameItem);

            var quantity = 2;
            ItemDetailsPage.QuantityField.Clear();
            ItemDetailsPage.QuantityField.TypeText(quantity.ToString());

            ItemDetailsPage.AddToCart.Click();

            AddToCartPopup.WaitToDisplay<AddToCartPopup>();
            Thread.Sleep(1000); // robust wait must be added 
            
            StringAssert.Contains(quantity.ToString(), AddToCartPopup.CartQuantity.Text,
                $"Expected the Quantity to contain '{quantity}'.");

            var itemsTotalString = AddToCartPopup.TotalPrice.Text?.Trim();
            Assert.That(itemsTotalString, Is.Not.Empty, "Items total text was empty in AddToCart popup.");

            var itemsTotal = StringExtensions.ParsePrice(itemsTotalString);

            var expectedTotal = gamePrice * quantity;

            Assert.That(itemsTotal, Is.EqualTo(expectedTotal),
                $"Expected items total ({itemsTotalString}) to equal {quantity} x game price ({priceString}).");
        }

    }
    
}
