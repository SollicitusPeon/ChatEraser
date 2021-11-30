using ChatEraser.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatEraser.Service
{
    public class PageInteractionService : BasePageService
    {
        const string webExUrl = "https://teams.webex.com/signin";
        public WebExLoginPage webExLogin { get; private set; }


        public WebExPage webEx { get; private set; }
        public PageInteractionService()
        {
            _Driver = GetChromeDriver();
            webExLogin = new WebExLoginPage(_Driver);
        }



        public void Login(string userName)
        {
            webExLogin.GoToLogin(webExUrl);
            webEx = webExLogin.ShrinkText();
            webEx = webExLogin.Login(userName);
            
        }
        internal void Login(string user, string pass)
        {
            webExLogin.GoToLogin(webExUrl);
            webEx = webExLogin.Login(user, pass);
        }
        internal void DeleteMessages()
        {
            webEx.DeleteMessages();
        }
        public void ExpandLeftNav()
        {
            webEx.ExpandLeftNav();
        }
    }
}
