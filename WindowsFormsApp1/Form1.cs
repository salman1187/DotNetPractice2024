using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void DrawRedRectangles(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                Graphics g = panel1.CreateGraphics();
                Random rnd = new Random();
                for (int i = 1; i <= 1000; i++)
                {
                    int x = rnd.Next(panel1.Height);
                    int y = rnd.Next(panel1.Width);
                    g.DrawRectangle(Pens.Red, x, y, 20, 20);
                    Thread.Sleep(100);
                }
            }).Start();
        }

        private void DrawBlueRectangles(object sender, EventArgs e)
        {
            new Task(() =>
            {
                Graphics g = panel2.CreateGraphics();
                Random rnd = new Random();
                for (int i = 1; i <= 1000; i++)
                {
                    int x = rnd.Next(panel2.Height);
                    int y = rnd.Next(panel2.Width);
                    g.DrawRectangle(Pens.Blue, x, y, 20, 20);
                    Thread.Sleep(100);
                }
            }).Start();
        }
    }
}
