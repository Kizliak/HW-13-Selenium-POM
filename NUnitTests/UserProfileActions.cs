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
    class UserProfileActions
    {
        private IWebDriver _webDriver;

        [SetUp]
        public void Setup()
        {
            _webDriver = new ChromeDriver();
            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            _webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(20);
            _webDriver.Manage().Window.Maximize();
        }

        [TestCase("6011000000000004", "1224", "123")]
        public void AddCreditCard(string cardNumber, string cardExpDate, string cardCvv)
        {
            string cardHolderName = Generators.GetRandName() + " " + Generators.GetRandName();

            //register
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

            //goto profile page
            var loggenInPage = new LoggedInPage(_webDriver);
            loggenInPage.GoToUserProfileActionsPage();

            //add credit card
            var profilePage = new UserProfileActionsPage(_webDriver);
            profilePage.CreditCardAdd(cardHolderName, cardNumber, cardExpDate, cardCvv);
            Assert.AreEqual(expected: true, actual: profilePage.CheckIfCreditCardAdded());
        }

        [TestCase("sacbdwplb@supere.ml", "1Asacbdwplb@supere.ml")]
        public void Logout(string userLogin, string userPassword)
        {
            //login
            var signInPage = new SignInPage(_webDriver);
            signInPage.GoToSingInPage()
                .InputEmailField(userLogin)
                .InputPasswordField(userPassword)
                .ClickLoginButton();

            //goto profile page
            var loggenInPage = new LoggedInPage(_webDriver);
            loggenInPage.ClosePopupWindow()
                .GoToUserProfileActionsPage();

            //logout
            var profilePage = new UserProfileActionsPage(_webDriver);
            profilePage.ClickLogout();

            //check if login page again
            var loginPage = new SignInPage(_webDriver);
            loginPage.CheckIfLoginButtonExsist();
            Assert.AreEqual(expected: "https://newbookmodels.com/auth/signin", actual: loginPage.GetCurrentUrl());
        }

        [TestCase("1Asacbdwplb@supere.ml", "1Asacbdwplb@supere.ml")]
        public void ChangePassword(string userPassword, string userPasswordNew)
        {
            //register
            var signUpPage = new SignUpPage(_webDriver);
            string userEmail = Generators.GetRndEmail();
            signUpPage.GoToSignUpPage()
                .InputFirstName(Generators.GetRandName())
                .InputLastName(Generators.GetRandName())
                .InputEmail(userEmail)
                .InputPassword(userPassword)
                .InputPasswordConfirm(userPassword)
                .InputMobile(Generators.GetRndPhone())
                .ClickNextButton();

            var signUpPage2 = new SignUpPage_2(_webDriver);
            signUpPage2.InputCompany(Generators.GetRandName())
                .InputCompanyUrl(Generators.GetRndCompanyUrl())
                .InputCompanyLocation(Generators.GetAddress())
                .ChooseIndustry(Generators.Randomchik.Next(0, 4))
                .ClickFinishtButton();

            //goto profile page
            var loggenInPage = new LoggedInPage(_webDriver);
            loggenInPage.GoToUserProfileActionsPage();

            //logout
            var profilePage = new UserProfileActionsPage(_webDriver);
            profilePage.InputNewPassword(userPassword, userPasswordNew)
                .ClickLogout();

            //login with new password
            var signInPage = new SignInPage(_webDriver);
            signInPage.CheckIfLoginButtonExsist()
                .InputEmailField(userEmail)
                .InputPasswordField(userPasswordNew)
                .ClickLoginButton();

            //check if loged in correct
            var loggenInPage2 = new LoggedInPage(_webDriver);
            loggenInPage2.ClosePopupWindow()
                .GoToUserProfileActionsPage();

            //check if same user email displayed in profile
            var profilePage2 = new UserProfileActionsPage(_webDriver);
            Assert.AreEqual(expected: "https://newbookmodels.com/account-settings/account-info/edit", actual: profilePage2.GetCurrentUrl());
            Assert.AreEqual(userEmail, profilePage2.GetEmailFromProfile());
        }

        [TestCase("123Asacbdwplb@supere.ml", "124Asacbdwplb@supere.ml")]
        public void ChangeEmail(string emailOld, string emailNew)
        {
            string userPassword = Generators.GetRndPass();
            emailOld = Generators.GetRandName() + emailOld;
            emailNew = Generators.GetRandName() + emailNew;

            //register
            var signUpPage = new SignUpPage(_webDriver);
            signUpPage.GoToSignUpPage()
                .InputFirstName(Generators.GetRandName())
                .InputLastName(Generators.GetRandName())
                .InputEmail(emailOld)
                .InputPassword(userPassword)
                .InputPasswordConfirm(userPassword)
                .InputMobile(Generators.GetRndPhone())
                .ClickNextButton();

            var signUpPage2 = new SignUpPage_2(_webDriver);
            signUpPage2.InputCompany(Generators.GetRandName())
                .InputCompanyUrl(Generators.GetRndCompanyUrl())
                .InputCompanyLocation(Generators.GetAddress())
                .ChooseIndustry(Generators.Randomchik.Next(0, 4))
                .ClickFinishtButton();

            //goto profile page
            var loggenInPage = new LoggedInPage(_webDriver);
            loggenInPage.GoToUserProfileActionsPage();

            //submit new email and check
            var profilePage = new UserProfileActionsPage(_webDriver);
            profilePage.ChangeEmail(userPassword, emailNew);
            Assert.AreEqual(emailNew, profilePage.GetEmailFromProfile());
        }

        [TestCase("Valera", "Gnidoy", "123 South Figueroa Street", "Cosmetics")]
        public void ChangeGeneralInfo(string newFirstName, string newLastName, string newCompanyLocation, string newIndustry)
        {
            //register
            string password = Generators.GetRndPass();
            var signUpPage = new SignUpPage(_webDriver);
            signUpPage.GoToSignUpPage()
                .InputFirstName(Generators.GetRandName())
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

            //goto profile page
            var loggenInPage = new LoggedInPage(_webDriver);
            loggenInPage.GoToUserProfileActionsPage();

            //submit new GeneralInfo
            var profilePage = new UserProfileActionsPage(_webDriver);
            (string nameDisplayed, string companyLocationDisplayed, string industryDisplayed) result = profilePage.ChangeGeneralInfo(newFirstName, newLastName, newCompanyLocation, newIndustry);
            Assert.AreEqual(newFirstName + " " + newLastName, result.nameDisplayed);
            Assert.AreEqual(newIndustry, result.industryDisplayed);
        }

        [TearDown]
        public void TearDown()
        {
            _webDriver.Dispose();
            _webDriver.Quit();
        }
    }
}
