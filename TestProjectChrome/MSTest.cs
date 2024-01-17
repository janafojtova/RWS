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

            //vyhled�n� nejlevn�j�� televize
            televisionPage.NavigatedCheapest();
            NUnit.Framework.Assert.AreEqual("https://www.alza.cz/levne-televize/18849604.htm", driver.Url, "It is not same web site.");

            //z�sk�n� ceny ze seznamu televiz�
            int priceProductInList = televisionPage.GetPrice();         
            string nameProductInList = televisionPage.GetName();
            
            //p�id�n� do ko��ku
            televisionPage.AddCart();
            try
            {
                televisionPage.ClickUnboxProduct();                
            } 
            catch{
                Console.WriteLine("There is no option to choose between new or unboxed.");
            }

            //ov��en� zda vyskakuje chat
            try
            {
                televisionPage.ClickChat();           
            }
            catch
            {
                Console.WriteLine("The chat is not popping up.");
            }

            //jdu do ko��ku
            televisionPage.NavigateCart(); 
            
            //ov��en�, �e se tam nach�z� nejlevn�� televize podle ceny a podle n�zvu
            int priceOneTelevision = cartPage.GetPrice();
            string nameProduct = cartPage.GetName();

            NUnit.Framework.Assert.AreEqual(priceProductInList, priceOneTelevision, "The price of the product and the price in the basket are not the same.");
            NUnit.Framework.Assert.AreEqual(nameProductInList, nameProduct, "The name of the product and the name in the basket are not the same.");
          
            //nav��en� po�tu televiz� na 2
            cartPage.AddCount();
            int priceTwoTelevision = cartPage.GetPrice();
            Console.WriteLine(priceTwoTelevision);

            //ov��en�, �e se nav��en� poda�ilo
            NUnit.Framework.Assert.AreEqual(priceOneTelevision * 2, priceTwoTelevision, "It is not possible to increase the quantity of the item.Only one piece can be purchased.");
                        
            //zav�i aplikaci
            driver.Quit();
        }
    }
}