using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TestProjectChrome.PageObjects
{
    public abstract class BasePageObject
    {
        protected IWebDriver driver;
        protected const int WAIT_TIMEOUT = 10;

        public BasePageObject(IWebDriver driver)
        {
            this.driver = driver;
        }
        protected void WaitForObject(By identification)
        {
            WebDriverWait? wait = new WebDriverWait(driver, new TimeSpan(WAIT_TIMEOUT));
            wait.Until(condition =>
            {
                try
                {
                    IWebElement? elementToBeDisplayed = driver.FindElement(by: identification);
                    return elementToBeDisplayed.Displayed;
                }

                catch (StaleElementReferenceException)
                {
                    return false;
                }

                catch (NoSuchElementException)
                {
                    return false;
                }
            });
        }
    }
}
