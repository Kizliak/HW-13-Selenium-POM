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
    class SignUpPage_2
    {
        private readonly IWebDriver _webDriver;

        private readonly By _companyNameField = By.CssSelector("input[name=\"company_name\"]");
        private readonly By _companyUrlField = By.CssSelector("input[name=\"company_website\"]");
        private readonly By _locationField = By.CssSelector("input[name=\"location\"]");
        private readonly By _industryField = By.CssSelector("input[name=\"industry\"]");
        private readonly By _industryDropDown = By.ClassName("Select__option--1IbG6");
        private readonly By _industryDropDownChecker = By.CssSelector("div[role=\"listbox\"]");
        private readonly By _FinishButton = By.CssSelector("button[class=\"SignupCompanyForm__submitButton--3mz3p Button__button---rQSB Button__themePrimary--E5ESP Button__sizeMedium--uLCYD Button__fontSmall--1EPi5 Button__withTranslate--1qGAH\"]");
        WebDriverWait Wait;

        public SignUpPage_2(IWebDriver webDriver)
        {
            _webDriver = webDriver;
            Wait = new WebDriverWait(_webDriver, new TimeSpan(0, 0, 15));
        }

        public SignUpPage_2 InputCompany(string companyName)
        {
            _webDriver.FindElement(_companyNameField).SendKeys(companyName);
            return this;
        }

        public SignUpPage_2 InputCompanyUrl(string companyUrl)
        {
            _webDriver.FindElement(_companyUrlField).SendKeys(companyUrl);
            return this;
        }

        public SignUpPage_2 InputCompanyLocation(string location)
        {
            _webDriver.FindElement(_locationField).SendKeys(location);
            Wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div[class=\"pac-container pac-logo\"]")));
            _webDriver.FindElement(_locationField).SendKeys(Keys.ArrowDown);
            _webDriver.FindElement(_locationField).SendKeys(Keys.Enter);
            Thread.Sleep(2500);
            return this;
        }

        public SignUpPage_2 ChooseIndustry(int industryNum)
        {
            _webDriver.FindElement(_industryField).Click();
            Wait.Until(ExpectedConditions.ElementExists(_industryDropDownChecker));
            _webDriver.FindElements(_industryDropDown)[industryNum].Click();
            Thread.Sleep(2500);
            return this;
        }

        public void ClickFinishtButton() =>
            _webDriver.FindElement(_FinishButton).Click();

        public string GetCurrentUrl() =>
            _webDriver.Url;
    }
}
