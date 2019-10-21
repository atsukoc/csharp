using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
    
namespace TestHelpers
{
    public class Getters
    {
        static public IWebElement getMemuItemByCssSelector(string cssSelector, WebDriverWait wait)
        {
            var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(cssSelector)));
            return element;
        }

    };

}

