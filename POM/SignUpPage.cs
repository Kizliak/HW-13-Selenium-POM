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
        private readonly By _passwordInvalidErrors = By.CssSelector("div[class=\"PasswordValidation__icon--ZAHuS\"] svg");
        private readonly By _firstNameError = By.XPath("//div[@class=\"SignupFormLayout__field--2rfUP\"]/label/input[@name=\"first_name\"]/ancestor::div[@class=\"SignupFormLayout__field--2rfUP\"]/label//div[@class=\"FormErrorText__error---nzyq\"]/div");
        private readonly By _lastNameError = By.XPath("//div[@class=\"SignupFormLayout__field--2rfUP\"]/label/input[@name=\"last_name\"]/ancestor::div[@class=\"SignupFormLayout__field--2rfUP\"]/label//div[@class=\"FormErrorText__error---nzyq\"]/div");
        private readonly By _emailError = By.XPath("//div[@class=\"SignupFormLayout__field--2rfUP\"]/label/input[@name=\"email\"]/ancestor::div[@class=\"SignupFormLayout__field--2rfUP\"]/label//div[@class=\"FormErrorText__error---nzyq\"]/div");
        private readonly By _passwordError = By.XPath("//div[@class=\"SignupFormLayout__field--2rfUP\"]/label/input[@name=\"password\"]/ancestor::div[@class=\"SignupFormLayout__field--2rfUP\"]/label//div[@class=\"FormErrorText__error---nzyq\"]/div");
        private readonly By _passwordConfirmError = By.XPath("//div[@class=\"SignupFormLayout__field--2rfUP\"]/label/input[@name=\"password_confirm\"]/ancestor::div[@class=\"SignupFormLayout__field--2rfUP\"]/label//div[@class=\"FormErrorText__error---nzyq\"]/div");
        private readonly By _phoneError = By.XPath("//div[@class=\"SignupFormLayout__field--2rfUP\"]/label/input[@name=\"phone_number\"]/ancestor::div[@class=\"SignupFormLayout__field--2rfUP\"]/label//div[@class=\"FormErrorText__error---nzyq\"]/div");

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

        public string CheckFirstNameErrorText()
        {
            return _webDriver.FindElement(_firstNameError).Text;
        }
        public string CheckLastNameErrorText()
        {
            return _webDriver.FindElement(_lastNameError).Text;
        }
        public string CheckEmailNameErrorText()
        {
            return _webDriver.FindElement(_emailError).Text;
        }
        public string CheckPasswordErrorText()
        {
            return _webDriver.FindElement(_passwordError).Text;
        }
        public string CheckConfirmPasswordErrorText()
        {
            return _webDriver.FindElement(_passwordConfirmError).Text;
        }
        public string CheckPhoneErrorText()
        {
            return _webDriver.FindElement(_phoneError).Text;
        }

        public bool CheckIfValidSvgHintShow(int errorNumber)
        {
            var errorSVG = _webDriver.FindElements(_passwordInvalidErrors)[errorNumber];
            if (errorSVG.GetAttribute("width") == "14" && errorSVG.GetAttribute("height") == "13")
            {
                return false;
            }
            return true;
        }

        public void ClickNextButton() =>
            _webDriver.FindElement(_NextButton).Click();

    }
}
