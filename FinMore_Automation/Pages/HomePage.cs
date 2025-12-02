using OpenQA.Selenium;
using FinMore_Automation.Framework;
using OpenQA.Selenium.Support.UI;
using FinMore_Automation.Models;
using Newtonsoft.Json;

namespace FinMore_Automation.Pages;

public class HomePage : BasePage
{
    private static readonly By SwitchToRegistrationButton = TestId("switch-to-register-button");
    private static readonly By LoginForm = TestId("login-form");
    private static readonly By LoginPage = TestId("login-page");
    private static readonly By LoginButton = TestId("login-submit-button");
    private static readonly By LoginEmailInput = TestId("login-email-input");
    private static readonly By LoginPasswordInput = TestId("login-password-input");
    private static readonly By PasswordToggle = TestId("toggle-password-visibility");
    private static readonly By LoginTitle = TestId("login-title");

    private IWebDriver _driver;
    protected WebDriverWait _wait;
    public HomePage(IWebDriver driver, WebDriverWait wait) : base(driver)
    {
        _driver = driver;
        _wait = wait;
    }

    public void Open()
    {
        _driver.Navigate().GoToUrl(Constants.FINMORE_URL);
    }

    public string GetUrl()
    {
        return _driver.Url;
    }

    public string GetTitle()
    {
        return _driver.Title;
    }

    public bool IsLoginPageVisible()
    {
        return IsVisible(LoginPage);
    }

    public bool IsLoginEmailInputVisible()
    {
        return IsVisible(LoginEmailInput);
    }

    public bool IsLoginPasswordInputVisible()
    {
        return IsVisible(LoginPasswordInput);
    }
    
    public string GetStringValueFromLoginEmailInputByAttribute(string attribute)
    {
        SendKeys(LoginEmailInput, attribute);
        string attributeValue = GetAttribute(LoginEmailInput, Constants.ATTRIBUTE_VALUE);

        return attributeValue;
    }

    public string GetStringValueFromLoginPasswordInputByAttribute(string attribute)
    {
        SendKeys(LoginPasswordInput, attribute);
        string attributeValue = GetAttribute(LoginPasswordInput, Constants.ATTRIBUTE_VALUE);

        return attributeValue;
    }
    
    public void UserLogin(string email, string password)
    {
        SendKeys(LoginEmailInput, email);
        SendKeys(LoginPasswordInput, password);
        
        Click(LoginButton);
    }

    public void ClickRegistration() => Click(SwitchToRegistrationButton);

        
    public UserModel GetLastUserDataFromLocalStorage()
    {
        UserModel[] localValues = JsonConvert.DeserializeObject<UserModel[]>(GetLocalStorageByUserKey());

        int lastRecord = localValues.Count() - 1;

        return localValues[lastRecord];
    }
    
    private string GetLocalStorageByUserKey()
    {
        return Helpers.GetItemFromLocalStorage(_driver, Constants.LOCAL_STORAGE_KEY_USERS);
    }
}
