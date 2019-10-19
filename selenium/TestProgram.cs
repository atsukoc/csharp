using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;

public class TestUserTestingDotCom
{
    private IWebDriver driver;
    private string website = "https://www.usertesting.com/";

    [SetUp]
    public void SetupTest()
    {
        driver = new ChromeDriver("/Users/bailey/drivers/");      
        driver.Url = website;  
    }

    [TearDown]
    public void TeardownTest()
    {
        driver.Quit();
    }
    
    [Test, Order(1)]
    public void testTitle() 
    {   
        string expectedTitle = "Create A Better Customer Experience | UserTesting";
        string actualTitle = driver.Title;
        Assert.AreEqual(expectedTitle, actualTitle);
    }

    [Test, Order(2)]
    public void testPrimaryNavigation()
    {
        String menu1 = "Platform";
        String menu2 = "Solutions";
        String menu3 = "Customers";
        String menu4 = "Partners";
        String menu5 = "Resources";
        String menu6 = "Get Paid to Test";

        var navDict = new Dictionary<int, string>();
        for(int i = 1; i <= 6; i++)
        {
            string path = String.Format("/html/body/div[4]/div[1]/header/div/nav[1]/ul/li[{0}]/a", i);
            string text = driver.FindElement(By.XPath(path)).Text;
            navDict[i] = text;
        }

        Assert.True(navDict.Count == 6);
        Assert.AreEqual(menu1, navDict[1]);
        Assert.AreEqual(menu2, navDict[2]);
        Assert.AreEqual(menu3, navDict[3]);
        Assert.AreEqual(menu4, navDict[4]);
        Assert.AreEqual(menu5, navDict[5]);
        Assert.AreEqual(menu6, navDict[6]);

    }




}