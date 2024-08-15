using OpenQA.Selenium;
using TaskEPAM.Utils;

namespace TaskEPAM.Pages
{
    public  class YourCartPage
    {
        private readonly IWebDriver driver;

        IWebElement RemoveBtn => driver.FindElement(By.Id("remove-sauce-labs-backpack"));
        IWebElement CheckoutBtn => driver.FindElement(By.Id("checkout"));
        IWebElement ContinueShoppingBtn => driver.FindElement(By.Id("continue-shopping"));
        IWebElement FirstName => driver.FindElement(By.Id("first-name"));
        IWebElement LastName => driver.FindElement(By.Id("last-name"));
        IWebElement PostalCode => driver.FindElement(By.Id("postal-code"));
        IWebElement ContinueBtn => driver.FindElement(By.Id("continue"));
        IWebElement CancelBtn => driver.FindElement(By.Id("cancel"));
        IWebElement ItemQuantity => driver.FindElement(By.XPath("//div[@data-test='item-quantity']"));
        IWebElement FinishBtn => driver.FindElement(By.Id("finish"));
        IWebElement ThankYouLbl => driver.FindElement(By.XPath("//h2[@data-test='complete-header']"));

        public YourCartPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public bool VerifyPageElements()
        {
            return RemoveBtn.Displayed &&
                   CheckoutBtn.Displayed &&
                   ContinueShoppingBtn.Displayed;
        }

        public void ClickCheckoutBtn()
        {
            WaitUtils.WaitForVisibleElement(CheckoutBtn, driver);
            CheckoutBtn.ClickElement();
        }

        public void CheckOutDetails(string firstName, string lastName, string postalCode)
        {
            WaitUtils.WaitForVisibleElement(FirstName, driver);
            FirstName.Clear();
            FirstName.EnterText(firstName);
            LastName.Clear();
            LastName.EnterText(lastName);
            PostalCode.Clear();
            PostalCode.EnterText(postalCode);

            ContinueBtn.ClickElement();
        }

        public string CheckItemQuantity()
        {
            WaitUtils.WaitForVisibleElement(ItemQuantity, driver);
            return ItemQuantity.GetText();
        }

        public void ClickFinishBtn()
        {
            WaitUtils.WaitForVisibleElement(FinishBtn, driver);
            FinishBtn.ClickElement();
        }

        public string OrderCompletedMsg()
        {
            WaitUtils.WaitForVisibleElement(ThankYouLbl, driver);
            return ThankYouLbl.GetText();
        }
    }
}
