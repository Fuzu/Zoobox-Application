using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Reflection;
using Xunit;

namespace TestExample
{
    [TestClass]
    public class UnitTest1
    {

        
        [TestMethod]
        public void TestWithChromeDriver()
        {
            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            {
                try
                {
                    driver.Navigate().GoToUrl("http://localhost:59461");

                    IWebElement email = driver.FindElement(By.Id("emailInput"));
                    email.SendKeys("Teste@test");

                    var body = driver.FindElement(By.TagName("body")); // then you find the body

                }
                catch (Exception e)
                {

                }
            }
        }
    }
}
