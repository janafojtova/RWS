using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectChrome.PageObjects
{
    internal class AlzaMainPage : BasePageObject
    {
        private IWebElement acceptedCookies => driver.FindElement(By.ClassName("js-cookies-info-accept"));        
        private IWebElement sectionTV => driver.FindElement(By.Id("litp18852655"));
        private string televisionXPath = "//*[@id='ltp']/div[3]/div/div/div[1]/div[1]/a/span";
        private IWebElement television => driver.FindElement(By.XPath(televisionXPath));
        private WebDriverWait wait;

        public AlzaMainPage(IWebDriver driver) : base(driver)
        {
            wait = new WebDriverWait(driver, new TimeSpan(WAIT_TIMEOUT));
        }
        public AlzaMainPage AcceptedCookies()
        {
            acceptedCookies.Click();
            wait.Until(d => !driver.PageSource.Contains("js-cookies-info"));
            return this;
        }

        public AlzaMainPage NavigateSectionTV()
        {
            sectionTV.Click();       
            wait.Until(d=> driver.FindElement(By.XPath("//div[@class='float-block']//a[text()='Televize']/../../../..")).GetAttribute("style")).Contains("block");
            return this;
        }
        public AlzaMainPage NavigateSectionTelevision()
        {
            wait.Until(d => television.Text.Length > 0);
            string televisionElement = television.Text;
            string pokracovat = "ne";
            Console.WriteLine(televisionElement);
            for (int i = 0; i < 15; i++)
            {
                while (televisionElement == "Televize")
                {
                    driver.FindElement(By.XPath(televisionXPath)).Click();
                    pokracovat = "ano";
                    televisionElement = "nesmysl";
                }
                if (pokracovat == "ne")
                {
                    televisionXPath = televisionXPath.Replace("div[" + (i + 1) + "]/a/span", "div[" + (i + 2) + "]/a/span");
                    televisionElement = driver.FindElement(By.XPath(televisionXPath)).Text;
                }
            }
            wait.Until(d => !driver.PageSource.Contains(televisionXPath));
            return this;
        }                
    }
}
