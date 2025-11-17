using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace FinMore_Automation.Pages;

public class AppContainer : BasePage
{
    private static readonly By AppContainerFormPage = TestId("app-container");

protected WebDriverWait _wait;

    public AppContainer(IWebDriver driver, WebDriverWait wait) : base(driver)
    {
        _driver = driver;
        _wait = wait;
    }

    public bool IsAppContainerPageVisible()
    {
        return IsVisible(AppContainerFormPage);
    }
}
