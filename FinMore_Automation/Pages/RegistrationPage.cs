using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using FinMore_Automation.Framework;
using System.Diagnostics;
using Newtonsoft.Json;
using FinMore_Automation.Models;
using NUnit.Framework.Internal;

namespace FinMore_Automation.Pages;

public class RegistrationPage : BasePage

{
    private static readonly By RegistrationFormPage = TestId("register-page");
    private static readonly By RegistrationPageTitle = TestId("register-title");
    private static readonly By RegistrationFullNameInput = TestId("register-name-input");
    private static readonly By RegistrationEmailInput = TestId("register-email-input");
    private static readonly By RegistrationPasswordInput = TestId("register-password-input");
    private static readonly By RegistrationConfirmPasswordInput = TestId("register-confirm-password-input");
    private static readonly By RegistrationCurrencySelect = TestId("register-currency-select");
    private static readonly By RegistrationSubmitButton = TestId("register-submit-button");
    private static readonly By RegistrationToLoginSwitch = TestId("switch-to-login-button");
    private static readonly By RegistrationCurrencyUAH = TestId("currency-option-UAH");
    private static readonly By RegistrationCurrencyUSD = TestId("currency-option-USD");
    private static readonly By RegistrationCurrencyGBP = TestId("currency-option-GBP");
    private static readonly By RegistrationCurrencyEUR = TestId("currency-option-EUR");


    protected WebDriverWait _wait;

    public RegistrationPage(IWebDriver driver, WebDriverWait wait) : base(driver)
    {
        _driver = driver;
        _wait = wait;
    }

    public bool IsRegistrationPageVisible()
    {
        return IsVisible(RegistrationFormPage);
    }

    public bool IsRegistrationTitleVisible()
    {
        return IsVisible(RegistrationPageTitle);
    }

    public bool IsRegistrationFullNameInputVisible()
    {
        return IsVisible(RegistrationFullNameInput);
    }

    public bool IsRegistrationEmailInputVisible()
    {
        return IsVisible(RegistrationEmailInput);
    }

    public bool IsRegistrationPasswordInputVisible()
    {
        return IsVisible(RegistrationPasswordInput);
    }

    public bool IsRegistrationConfirmPasswordInputVisible()
    {
        return IsVisible(RegistrationConfirmPasswordInput);
    }

    public bool IsRegistrationCurrencySelectVisible()
    {
        return IsVisible(RegistrationCurrencySelect);
    }

    public bool IsRegistrationSubmitButtonVisible()
    {
        return IsVisible(RegistrationSubmitButton);
    }

    public bool IsRegistrationToLoginSwitchVisible()
    {
        return IsVisible(RegistrationToLoginSwitch);
    }

    public string GetStringValueFromRegistrationFullNameInputByAttribute(string attribute)
    {
        SendKeys(RegistrationFullNameInput, attribute);
        string attributeValue = GetAttribute(RegistrationFullNameInput, Constants.ATTRIBUTE_VALUE);

        return attributeValue;
    }

    public string GetStringValueFromRegistrationEmailInputByAttribute(string attribute)
    {
        SendKeys(RegistrationEmailInput, attribute);
        string attributeValue = GetAttribute(RegistrationEmailInput, Constants.ATTRIBUTE_VALUE);

        return attributeValue;
    }

    public string GetStringValueFromRegistrationPasswordInputByAttribute(string attribute)
    {
        string attributeValue = GetInputValue(RegistrationPasswordInput, attribute);

        return attributeValue;
    }

    public string GetStringValueFromRegistrationConfirmPasswordInputByAttribute(string attribute)
    {

        string attributeValue = GetInputValue(RegistrationConfirmPasswordInput, attribute);

        return attributeValue;
    }

    public string SelectCurrencyOnRegistration(string currency)
    {
        Click(RegistrationCurrencySelect);

        By currencyLocator = GetLocatorByCurrency(currency);
        Click(currencyLocator);
        string attributeValue = GetAttribute(currencyLocator, Constants.ATTRIBUTE_VALUE);

        return attributeValue;
    }

    public void UserRegistration(string fullName, string email, string password, string currency)
    {    
        By currencyLocator = GetLocatorByCurrency(currency);

        SendKeys(RegistrationFullNameInput, fullName);
        SendKeys(RegistrationEmailInput, email);
        SendKeys(RegistrationPasswordInput, password);
        SendKeys(RegistrationConfirmPasswordInput, password);
        Click(currencyLocator);
        Click(RegistrationSubmitButton);
    }

    public void ClickLoginSwitchButton() => Click(RegistrationToLoginSwitch);


    public UserModel GetLastUserDataFromLocalStorage()
    {
        UserModel[] localValues = JsonConvert.DeserializeObject<UserModel[]>(GetLocalStorageByUserKey());

        int lastRecord = localValues.Count() - 1;

        return localValues[lastRecord];
    }

    private By GetLocatorByCurrency(string currency)
    {
        By currencyLocator = currency switch
        {
            Constants.CURRENCY_GBP => RegistrationCurrencyGBP,
            Constants.CURRENCY_UAH => RegistrationCurrencyUAH,
            Constants.CURRENCY_USD => RegistrationCurrencyUSD,
            Constants.CURRENCY_EUR => RegistrationCurrencyEUR,
            _ => throw new ArgumentException($"Unknown currency - '{currency}'"),
        };

        return currencyLocator;
    }
    
    private string GetLocalStorageByUserKey()
    {
        return Helpers.GetItemFromLocalStorage(_driver, Constants.LOCAL_STORAGE_KEY_USERS);
    }
}