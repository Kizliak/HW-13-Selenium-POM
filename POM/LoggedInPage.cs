using OpenQA.Selenium;
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

        //private readonly By _companyNameField = By.CssSelector("input[name=\"company_name\"]");
        private readonly By _avatarImage = By.ClassName("AvatarClient__avatar--3TC7_");
        private readonly string _pageUrl;

        WebDriverWait Wait;

        public LoggedInPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
            Wait = new WebDriverWait(_webDriver, new TimeSpan(0, 0, 15));
            Wait.Until(ExpectedConditions.ElementExists(_avatarImage));
            _pageUrl = _webDriver.Url;
        }

        public string GetCurrentUrl()
        {
            return _pageUrl;
        }
    }
}
