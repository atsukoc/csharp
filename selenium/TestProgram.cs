using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;


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

    //[Ignore("This is how to skip the test")]
    [Test, Order(2)]
    public void testPrimaryNavigation()
    {
        String menu1 = "Platform";
        String menu2 = "Solutions";
        String menu3 = "Customers";
        String menu4 = "Partners";
        String menu5 = "Resources";
        String menu6 = "Get Paid to Test";
        IList<string> xpaths = new List<string>();

        // create XPaths for each item in navigation
        for(int i = 1; i <= 6; i++)
        {
            string xPath = String.Format("/html/body/div[4]/div[1]/header/div/nav[1]/ul/li[{0}]/a", i);
            xpaths.Add(xPath);
        }
    
        // verify the name of each navigation item 
        IList<string> navigationMenuItems = new List<string>();
        
        foreach(string path in xpaths)
        {
            string text = driver.FindElement(By.XPath(path)).Text;
            navigationMenuItems.Add(text);
        }

        Assert.True(navigationMenuItems.Count == 6);
        Assert.AreEqual(menu1, navigationMenuItems[0]);
        Assert.AreEqual(menu2, navigationMenuItems[1]);
        Assert.AreEqual(menu3, navigationMenuItems[2]);
        Assert.AreEqual(menu4, navigationMenuItems[3]);
        Assert.AreEqual(menu5, navigationMenuItems[4]);
        Assert.AreEqual(menu6, navigationMenuItems[5]);

        // verify hovering each navigation item
        Actions action = new Actions(driver);

        var platform = driver.FindElement(By.XPath(xpaths[0]));
        action.MoveToElement(platform).Perform();

    }

    [Test, Order(3)]
    public void hoverNavigation()
    {
        // First, find Platform menu in navigation
        var platformMenu = driver.FindElement(By.XPath("/html/body/div[4]/div[1]/header/div/nav[1]/ul/li[1]/a"));
        
        // Hover action
        Actions action = new Actions(driver);
        action.MoveToElement(platformMenu).Perform();

        // Verify the secondary navigation is shown 
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
        var webElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[4]/div[1]/header/div/nav[1]/ul/li[1]/nav/div/div[2]/ul")));
        Assert.IsNotNull(webElement);


    }



}