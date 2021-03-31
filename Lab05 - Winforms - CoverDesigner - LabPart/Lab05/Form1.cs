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
            public int x;
            public int y;
        }
        List<addedText> addedTexts = new List<addedText>();
            
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
            int x = Canvas.Width / 4;
            int y = Canvas.Height / 4;
            int margin = 20;

            e.Graphics.DrawRectangle(Pens.DarkGray, x, y, x - margin, 2*y);
            e.Graphics.DrawRectangle(Pens.DarkGray, 2*x - margin, y, 2*margin, 2*y);
            e.Graphics.DrawRectangle(Pens.DarkGray, 2*x + margin, y, x - margin, 2*y);

            FontFamily fontFamily = new FontFamily("Arial");
            Font font = new Font(
                       fontFamily,
                       16,
                       FontStyle.Regular,
                       GraphicsUnit.Pixel);
            StringFormat myStringFormat = new StringFormat();

            foreach (var t in addedTexts)
                e.Graphics.DrawString(t.text, font, Brushes.Black, t.x, t.y, myStringFormat);
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            addText = true;
        }

        private void Canvas_MouseEnter(object sender, EventArgs e)
        {
            if (addText)
                this.Cursor = Cursors.Cross;
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


                addedTexts.Add( new addedText { text = addTextBox.Text, x = e.X - (int)textWidth/2, y = e.Y - (int)textHeight / 2 } );

                foreach (var t in addedTexts) 
                    Debug.WriteLine($"{t.text}, {t.x},  {t.y}");
                

                

                foreach (var t in addedTexts)
                    g.DrawString(t.text, font, Brushes.Black, t.x, t.y, myStringFormat);

                addTextBox.Text = "";
                addText = false;
            }
        }
    }

    
}
