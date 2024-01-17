using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectChrome.PageObjects
{
    internal class CartPage : BasePageObject
    {
        private IWebElement priceProduct => driver.FindElement(By.XPath("//div[@id='o1basket']//td[@class='c5']"));
        private IWebElement nameProduct => driver.FindElement(By.XPath("//div[@id='o1basket']//a[@class='mainItem']"));
        private IWebElement countPlus => driver.FindElement(By.ClassName("countPlus"));
        
        private WebDriverWait wait;


        public CartPage(IWebDriver driver) : base(driver)
        {
            wait = new WebDriverWait(driver, new TimeSpan(WAIT_TIMEOUT));
        }
        public CartPage AddCount()
        {
            wait.Until(d => countPlus.Displayed);
            countPlus.Click();
            wait.Until(d => !driver.FindElement(By.XPath("//div[@id='o1basket']//td[@class='c4']")).Equals(""));
            return this;
        }
        public string GetName()
        {
            string nameProductInCart = nameProduct.Text;
            nameProductInCart = nameProductInCart.Replace("Televize ", "");
            Console.WriteLine(nameProductInCart);
            return nameProductInCart;
        }
        public int GetPrice()
        {
            string priceProductInCart = priceProduct.Text;
            priceProductInCart=priceProductInCart.Replace("Kč", "").Replace(" ", "").Trim();
            Console.WriteLine(priceProductInCart);
            int priceProductInCartNumber = int.Parse(priceProductInCart);
            return priceProductInCartNumber;
        }

    }
}

         
