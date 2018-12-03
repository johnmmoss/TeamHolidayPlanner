using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Configuration;
using System.IO;
using System.Reflection;
using TechTalk.SpecFlow;
using Web.AcceptanceTests.Pages;

namespace Web.AcceptanceTests
{
    [Binding]
    public sealed class Setup
    {
        private static string websiteUrl = ConfigurationSettings.AppSettings["WebSiteUrl"];
        private static string connectionString = ConfigurationSettings.AppSettings["ConnectionString"];
        private static string dataFilePath = ConfigurationSettings.AppSettings["DataFilePath"];

        [BeforeFeature]
        public void BeforeFeature()
        {
            var directoryPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var sqlFilePath = Path.Combine(directoryPath, dataFilePath);
            var database = new Database(connectionString, sqlFilePath);

            database.Reset();

            IWebDriver driver = new ChromeDriver();
            FeatureContext.Current.Set(driver);
            driver.Navigate().GoToUrl(websiteUrl);
        }

        [AfterFeature]
        public void AfterFeature()
        {
            // Clear up the webdriver
            var webDriver = FeatureContext.Current.Get<IWebDriver>();
            webDriver.Quit();
            webDriver.Dispose();
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            // At the begining of the scenario, we are on the homepage
            var webDriver = FeatureContext.Current.Get<IWebDriver>();
            var homePage = new HomePage(webDriver);
            ScenarioContext.Current.Set<HomePage>(homePage);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            //TODO: implement logic that has to run after executing each scenario
        }
    }
}
