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
using HW13.Utils;

namespace HW13
{
    class SingUp
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
        public void SignUpValid()
        {
            var signUpPage = new SignUpPage(_webDriver);
            string password = Generators.GetRndPass();
            string name = Generators.GetRandName();
            signUpPage.GoToSignUpPage()
                .InputFirstName(name)
                .InputLastName(Generators.GetRandName())
                .InputEmail(Generators.GetRndEmail())
                .InputPassword(password)
                .InputPasswordConfirm(password)
                .InputMobile(Generators.GetRndPhone())
                .ClickNextButton();

            var signUpPage2 = new SignUpPage_2(_webDriver);
            signUpPage2.InputCompany(Generators.GetRandName())
                .InputCompanyUrl(Generators.GetRndCompanyUrl())
                .InputCompanyLocation(Generators.GetAddress())
                .ChooseIndustry(Generators.Randomchik.Next(0, 4))
                .ClickFinishtButton();

            var registeredPage = new LoggedInPage(_webDriver);
            bool actualResultMessage = registeredPage.CheckIfNameDisplayOnPage(name);

            Assert.AreEqual(expected: true, actual: actualResultMessage);
        }

        [TearDown]
        public void TearDown()
        {
            _webDriver.Dispose();
            _webDriver.Quit();
        }
    }
}
