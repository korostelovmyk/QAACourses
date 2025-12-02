using System;
using OpenQA.Selenium;
using FinMore_Automation.Models;
using Newtonsoft.Json;

namespace FinMore_Automation.Framework;

public class Helpers
{
    public static string GenerateUniqueTestEmail(string prefix = "test_", string domain = "test.com")
    {
        string uniquePart = Guid.NewGuid().ToString("N").Substring(0, 8);
        return $"{prefix}.{uniquePart}@{domain}";
    }

    public static string GenerateUniqueFullName(string prefix = "qa_")
    {
        const string letters = "abcdefghijklmnopqrstuvwxyz";

        string uniqueName = new string(
            Enumerable.Range(0, 6)
                      .Select(_ => letters[Random.Shared.Next(letters.Length)])
                      .ToArray());

        string uniqueSurname = new string(
            Enumerable.Range(0, 10)
                      .Select(_ => letters[Random.Shared.Next(letters.Length)])
                      .ToArray());
                      
        return $"{prefix}{uniqueName} {uniqueSurname}";
    }

    public static string GetItemFromLocalStorage(IWebDriver driver, string key)
    {
        return (string)((IJavaScriptExecutor)driver).ExecuteScript("return window.localStorage.getItem(arguments[0]);", key);
    }

}
