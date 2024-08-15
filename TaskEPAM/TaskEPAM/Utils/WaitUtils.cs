using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace TaskEPAM.Utils
{
    public static class WaitUtils
    {
        private static readonly TimeSpan LONG_WAIT_TIME = TimeSpan.FromSeconds(60);
        private static readonly TimeSpan WAIT_TIME = TimeSpan.FromSeconds(10);// in seconds
        private static readonly TimeSpan POLL_FREQUENCY_INTERVAL = TimeSpan.FromMilliseconds(2); // in milliseconds
        public static IWebElement WaitForVisibleElement(By locator, IWebDriver driver)
        {
            Console.WriteLine("visibility: YES  for" + locator);
            return WaitForVisibleElement(locator, driver, LONG_WAIT_TIME, POLL_FREQUENCY_INTERVAL);
        }

        public static bool WaitForVisibleElement(By element, IWebDriver driver, string text)
        {
            Console.WriteLine("visibility: YES  for" + element);
            return WaitForVisibleElement(element, driver, LONG_WAIT_TIME, POLL_FREQUENCY_INTERVAL, text);
        }

        public static void WaitForVisibleElement(IWebElement element, IWebDriver driver)
        {
            Console.WriteLine("visibility: YES  for" + element);
            WaitForVisibleElement(driver, element, WAIT_TIME);
        }

        public static IWebElement WaitForVisibleElement(By locator, IWebDriver driver, TimeSpan timeOutInSeconds, TimeSpan pollIntervalInMs)
        {
            WebDriverWait wait = CreateWaitInstance(driver, timeOutInSeconds, pollIntervalInMs);
            return wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        public static WebDriverWait CreateWaitInstance(IWebDriver driver, TimeSpan timeOutInSeconds, TimeSpan pollIntervalInMs)
        {
            WebDriverWait wait = new WebDriverWait(driver, timeOutInSeconds);
            wait.IgnoreExceptionTypes();
            return wait;
        }

        public static bool WaitForVisibleElement(By element, IWebDriver driver, TimeSpan timeOutInSeconds, TimeSpan pollIntervalInMs, string text)
        {
            WebDriverWait wait = CreateWaitInstance(driver, timeOutInSeconds, pollIntervalInMs);
            return wait.Until(ExpectedConditions.TextToBePresentInElementLocated(element, text));
        }

        public static void WaitForVisibleElement(IWebDriver driver, IWebElement element, TimeSpan timeoutInSeconds)
        {
            WebDriverWait wait = new WebDriverWait(driver, timeoutInSeconds);

            // Wait until the element is visible
            wait.Until(driver =>
            {
                try
                {
                    return element.Displayed;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            });
        }

        public static System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> WaitForListOfElementsToBeVisible(By locator, IWebDriver driver)
        {
            WebDriverWait wait = CreateWaitInstance(driver, LONG_WAIT_TIME, POLL_FREQUENCY_INTERVAL);
            return wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(locator));
        }

    }
}
