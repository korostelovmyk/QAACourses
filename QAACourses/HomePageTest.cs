using System.Reflection.Metadata;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace QAACourses
{
    public class HomePageTest
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;

        private const string _url = "https://theconnectedshop.uk";

        [SetUp]
        public void Setup()
        {
            var options = new ChromeOptions();
            // options.AddArgument("--headless");

            _driver = new ChromeDriver(options);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            _driver.Navigate().GoToUrl(_url);


            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        [TearDown]
        public void Teardown()
        {
            _driver.Dispose();
        }

        [Test]
        public void OpenSite()
        {
            // Assert.That(_driver.Url.StartsWith(url + "/"), Is.True);
            Assert.That(_driver.Url.StartsWith(_url + "/"), Is.True);
        }

        [Test]
        public void CheckTitle()
        {
            _wait.Until(d => d.Title.Length > 0);

            string actualTitle = _driver.Title;
            Console.WriteLine("Title сторінки: " + actualTitle);

            string expectedTitle = "The Connected Shop - Smart Locks, Smart Sensors, Smart Home & Office";
            Assert.AreEqual(expectedTitle, actualTitle, "Title сторінки не збігається");
        }

        [Test]
        public void CheckHeadLogo()
        {
            //string headerLogo = "header__heading-link";
            var logoElement = _wait.Until(ExpectedConditions.ElementExists(
              By.CssSelector("img.header__heading-logo")));

            string actualAlt = logoElement.GetAttribute("alt");
            string expectedAlt = "The Connected Shop";
            string actualSrc = logoElement.GetAttribute("src");


            Assert.AreEqual(expectedAlt, actualAlt, "Alt-текст логотипа не збігається");
            Assert.That(actualSrc, Does.Contain("The_Connected_Shop_Logo"), "Src логотипа не містить очікуваний файл");
        }

        [Test]
        public void CheckLogoLink()
        {
            var logoLink = _wait.Until(ExpectedConditions.ElementToBeClickable(
                By.CssSelector("a.header__heading-link")));

            string currentUrl = _driver.Url;
            Console.WriteLine("URL перед кліком: " + currentUrl);

            logoLink.Click();

            _wait.Until(d => d.Url.StartsWith(_url));
            string afterUrl = _driver.Url;
            Console.WriteLine("URL після кліку: " + afterUrl);
            Assert.That(afterUrl, Is.EqualTo(_url + "/"), "Посилання логотипа не веде на головну сторінку");
        }

        [Test]
        public void ChecklocalizationForm()
        {
            var localizationSection = _wait.Until(ExpectedConditions.ElementExists(
                By.Id("HeaderCountryForm")));

            var localizationSectionElement = localizationSection.FindElement(By.CssSelector("summary.disclosure__button"));


            string actualAriaControls = localizationSectionElement.GetAttribute("aria-controls");
            string expectedAriaControls = "HeaderCountryList";
            bool actualAriaExpanded = bool.Parse(localizationSectionElement.GetAttribute("aria-expanded"));
            bool expectedAriaExpanded = false;


            Assert.AreEqual(expectedAriaControls, actualAriaControls, $"Aria-controls element in HeaderCountryForm is {actualAriaControls} but was {expectedAriaControls}");
            Assert.AreEqual(expectedAriaExpanded, actualAriaExpanded, $"Arial expanded element in HeaderCountryForm is {actualAriaExpanded} but was {expectedAriaExpanded}");
        }

        [Test]
        public void ChecklocalizationFormOnClick()
        {
            var localizationSection = _wait.Until(ExpectedConditions.ElementToBeClickable(
                By.Id("HeaderCountryForm")));
            var localizationSectionElement = localizationSection.FindElement(By.CssSelector("summary.disclosure__button"));

            localizationSection.Click();

            bool actualAriaExpanded = bool.Parse(localizationSectionElement.GetAttribute("aria-expanded"));
            bool expectedAriaExpanded = true;

            Assert.AreEqual(expectedAriaExpanded, actualAriaExpanded, $"Arial expanded element in HeaderCountryForm is {actualAriaExpanded} but was {expectedAriaExpanded}");
        }

        [Test]
        public void CheckLanguageForm()
        {
            var languageSection = _wait.Until(ExpectedConditions.ElementExists(
                By.Id("HeaderLanguageForm")));

            var languageSectionElement = languageSection.FindElement(By.CssSelector("summary.disclosure__button"));

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
            var languageSection = _wait.Until(ExpectedConditions.ElementToBeClickable(
                By.Id("HeaderLanguageForm")));
            var languageSectionElement = languageSection.FindElement(By.CssSelector("summary.disclosure__button"));

            languageSection.Click();

            bool actualAriaExpanded = bool.Parse(languageSectionElement.GetAttribute("aria-expanded"));
            bool expectedAriaExpanded = true;

            Assert.AreEqual(expectedAriaExpanded, actualAriaExpanded, $"Arial expanded element in HeaderLanguageForm is {actualAriaExpanded} but was {expectedAriaExpanded}");
        }

        [Test]
        public void CheckSearchForm()
        {
            var searchSection = _wait.Until(ExpectedConditions.ElementExists(
                By.Id("Search-In-Inline")));

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
            var searchSection = _wait.Until(ExpectedConditions.ElementExists(
                By.Id("Search-In-Inline")));
            searchSection.Clear();
            searchSection.SendKeys("3");

            bool actualAriaExpanded = bool.Parse(searchSection.GetAttribute("aria-expanded"));
            bool expectedAriaExpanded = true;

            Assert.AreEqual(expectedAriaExpanded, actualAriaExpanded, $"Arial expanded element in SearchForm is {actualAriaExpanded} but was {expectedAriaExpanded}");
        }

        [Test]
        public void CheckCartForm()
        {
            var searchSection = _wait.Until(ExpectedConditions.ElementExists(
                By.Id("cart-icon-bubble")));

            string actualRole = searchSection.GetAttribute("role");
            string expectedRole = "search";
            bool actualAriaExpanded = bool.Parse(searchSection.GetAttribute("aria-expanded"));
            bool expectedAriaExpanded = false;
            string actuaAriaHashPopUp = searchSection.GetAttribute("aria-haspopup");
            string expectedAriaHashPopUp = "listbox";

            Assert.AreEqual(expectedRole, actualRole, $"Aria-controls element in SearchForm is {actualRole} but was {expectedRole}");
            Assert.AreEqual(expectedAriaExpanded, actualAriaExpanded, $"Arial expanded element in SearchForm is {actualAriaExpanded} but was {expectedAriaExpanded}");
            Assert.AreEqual(expectedAriaHashPopUp, actuaAriaHashPopUp, $"Aria-controls element in SearchForm is {actuaAriaHashPopUp} but was {expectedAriaHashPopUp}");
        }
    }
}