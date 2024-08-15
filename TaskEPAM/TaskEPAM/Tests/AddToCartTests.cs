using AventStack.ExtentReports;
using NUnit.Framework;
using System.Text.Json;
using TaskEPAM.Constants;
using TaskEPAM.Pages;
using TaskEPAM.Utils;

namespace TaskEPAM.Tests
{
    [TestFixture]   
    public class AddToCartTests : TestBase
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
        public void VerifyAllElementsPresentInAddToCartPage()
        {
            test = extent.CreateTest("VerifyAllElementsInPage").Info("Test Started");

            HomePage homePage = new HomePage(driver);
            bool isdisplayed = homePage.VerifyPageElements();
            Assert.IsTrue(isdisplayed);

            test.Log(Status.Pass, "Verify All Elements In Page test passed");
        }

        [Test]
        public void VerifyAddToCart()
        {
            test = extent.CreateTest("VerifyAddToCart").Info("Test Started");

            HomePage homePage = new HomePage(driver);

            homePage.ClickAddToCartButton(ProductConstants.Backpack);
            test.Log(Status.Info, "Click on Add to Cart Button for selected item");

            // Verify item number increment in the cart
            Assert.IsTrue(homePage.IsShoppingCartBadgeisVisiblewithText("1"));
            test.Log(Status.Pass, "Shopping cart Badge has value 1");

            // Verify items count in cart
            Assert.That(homePage.GetItemsCountInCart(), Is.EqualTo("1"));
            test.Log(Status.Pass, "Cart Items Count has 1");

            test.Log(Status.Pass, "Add to Cart is test passed");
        }

        [Test]
        public void VerifySelectedProductInCart()
        {
            test = extent.CreateTest("VerifySelectedProductInCart").Info("Test Started");

            HomePage homePage = new HomePage(driver);
            homePage.ClickAddToCartButton(ProductConstants.Backpack);
            test.Log(Status.Info, "Click on Add to Cart Button for selected item");

            // Click on Cart Button
            homePage.ClickCartButton();
            test.Log(Status.Info, "Click on Cart Button");

            // Verify item number increment in the cart
            Assert.IsTrue(homePage.IsItemAvailableInShoppingCart(ProductConstants.Backpack));
            test.Log(Status.Pass, "Item that added to cart is available");

            test.Log(Status.Pass, "Checking Selected Product In Cart is test passed");
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
