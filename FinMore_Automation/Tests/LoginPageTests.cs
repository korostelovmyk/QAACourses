using System;
using OpenQA.Selenium;
using FinMore_Automation.Pages;
using OpenQA.Selenium.Support.UI;
using FinMore_Automation.Drivers;
using FinMore_Automation.Framework;
using FinMore_Automation.Models;

namespace FinMore_Automation.Tests;

public class LoginPageTests
{

    private IWebDriver _driver;
    private HomePage _homePage;
    private RegistrationPage _registrationPage;
    private AppContainer _appContainer;
    private WebDriverWait _wait;

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
        _registrationPage.UserRegistration(
            Helpers.GenerateUniqueFullName(), Helpers.GenerateUniqueTestEmail(),
            Constants.TEST_PASSWORD, Constants.CURRENCY_EUR);
        _appContainer.ClickUserMenu();
        _appContainer.ClickLogoutButton();
        _registrationPage.ClickLoginSwitchButton();
    }

    [TearDown]
    public void Teardown()
    {
        _driver.Quit();
    }

    [Test]
    public void CheckLogin()
    {
        UserModel localStorage = _homePage.GetLastUserDataFromLocalStorage();
        string testEmail = localStorage.Email;
        string password = Constants.TEST_PASSWORD;

        _homePage.UserLogin(testEmail, password);

        Assert.That(_appContainer.IsAppContainerPageVisible(), Is.True, $"AppContainer page was not displayed after registration.");
    }
}
