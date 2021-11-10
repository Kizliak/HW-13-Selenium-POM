using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HW13.POM
{
    class SignInPage
    {
        private readonly IWebDriver _webDriver;
        private WebDriverWait wait;
        private readonly string _pageUrl;

        private readonly By _emailField = By.CssSelector("input[type=email]");
        private readonly By _passwordField = By.CssSelector("input[type=password]");
        private readonly By _loginButton = By.CssSelector("button[class^=SignInForm__submitButton]");
        private readonly By _errorMessage = By.XPath("//*[contains(@class, 'SignInForm__submitButton')]/../../div[contains(@class, 'PageFormLayout__errors--3dFcq')]/div/div");
        
        public SignInPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
            wait = new WebDriverWait(_webDriver, new TimeSpan(0, 0, 15));
            _pageUrl = _webDriver.Url;
        }

        public SignInPage GoToSingInPage()
        {
            _webDriver.Navigate().GoToUrl("https://newbookmodels.com/auth/signin");
            return this;
        }

        public SignInPage InputEmailField(string email)
        {
            _webDriver.FindElement(_emailField).SendKeys(email);
            return this;
        }

        public SignInPage InputPasswordField(string password)
        {
            _webDriver.FindElement(_passwordField).SendKeys(password);
            return this;
        }

        public SignInPage CheckIfLoginButtonExsist()
        {
            wait.Until(ExpectedConditions.ElementExists(_loginButton));
            return this;
        }

        public string GetCurrentUrl()
        {
            return _webDriver.Url;
        }

        public void ClickLoginButton() => 
            _webDriver.FindElement(_loginButton).Click();

        public string GetErrorMessage() => 
            _webDriver.FindElement(_errorMessage).Text;

    }
}
