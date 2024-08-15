using OpenQA.Selenium;
using System.Text.Json;
using TaskEPAM.Utils;

namespace TaskEPAM.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver driver;
        IWebElement UserNameTxtBox => driver.FindElement(By.Id("user-name"));
        IWebElement PasswordTxtBox => driver.FindElement(By.Id("password"));
        IWebElement LoginBtn => driver.FindElement(By.Id("login-button"));

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public bool VerifyPageElements()
        {
            return UserNameTxtBox.Displayed &&
                   PasswordTxtBox.Displayed && 
                   LoginBtn.Displayed;
        }

        public void Login(string username, string password)
        {
            UserNameTxtBox.Clear();
            UserNameTxtBox.EnterText(username);
            PasswordTxtBox.Clear();
            PasswordTxtBox.EnterText(password);
        }        

        public void ClickLoginBtn()
        {
            LoginBtn.ClickElement();
        }
        public LoginModel LoadLoginModel(string fileName)
        {
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            return JsonSerializer.Deserialize<LoginModel>(File.ReadAllText(jsonFilePath));
        }
    }
}
