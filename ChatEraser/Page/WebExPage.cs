using ChatEraser.Helper;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatEraser.Page
{
    public class WebExPage : BasePage
    {
        public WebExPage(IWebDriver driver) : base(driver) { }
        internal WebExPage ExpandLeftNav()
        {
            string viewOlderSpaces = "//span[contains(text(),'View Older Spaces')]";
            helper.ElementAvailable(_Driver, viewOlderSpaces, 120);

            bool isViewOlderSpaces = IsElementPresent(By.XPath(viewOlderSpaces));

            while (isViewOlderSpaces)
            {
                helper.HighlightElement(_Driver, By.XPath(viewOlderSpaces));
                helper.ClickOnElement(_Driver, viewOlderSpaces, 60);
                Thread.Sleep(5000);
                isViewOlderSpaces = IsElementPresent(By.XPath(viewOlderSpaces));
            }
            return new WebExPage(_Driver);
        }

        internal WebExPage DeleteMessages()
        {
            //Loop through Left Nav
            ICollection<IWebElement> leftNav = GetLeftNav();
            foreach (IWebElement element in leftNav)
            {
                ProcessIndividualSpace(element);
            }
            return new WebExPage(_Driver);
        }

        private void ProcessIndividualSpace(IWebElement element)
        {
            helper.HighlightElement(_Driver, element);
            DeleteMessageBlock(element);
        }

        private void DeleteMessageBlock(IWebElement spaceConversation)
        {
            string spaceName = helper.GetElementText(spaceConversation);

            ICollection<IWebElement> allMessagesByYou = GetListOfAllMessages(spaceConversation);
            foreach (IWebElement message in allMessagesByYou)
            {
                helper.HighlightElement(_Driver, message);
                //get trash can
                DeleteMessage(spaceName, message);
            }
        }

        /// <summary>
        /// It will try to delete the messsage, and if unable it will write to console
        /// </summary>
        /// <param name="message"></param>
        private void DeleteMessage(string spaceName, IWebElement message)
        {
            string msgText = string.Empty;
            try
            {
                msgText = helper.GetElementText(message);

                if (!msgText.Contains($"You deleted your reply"))
                {
                    ClickOnMessage(message);
                    ClickOnTrashCan();

                    string confirmDelete = $"//span[contains(@class, 'md-button__children')][text()='Delete']";
                    IWebElement deleteElement = _Driver.FindElement(By.XPath(confirmDelete));
                    helper.HighlightElement(_Driver, deleteElement);
                    helper.ClickOnElement(_Driver, deleteElement, 60);
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"Unable to delete Space = {spaceName} Message = {msgText} ");
            }
        }

        private void ClickOnTrashCan()
        {
            string trashCan = $"//i[@class = 'md-icon icon icon-delete_16']";
            IWebElement trashCanElement = _Driver.FindElement(By.XPath(trashCan));
            HoverElement(trashCanElement);
            helper.ClickOnElement(_Driver, trashCanElement, 60); //trash can
        }

        private void ClickOnMessage(IWebElement message)
        {
            IWebElement messageHeader = message.FindElement(By.XPath($"//div[contains(@class, 'activity-item-sender-meta')]"));
            HoverElement(message);
            //helper.ClickOnElement(_Driver, message, 60); //looks redundant, but this is a workaround for an implicit wait

            Actions action = new Actions(_Driver);
            action.DoubleClick(message).Perform();
        }

        private ICollection<IWebElement> GetListOfAllMessages(IWebElement elementSpace)
        {
            UncollapseMessages(elementSpace);
            //GetAnyMessageToDelete(elementSpace);
            IWebElement messageBody = _Driver.FindElement(By.Id("activity-list"));
            ICollection <IWebElement>  allMessagesByYou = messageBody.FindElements(By.XPath($"//div[contains(@aria-label, 'Message from You')]"));

            return allMessagesByYou;
        }

        private void GetAnyMessageToDelete(IWebElement elementSpace)
        {
            helper.ClickOnElement(_Driver, elementSpace, 60); //get to textbox from space
            string messageLocation = $"//div[@class = 'activity-item--message'][1]";
            helper.ClickOnElement(_Driver, messageLocation, 30);
        }

        /// <summary>
        /// Click on an indiviudal space in the left nav, then we want to expand all the messages in that space
        /// </summary>
        /// <param name="elementSpace"></param>
        private void UncollapseMessages(IWebElement elementSpace)
        {
            helper.ClickOnElement(_Driver, elementSpace, 60); //get to textbox from space

            string firstMessage = string.Empty, secondMessage = "1";
            int counter = 10, sleep = 0, multiplier = 100;
            while (firstMessage != secondMessage)
            {
                counter++;
                if (counter > 25)
                {
                    multiplier = 200;

                }
                if (counter > 50)
                {
                    multiplier = 400;

                }
                if (counter > 100)
                {
                    multiplier = 1000;

                }

                sleep = counter * multiplier;

                string messageLocation = $"//div[@class = 'activity-item--message'][1]";

                helper.ClickOnElement(_Driver, messageLocation, 30);
                IWebElement messageLocationElement1 = _Driver.FindElement(By.XPath(messageLocation));
                Thread.Sleep(sleep);
                firstMessage = helper.GetElementText(messageLocationElement1);

                helper.ClickOnElement(_Driver, messageLocation, 30);
                IWebElement messageLocationElement2 = _Driver.FindElement(By.XPath(messageLocation));
                Thread.Sleep(sleep);
                secondMessage = helper.GetElementText(messageLocationElement2);
            }
        }

        private ICollection<IWebElement> GetLeftNav()
        {
            IWebElement leftPane = _Driver.FindElement(By.XPath("//div[contains (@class, 'space-list-wrapper')]"));
            ICollection<IWebElement> allSpaces = leftPane.FindElements(By.XPath("//div[contains (@class, 'space-list-item-wrapper')]"));

            return allSpaces;
        }

        private void HoverElement(IWebElement webElement)
        {
            Actions builder = new Actions(_Driver);
            builder.MoveToElement(webElement).Build().Perform();
        }

    }
}
