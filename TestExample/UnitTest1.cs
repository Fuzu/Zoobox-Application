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
    public class UnitTest1
    {

        
        [Fact]
        public void TestWithChromeDriver()
        {
            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            {
                try
                {
                    driver.LoadPage(TimeSpan.FromSeconds(10), @"http://localhost:59461");
                }catch (Exception e)
                {

                }
            }
        }
    }
}
