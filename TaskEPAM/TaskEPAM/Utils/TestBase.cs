using AventStack.ExtentReports;
using AventStack.ExtentReports.Model;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using OpenQA.Selenium;
using TaskEPAM.Constants;
using TaskEPAM.Drivers;

namespace TaskEPAM.Utils
{
    public class TestBase
    {
        public IWebDriver driver;
        public static ExtentReports extent;
        public static ExtentTest test;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            extent = ExtentManager.GetInstance();
        }

        [SetUp]
        public void Setup()
        {
            string browserName = BrowserTarget.BrowerName;
            driver = WebDriverFactory.CreateDriver(browserName);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(GeneralConstants.BaseUrl);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [TearDown]
        public void Teardown()
        {
            if (driver != null)
            {
                var name = TestContext.CurrentContext.Test.Name;
                var status = TestContext.CurrentContext.Result.Outcome.Status;
                var stackTrace = TestContext.CurrentContext.Result.StackTrace;
                var errorMessage = TestContext.CurrentContext.Result.Message;

                if (status == NUnit.Framework.Interfaces.TestStatus.Failed)
                {
                    test.Fail("Test Failed", MediaEntityBuilder.CreateScreenCaptureFromPath(TakeScreenshot(name)).Build());
                    test.Fail("Stacktrace: " + stackTrace);
                    test.Fail("Error Message: " + errorMessage);
                }

                driver.Quit();
            }
        }

        public string TakeScreenshot(string testcaseName)
        {
            string path = Directory.GetParent(@"../../../").FullName
                   + Path.DirectorySeparatorChar + "Reports";

            ITakesScreenshot screenshotDriver = driver as ITakesScreenshot;
            Screenshot screenshot = screenshotDriver.GetScreenshot();
            string screenshotPath = Path.Combine(path + "_" + testcaseName, $"_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.png");
            screenshot.SaveAsFile(screenshotPath);
            return screenshotPath;
        }

        [OneTimeTearDown]
        public void OneTimeTeardown()
        {
            if (driver != null)
            {
                driver.Quit();
            }

            extent.Flush();
        }
    }
}
