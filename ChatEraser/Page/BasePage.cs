using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatEraser.Page
{
    public class BasePage
    {
        public IWebDriver _Driver { get; set; }
        public BasePage(IWebDriver driver)
        {
            _Driver = driver;
        }

        /// <summary>
        /// Check if the element exists
        /// </summary>
        /// <param name="by"></param>
        /// <returns></returns>
        public bool IsElementPresent(By by)
        {
            try
            {
                _Driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

    }
}
