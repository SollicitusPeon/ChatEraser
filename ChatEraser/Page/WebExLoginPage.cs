using ChatEraser.Helper;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaitHelpers = SeleniumExtras.WaitHelpers;

namespace ChatEraser.Page
{
    public class WebExLoginPage : BasePage
    {
        public WebExLoginPage(IWebDriver driver) : base(driver){ }
        public IWebElement emailTextbox => _Driver.FindElement(By.TagName("input"));
        public IWebElement nextButton => _Driver.FindElement(By.XPath("//span[contains(text(),'Next')]"));

        internal WebExLoginPage GoToLogin(string url)
        {
            _Driver.Navigate().GoToUrl(url);
            return new WebExLoginPage(_Driver);
        }

        internal WebExPage ShrinkText()
        {
            return new WebExPage(_Driver);
        }
        internal WebExPage Login(string userName)
        {
            emailTextbox.SendKeys(userName);
            Actions action = new Actions(_Driver);
            action.DoubleClick(nextButton).Perform();
            return new WebExPage(_Driver);
        }
        internal WebExPage Login(string user, string pass)
        {
            emailTextbox.SendKeys(user);
            Actions action = new Actions(_Driver);
            action.DoubleClick(nextButton).Perform();

            //Okta sign up

            IWebElement oktaUser;
            helper.ClickOnElement(_Driver, $"//input[@id='okta-signin-username']", 60);

            oktaUser = _Driver.FindElement(By.XPath($"//input[@id='okta-signin-username']"));
            oktaUser.SendKeys(user);
            IWebElement oktaPass = _Driver.FindElement(By.XPath($"//input[@id='okta-signin-password']"));
            oktaPass.SendKeys(pass);
            IWebElement oktaSubmit = _Driver.FindElement(By.XPath($"//input[@id='okta-signin-submit']"));

            oktaSubmit.Submit();
            helper.ClickOnElement(_Driver, $"//*[@type='submit']", 60);
            IWebElement sendPush = _Driver.FindElement(By.XPath($"//*[@type='submit']"));
            sendPush.Submit();

            string viewOlderSpaces = "//span[contains(text(),'View Older Spaces')]";
            helper.ElementAvailable(_Driver, viewOlderSpaces, 120);

            return new WebExPage(_Driver);
        }
    }
}
