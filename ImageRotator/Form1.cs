using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageRotator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //rotate images and save
            new Task(() =>
            {
                var imageFiles = Directory.GetFiles("C:\\Users\\MOHAMMAD SALMAN\\source\\repos\\DotnetPractice2024\\ImageRotator\\images");
                
                //foreach (var imageFile in imageFiles)
                Parallel.ForEach(imageFiles, imageFile =>
                {
                    {
                        Bitmap bitmap = new Bitmap(imageFile);
                        bitmap.RotateFlip(RotateFlipType.Rotate180FlipX);
                        FileInfo fileInfo = new FileInfo(imageFile);
                        string fileName = fileInfo.Name;
                        bitmap.Save("C:\\Users\\MOHAMMAD SALMAN\\source\\repos\\DotnetPractice2024\\ImageRotator\\RotatedImages\\" + fileName);
                    }
                });
            }).Start();
        }
    }
}
