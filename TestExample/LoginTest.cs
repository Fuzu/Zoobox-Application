using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Reflection;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace TestZoobox
{
    
    [TestClass]
    public class LoginTest
    {
        public string Site = "https://localhost:44381";

        [TestMethod]
        public void Login()
        {

            //FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(@"C:\UnitTests");
            //service.FirefoxBinaryPath = @"C:\Program Files\Mozilla Firefox\firefox.exe";
            //using (var driver = new FirefoxDriver(service))
            //{
            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            {
                try
                {

                    IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

                    driver.Navigate().GoToUrl(Site);

                    IWebElement email = driver.FindElement(By.Id("emailInput"));
                    email.SendKeys("teste@teste");

                    IWebElement password = driver.FindElement(By.Id("passwordInput"));
                    password.SendKeys("Teste1_");
                    var body = driver.FindElement(By.TagName("body")); // then you find the body

                    var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));

                    var button = driver.FindElement(By.Id("submitButton"));

                    //This will scroll the page till the element is found		
                    js.ExecuteScript("arguments[0].scrollIntoView();", button);

                    Actions actions = new Actions(driver);
                    actions.MoveToElement(button);
                    actions.Perform();

                    button.Submit();


                }
                catch (Exception e)
                {

                }
            }
        }


        [TestMethod]
        public void RecuperarPasswordTest()
        {
            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            {
                try
                {
                    driver.Navigate().GoToUrl(Site);

                    var button = driver.FindElement(By.Id("forgotPassword"));
                    button.Click();

                    String Url = driver.Url;

                    Assert.AreEqual(Url, Site+"/Identity/Account/ForgotPassword");

                    IWebElement email = driver.FindElement(By.Id("email"));
                    email.SendKeys("teste@teste");

                    var buttonRecover = driver.FindElement(By.Id("submitButton"));

                    buttonRecover.Click();
                   

                    Assert.AreEqual(Url, Site+"/Identity/Account/ForgotPasswordConfirmation");



                }
                catch (Exception e)
                {

                }
            }
        }

        public void CreateAnimalTest()
        {

            
        }
        public void UpdateAnimalTest()
        {


        }

        public void ListAnimalsTest()
        {


        }

        public void AddRace()
        {


        }

        public void AddSpecie()
        {


        }
    }
}
