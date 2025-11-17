using FinMore_Automation.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace FinMore_Automation.Drivers
{
    public class DriverFactory
    {
        public static IWebDriver CreateDriver()
        {
            var options = new ChromeOptions();
            // options.AddArgument("--headless"); 
           
            var driver = new ChromeDriver(options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Constants.WAIT_TIME);
            
            return driver;
        }
    }
}