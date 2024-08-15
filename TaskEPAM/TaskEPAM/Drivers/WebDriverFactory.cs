using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace TaskEPAM.Drivers
{
    public static class WebDriverFactory
    {
        public static IWebDriver CreateDriver(string browserName)
        {
            IWebDriver driver;

            switch (browserName.ToLower())
            {
                case "chrome":
                    driver = new ChromeDriver();
                    break;

                case "firefox":
                    driver = new FirefoxDriver();
                    break;

                default:
                    throw new ArgumentException($"Browser '{browserName}' is not supported.");
            }

            return driver;
        }
    }
}
