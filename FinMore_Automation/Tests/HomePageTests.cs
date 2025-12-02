using FinMore_Automation.Pages;
using OpenQA.Selenium;
using NUnit.Framework;
using FinMore_Automation.Drivers;
using OpenQA.Selenium.Support.UI;
using FinMore_Automation.Framework;
using FinMore_Automation.Models;

namespace FinMore_Automation.Tests;

public class HomePageTests
{
    private IWebDriver _driver;
    private HomePage _homePage;
    private RegistrationPage _registrationPage;
    private BasePage _basePage;
    private WebDriverWait _wait;
    private AppContainer _appContainer;

    private const string REGISTRATION_PAGE_ID = "register-page";


    [SetUp]
    public void Setup()
    {
        _driver = DriverFactory.CreateDriver();
        _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(Constants.WAIT_TIME));
        _homePage = new HomePage(_driver, _wait);
        _registrationPage = new RegistrationPage(_driver, _wait);
        _homePage.Open();
    }

    [TearDown]
    public void Teardown()
    {
        _driver.Quit();
    }

    [Test]
    public void OpenSite()
    {
        Assert.That(_homePage.GetUrl().StartsWith(Constants.FINMORE_URL.TrimEnd(Constants.TRIM_END.ToCharArray())), Is.True, "Url is incorrect");
    }

    [Test]
    public void CheckLoginPage()
    {
        Assert.That(_homePage.IsLoginPageVisible(), Is.True, $"Registration page was not displayed after click.");
    }

    [Test]
    public void CheckRegistrationButtonOnClick()
    {
        _homePage.ClickRegistration();

        Assert.That(_registrationPage.IsRegistrationPageVisible(), Is.True, $"Registration page was not displayed after click.");
    }

    [Test]
    public void CheckLoginEmailInputVisible()
    {
        Assert.That(_homePage.IsLoginEmailInputVisible(), Is.True, "Login email input is not displayed.");
    }

    [Test]
    public void CheckLoginPasswordInputVisible()
    {
        Assert.That(_homePage.IsLoginPasswordInputVisible(), Is.True, "Login password input is not displayed.");
    }

    [Test]
    public void CheckLoginPasswordInputValue()
    {
        string testPassword = Constants.TEST_PASSWORD;
        string actualAttributeValue = _homePage.GetStringValueFromLoginPasswordInputByAttribute(testPassword);

        Assert.That(actualAttributeValue, Is.EqualTo(testPassword), $"Login password value {actualAttributeValue} is not equal to test email '{testPassword}'.");
    }

    [Test]
    public void CheckLoginEmailInputValue()
    {
        string testEmail = Helpers.GenerateUniqueTestEmail();
        string actualAttributeValue = _homePage.GetStringValueFromLoginEmailInputByAttribute(testEmail);

        Assert.That(actualAttributeValue, Is.EqualTo(testEmail), $"Login email input value {actualAttributeValue} is not equal to test email '{testEmail}'.");
    }
}