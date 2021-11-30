using ChatEraser.Service;
using System;

namespace ChatEraser
{
    class Program
    {
        static void Main(string[] args)
        {
            //ultimateqa.com - contains selenium error messages
            PageInteractionService page = new PageInteractionService();

            if (args.Length == 1)
            {
            page.Login(args[0]);
            }
            else
            {
                page.Login(args[0], args[1]);
            }

            page.ExpandLeftNav();
            page.DeleteMessages();

            Console.WriteLine("Hello World!");
        }
    }
}
