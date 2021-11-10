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
        public void SignUpFullValid()
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
            bool nameDisplayOnPage = registeredPage.CheckIfNameDisplayOnPage(name);

            Assert.AreEqual(expected: "https://newbookmodels.com/explore", actual: registeredPage.GetCurrentUrl());
            Assert.AreEqual(expected: true, actual: nameDisplayOnPage);
        }

        [TestCase(" ", "wrhjkwehj", "fakejlfunajeb@laste.ml", "A1jlfunajeb@laste.ml", "A1jlfunajeb@laste.ml", "1634543545", "This field may not be blank.")]
        [TestCase("", "wrhjkwehj", "fakejlfunajeb@laste.ml", "A1jlfunajeb@laste.ml", "A1jlfunajeb@laste.ml", "1634543545", "Required")]
        public void FirstNameIsInvalid(string firstname, string secondname, string email, string password, string confirmPassword, string mobile, string errorText)
        {
            var signUpPage = new SignUpPage(_webDriver);
            signUpPage.GoToSignUpPage()
                .InputFirstName(firstname)
                .InputLastName(secondname)
                .InputEmail(email)
                .InputPassword(password)
                .InputPasswordConfirm(confirmPassword)
                .InputMobile(mobile)
                .ClickNextButton();

            Assert.AreEqual(expected: errorText, actual: signUpPage.CheckFirstNameErrorText());

            var signUpPage2 = new SignUpPage_2(_webDriver);
            Assert.AreEqual(expected: "https://newbookmodels.com/join", actual: signUpPage2.GetCurrentUrl());
        }

        [TestCase("dsfdsfdffs", " ", "fakejlfunajeb@laste.ml", "A1jlfunajeb@laste.ml", "A1jlfunajeb@laste.ml", "1634543545", "This field may not be blank.")]
        [TestCase("dsfdsfdffs", "", "fakejlfunajeb@laste.ml", "A1jlfunajeb@laste.ml", "A1jlfunajeb@laste.ml", "1634543545", "Required")]
        public void LastNameIsInvalid(string firstname, string secondname, string email, string password, string confirmPassword, string mobile, string errorText)
        {
            var signUpPage = new SignUpPage(_webDriver);
            signUpPage.GoToSignUpPage()
                .InputFirstName(firstname)
                .InputLastName(secondname)
                .InputEmail(email)
                .InputPassword(password)
                .InputPasswordConfirm(confirmPassword)
                .InputMobile(mobile)
                .ClickNextButton();

            Assert.AreEqual(expected: errorText, actual: signUpPage.CheckLastNameErrorText());

            var signUpPage2 = new SignUpPage_2(_webDriver);
            Assert.AreEqual(expected: "https://newbookmodels.com/join", actual: signUpPage2.GetCurrentUrl());
        }

        [TestCase("dsfdsfdffs", "wrhjkwehj", "", "A1jlfunajeb@laste.ml", "A1jlfunajeb@laste.ml", "1634543545", "Required")]
        [TestCase("dsfdsfdffs", "wrhjkwehj", " ", "A1jlfunajeb@laste.ml", "A1jlfunajeb@laste.ml", "1634543545", "Required")]
        [TestCase("dsfdsfdffs", "wrhjkwehj", "fakejlfunajeb", "A1jlfunajeb@laste.ml", "A1jlfunajeb@laste.ml", "1634543545", "Invalid Email")]
        [TestCase("dsfdsfdffs", "wrhjkwehj", "fakejlfunajeb@laste", "A1jlfunajeb@laste.ml", "A1jlfunajeb@laste.ml", "1634543545", "Invalid Email")]
        [TestCase("dsfdsfdffs", "wrhjkwehj", "!!!!@laste.ml", "A1jlfunajeb@laste.ml", "A1jlfunajeb@laste.ml", "1634543545", "Invalid Email")]
        [TestCase("dsfdsfdffs", "wrhjkwehj", "@laste", "A1jlfunajeb@laste.ml", "A1jlfunajeb@laste.ml", "1634543545", "Invalid Email")]
        [TestCase("dsfdsfdffs", "wrhjkwehj", "@laste.ml", "A1jlfunajeb@laste.ml", "A1jlfunajeb@laste.ml", "1634543545", "Invalid Email")]
        public void EmailIsInvalid(string firstname, string secondname, string email, string password, string confirmPassword, string mobile, string errorText)
        {
            var signUpPage = new SignUpPage(_webDriver);
            signUpPage.GoToSignUpPage()
                .InputFirstName(firstname)
                .InputLastName(secondname)
                .InputEmail(email)
                .InputPassword(password)
                .InputPasswordConfirm(confirmPassword)
                .InputMobile(mobile)
                .ClickNextButton();

            var signUpPage2 = new SignUpPage_2(_webDriver);
            Assert.AreEqual(expected: "https://newbookmodels.com/join", actual: signUpPage2.GetCurrentUrl());
            Assert.AreEqual(expected: errorText, actual: signUpPage.CheckEmailNameErrorText());
        }

        [TestCase("", "Invalid phone format")]
        [TestCase(" ", "Invalid phone format")]
        [TestCase("sadasdsad", "Invalid phone format")]
        [TestCase("163454354", "Invalid phone format")]
        [TestCase("0000000000", "Invalid phone format")]
        public void PhoneIsInvalid(string mobile, string errorText)
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
                .InputMobile(mobile)
                .ClickNextButton();

            var signUpPage2 = new SignUpPage_2(_webDriver);
            Assert.AreEqual(expected: "https://newbookmodels.com/join", actual: signUpPage2.GetCurrentUrl());
            Assert.AreEqual(expected: errorText, actual: signUpPage.CheckPhoneErrorText());
        }

        //[TestCase("A1jlfunajeb@laste.ml", "A1jlfunajeb@laste.ml", true)] //valid
        [TestCase("Ajlfunajeb@laste.ml", "Ajlfunajeb@laste.ml", false)] //invalid
        public void PasswordMustContainNumber(string password, string confirmPassword, bool error)
        {
            var signUpPage = new SignUpPage(_webDriver);
            signUpPage.GoToSignUpPage()
                .InputFirstName(Generators.GetRandName())
                .InputLastName(Generators.GetRandName())
                .InputEmail(Generators.GetRndEmail())
                .InputPassword(password)
                .InputPasswordConfirm(confirmPassword)
                .InputMobile(Generators.GetRndPhone())
                .ClickNextButton();

            var signUpPage2 = new SignUpPage_2(_webDriver);
            Thread.Sleep(2000);
            Assert.AreEqual(expected: "https://newbookmodels.com/join", actual: signUpPage2.GetCurrentUrl());
            Assert.AreEqual(error, signUpPage.CheckIfValidSvgHintShow(1));
        }

        //[TestCase("A1jlfunajeb@laste.ml", "A1jlfunajeb@laste.ml", true)] //valid
        [TestCase("Aj1@fg0", "Aj1@fg0", false)] //invalid
        public void PasswordMustBeLonger7chars(string password, string confirmPassword, bool error)
        {
            var signUpPage = new SignUpPage(_webDriver);
            signUpPage.GoToSignUpPage()
                .InputFirstName(Generators.GetRandName())
                .InputLastName(Generators.GetRandName())
                .InputEmail(Generators.GetRndEmail())
                .InputPassword(password)
                .InputPasswordConfirm(confirmPassword)
                .InputMobile(Generators.GetRndPhone())
                .ClickNextButton();

            var signUpPage2 = new SignUpPage_2(_webDriver);
            Thread.Sleep(2000);
            Assert.AreEqual(expected: "https://newbookmodels.com/join", actual: signUpPage2.GetCurrentUrl());
            Assert.AreEqual(error, signUpPage.CheckIfValidSvgHintShow(0));
        }

        //[TestCase("A1jlfunajeb@laste.ml", "A1jlfunajeb@laste.ml", true)] //valid
        [TestCase("Aj1@fg0ffgfdgfdgfdgfdgfdgg", "Aj1@fg0ffgfdgfdgfdgfdgfdgg", false)] //invalid
        public void PasswordMustBeLess26chars(string password, string confirmPassword, bool error)
        {
            var signUpPage = new SignUpPage(_webDriver);
            signUpPage.GoToSignUpPage()
                .InputFirstName(Generators.GetRandName())
                .InputLastName(Generators.GetRandName())
                .InputEmail(Generators.GetRndEmail())
                .InputPassword(password)
                .InputPasswordConfirm(confirmPassword)
                .InputMobile(Generators.GetRndPhone())
                .ClickNextButton();

            var signUpPage2 = new SignUpPage_2(_webDriver);
            Thread.Sleep(2000);
            Assert.AreEqual(expected: "https://newbookmodels.com/join", actual: signUpPage2.GetCurrentUrl());
            Assert.AreEqual(error, signUpPage.CheckIfValidSvgHintShow(0));
        }

        //[TestCase("A1jlfunajeb@laste.ml", "A1jlfunajeb@laste.ml", true)] //valid
        [TestCase("a1jlfunajeb@laste.ml", "a1jlfunajeb@laste.ml", false)] //invalid
        public void PasswordMustContainCapitalLetter(string password, string confirmPassword, bool error)
        {
            var signUpPage = new SignUpPage(_webDriver);
            signUpPage.GoToSignUpPage()
                .InputFirstName(Generators.GetRandName())
                .InputLastName(Generators.GetRandName())
                .InputEmail(Generators.GetRndEmail())
                .InputPassword(password)
                .InputPasswordConfirm(confirmPassword)
                .InputMobile(Generators.GetRndPhone())
                .ClickNextButton();

            var signUpPage2 = new SignUpPage_2(_webDriver);
            Thread.Sleep(2000);
            Assert.AreEqual(expected: "https://newbookmodels.com/join", actual: signUpPage2.GetCurrentUrl());
            Assert.AreEqual(error, signUpPage.CheckIfValidSvgHintShow(2));
        }


        //[TestCase("A1jlfunajeb@laste.ml", "A1jlfunajeb@laste.ml", true)] //valid
        [TestCase("A1JLFUNA@LFU.NA", "A1JLFUNA@LFU.NA", false)] //invalid
        public void PasswordMustContainLowercaseChar(string password, string confirmPassword, bool error)
        {
            var signUpPage = new SignUpPage(_webDriver);
            signUpPage.GoToSignUpPage()
                .InputFirstName(Generators.GetRandName())
                .InputLastName(Generators.GetRandName())
                .InputEmail(Generators.GetRndEmail())
                .InputPassword(password)
                .InputPasswordConfirm(confirmPassword)
                .InputMobile(Generators.GetRndPhone())
                .ClickNextButton();

            var signUpPage2 = new SignUpPage_2(_webDriver);
            Thread.Sleep(2000);
            Assert.AreEqual(expected: "https://newbookmodels.com/join", actual: signUpPage2.GetCurrentUrl());
            Assert.AreEqual(error, signUpPage.CheckIfValidSvgHintShow(3));
        }

        //[TestCase("A1jlfunajeb@laste.ml", "A1jlfunajeb@laste.ml", true)] //valid
        [TestCase("A1jlfunajeblasteml", "A1jlfunajeblasteml", false)] //invalid
        public void PasswordMustContainSpecialChar(string password, string confirmPassword, bool error)
        {
            var signUpPage = new SignUpPage(_webDriver);
            signUpPage.GoToSignUpPage()
                .InputFirstName(Generators.GetRandName())
                .InputLastName(Generators.GetRandName())
                .InputEmail(Generators.GetRndEmail())
                .InputPassword(password)
                .InputPasswordConfirm(confirmPassword)
                .InputMobile(Generators.GetRndPhone())
                .ClickNextButton();

            var signUpPage2 = new SignUpPage_2(_webDriver);
            Thread.Sleep(2000);
            Assert.AreEqual(expected: "https://newbookmodels.com/join", actual: signUpPage2.GetCurrentUrl());
            Assert.AreEqual(error, signUpPage.CheckIfValidSvgHintShow(4));
        }

        //[TestCase("A1jlfunajeb@laste.ml", "A1jlfunajeb@laste.ml", true)] //valid
        [TestCase("A1jlfunajeb@laste.ml", "A1jlfunajeb@laste.m", false)] //invalid
        public void PasswordMustMatchConfirmation(string password, string confirmPassword, bool error)
        {
            var signUpPage = new SignUpPage(_webDriver);
            signUpPage.GoToSignUpPage()
                .InputFirstName(Generators.GetRandName())
                .InputLastName(Generators.GetRandName())
                .InputEmail(Generators.GetRndEmail())
                .InputPassword(password)
                .InputPasswordConfirm(confirmPassword)
                .InputMobile(Generators.GetRndPhone())
                .ClickNextButton();

            var signUpPage2 = new SignUpPage_2(_webDriver);
            Thread.Sleep(2000);
            Assert.AreEqual(expected: "https://newbookmodels.com/join", actual: signUpPage2.GetCurrentUrl());
            Assert.AreEqual(error, signUpPage.CheckIfValidSvgHintShow(5));
        }

        [TearDown]
        public void TearDown()
        {
            _webDriver.Dispose();
            _webDriver.Quit();
        }
    }
}
