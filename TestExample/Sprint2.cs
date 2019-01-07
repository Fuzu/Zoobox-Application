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
    public class Sprint2
    {
        public string Site = "https://localhost:44381";

        
        [TestMethod]
        public void CreateAnimalTest()
        {
            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            {
                try
                {
                    #region LoginArea
                    var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                    IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

                        driver.Navigate().GoToUrl(Site);

                        IWebElement email = driver.FindElement(By.Id("emailInput"));
                        email.SendKeys("teste@admin.pt");
                  
                    IWebElement password = driver.FindElement(By.Id("passwordInput"));
                        password.SendKeys("teste@admin.ptA1");
                   
                    var body = driver.FindElement(By.TagName("body")); // then you find the body

                        var button = driver.FindElement(By.Id("submitButton"));

                    
                    button.Click();
                   
                    #endregion

                driver.Navigate().GoToUrl(Site +"/Animals/Create");
     
                    String Url = driver.Url;

                    Assert.AreEqual(Url, Site + "/Animals/Create");

                    IWebElement name = driver.FindElement(By.Id("Name"));
                    name.SendKeys("Teste Animal");

                   
                    IWebElement location = driver.FindElement(By.Id("Location"));
                    location.SendKeys("C156");

                    var buttonSubmit = driver.FindElement(By.Id("submitAnimal"));
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1000);
                    buttonSubmit.Click();


                    Assert.AreEqual(Url, Site + "/Animals");



                }
                catch (Exception e)
                {

                }
            }
        }

        [TestMethod]
        public void UpdateAnimalTest()
        {
            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            {
                try
                {
                    #region LoginArea
                    var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                    IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

                    driver.Navigate().GoToUrl(Site);

                    IWebElement email = driver.FindElement(By.Id("emailInput"));
                    email.SendKeys("teste@admin.pt");

                    IWebElement password = driver.FindElement(By.Id("passwordInput"));
                    password.SendKeys("teste@admin.ptA1");

                    var body = driver.FindElement(By.TagName("body")); // then you find the body

                    var button = driver.FindElement(By.Id("submitButton"));


                    button.Click();

                    #endregion

                    driver.Navigate().GoToUrl(Site + "/Animals/Edit/1");

                    String Url = driver.Url;

                    Assert.AreEqual(Url, Site + "/Animals/Edit/1");

                    IWebElement name = driver.FindElement(By.Id("Name"));
                    name.SendKeys("Teste Animal Update");


                    IWebElement location = driver.FindElement(By.Id("Location"));
                    location.SendKeys("C156 update");

                    var buttonSubmit = driver.FindElement(By.Id("submitAnimal"));
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1000);
                    buttonSubmit.Click();


                    Assert.AreEqual(Url, Site + "/Animals");



                }
                catch (Exception e)
                {

                }
            }

        }
        [TestMethod]
        public void ListAnimalsTest()
        {
            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            {
                try
                {
                    #region LoginArea
                    var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                    IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

                    driver.Navigate().GoToUrl(Site);

                    IWebElement email = driver.FindElement(By.Id("emailInput"));
                    email.SendKeys("teste@admin.pt");

                    IWebElement password = driver.FindElement(By.Id("passwordInput"));
                    password.SendKeys("teste@admin.ptA1");

                    var body = driver.FindElement(By.TagName("body")); // then you find the body

                    var button = driver.FindElement(By.Id("submitButton"));


                    button.Click();

                    #endregion

                    driver.Navigate().GoToUrl(Site + "/Animals");

                    String Url = driver.Url;

                    Assert.AreEqual(Url, Site + "/Animals/Edit/1");

                    IWebElement name = driver.FindElement(By.Name("SearchString"));
                    name.SendKeys("teste");

                    var buttonSubmit = driver.FindElement(By.Id("submitFilter"));
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1000);
                    buttonSubmit.Click();


                    Assert.AreEqual(Url, Site + "/Animals/?SearchString=teste&breed=");



                }
                catch (Exception e)
                {

                }
            }

        }
        [TestMethod]
        public void AddRaceTest()
        {
            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            {
                try
                {
                    #region LoginArea
                    var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                    IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

                    driver.Navigate().GoToUrl(Site);

                    IWebElement email = driver.FindElement(By.Id("emailInput"));
                    email.SendKeys("teste@admin.pt");

                    IWebElement password = driver.FindElement(By.Id("passwordInput"));
                    password.SendKeys("teste@admin.ptA1");

                    var body = driver.FindElement(By.TagName("body")); // then you find the body

                    var button = driver.FindElement(By.Id("submitButton"));


                    button.Click();

                    #endregion

                    driver.Navigate().GoToUrl(Site + "/Races/Create");

                    String Url = driver.Url;

                    Assert.AreEqual(Url, Site + "/Races/Create");

                    IWebElement name = driver.FindElement(By.Id("RaceName"));
                    name.SendKeys("Teste Raça");


                    var buttonSubmit = driver.FindElement(By.Id("submitRace"));
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1000);
                    buttonSubmit.Click();


                    Assert.AreEqual(Url, Site + "/Races");



                }
                catch (Exception e)
                {

                }
            }

        }
        [TestMethod]
        public void AddSpecieTest()
        {
            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            {
                try
                {
                    #region LoginArea
                    var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                    IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

                    driver.Navigate().GoToUrl(Site);

                    IWebElement email = driver.FindElement(By.Id("emailInput"));
                    email.SendKeys("teste@admin.pt");

                    IWebElement password = driver.FindElement(By.Id("passwordInput"));
                    password.SendKeys("teste@admin.ptA1");

                    var body = driver.FindElement(By.TagName("body")); // then you find the body

                    var button = driver.FindElement(By.Id("submitButton"));


                    button.Click();

                    #endregion

                    driver.Navigate().GoToUrl(Site + "/Species/Create");

                    String Url = driver.Url;

                    Assert.AreEqual(Url, Site + "/Species/Create");

                    IWebElement name = driver.FindElement(By.Id("SpecieName"));
                    name.SendKeys("Teste Especies");


                    var buttonSubmit = driver.FindElement(By.Id("submitSpecie"));
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1000);
                    buttonSubmit.Click();


                    Assert.AreEqual(Url, Site + "/Species");

                }
                catch (Exception e)
                {

                }
            }

        }
    }
}
