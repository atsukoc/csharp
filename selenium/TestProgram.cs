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
    
        /* verify the name of each item in the navigation */

        IList<string> navigationMenuItems = new List<string>();
        
        foreach(string path in xpaths)
        {
            try
            {
                string text = driver.FindElement(By.XPath(path)).Text;
                navigationMenuItems.Add(text);
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        Assert.True(navigationMenuItems.Count == 6);
        Assert.AreEqual(menu1, navigationMenuItems[0]);
        Assert.AreEqual(menu2, navigationMenuItems[1]);
        Assert.AreEqual(menu3, navigationMenuItems[2]);
        Assert.AreEqual(menu4, navigationMenuItems[3]);
        Assert.AreEqual(menu5, navigationMenuItems[4]);
        Assert.AreEqual(menu6, navigationMenuItems[5]);

        /* Verify hovering action over each item in the navigation */

        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
        Actions action = new Actions(driver);

        IWebElement platform = driver.FindElement(By.XPath(xpaths[0]));
        IWebElement solutions = driver.FindElement(By.XPath(xpaths[1]));
        IWebElement customers = driver.FindElement(By.XPath(xpaths[2]));
        IWebElement partners = driver.FindElement(By.XPath(xpaths[3]));
        IWebElement resources = driver.FindElement(By.XPath(xpaths[4]));
        IWebElement getPaidToTest = driver.FindElement(By.XPath(xpaths[5]));

        action.MoveToElement(platform).Perform();
        var platformBox = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("div.platform-nav")));
        Assert.IsNotNull(platformBox);

        action.MoveToElement(solutions).Perform();
        var solutionsBox = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("div.solutions-nav")));
        Assert.IsNotNull(solutionsBox);
        
        // customers menu has no dropdown menu, so it should throw a timeout exception
        action.MoveToElement(customers).Perform();    
        try
        {
            var customersBox = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("div.customers-nav")));
            Assert.Fail();
        }catch(Exception e)
        {
            WebDriverTimeoutException expected = new WebDriverTimeoutException();
            Assert.AreEqual(expected.GetType(), e.GetType());
        }
    }

}