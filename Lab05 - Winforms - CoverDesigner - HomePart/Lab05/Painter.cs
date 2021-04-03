using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;



namespace Lab05
{
    public struct text_t
    {
        public string text;
        public int xOff;
        public int yOff;
        public Font font;
        public StringFormat format;
    }

    public class Painter
    {
        public static bool addText { get; set; } = false;
        public static PictureBox Canvas;
        public static TextBox titleTextBox; 
        public static TextBox authorTextBox;
        public static TextBox addTextBox;

        private static int bookWidth { get; set; } = 300;
        private static int bookHeight { get; set; } = 500;
        private static int spineWidth { get; set; } = 30;
        private static List<text_t> LAddedTexts = new();
        private static text_t titleText_t { get;  set; }

        private static void paintBorders(PaintEventArgs e)
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

            foreach (var t in LAddedTexts)
                e.Graphics.DrawString(t.text, t.font, Brushes.Black, xCenter + t.xOff, yCenter + t.yOff, t.format);
        }

        public static void paint(PaintEventArgs e)
        {
            paintBorders(e);
            paintCoverTitle(e);
        }

        private static Font getFont(string type, int size)
        {
            FontFamily fontFamily = new FontFamily(type);
            Font font = new Font(
                       fontFamily,
                       size,
                       FontStyle.Regular,
                       GraphicsUnit.Pixel);
            return font;
        }

        public static void processCoverTitle()
        {
            int fSize = 33, titleWidth, titleHeight; 
            Font font;
            Graphics g = Canvas.CreateGraphics();

            do
            {
                font = getFont("Arial", --fSize);
                titleWidth = (int)g.MeasureString(titleTextBox.Text, font).Width;
                titleHeight = (int)g.MeasureString(titleTextBox.Text, font).Height;
            } while (titleHeight > bookHeight / 3 || titleWidth > bookWidth);

            StringFormat titleFormat = new StringFormat();
            titleFormat.Alignment = StringAlignment.Near;
            
            int xOff = (bookWidth + spineWidth - titleWidth) / 2;
            int yOff = - bookHeight / 2 + bookHeight / 10 ;

            titleText_t = new text_t { text = titleTextBox.Text, xOff = xOff, yOff = yOff, font = font};

            Canvas.Refresh();
        }

        private static void paintCoverTitle(PaintEventArgs e)
        {
            int xCenter = Canvas.Width / 2;
            int yCenter = Canvas.Height / 2; 
            e.Graphics.DrawString(titleText_t.text, titleText_t.font, Brushes.Black, xCenter + titleText_t.xOff, yCenter + titleText_t.yOff, titleText_t.format);
        }

        
        public static void paintString(int xCursor, int yCursor)
        {
            if (addText)
            {
                Graphics g = Canvas.CreateGraphics();

                Font font = getFont("Arial", 16);
                StringFormat format = new StringFormat();

                int textWidth = (int)g.MeasureString(addTextBox.Text, font).Width;
                int textHeight = (int)g.MeasureString(addTextBox.Text, font).Height;
                int xPos = xCursor - textWidth / 2;
                int yPos = yCursor - textHeight / 2;

                int xCenter = Canvas.Width / 2;
                int yCenter = Canvas.Height / 2;

                int xOff = xPos - xCenter;
                int yOff = yPos - yCenter;

                LAddedTexts.Add(new text_t { text = addTextBox.Text, xOff = xOff, yOff = yOff, font = font, format = format });

                addTextBox.Text = "";
                addText = false;
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
