using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
 
namespace QAACourses.Pages

{
    public abstract class BasePage
    {
        protected IWebDriver Driver;
        protected WebDriverWait Wait;
        
        protected BasePage(IWebDriver driver)
        {
            Driver = driver;
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }
 
        public IWebElement WaitForElement(By locator)
        {
            return Wait.Until(ExpectedConditions.ElementExists(locator));
        }
 
        protected IWebElement WaitForClickable(By locator)
        {
            return Wait.Until(ExpectedConditions.ElementToBeClickable(locator));
        }
 
        protected void Click(By locator)
        {
            WaitForClickable(locator).Click();
        }
 
        protected string GetText(By locator)
        {
            return WaitForElement(locator).Text;
        }
 
        protected string GetAttribute(By locator, string attribute)
        {
            return WaitForElement(locator).GetAttribute(attribute);
        }
    }
}

 