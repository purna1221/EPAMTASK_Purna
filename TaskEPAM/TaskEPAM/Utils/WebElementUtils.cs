using AngleSharp.Dom;
using NUnit.Framework;
using OpenQA.Selenium;

namespace TaskEPAM.Utils
{
    public class WebElementUtils
    {
        private static readonly TimeSpan LONG_WAIT_TIME = TimeSpan.FromSeconds(60); // in seconds
        private static readonly TimeSpan POLL_FREQUENCY_INTERVAL = TimeSpan.FromMilliseconds(2); // in milliseconds
        public static void ClickElementWithTextFromList(string selectedtext, List<IWebElement> listOfElements)
        {
            foreach (IWebElement element in listOfElements)
            {
                if (element.Text.Equals(selectedtext))
                {
                    element.Click();
                    break;
                }
            }
        }

        public static bool VerifyElementWithTextInList(string text, List<IWebElement> listOfElements)
        {
            bool flag = false;
            foreach (IWebElement element in listOfElements)
            {
                if (element.Text.Equals(text))
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }

        public static bool VerifyListsAreEqual(List<string> actualList, List<string> expectedList)
        {
            // Check if the two lists have the same elements in the same order
            bool areEqual = actualList.SequenceEqual(expectedList);

            // Assertion to verify that the lists are equal
            Assert.IsTrue(areEqual, "The lists are not equal!");

            return areEqual;
        }

        public static bool AssertElementNotPresent(IWebDriver driver, By locator)
        {
            bool flag = false;
            try
            {
                IWebElement element = driver.FindElement(locator);
                if (element != null) { flag = false; }
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("visibility: NO  for " + locator);
                flag = true;
            }
            return flag;
        }
    }
}
