using AventStack.ExtentReports;
using OpenQA.Selenium;

namespace TaskEPAM.Pages
{
    public static class ActionPage
    {
       public static string projectLoc = Path.Combine(Directory.GetCurrentDirectory(), "ScreenShots");

        static string path = Directory.GetParent(@"../../../").FullName
                  + Path.DirectorySeparatorChar + "Reports"
                  + Path.DirectorySeparatorChar + "Result_" + DateTime.Now.ToString("ddMMyyyy HHmmss");
        public static void ClickElement(this IWebElement locator) 
        { 
            locator.Click();
        }

        public static void EnterText(this IWebElement locator, string text)
        {
            locator.Clear();
            locator.SendKeys(text);
        }

        public static string GetText(this IWebElement locator)
        {
          return locator.Text;
        }

        public static bool VerifyElementDisplayed(this IWebElement locator)
        {
            bool flag = false;
           
                for (int i = 0; i < 5; i++)
                {
                    if (locator.Displayed)
                    {
                        flag = true;
                        break;
                    }
                    else
                    {
                        Thread.Sleep(5);
                    }
                }
            return flag;
        }

    }
}
