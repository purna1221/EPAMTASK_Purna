using AventStack.ExtentReports;
using NUnit.Framework;
using TaskEPAM.Constants;
using TaskEPAM.Pages;
using TaskEPAM.Utils;

namespace TaskEPAM.Tests
{
    [TestFixture]
    public class LoginTests : TestBase
    {
        [Test]
        public void Should_DisplayAllElementsInLoginPage()
        {
            test = extent.CreateTest("VerifyAllElementsInPage").Info("Test Started");

            LoginPage loginPage = new LoginPage(driver);
            Assert.IsTrue(loginPage.VerifyPageElements(), "All elements should be displayed on the login page.");
        }

        [Test]
        public void Should_LoginSuccessfully()
        {
            test = extent.CreateTest("VerifyLogin").Info("Test Started");

            LoginPage loginPage = new(driver);
            HomePage homePage = new HomePage(driver);

            var loginModel = loginPage.LoadLoginModel("login.json");            

            loginPage.Login(loginModel.UserName, loginModel.Password);
            loginPage.ClickLoginBtn();

            test.Log(Status.Info, "Navigated to login page and performed login");

            Assert.IsTrue(homePage.GetTitle().Contains(GeneralConstants.PageTitle), "The homepage title should contain the expected value.");

            test.Log(Status.Pass, "Login test passed");
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