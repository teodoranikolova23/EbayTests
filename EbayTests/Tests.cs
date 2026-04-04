using EbayTests.Extensions;
using EbayTests.Pages;
using EbayTests.Pages.Popups;
using FluentAssertions;
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
            //Browser.WaitForHomePageLoaded("Bulgaria");
        }

        /// <summary>
        ///Verify that the page is correct and opened
        /// </summary>
        [Test]
        public void NavigateToEbay_PageIsLoaded()
        {
            driver.Url.Should().Contain("ebay.com", "eBay home page should be loaded");
        }

        /// <summary>
        ///Verify that there is a shipping to : Bulgaria 
        /// </summary>
        [Test]
        public void SearchBYNameMonopoly_ShippingToBulgariaDisplays()
        {
            HomePage.ShippingZipCode("Bulgaria").Displayed.Should().BeTrue($"shipping location label '{0}' should be displayed", "Bulgaria");
        }

        /// <summary>
        /// 1.Verify that the first items has the title: Monopoly Board Game and 2.that there is a price of the item 
        /// </summary>
        [Test]
        public void SearchBYNameMonopolyBoardGame_MonopolyBoardGameWithPriceDisplays()
        {
            HomePage.MonopolyBoardGameItem.Text.Should().Contain("Monopoly Board Game",
                  "The first Monopoly item title should contain 'Monopoly Board Game'");

            HomePage.GamePrice.Displayed.Should().BeTrue(
                "The price for the first 'Monopoly Board Game' item should be displayed");
        }

        /// <summary>
        ///Verify that the title of the item contains “Monopoly” 
        /// </summary>
        [Test]
        public void NavigateToDetailedView_TitleDisplaysMonopoly()
        {
            OpenFirstItemInNewTab(HomePage.MonopolyBoardGameItem);

            ItemDetailsPage.ItemDetailsName.Text.Should().Contain("Monopoly", "The item details name should contain 'Monopoly'");
        }

        /// <summary>
        ///Verify that the price is the same as on the first page
        /// </summary>
        [Test]
        public void NavigateToDetailedView_PriceMatchesValueInHomePage()
        {
            var gamePriceText = HomePage.GamePrice.Text?.Trim();

            gamePriceText.Should().NotBeNullOrWhiteSpace("GamePrice text should not be empty on HomePage");

            var gamePrice = StringExtensions.ParsePrice(gamePriceText);

            OpenFirstItemInNewTab(HomePage.MonopolyBoardGameItem);

            var detailsPriceText = ItemDetailsPage.ItemDetailsPrice.Text?.Trim();

            detailsPriceText.Should().NotBeNullOrWhiteSpace("ItemDetailsPrice text should not be empty on DetailedViewPage");

            var detailsPrice = StringExtensions.ParsePrice(detailsPriceText);

            detailsPrice.Should().Be(gamePrice,
                $"ItemDetailsPrice ({0}) should equal GamePrice ({1})", detailsPriceText, gamePriceText);
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

            SeeDetailsPopup.ReturnsValue.Text.Should().Contain(expectedReturnsText, $"Returns text should contain '{0}'", expectedReturnsText);

            SeeDetailsPopup.ShowMore.Click();

            SeeDetailsPopup.ShipsTo.Text.Should().Contain("Bulgaria", "Ships-to text should contain 'Bulgaria'");
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

            PaymentsPopup.LabelStandard.IsDisplayed().Should().BeTrue("Label 'Standard' should be displayed.");
        }

        /// <summary>
        ///Verify that you are on “https://cart.payments.ebay.com/”
        /// </summary>
        [Test]
        [Ignore("Currently the user is redirected to ebay.com/itm")]
        public void TwoItemsAddedToChart_CartPaymentsPageIsLoaded()
        {
            OpenFirstItemInNewTab(HomePage.MonopolyBoardGameItem);

            var quantity = 2;

            ItemDetailsPage.FillQuantity(quantity);

            driver.Url.Should().Be("cart.payments.ebay.com");
        }

        /// <summary>
        ///1. Verify that in the Qty Drop Down List the quantity is “2” and 2.that the price is displayed for 2 items
        /// </summary>
        [Test]
        public void TwoItemsAddedToChart_QuantityAndPriceAffected()
        {
            var priceString = HomePage.GamePrice.Text?.Trim();

            priceString.Should().NotBeNullOrWhiteSpace("GamePrice text should not be empty on HomePage");

            var gamePrice = StringExtensions.ParsePrice(priceString);

            OpenFirstItemInNewTab(HomePage.MonopolyBoardGameItem);

            var quantity = 2;

            ItemDetailsPage.FillQuantity(quantity);

            AddToCartPopup.WaitToDisplay<AddToCartPopup>();

            AddToCartPopup.CartQuantity.Text.Should().Contain(quantity.ToString(),
                "Cart quantity should contain '{0}'", quantity);

            var itemsTotalString = AddToCartPopup.TotalPrice.Text?.Trim();
            itemsTotalString.Should().NotBeNullOrWhiteSpace("Items total text should not be empty in AddToCart popup");

            var itemsTotal = StringExtensions.ParsePrice(itemsTotalString);

            var expectedTotal = gamePrice * quantity;

            itemsTotal.Should().Be(expectedTotal,
                $"items total ({0}) should equal {1} x game price ({2})", itemsTotalString, quantity, priceString);
        }
    }
}
