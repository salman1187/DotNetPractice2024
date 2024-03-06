using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesLab2
{
    delegate void ButtonClicked();
    internal class Program
    {
        static void Main(string[] args)
        {
            Form f = new Form();
            Button b = new Button();
            f.b = b;
            f.ButtonClickEventHandler(); 
        }
    }
    class Button
    {
        public static event ButtonClicked ButtonDelegate;
        public void Click()
        {
            if(ButtonDelegate != null)
            {
                ButtonDelegate();
            }
        }
    }
    class Form
    {
        public Button b;
        public void SubscribeToButtonClick()
        {
            Button.ButtonDelegate += delegate () { Console.WriteLine("subscription added"); };
        }
        public void ButtonClickEventHandler()
        {
            SubscribeToButtonClick();
            b.Click();
        }
    }
}
