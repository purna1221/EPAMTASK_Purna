using AventStack.ExtentReports;
using NUnit.Framework;
using TaskEPAM.Constants;
using TaskEPAM.Pages;
using TaskEPAM.Utils;

namespace TaskEPAM.Tests
{
    public class CheckOutCartTests : TestBase
    {
        [SetUp]
        public void BeforeTest()
        {
            LoginPage loginPage = new(driver);

            var loginModel = loginPage.LoadLoginModel("login.json");

            loginPage.Login(loginModel.UserName, loginModel.Password);
            loginPage.ClickLoginBtn();
        }

        [Test]
        public void VerifyAllElementsPresentInCheckOutCartPage()
        {
            test = extent.CreateTest("VerifyAllElementsInPage").Info("Test Started");

            HomePage homePage = new HomePage(driver);
            YourCartPage yourCartPage = new YourCartPage(driver);

            homePage.ClickAddToCartButton(ProductConstants.Backpack);
            test.Log(Status.Info, "Click on Add to Cart Button for selected item");

            homePage.ClickCartButton();
            test.Log(Status.Info, "Click on Cart Button");
            
            bool isdisplayed = yourCartPage.VerifyPageElements();
            Assert.IsTrue(isdisplayed);
            test.Log(Status.Pass, "Verify All Elements In Page test passed");
        }

        [Test]
        public void VerifyResetAppState()
        {
            test = extent.CreateTest("VerifyResetAppState").Info("Test Started");

            HomePage homePage = new HomePage(driver);
            homePage.ClickAddToCartButton(ProductConstants.Backpack);
            test.Log(Status.Info, "Click on Add to Cart Button for selected item");

            // Verify items count in cart
            Assert.That(homePage.GetItemsCountInCart(), Is.EqualTo("1"));
            test.Log(Status.Pass, "Cart Items Count has 1");

            // Click on App Reset
            homePage.ClickBurgerButton();            
            homePage.ClickMenuItem(GeneralConstants.MenuItemResetApp);
            test.Log(Status.Info, "Click on App Reset Menu item");

            // Verify Cart is Empty
            Assert.IsTrue(homePage.CartShouldBeEmpty());
            test.Log(Status.Pass, " Cart should be Empty");

            test.Log(Status.Pass, "App Reset Testcase is Passed");

        }

        [Test]
        public void VerifyCheckOut()
        {
            test = extent.CreateTest("VerifyCheckOut").Info("Test Started");

            HomePage homePage = new HomePage(driver);
            YourCartPage yourCartPage = new YourCartPage(driver);

            // Add To Cart
            homePage.ClickAddToCartButton(ProductConstants.Backpack);
            test.Log(Status.Info, "Click on Add to Cart Button for selected item");

            // Verify items count in cart
            Assert.That(homePage.GetItemsCountInCart(), Is.EqualTo("1"));
            test.Log(Status.Pass, "Cart Items Count has 1");

            // Click on Cart Button
            homePage.ClickCartButton();
            test.Log(Status.Info, "Click on Cart Button");

            // Verify item in the cart
            Assert.IsTrue(homePage.IsItemAvailableInShoppingCart(ProductConstants.Backpack));
            test.Log(Status.Pass, "Item that added to cart is available");

            // Verify Quantity
            Assert.That(yourCartPage.CheckItemQuantity(), Is.EqualTo("1"));
            test.Log(Status.Pass, "Quantity Count has 1");

            // Click on Checkout Button
            yourCartPage.ClickCheckoutBtn();
            test.Log(Status.Info, "Click on Checkout Button");

            // Provide CheckOut Details
            yourCartPage.CheckOutDetails(GeneralConstants.FirtName, GeneralConstants.LastName, GeneralConstants.PostalCode);
            test.Log(Status.Info, "Fill the Checkout details like first name, last Name, Postal Code");

            // Verify item name and quantity
            Assert.IsTrue(homePage.IsItemAvailableInShoppingCart(ProductConstants.Backpack));
            test.Log(Status.Pass, "Item that added to cart is available");

            Assert.That(yourCartPage.CheckItemQuantity(), Is.EqualTo("1"));
            test.Log(Status.Pass, "Quantity Count has 1");

            // Click on Finish Button
            yourCartPage.ClickFinishBtn();
            test.Log(Status.Info, "Click on Finish Button");

            // Verify Thank You Message
            Assert.That(yourCartPage.OrderCompletedMsg(), Is.EqualTo("Thank you for your order!"));
            test.Log(Status.Pass, "Thank You Message Verified");

            test.Log(Status.Pass, "Check Out Testcase is Passed");
        }

        [TearDown]
        public void Aftermethod()
        {
            var testStatus = TestContext.CurrentContext.Result.Outcome.Status;
            var name = TestContext.CurrentContext.Test.Name;
            var stackTrace = TestContext.CurrentContext.Result.StackTrace;
            var errorMessage = TestContext.CurrentContext.Result.Message;
            if (testStatus == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                test.Fail("Test Failed", MediaEntityBuilder.CreateScreenCaptureFromPath(TakeScreenshot(name)).Build());
                test.Fail("Stacktrace: " + stackTrace);
                test.Fail("Error Message: " + errorMessage);
            }
        }
    }
}
