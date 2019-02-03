using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.AcceptanceTests.Pages
{
    public class HomePage
    {
        private readonly IWebDriver webDriver;

        public HomePage(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        public LoginPage ClickLoginLink()
        {
            var loginLink = webDriver.FindElement(By.Id("LoginLink"));
            loginLink.Click();
            return new LoginPage(webDriver);
        }

        public bool IsLoggedIn()
        {
            return webDriver.FindElement(By.Id("LogoutLink")).Displayed;
        }
    }
}
