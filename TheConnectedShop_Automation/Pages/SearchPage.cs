using System;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using NUnit.Framework;

namespace QAACourses.Pages;

public class SearchPage : BasePage
{
    private readonly By _searchInput = By.CssSelector("input.search__input--header#Search-In-Inline");//By.CssSelector("input[type='search'][id='Search-In-Inline']");
    private readonly By _suggestionsContainer = By.CssSelector("div.predictive-search__results");
    private readonly By _suggestionsList = By.CssSelector(".predictive-search__group.predictive-search__queries li");
    private readonly By _productList = By.CssSelector(".predictive-search__group--with-media ul.predictive-search__item-list li");
    private readonly By _collectionsList = By.CssSelector(".predictive-search__group--with-media:nth-of-type(2) ul li");
    private readonly By _pagesList = By.CssSelector(".predictive-search__pages ul li");
    private readonly By _viewAllButton = By.CssSelector(".predictive-search__button");


    public SearchPage(IWebDriver driver) : base(driver) { }

    public IWebElement GetSearchInput()
    {
        return Wait.Until(ExpectedConditions.ElementIsVisible(_searchInput));
    }

    public void SearchText(By cssSelector, string text)
    {
        SendKeys(cssSelector, text);
    }
    //TEMPOARY left
    // Check that new SearchText cover old flow
/*
    public void SearchText(string query)
        {
            var input = GetSearchInput();
            input.Clear();
            TestContext.WriteLine($"Введення запиту у пошук: {query}");
            input.SendKeys(query);
        }
    */
}

