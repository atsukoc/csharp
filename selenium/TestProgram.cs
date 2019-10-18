using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

public class TestProgram
{
    private IWebDriver driver;
    private string website = "https://www.aprilpricecoaching.com/";

    [SetUp]
    public void SetupTest()
    {
        driver = new ChromeDriver("/Users/bailey/drivers");
        driver.Url = website; // This command is to open a specific URL
    }

    [TearDown]
    public void TeardownTest()
    {
        driver.Quit();
    }

    
    [Test, Order(1)]
    public void testTitle()
    {   
        string expectedTitle = "April Price Coaching";
        string actualTitle = driver.Title;
        Assert.AreEqual(expectedTitle, actualTitle);
    }

    [Test, Order(2)]
    public void testPageSource()
    {
        String pageSource = driver.PageSource;
        Assert.AreEqual(pageSource, "hello world");
    }
}