using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ChatEraser.Helper
{
    public class helper
    {
        internal static void ClickOnElement(IWebDriver driver, string xPathLocation, double seconds)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(xPathLocation)));

            IWebElement actionToTake = driver.FindElement(By.XPath(xPathLocation));
            Actions action = new Actions(driver);
            action.Click(actionToTake).Perform();
        }

        internal static void ClickOnElement(IWebDriver driver, IWebElement element, double seconds)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));

            Actions action = new Actions(driver);
            action.Click(element).Perform();
        }

        internal static void ElementAvailable(IWebDriver driver, string xPathLocation, double seconds)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(xPathLocation)));
        }

        internal static void HighlightElement(IWebDriver driver, By by)
        {
            IWebElement element = driver.FindElement(by);
            var jsDriver = (IJavaScriptExecutor)driver;
            string highlightJavascript =
                @"arguments[0].style.cssText = ""background-color: yellow; border-width: 4px; border-style: solid; border-color: red"";";
            jsDriver.ExecuteScript(highlightJavascript, new object[] { element });
        }

        internal static void HighlightElement(IWebDriver driver, IWebElement element)
        {
            var jsDriver = (IJavaScriptExecutor)driver;
            string highlightJavascript =
                @"arguments[0].style.cssText = ""background-color: yellow; border-width: 4px; border-style: solid; border-color: red"";";
            jsDriver.ExecuteScript(highlightJavascript, new object[] { element });

        }
        internal static void BrowserZoomOut(IWebDriver driver)
        {
            var jsDriver = (IJavaScriptExecutor)driver;
            jsDriver.ExecuteScript("document.body.style.zoom = '.75'");
        }
        internal static string GetElementText(IWebElement element)
        {
            string text;
            if (element == null)
                return "";

            string elementTxt = element.Text;
            string attributeTxt = element.GetAttribute("text");
            string valueTxt = element.GetAttribute("value");
            string innerTxt = element.GetAttribute("innerHTML");
            if (!String.IsNullOrEmpty(elementTxt))
                text = elementTxt;
            else if (!String.IsNullOrEmpty(attributeTxt))
                text = attributeTxt;
            else if (!String.IsNullOrEmpty(valueTxt))
                text = valueTxt;
            else
                text = innerTxt;
            return text;
        }
        internal static void SendControlChar(IWebElement html, char character)
        {
            html.SendKeys(Keys.Control + character);
        }
        internal static void BrowserZoomOut(IWebDriver driver, IWebElement html)
        {
            Actions action = new Actions(driver);
            action.SendKeys(html, Keys.Control + '-' + Keys.Null).Perform();
        }
    }
}
