using QAACourses.Pages;
using OpenQA.Selenium;
using NUnit.Framework;
using QAACourses.Drivers;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace QAACourses.Tests;

public class HomPageTests
{
    private IWebDriver _driver;
    private HomePage _homePage;
    private BasePage _basePage;

    private WebDriverWait _wait;

    private const string _url = "https://theconnectedshop.uk";
    private readonly By SearchInput = By.CssSelector("input[type='search']");


    [SetUp]
    public void Setup()
    {
        _driver = DriveFactory.CreateDriver();
        _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        _homePage = new HomePage(_driver);
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
        // Assert.That(_driver.Url.StartsWith(url + "/"), Is.True);
        Assert.IsTrue(_homePage.GetUrl().StartsWith(_url + "/"), "Url is incorrect");
    }

    [Test]
    public void CheckTitle()
    {
        _wait.Until(d => d.Title.Length > 0);

        string actualTitle = _homePage.GetTitle();
        Console.WriteLine("Title сторінки: " + actualTitle);

        string expectedTitle = "The Connected Shop - Smart Locks, Smart Sensors, Smart Home & Office";
        Assert.AreEqual(expectedTitle, actualTitle, "Title сторінки не збігається");
    }

    [Test]
    public void CheckHeadLogo()
    {
        string actualAlt = _homePage.GetLogoAlt();
        string expectedAlt = "The Connected Shop";
        string actualSrc = _homePage.GetLogoSrc();


        Assert.AreEqual(expectedAlt, actualAlt, "Alt-текст логотипа не збігається");
        Assert.That(actualSrc, Does.Contain("The_Connected_Shop_Logo"), "Src логотипа не містить очікуваний файл");
    }

    [Test]
    public void CheckLogoLink()
    {
        string currentUrl = _driver.Url;
        Console.WriteLine("URL перед кліком: " + currentUrl);//TestContext, Logger

        _homePage.ClickLogo();

        _wait.Until(d => d.Url.StartsWith(_url));
        string afterUrl = _driver.Url.TrimEnd('/');
        Console.WriteLine("URL після кліку: " + afterUrl);
        Assert.AreEqual(afterUrl, _url.TrimEnd('/'), "Посилання логотипа не веде на головну сторінку");
    }

    [Test]
    public void CheckLocalizationForm()
    {
        var localizationSectionElement = _homePage.GetLocalizationButton();

        string actualAriaControls = localizationSectionElement.GetAttribute("aria-controls");
        string expectedAriaControls = "HeaderCountryList";
        bool actualAriaExpanded = bool.Parse(localizationSectionElement.GetAttribute("aria-expanded"));
        bool expectedAriaExpanded = false;

        Assert.AreEqual(expectedAriaControls, actualAriaControls, $"Aria-controls element in HeaderCountryForm is {actualAriaControls} but was {expectedAriaControls}");
        Assert.AreEqual(expectedAriaExpanded, actualAriaExpanded, $"Arial expanded element in HeaderCountryForm is {actualAriaExpanded} but was {expectedAriaExpanded}");
    }

    [Test]
    public void CheckLocalizationFormOnClick()
    {
        var localizationSectionElement = _homePage.GetLocalizationButton();

        _homePage.ClickLocalization();

        bool actualAriaExpanded = bool.Parse(localizationSectionElement.GetAttribute("aria-expanded"));
        bool expectedAriaExpanded = true;

        Assert.AreEqual(expectedAriaExpanded, actualAriaExpanded, $"Arial expanded element in HeaderCountryForm is {actualAriaExpanded} but was {expectedAriaExpanded}");
    }

    [Test]
    public void CheckLanguageForm()
    {
        var languageSectionElement = _homePage.GetLanguageButton();

        string actualAriaControls = languageSectionElement.GetAttribute("aria-controls");
        string expectedAriaControls = "HeaderLanguageList";
        bool actualAriaExpanded = bool.Parse(languageSectionElement.GetAttribute("aria-expanded"));
        bool expectedAriaExpanded = false;

        Assert.AreEqual(expectedAriaControls, actualAriaControls, $"Aria-controls element in HeaderLanguageForm is {actualAriaControls} but was {expectedAriaControls}");
        Assert.AreEqual(expectedAriaExpanded, actualAriaExpanded, $"Arial expanded element in HeaderLanguageForm is {actualAriaExpanded} but was {expectedAriaExpanded}");
    }

    [Test]
    public void CheckLanguageFormOnClick()
    {
        var languageSectionElement = _homePage.GetLanguageButton();

        _homePage.ClickLanguage();

        bool actualAriaExpanded = bool.Parse(languageSectionElement.GetAttribute("aria-expanded"));
        bool expectedAriaExpanded = true;

        Assert.AreEqual(expectedAriaExpanded, actualAriaExpanded, $"Arial expanded element in HeaderLanguageForm is {actualAriaExpanded} but was {expectedAriaExpanded}");
    }

    [Test]
    public void CheckSearchForm()
    {
        var searchSection = _homePage.GetSearchInput();

        string actualType = searchSection.GetAttribute("type");
        string expectedType = "search";
        bool actualAriaExpanded = bool.Parse(searchSection.GetAttribute("aria-expanded"));
        bool expectedAriaExpanded = false;
        string actuaAriaHashPopUp = searchSection.GetAttribute("aria-haspopup");
        string expectedAriaHashPopUp = "listbox";

        Assert.AreEqual(expectedType, actualType, $"Aria-controls element in SearchForm is {actualType} but was {expectedType}");
        Assert.AreEqual(expectedAriaExpanded, actualAriaExpanded, $"Arial expanded element in SearchForm is {actualAriaExpanded} but was {expectedAriaExpanded}");
        Assert.AreEqual(expectedAriaHashPopUp, actuaAriaHashPopUp, $"Aria-controls element in SearchForm is {actuaAriaHashPopUp} but was {expectedAriaHashPopUp}");
    }

    [Test]
    public void CheckSearchFormOnClickListOpen()
    {
        var searchSection = _homePage.GetSearchInput();

        _homePage.SearchText("smart");

        bool actualAriaExpanded = bool.Parse(searchSection.GetAttribute("aria-expanded"));
        bool expectedAriaExpanded = true;
       // bool expectedIsSuggestionsListVisible = true;

        Assert.AreEqual(expectedAriaExpanded, actualAriaExpanded, $"Arial expanded element in SearchForm is {actualAriaExpanded} but was {expectedAriaExpanded}");
      //  Assert.AreEqual(expectedIsSuggestionsListVisible, _homePage.IsSuggestionsListVisible(), "Suggestions list was not displayed");

        Console.WriteLine("CheckSearchFormOnClickListOpen test passed");
    }

    [Test]
    public void CheckCartForm()
    {
        var cartSection = _homePage.GetCartIcon();

        string actualRole = cartSection.GetAttribute("role");
        string expectedRole = "button";
        string actuaAriaHashPopUp = cartSection.GetAttribute("aria-haspopup");
        string expectedAriaHashPopUp = "dialog";

        Assert.AreEqual(expectedRole, actualRole, $"Aria-controls element in SearchForm is {actualRole} but was {expectedRole}");
        Assert.AreEqual(expectedAriaHashPopUp, actuaAriaHashPopUp, $"Aria-controls element in SearchForm is {actuaAriaHashPopUp} but was {expectedAriaHashPopUp}");
    }

    #region Private methods
    /*
            protected void SendKeys(By locator, string text)
        {
            TestContext.WriteLine($"Введення тексту '{text}' у поле: {locator}");
            try
            {
                var input = _basePage.WaitForElement(locator);
                input.Clear();
                input.SendKeys(text);
                TestContext.WriteLine($"Текст введено успішно: '{text}'");
            }
            catch (Exception ex)
            {
                TestContext.WriteLine($"Помилка при введенні тексту '{text}': {ex.Message}");
                throw;
            }
        }
    */
    #endregion Private methods
}
