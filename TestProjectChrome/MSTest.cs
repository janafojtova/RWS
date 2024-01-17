using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using TestProjectChrome.PageObjects;

namespace TestProjectChrome
{
    [TestClass]
    public class MSTest
    {
        private const int TIMEOUT = 10;
        [TestMethod]
        public void RunSeleniumTest()
        {
            var driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.alza.cz/");
            driver.Manage().Timeouts().ImplicitWait = new TimeSpan(TIMEOUT);
            Console.WriteLine("I am on Alza.:-)");

            AlzaMainPage alzaMainPage = new AlzaMainPage(driver);
            TelevisionPage televisionPage = new TelevisionPage(driver);
            CartPage cartPage = new CartPage(driver);
            
            alzaMainPage.AcceptedCookies();         
            
            //sekce TV,foto,audio-video
            alzaMainPage.NavigateSectionTV();  

            //sekce televize
            alzaMainPage.NavigateSectionTelevision();              

            //vyhledání nejlevnìjší televize
            televisionPage.NavigatedCheapest();
            NUnit.Framework.Assert.AreEqual("https://www.alza.cz/levne-televize/18849604.htm", driver.Url, "It is not same web site.");

            //získání ceny ze seznamu televizí
            int priceProductInList = televisionPage.GetPrice();         
            string nameProductInList = televisionPage.GetName();
            
            //pøidání do košíku
            televisionPage.AddCart();
            try
            {
                televisionPage.ClickUnboxProduct();                
            } 
            catch{
                Console.WriteLine("There is no option to choose between new or unboxed.");
            }

            //ovìøení zda vyskakuje chat
            try
            {
                televisionPage.ClickChat();           
            }
            catch
            {
                Console.WriteLine("The chat is not popping up.");
            }

            //jdu do košíku
            televisionPage.NavigateCart(); 
            
            //ovìøení, že se tam nachází nejlevnìší televize podle ceny a podle názvu
            int priceOneTelevision = cartPage.GetPrice();
            string nameProduct = cartPage.GetName();

            NUnit.Framework.Assert.AreEqual(priceProductInList, priceOneTelevision, "The price of the product and the price in the basket are not the same.");
            NUnit.Framework.Assert.AreEqual(nameProductInList, nameProduct, "The name of the product and the name in the basket are not the same.");
          
            //navýšení poètu televizí na 2
            cartPage.AddCount();
            int priceTwoTelevision = cartPage.GetPrice();
            Console.WriteLine(priceTwoTelevision);

            //ovìøení, že se navýšení podaøilo
            NUnit.Framework.Assert.AreEqual(priceOneTelevision * 2, priceTwoTelevision, "It is not possible to increase the quantity of the item.Only one piece can be purchased.");
                        
            //zavøi aplikaci
            driver.Quit();
        }
    }
}