using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectChrome.PageObjects
{
    internal class TelevisionPage : BasePageObject
    {
        private IWebElement sectionCheapest => driver.FindElement(By.Id("ui-id-4"));//5 je nedražší televize
        private IWebElement priceProduct => driver.FindElement(By.XPath("//div[@id='boxes']//span[@class='price-box__price-text']"));
        private IWebElement nameProduct => driver.FindElement(By.CssSelector("#boxes .fb a"));////div[@id='boxes']//div[@class='fb']/a"));//#boxes .fb a
        private IWebElement intoCart => driver.FindElement(By.ClassName("btnk1"));//"//div[@id='boxes']//a[@class='btnk1']"
        private IWebElement unBoxedProduct => driver.FindElement(By.XPath("//div[@id='alzaDialog']//span[@class='btnx normal green']"));//gray
        private IWebElement chat => driver.FindElement(By.Id("chat-open-button"));
        private IWebElement iconCart => driver.FindElement(By.XPath("//a[@data-testid='headerBasketIcon']"));
        private WebDriverWait wait;

        public TelevisionPage(IWebDriver driver) : base(driver)
        {
            wait = new WebDriverWait(driver, new TimeSpan(WAIT_TIMEOUT));
        }
        public TelevisionPage NavigatedCheapest()
        {
            wait.Until(d => sectionCheapest.Displayed);
            sectionCheapest.Click();
            return this;
        }
        public TelevisionPage AddCart()
        {
            wait.Until(d => !driver.PageSource.Contains("//a[@data-testid='headerBasketIcon']"));//*[@id='page']/div[1]/div/div/div[1]/div/header/div[4]/a/span"));
            intoCart.Click();
            return this;
        }
        public TelevisionPage ClickUnboxProduct()
        {
            unBoxedProduct.Click();
            wait.Until(d => !driver.PageSource.Contains("//div[@id='alzaDialog']//span[@class='btnx normal green']"));
            return this;
        }
        public TelevisionPage ClickChat()
        {
            chat.Click();
            return this;
        }
        public TelevisionPage NavigateCart()
        {
            wait.Until(d => driver.FindElement(By.XPath("//a[@data-testid='headerBasketIcon']//span")).Text.Equals("1"));
            iconCart.Click();
            return this;
        }
        public string GetName()
        {
            string nameProductInList = nameProduct.Text;
            Console.WriteLine(nameProductInList);
            return nameProductInList;
        }
        public int GetPrice()
        {
            wait.Until(d => driver.FindElement(By.ClassName("circle-loader-container"))).GetAttribute("style").Equals("display:none");
            string priceProductInList = priceProduct.Text;
            priceProductInList = priceProductInList.Replace("Kč", "").Replace(" ", "").Replace(",-","").Trim();
            Console.WriteLine(priceProductInList);
            int priceProductInListNumber = int.Parse(priceProductInList);
            return priceProductInListNumber;
        }
    }

}
