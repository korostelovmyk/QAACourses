using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace QAACourses.Drivers
{
    public class DriverFactory
    {
        public static IWebDriver CreateDriver()
        {
            var options = new ChromeOptions();
            // options.AddArgument("--headless"); 
           
            var driver = new ChromeDriver(options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            
            return driver;
        }
        /*
        private IWebDriver _driver;
        private WebDriverWait _wait;

        private const string _url = "https://theconnectedshop.uk";

        [SetUp]
        public void Setup()
        {
            var options = new ChromeOptions();
            // options.AddArgument("--headless");

            _driver = new ChromeDriver(options);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            _driver.Navigate().GoToUrl(_url);


            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        [TearDown]
        public void Teardown()
        {
            _driver.Dispose();
        }*/
    }
}