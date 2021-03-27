using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsGallery
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void colorButton_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                galleryLayoutPanel.GetControlFromPosition(1, 1).BackColor = colorDialog.Color;
                galleryLayoutPanel.GetControlFromPosition(0, 1).BackColor = colorDialog.Color;
            }
        }

        private void imageButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                galleryLayoutPanel.GetControlFromPosition(0, 0).BackgroundImage = new Bitmap(fileDialog.FileName);
                galleryLayoutPanel.GetControlFromPosition(0, 0).BackgroundImageLayout = ImageLayout.Stretch;
            }
        }
    }
}
