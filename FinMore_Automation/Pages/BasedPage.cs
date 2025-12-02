using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using FinMore_Automation.Framework;
using FinMore_Automation.Models;
using Newtonsoft.Json;

namespace FinMore_Automation.Pages

{
    public abstract class BasePage
    {
        protected IWebDriver _driver;
        protected WebDriverWait _wait;


        protected BasePage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Constants.WAIT_TIME));
        }

        public IWebElement WaitForElement(By locator)
        {
            return _wait.Until(ExpectedConditions.ElementExists(locator));
        }

        protected IWebElement WaitForClickable(By locator)
        {
            return _wait.Until(ExpectedConditions.ElementToBeClickable(locator));
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

        protected bool IsVisible(By locator)
        {
            try
            {
                _wait.Until(d => d.FindElements(locator).Any(e => e.Displayed));
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public void SendKeys(By locator, string text)
        {
            TestContext.WriteLine($"Text '{text}' input to : {locator}");
            try
            {
                var input = WaitForElement(locator);
                input.SendKeys(text);
                TestContext.WriteLine($"Text '{text}' input passed");
            }
            catch (Exception ex)
            {
                TestContext.WriteLine($"Text '{text}' input failed with error: {ex.Message}");
                throw;
            }
        }

        protected string GetItem(IWebDriver driver, string key)
        {
            return (string)((IJavaScriptExecutor)driver).ExecuteScript("return window.localStorage.getItem(arguments[0]);", key);
        }

        public static By TestId(string id)
        {
            return By.CssSelector($"[data-testid='{id}']");
        }

        protected string GetInputValue(By locator, string valueToType)
        {
            SendKeys(locator, valueToType);
            return GetAttribute(locator, Constants.ATTRIBUTE_VALUE);
        }

        public void SaveToLocalStorage(string key, string value)
    {
        IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
        js.ExecuteScript($"window.localStorage.setItem('{key}', '{value}');");
    }
 
   
    public string GetFromLocalStorage(string key)
    {
        IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
        return (string)js.ExecuteScript($"return window.localStorage.getItem('{key}');");
    }
 
   
    public void ClearLocalStorage()
    {
        IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
        js.ExecuteScript("window.localStorage.clear();");
    }
 
    
    public void SaveUserToLocalStorage(UserModel user)
    {
        IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;

        js.ExecuteScript($"window.localStorage.setItem('userName', '{user.Name}');");
        js.ExecuteScript($"window.localStorage.setItem('userEmail', '{user.Email}');");
        js.ExecuteScript($"window.localStorage.setItem('userCurrency', '{user.Currency}');");
        js.ExecuteScript($"window.localStorage.setItem('userCreatedAt', '{user.CreatedAt:yyyy-MM-ddTHH:mm:ss.fffZ}');");
        if (!string.IsNullOrEmpty(user.Token))
        {
            js.ExecuteScript($"window.localStorage.setItem('authToken', '{user.Token}');");
        }
    }
 
    
    public bool IsUserLoggedIn()
    {
        string token = GetFromLocalStorage("authToken");
        return !string.IsNullOrEmpty(token);
    }
 
    
    public string GetAuthToken()
    {
        return GetFromLocalStorage("authToken");
    }
    }
}