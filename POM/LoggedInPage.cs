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
    class LoggedInPage
    {
        private readonly IWebDriver _webDriver;
        WebDriverWait Wait;
        Actions action;
        private readonly string _pageUrl;

        private readonly By _avatarImage = By.ClassName("AvatarClient__avatar--3TC7_");
        private readonly By _welcomeMessage = By.CssSelector("div[class=\"WelcomePage__welcomeBackSection--1fVmu\"] section div");
        private readonly By _popupWindowEmailActivation = By.CssSelector("div[class=\"resend-email__root\"]");

        public LoggedInPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
            Wait = new WebDriverWait(_webDriver, new TimeSpan(0, 0, 15));
            Wait.Until(ExpectedConditions.ElementExists(_welcomeMessage));
            Wait.Until(ExpectedConditions.ElementIsVisible(_avatarImage));
            _pageUrl = _webDriver.Url;
            action = new Actions(_webDriver);
        }

        public LoggedInPage ClosePopupWindow()
        {
            Wait.Until(ExpectedConditions.ElementExists(_popupWindowEmailActivation));
            _webDriver.FindElement(_popupWindowEmailActivation).Click();
            action.SendKeys(Keys.Escape).Perform();
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
