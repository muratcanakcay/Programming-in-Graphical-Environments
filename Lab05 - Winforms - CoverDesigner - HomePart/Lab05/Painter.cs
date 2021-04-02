using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;



namespace Lab05
{
    public struct addedText
    {
        public string text;
        public int xOff;
        public int yOff;
    }

    public class Painter
    {
        public static bool addText { get; set; } = false;
        public static int bookWidth { get; private set; } = 300;
        public static int bookHeight { get; private set; } = 500;
        public static int spineWidth { get; private set; } = 30;
        public static List<addedText> LAddedTexts = new();
        public static PictureBox Canvas;
        public static TextBox titleTextBox; 
        public static TextBox authorTextBox;
        public static TextBox addTextBox;

        public static void drawCanvas(PaintEventArgs e)
        {
            int xCenter = Canvas.Width / 2;
            int yCenter = Canvas.Height / 2;

            e.Graphics.DrawRectangle(Pens.DarkGray,
                xCenter - bookWidth - spineWidth / 2, yCenter - bookHeight / 2,
                2 * bookWidth + spineWidth, bookHeight
                );

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

            foreach (var t in Painter.LAddedTexts)
                e.Graphics.DrawString(t.text, font, Brushes.Black, xCenter + t.xOff, yCenter + t.yOff, myStringFormat);
        }

        
        public static void paintString(int xCursor, int yCursor)
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
                int xPos = xCursor - (int)textWidth / 2;
                int yPos = yCursor - (int)textHeight / 2;

                int xCenter = Canvas.Width / 2;
                int yCenter = Canvas.Height / 2;

                int xOff = xPos - xCenter;
                int yOff = yPos - yCenter;

                Painter.LAddedTexts.Add(new addedText { text = addTextBox.Text, xOff = xOff, yOff = yOff });

                addTextBox.Text = "";
                Painter.addText = false;
                Canvas.Refresh();
            }
        }
        
        public static void newBook(int newWidth, int newHeight, int newSpineWidth)
        {
            bookWidth = newWidth;
            bookHeight = newHeight;
            spineWidth = newSpineWidth;

            LAddedTexts.Clear();
            authorTextBox.Text = "";
            titleTextBox.Text = "";            
            addTextBox.Text = "";

            Canvas.Refresh();
        }
    }
}
