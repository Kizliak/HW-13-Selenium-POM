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
    public class SignIn
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

        [TestCase ("wrongEmail@gmail.com", "wrongPassword123")]
        public void LoginUsingWrongCredentials(string login, string passwrod)
        {
            var signInPage = new SignInPage(_webDriver);
            signInPage.GoToSingInPage()
                .InputEmailField(login)
                .InputPasswordField(passwrod)
                .ClickLoginButton();
            var actualResultMessage = signInPage.GetErrorMessage();

            Assert.AreEqual(expected:"Please enter a correct email and password.", actualResultMessage);
        }

        [TestCase("sacbdwplb@supere.ml", "1Asacbdwplb@supere.ml", "fdsf")]
        public void LoginUsingRegisteredCredentials(string login, string passwrod, string name)
        {
            var signInPage = new SignInPage(_webDriver);
            signInPage.GoToSingInPage()
                .InputEmailField(login)
                .InputPasswordField(passwrod)
                .ClickLoginButton();

            var registeredPage = new LoggedInPage(_webDriver);
            bool nameDisplayOnPage = registeredPage.CheckIfNameDisplayOnPage(name);

            Assert.AreEqual(expected: "https://newbookmodels.com/explore", actual: registeredPage.GetCurrentUrl());
            Assert.AreEqual(expected: true, actual: nameDisplayOnPage);
        }

        [TearDown]
        public void TearDown()
        {
            _webDriver.Dispose();
            _webDriver.Quit();
        }
    }
}