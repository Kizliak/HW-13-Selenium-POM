using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using HW13.POM;

namespace HW13
{
    public class Tests
    {
        private IWebDriver _webDriver;

        [SetUp]
        public void Setup()
        {
            _webDriver = new ChromeDriver();
            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            _webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
            _webDriver.Manage().Window.Maximize();
        }

        [Test]
        public void LoginWithWrongCredentials()
        {
            var signInPage = new SignInPage(_webDriver);
            signInPage.GoToSingInPage()
                .InputEmailField("wrongEmail@gmail.com")
                .InputPasswordField("wrongPassword123")
                .ClickLoginButton();
            var actualResultMessage = signInPage.GetErrorMessage();

            Assert.AreEqual(expected:"Please enter a correct email and password.", actualResultMessage);
        }

        [TearDown]
        public void TearDown()
        {
            _webDriver.Dispose();
            _webDriver.Quit();
        }
    }
}