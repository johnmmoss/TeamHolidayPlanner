using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.AcceptanceTests.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver webDriver;

        public LoginPage(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        public void EnterUsername(string username)
        {
            webDriver.FindElement(By.Id("Username")).SendKeys(username);
        }

        public void EnterPassword(string password)
        {
            webDriver.FindElement(By.Id("Password")).SendKeys(password);
        }

        public void ClickLogin()
        {
            webDriver.FindElement(By.Id("Login")).Click();
        }
    }
}
