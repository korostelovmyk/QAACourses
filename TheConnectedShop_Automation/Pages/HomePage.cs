using OpenQA.Selenium;

namespace QAACourses.Pages;

public class HomePage : BasePage
{
    private const string Url = "https://theconnectedshop.uk";
    private readonly By LogoImg = By.CssSelector("img.header__heading-logo");
    private readonly By LogoLink = By.CssSelector("a.header__heading-link");
    private readonly By CountryForm = By.Id("HeaderCountryForm");
    private readonly By LanguageForm = By.Id("HeaderLanguageForm");
    private readonly By SearchInput = By.CssSelector("input[type='search']");
    private readonly By CartIcon = By.Id("cart-icon-bubble");
    private readonly By SuggestionsList = By.CssSelector(".predictive-search");

    public HomePage(IWebDriver driver) : base(driver) { }

    public void Open()
    {
        Driver.Navigate().GoToUrl(Url);
    }

    public string GetUrl()
    {
        return Driver.Url;
    }

    public string GetTitle()
    {
        return Driver.Title;
    }

    public string GetLogoAlt()
    {
        return GetAttribute(LogoImg, "alt");
    }
    public string GetLogoSrc() => GetAttribute(LogoImg, "src");
    public void ClickLogo() => Click(LogoLink);


    public IWebElement GetLocalizationButton()
    {
        return WaitForElement(By.CssSelector("#HeaderCountryForm summary.disclosure__button"));
    }

    public void ClickLocalization() => Click(CountryForm);


    public IWebElement GetLanguageButton()
    {
        return WaitForElement(By.CssSelector("#HeaderLanguageForm summary.disclosure__button"));
    }

    public void ClickLanguage() => Click(LanguageForm);

    public IWebElement GetSearchInput() => WaitForElement(SearchInput);

    public bool IsSuggestionsListVisible()
    {
        var element = WaitForElement(SuggestionsList);
        return element.Displayed;
    }

    public IWebElement GetCartIcon() => WaitForElement(CartIcon);
}
