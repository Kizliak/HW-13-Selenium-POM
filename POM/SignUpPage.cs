using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW13.POM
{
    class SignUpPage
    {
        private readonly IWebDriver _webDriver;

        private readonly By _firstNameField = By.CssSelector("input[name=\"first_name\"]");
        private readonly By _lastNameField = By.CssSelector("input[name=\"last_name\"]");
        private readonly By _emailField = By.CssSelector("input[name=\"email\"]");
        private readonly By _passwordField = By.CssSelector("input[name=\"password\"]");
        private readonly By _passwordConfirmField = By.CssSelector("input[name=\"password_confirm\"]");
        private readonly By _mobileField = By.CssSelector("input[name=\"phone_number\"]");
        private readonly By _NextButton = By.CssSelector("button[class=\"SignupForm__submitButton--1m1C2 Button__button---rQSB Button__themePrimary--E5ESP Button__sizeMedium--uLCYD Button__fontSmall--1EPi5 Button__withTranslate--1qGAH\"]");

        public SignUpPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public SignUpPage GoToSignUpPage()
        {
            _webDriver.Navigate().GoToUrl("https://newbookmodels.com/join");
            return this;
        }

        public SignUpPage InputFirstName(string firstName)
        {
            _webDriver.FindElement(_firstNameField).SendKeys(firstName);
            return this;
        }

        public SignUpPage InputLastName(string secondName)
        {
            _webDriver.FindElement(_lastNameField).SendKeys(secondName);
            return this;
        }

        public SignUpPage InputEmail(string email)
        {
            _webDriver.FindElement(_emailField).SendKeys(email);
            return this;
        }

        public SignUpPage InputPassword(string password)
        {
            _webDriver.FindElement(_passwordField).SendKeys(password);
            return this;
        }

        public SignUpPage InputPasswordConfirm(string password)
        {
            _webDriver.FindElement(_passwordConfirmField).SendKeys(password);
            return this;
        }

        public SignUpPage InputMobile(string mobile)
        {
            _webDriver.FindElement(_mobileField).SendKeys(mobile);
            return this;
        }

        public void ClickNextButton() =>
            _webDriver.FindElement(_NextButton).Click();

        //public string GetErrorMessage() =>
        //    _webDriver.FindElement(_errorMessage).Text;

    }
}
