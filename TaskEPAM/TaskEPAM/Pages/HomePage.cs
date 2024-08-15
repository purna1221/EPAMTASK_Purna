using AventStack.ExtentReports;
using OpenQA.Selenium;
using System.Collections.ObjectModel;
using TaskEPAM.Utils;

namespace TaskEPAM.Pages
{
    public class HomePage
    {
        private readonly IWebDriver driver;
        IWebElement Shoppingcart => driver.FindElement(By.XPath("//a[@data-test='shopping-cart-link']"));
        By Shoppingcartcount => By.XPath("//a[@data-test='shopping-cart-link']/span[@data-test='shopping-cart-badge']");
        IWebElement BurgerBtn => driver.FindElement(By.Id("react-burger-menu-btn"));
        IWebElement RemoveBtn => driver.FindElement(By.Id("remove-sauce-labs-backpack"));
        By MenuList => By.XPath("//div[@class='bm-menu']//a");
        List<IWebElement> AllElementsInCart => driver.FindElements(By.XPath("//div[@data-test='inventory-item-name']")).ToList();

        public By AddandRemoveToCartButton(string itemName)
        { 
            return By.XPath("//div[@class='inventory_item_name '][contains(text(),'" + itemName + "')]/../following-sibling::div/../following-sibling::div//button");
        }

        public List<string> AllItemsInCart()
        {
            List<string> itemsincart = new List<string>();

            foreach (IWebElement item in AllElementsInCart) 
            {
                string text = item.Text;
                itemsincart.Add(text);               
            }
            return itemsincart;
        }    

        public HomePage(IWebDriver driver) 
        {
            this.driver = driver;
        }

        public bool VerifyPageElements()
        {
            return Shoppingcart.Displayed &&
                   BurgerBtn.Displayed;
        }

        public string GetTitle()
        {
            return driver.Title;
        }

        public void ClickAddToCartButton(string itemName)
        {
            WaitUtils.WaitForVisibleElement(AddandRemoveToCartButton(itemName), driver);
            driver.FindElement(AddandRemoveToCartButton(itemName)).ClickElement();
        }

        public void ClickCartButton()
        {
           WaitUtils.WaitForVisibleElement(Shoppingcart, driver);
           Shoppingcart.ClickElement();
        }

        public string GetItemsCountInCart()
        {
            WaitUtils.WaitForVisibleElement(Shoppingcartcount, driver);            
            return driver.FindElement(Shoppingcartcount).GetText();
        }

        public bool IsShoppingCartBadgeisVisiblewithText(string Count)
        {
           return WaitUtils.WaitForVisibleElement(Shoppingcartcount, driver, Count);
        }

        public bool IsItemAvailableInShoppingCart(string ItemName)
        {
            return WebElementUtils.VerifyElementWithTextInList(ItemName, AllElementsInCart);
        }

        public bool IsItemAvailableInShoppingCart(List<string> expectedList)
        {
            return WebElementUtils.VerifyListsAreEqual(AllItemsInCart(), expectedList);
        }

        public void ClickBurgerButton()
        {
            WaitUtils.WaitForVisibleElement(BurgerBtn, driver);
            BurgerBtn.ClickElement();
        }

        public void ClickMenuItem(string ItemName)
        {
            WaitUtils.WaitForListOfElementsToBeVisible(MenuList, driver);
            List<IWebElement> menuList = driver.FindElements(MenuList).ToList();
            WebElementUtils.ClickElementWithTextFromList(ItemName, menuList);
        }

        public bool CartShouldBeEmpty()
        {
            //WaitUtils.WaitForVisibleElement(Shoppingcartcount, driver);
            return WebElementUtils.AssertElementNotPresent(driver, Shoppingcartcount);
        }
    }
}
