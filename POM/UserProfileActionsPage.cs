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
    class UserProfileActionsPage
    {
        private readonly IWebDriver _webDriver;
        private WebDriverWait _wait;
        private Actions _action;
        private readonly string _pageUrl;

        private readonly By _logoutLink = By.CssSelector("div[class=\"link link_type_logout link_active\"]");
        private readonly By _editSwitcherLink = By.CssSelector("div[class=\"edit-switcher__icon_type_edit\"]");
        private readonly By _phoneBlockSelector = By.TagName("nb-account-info-phone");
        private readonly By _cardHolderNameField = By.CssSelector("input[placeholder=\"Full name\"]");
        private readonly By _cardFrameAdd = By.CssSelector("iframe[title=\"Secure card payment input frame\"]");
        private readonly By _creaditCardNumberField = By.CssSelector("input[name=\"cardnumber\"]");
        private readonly By _creaditCardExpDateField = By.CssSelector("input[name=\"exp-date\"]");
        private readonly By _creaditCardCvvField = By.CssSelector("input[name=\"cvc\"]");
        private readonly By _cardAddedElement = By.CssSelector("common-input[class=\"stripe-card-view__cvv ng-untouched ng-pristine\"]");
        private readonly By _submitButtons = By.CssSelector("button[class=\"button button_type_default\"");
        private readonly By _passwordField = By.CssSelector("input[type=\"password\"]");
        private readonly By _emailCurrentUser = By.CssSelector("div[class=\"email-block\"] div span");
        private readonly By _lineSpliterElement = By.TagName("common-line");
        private readonly By _emailChangeCurrentPassword = By.CssSelector("input[placeholder=\"Enter Password\"][type=\"password\"]");
        private readonly By _emailChangeNewEmail = By.CssSelector("input[placeholder=\"Enter E-mail\"]");
        private readonly By _emailChangeSubmitButton = By.XPath("//common-Input[@formcontrolname=\"email\"]/../child::common-button-deprecated/button");

        public UserProfileActionsPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
            _wait = new WebDriverWait(_webDriver, new TimeSpan(0, 0, 15));
            _pageUrl = _webDriver.Url;
            _action = new Actions(_webDriver);
        }

        public void EditInformation(string whatToEdit)
        {
            int blockEditNum = 0;
            switch (whatToEdit)
            {
                case "General information":
                    blockEditNum = 0;
                    break;
                case "E-mail Address":
                    blockEditNum = 1;
                    break;
                case "Password":
                    blockEditNum = 2;
                    break;
                case "Phone Number":
                    blockEditNum = 3;
                    break;
            }
            _webDriver.FindElements(_editSwitcherLink)[blockEditNum].Click();
        }

        public UserProfileActionsPage CreditCardAdd(string cardHolderName, string cardNumber, string cardDate, string cvv)
        {
            _wait.Until(ExpectedConditions.ElementExists(_phoneBlockSelector));
            _action.MoveToElement(_webDriver.FindElement(_phoneBlockSelector)).Perform();
            _webDriver.FindElement(_cardHolderNameField).SendKeys(cardHolderName);
            _webDriver.SwitchTo().Frame(_webDriver.FindElement(_cardFrameAdd));
            _webDriver.FindElement(_creaditCardNumberField).SendKeys(cardNumber);
            _webDriver.FindElement(_creaditCardExpDateField).SendKeys(cardDate);
            _webDriver.FindElement(_creaditCardCvvField).SendKeys(cvv);
            Thread.Sleep(500);
            _webDriver.FindElement(_creaditCardCvvField).SendKeys(Keys.Enter);
            _webDriver.SwitchTo().DefaultContent();
            return this;
        }

        public bool CheckIfCreditCardAdded()
        {
            _wait.Until(ExpectedConditions.ElementExists(_cardAddedElement));
            var submitButtons = _webDriver.FindElements(_submitButtons);
            bool cardAdded = false;

            foreach (IWebElement button in submitButtons)
            {
                if (button.Text.Contains("Save Changes"))
                {
                    cardAdded = true;
                }
            }
            return cardAdded;
        }

        public UserProfileActionsPage InputNewPassword(string oldPassword, string newPassword)
        {
            _action.MoveToElement(_webDriver.FindElements(_lineSpliterElement)[2]).Perform();
            _webDriver.FindElements(_editSwitcherLink)[2].Click();
            _wait.Until(ExpectedConditions.ElementExists(_passwordField));
            _wait.Until(ExpectedConditions.ElementIsVisible(_passwordField));
            _webDriver.FindElements(_passwordField)[0].SendKeys(oldPassword);
            _webDriver.FindElements(_passwordField)[1].SendKeys(newPassword);
            _webDriver.FindElements(_passwordField)[2].SendKeys(newPassword);
            return this;
        }

        public UserProfileActionsPage ClickLogout()
        {
            var logoutLinkElement = _webDriver.FindElement(_logoutLink);
            _action.MoveToElement(logoutLinkElement).Perform();
            logoutLinkElement.Click();
            return this;
        }

        public string GetEmailFromProfile()
        {
            return _webDriver.FindElement(_emailCurrentUser).Text;
        }

        public UserProfileActionsPage SubmitNewEmail()
        {
            return this;
        }

        public UserProfileActionsPage ChangeEmail(string userPassword, string newEmail)
        {
            _webDriver.FindElements(_editSwitcherLink)[1].Click();
            _wait.Until(ExpectedConditions.ElementExists(_emailChangeCurrentPassword));
            _wait.Until(ExpectedConditions.ElementIsVisible(_emailChangeCurrentPassword));
            _webDriver.FindElement(_emailChangeCurrentPassword).SendKeys(userPassword);
            _webDriver.FindElement(_emailChangeNewEmail).SendKeys(newEmail);
            _webDriver.FindElement(_emailChangeSubmitButton).Click();
            _wait.Until(ExpectedConditions.ElementExists(_emailCurrentUser));
            _wait.Until(ExpectedConditions.ElementIsVisible(_emailCurrentUser));
            return this;
        }

        public string GetCurrentUrl()
        {
            return _pageUrl;
        }
    }
}
