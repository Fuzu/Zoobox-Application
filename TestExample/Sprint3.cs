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
    
    
    public class Sprint3
    {
        public string Site = "https://localhost:53076";

        [TestMethod]
        public void CreateTarefaTest()
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

                 driver.Navigate().GoToUrl(Site + "/jobs/Create");
     
                    String Url = driver.Url;

                    Assert.AreEqual(Url, Site + "/jobs/Create");

                    IWebElement abbreviation = driver.FindElement(By.Id("Abbreviation"));
                    abbreviation.SendKeys("Teste Abre");
                    IWebElement description = driver.FindElement(By.Id("Description"));
                    description.SendKeys("Tarefa Teste");


                    IWebElement BeginDay = driver.FindElement(By.Id("BeginDay"));
                    BeginDay.SendKeys("03/01/2019");

                    IWebElement EndDay = driver.FindElement(By.Id("EndDay"));
                    EndDay.SendKeys("03/11/2019");
                    IWebElement State = driver.FindElement(By.Id("State"));
                    State.SendKeys("incicio");
                    IWebElement ApplicationUser = driver.FindElement(By.Id("ApplicationUser"));
                    ApplicationUser.SendKeys("1");
                    var buttonSubmit = driver.FindElement(By.Id("addJob"));
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1000);
                    buttonSubmit.Click();
                    Assert.AreEqual(Url, Site + "/jobs");



                }
                catch (Exception e)
                {

                }
            }
        }

        [TestMethod]
        public void UpdateTarefaTest()
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

                    driver.Navigate().GoToUrl(Site + "/jobs/Edit/1");

                    String Url = driver.Url;

                    Assert.AreEqual(Url, Site + "/jobs/Edit/1");


                    IWebElement abbreviation = driver.FindElement(By.Id("Abbreviation"));
                    abbreviation.SendKeys("Teste Abre");
                    IWebElement description = driver.FindElement(By.Id("Description"));
                    description.SendKeys("Tarefa Teste");


                    IWebElement BeginDay = driver.FindElement(By.Id("BeginDay"));
                    BeginDay.SendKeys("03/01/2019");

                    IWebElement EndDay = driver.FindElement(By.Id("EndDay"));
                    EndDay.SendKeys("03/11/2019");
                    IWebElement State = driver.FindElement(By.Id("State"));
                    State.SendKeys("incicio");
                    IWebElement ApplicationUser = driver.FindElement(By.Id("ApplicationUser"));
                    ApplicationUser.SendKeys("1");
                    var buttonSubmit = driver.FindElement(By.Id("addJob"));
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1000);
                    buttonSubmit.Click();
                    Assert.AreEqual(Url, Site + "/jobs");

                }
                catch (Exception e)
                {

                }
            }

        }
        [TestMethod]
        public void ListTarefaTest()
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

                    driver.Navigate().GoToUrl(Site + "/jobs");

                    String Url = driver.Url;


                }
                catch (Exception e)
                {

                }
            }

        }
    }
}
