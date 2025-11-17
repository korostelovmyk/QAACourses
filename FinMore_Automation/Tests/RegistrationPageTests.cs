using System;
using FinMore_Automation.Drivers;
using OpenQA.Selenium.Support.UI;
using FinMore_Automation.Pages;
using FinMore_Automation.Framework;
using OpenQA.Selenium;
using NUnit.Framework.Internal;
using FinMore_Automation.Models;

namespace FinMore_Automation.Tests;

public class RegistrationPageTests
{

    private IWebDriver _driver;
    private HomePage _homePage;
    private RegistrationPage _registrationPage;
    private AppContainer _appContainer;
    private WebDriverWait _wait;
    private BasePage _basePage;

    [SetUp]
    public void Setup()
    {
        _driver = DriverFactory.CreateDriver();
        _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(Constants.WAIT_TIME));
        _homePage = new HomePage(_driver, _wait);
        _registrationPage = new RegistrationPage(_driver, _wait);
        _appContainer = new AppContainer(_driver, _wait);
        _homePage.Open();
        _homePage.ClickRegistration();
    }

    [TearDown]
    public void Teardown()
    {
        _driver.Quit();
    }

    [Test]
    public void IsRegistrationFullNameInputVisible()
    {
        bool isInputVisible = _registrationPage.IsRegistrationFullNameInputVisible();

        Assert.That(isInputVisible, Is.True, "Registration full name input is not displayed");
    }

    [Test]
    public void IsRegistrationEmailInputVisible()
    {
        bool isInputVisible = _registrationPage.IsRegistrationEmailInputVisible();

        Assert.That(isInputVisible, Is.True, "Registration email input is not displayed");
    }

    [Test]
    public void IsRegistrationPasswordInputVisible()
    {
        bool isInputVisible = _registrationPage.IsRegistrationPasswordInputVisible();

        Assert.That(isInputVisible, Is.True, "Registration password input is not displayed");
    }

    [Test]
    public void IsRegistrationConfirmPasswordInputVisible()
    {
        bool isInputVisible = _registrationPage.IsRegistrationConfirmPasswordInputVisible();

        Assert.That(isInputVisible, Is.True, "Registration confirmation password input is not displayed");
    }

    [Test]
    public void IsRegistrationSubmitButtonVisible()
    {
        bool isInputVisible = _registrationPage.IsRegistrationSubmitButtonVisible();

        Assert.That(isInputVisible, Is.True, "Registration submit button is not displayed");
    }

    [Test]
    public void IsRegistrationToLoginSwitchVisible()
    {
        bool isInputVisible = _registrationPage.IsRegistrationFullNameInputVisible();

        Assert.That(isInputVisible, Is.True, "Switch to login page is not displayed");
    }

    [Test]
    public void IsRegistrationCurrencySelectVisible()
    {
        bool isInputVisible = _registrationPage.IsRegistrationCurrencySelectVisible();

        Assert.That(isInputVisible, Is.True, "Registration currency list is not displayed");
    }


    [Test]
    public void IsRegistrationTitleVisible()
    {
        bool isInputVisible = _registrationPage.IsRegistrationTitleVisible();

        Assert.That(isInputVisible, Is.True, "Registration title is not displayed");
    }

    [Test]
    public void IsRegistrationFullNameInputExceptText()
    {
        string testFullName = Helpers.GenerateUniqueFullName();
        string actualAttributeValue = _registrationPage.GetStringValueFromRegistrationFullNameInputByAttribute(testFullName);

        Assert.That(actualAttributeValue, Is.EqualTo(testFullName), $"Registration full name value {actualAttributeValue} is not equal to test full name '{testFullName}'");
    }

    [Test]
    public void IsRegistrationEmailInputExceptText()
    {
        string testEmail = Helpers.GenerateUniqueTestEmail();
        string actualAttributeValue = _registrationPage.GetStringValueFromRegistrationFullNameInputByAttribute(testEmail);

        Assert.That(actualAttributeValue, Is.EqualTo(testEmail), $"Registration email value {actualAttributeValue} is not equal to test email '{testEmail}'");
    }

    [Test]
    public void IsRegistrationPasswordInputExceptText()
    {
        string testPassword = Constants.TEST_PASSWORD;
        string actualAttributeValue = _registrationPage.GetStringValueFromRegistrationPasswordInputByAttribute(testPassword);

        Assert.That(actualAttributeValue, Is.EqualTo(testPassword), $"Registration password value {actualAttributeValue} is not equal to test password '{testPassword}'");
    }

    [Test]
    public void IsRegistrationPasswordConfirmationInputExceptText()
    {
        string testPassword = Constants.TEST_PASSWORD;
        string actualAttributeValue = _registrationPage.GetStringValueFromRegistrationConfirmPasswordInputByAttribute(testPassword);

        Assert.That(actualAttributeValue, Is.EqualTo(testPassword), $"Registration password confirmation value {actualAttributeValue} is not equal to test password '{testPassword}'");
    }

    [Test]
    public void SelectCurrencyGBP()
    {
        string currency = Constants.CURRENCY_GBP;
        string actualAttributeValue = _registrationPage.SelectCurrencyOnRegistration(currency);

        Assert.That(actualAttributeValue, Is.EqualTo(currency), $"Registration currency value {actualAttributeValue} is not equal to test currency '{currency}'");
    }


    [Test]
    public void SelectCurrencyUAH()
    {
        string currency = Constants.CURRENCY_UAH;
        string actualAttributeValue = _registrationPage.SelectCurrencyOnRegistration(currency);

        Assert.That(actualAttributeValue, Is.EqualTo(currency), $"Registration currency value {actualAttributeValue} is not equal to test currency '{currency}'");
    }

    [Test]
    public void SelectCurrencyUSD()
    {
        string currency = Constants.CURRENCY_USD;
        string actualAttributeValue = _registrationPage.SelectCurrencyOnRegistration(currency);

        Assert.That(actualAttributeValue, Is.EqualTo(currency), $"Registration currency value {actualAttributeValue} is not equal to test currency '{currency}'");
    }

    [Test]
    public void SelectCurrencyEUR()
    {
        string currency = Constants.CURRENCY_EUR;
        string actualAttributeValue = _registrationPage.SelectCurrencyOnRegistration(currency);

        Assert.That(actualAttributeValue, Is.EqualTo(currency), $"Registration currency value {actualAttributeValue} is not equal to test currency '{currency}'");
    }

    [Test]
    public void CheckLoginSwitchButton()
    {
        _registrationPage.ClickLoginSwitchButton();
        
        Assert.That(_homePage.IsLoginPageVisible(), Is.True, $"Page was not switched from registration to login.'");
    }

    [Test]
    public void UserRegistration()
    {
        string testCurrency = Constants.CURRENCY_EUR;
        string testPassword = Constants.TEST_PASSWORD;
        string testEmail = Helpers.GenerateUniqueTestEmail();
        string testFullName = Helpers.GenerateUniqueFullName();
        DateTime dateTime = DateTime.Now;

        _registrationPage.UserRegistration(testFullName, testEmail, testPassword, testCurrency);

        UserModel localUser = _registrationPage.GetLastUserDataFromLocalStorage();

        Assert.That(_appContainer.IsAppContainerPageVisible(), Is.True, $"AppContainer page was not displayed after registration.");
        Assert.That(localUser.Name, Is.EqualTo(testFullName), $"Name from local storage is '{localUser.Name}' but expected to be '{testFullName}'");
        Assert.That(localUser.Email, Is.EqualTo(testEmail), $"Emil from local storage is '{localUser.Name}' but expected to be '{testEmail}'");
        Assert.That(localUser.Currency, Is.EqualTo(testCurrency), $"Currency from local storage is '{localUser.Name}' but expected to be '{testCurrency}'");
        Assert.That(localUser.CreatedAt, Is.EqualTo(dateTime).Within(TimeSpan.FromSeconds(2)));

        TestContext.WriteLine($"Current user name is {localUser.Name} and expected to be {testFullName}"
        + $" and email {localUser.Email} - expected {testEmail}"
        + $" and currency {localUser.Currency} - expected {testCurrency}"
        + $" and create time {localUser.CreatedAt} - and expected {dateTime}");
    }
}
