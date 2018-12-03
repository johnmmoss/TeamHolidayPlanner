using NUnit.Framework;
using System;
using TechTalk.SpecFlow;
using Web.AcceptanceTests.Pages;

namespace Web.AcceptanceTests
{
    [Binding]
    public class UserAdminSteps
    {
        [Given(@"I am a valid user of the site")]
        public void GivenIAmAValidUserOfTheSite()
        {
        }
        
        [Given(@"I have navigated to the login page")]
        public void GivenIHaveNavigatedToTheLoginPage()
        {
            var homePage = ScenarioContext.Current.Get<HomePage>();
            var loginPage = homePage.ClickLoginLink();
            ScenarioContext.Current.Set(loginPage);
        }
        
        [Given(@"I have entered a username of ""(.*)"" and a password of ""(.*)""")]
        public void GivenIHaveEnteredAUsernameOfAndAPasswordOf(string username, string password)
        {
            var loginPage = ScenarioContext.Current.Get<LoginPage>();
            loginPage.EnterUsername(username);
            loginPage.EnterPassword(password);
        }
        
        [When(@"I click the create button")]
        public void WhenIClickTheCreateButton()
        {
            var loginPage = ScenarioContext.Current.Get<LoginPage>();
            loginPage.ClickLogin();
        }
        
        [Then(@"I am logged in to the site")]
        public void ThenIAmLoggedInToTheSite()
        {
            var homePage = ScenarioContext.Current.Get<HomePage>();

            Assert.IsTrue(homePage.IsLoggedIn());
        }
    }
}
