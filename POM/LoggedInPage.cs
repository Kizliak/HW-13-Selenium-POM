using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace HW13.POM
{
    class LoggedInPage
    {
        private readonly IWebDriver _webDriver;
        private WebDriverWait _wait;
        private Actions _action;
        private readonly string _pageUrl;

        private readonly By _avatarImage = By.ClassName("AvatarClient__avatar--3TC7_");
        private readonly By _welcomeMessage = By.CssSelector("div[class=\"WelcomePage__welcomeBackSection--1fVmu\"] section div");
        private readonly By _popupWindowEmailActivation = By.CssSelector("div[class=\"resend-email__root\"]");

        public LoggedInPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
            _wait = new WebDriverWait(_webDriver, new TimeSpan(0, 0, 15));
            _wait.Until(ExpectedConditions.ElementExists(_welcomeMessage));
            _wait.Until(ExpectedConditions.ElementIsVisible(_avatarImage));
            _pageUrl = _webDriver.Url;
            _action = new Actions(_webDriver);
        }

        public LoggedInPage ClosePopupWindow()
        {
            _wait.Until(ExpectedConditions.ElementExists(_popupWindowEmailActivation));
            _webDriver.FindElement(_popupWindowEmailActivation).Click();
            _action.SendKeys(Keys.Escape).Perform();
            return this;
        }

        public LoggedInPage GoToUserProfileActionsPage()
        {
            _webDriver.FindElement(_avatarImage).Click();
            return this;
        }

        public string GetCurrentUrl()
        {
            return _pageUrl;
        }

        public bool CheckIfNameDisplayOnPage(string name)
        {
            if (_webDriver.FindElement(_welcomeMessage).Text.Contains(name))
            {
                return true;
            }
            return false;
        }
    }
}
