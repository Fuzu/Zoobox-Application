using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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


        [TestMethod]
        public void Login()
        {
            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            {
                try
                {

                    IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

                    driver.Navigate().GoToUrl("http://localhost:59461");

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
                    driver.Navigate().GoToUrl("http://localhost:59461");

                    var button = driver.FindElement(By.Id("forgotPassword"));
                    button.Click();

                    String Url = driver.Url;

                    Assert.AreEqual(Url, "http://localhost:59461/Identity/Account/ForgotPassword");

                    IWebElement email = driver.FindElement(By.Id("email"));
                    email.SendKeys("teste@teste");

                    var buttonRecover = driver.FindElement(By.Id("submitButton"));

                    buttonRecover.Click();
                   

                    Assert.AreEqual(Url, " http://localhost:59461/Identity/Account/ForgotPasswordConfirmation");



                }
                catch (Exception e)
                {

                }
            }
        }
    }
}
