using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Lab05
{
    public partial class Form1 : Form
    {
        bool addText = false;
        struct addedText
        {
            public string text;
            public int xOff;
            public int yOff;
        }
        List<addedText> LAddedTexts = new();
            
        public Form1()
        {
            InitializeComponent();
        }

        private void MainWindowLoad(object sender, EventArgs e)
        {
            //splitContainer.SplitterDistance = Convert.ToInt32(this.ClientSize.Width * 0.66);
            //Canvas.Width = splitContainer.Panel1.Width;
            //Canvas.Height = splitContainer.Panel1.Height;
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            int xCenter = Canvas.Width / 2;
            int yCenter = Canvas.Height / 2;
            int bookWidth = 400;
            int bookHeight = 600;
            int spineWidth = 50;

            e.Graphics.DrawRectangle(Pens.DarkGray, 
                xCenter - bookWidth - spineWidth/2, yCenter - bookHeight/2, 
                2*bookWidth + spineWidth, bookHeight);

            e.Graphics.DrawLine(Pens.DarkGray, 
                xCenter - spineWidth / 2, yCenter - bookHeight / 2,
                xCenter - spineWidth / 2, yCenter + bookHeight / 2
                );

            e.Graphics.DrawLine(Pens.DarkGray,
                xCenter + spineWidth / 2, yCenter - bookHeight / 2,
                xCenter + spineWidth / 2, yCenter + bookHeight / 2
                );

            FontFamily fontFamily = new FontFamily("Arial");
            Font font = new Font(
                       fontFamily,
                       16,
                       FontStyle.Regular,
                       GraphicsUnit.Pixel);
            StringFormat myStringFormat = new StringFormat();

            foreach (var t in LAddedTexts)
                e.Graphics.DrawString(t.text, font, Brushes.Black, xCenter + t.xOff, yCenter+ t.yOff, myStringFormat);
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            addText = true;
        }

        private void Canvas_MouseEnter(object sender, EventArgs e)
        {
            if (addText)
                Cursor = Cursors.Cross;
        }

        private void Canvas_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (addText)
            {
                Graphics g = Canvas.CreateGraphics();

                FontFamily fontFamily = new FontFamily("Arial");
                Font font = new Font(
                           fontFamily,
                           16,
                           FontStyle.Regular,
                           GraphicsUnit.Pixel);
                StringFormat myStringFormat = new StringFormat();

                float textWidth = g.MeasureString(addTextBox.Text, font).Width;
                float textHeight = g.MeasureString(addTextBox.Text, font).Height;
                int xPos = e.X - (int)textWidth / 2;
                int yPos = e.Y - (int)textHeight / 2;

                int xCenter = Canvas.Width / 2;
                int yCenter = Canvas.Height / 2;

                int xOff = xPos - xCenter;
                int yOff = yPos - yCenter;

                LAddedTexts.Add( new addedText { text = addTextBox.Text, xOff = xOff, yOff = yOff} );

                addTextBox.Text = "";
                addText = false;
                Canvas.Refresh();
            }
        }
    }

    
}
