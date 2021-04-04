using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.ComponentModel;
using System.Drawing.Drawing2D;



namespace Lab05
{
    public struct text_t
    {
        public string text;
        public int xOff;
        public int yOff;
        public Font font;
        public StringFormat format;
        public Color color;
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
        private static text_t titleCoverText_t { get; set; } = new text_t { color = Color.Black };
        private static text_t authorCoverText_t { get; set; } = new text_t { color = Color.Black };
        private static text_t titleSplineText_t { get; set; } = new text_t { color = Color.Black };
        private static text_t authorSplineText_t { get; set; } = new text_t { color = Color.Black };

        private static readonly System.Text.RegularExpressions.Regex sWhitespace = new System.Text.RegularExpressions.Regex(@"\s+"); 
        private static string ReplaceWhitespace(string input, string replacement) { return sWhitespace.Replace(input, replacement); }

        private static void paintBorders(PaintEventArgs e)
        {
            int xCenter = Canvas.Width / 2;
            int yCenter = Canvas.Height / 2; 
            
            e.Graphics.DrawRectangle(Pens.DarkGray,
                xCenter - bookWidth - spineWidth / 2, yCenter - bookHeight / 2,
                2 * bookWidth + spineWidth, bookHeight
                );

            e.Graphics.FillRectangle(new SolidBrush(Color.Red), xCenter - bookWidth - spineWidth / 2, yCenter - bookHeight / 2,
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

        public static void paintCanvas(PaintEventArgs e)
        {
            paintBorders(e);
            paintCoverText(e);
            paintSplineText(e);
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

        public static void processCoverText(string tag)
        {
            int size = tag.Equals("title") ? 33 : 25;
            int quotient = tag.Equals("title") ? 3 : 6;
            string text = tag.Equals("title") ? titleTextBox.Text : authorTextBox.Text;
            int textWidth, textHeight; 
            Font font;
            Graphics g = Canvas.CreateGraphics();

            do
            {
                font = getFont("Arial", --size);
                textWidth = (int)g.MeasureString(text, font).Width;
                textHeight = (int)g.MeasureString(text, font).Height;
            } while (textHeight > bookHeight / quotient || textWidth > bookWidth);

            StringFormat textFormat = new StringFormat();
            textFormat.Alignment = StringAlignment.Near;
            
            int xOff = (bookWidth + spineWidth - textWidth) / 2;
            int yOff = - bookHeight / 2 + bookHeight / 10 ;
            if (tag.Equals("author")) yOff += bookHeight / 5;

            if (tag.Equals("title"))
                titleCoverText_t = new text_t { text = text, xOff = xOff, yOff = yOff, font = font, format = textFormat };
            else if (tag.Equals("author"))            
                authorCoverText_t = new text_t { text = text, xOff = xOff, yOff = yOff, font = font, format = textFormat };

            Canvas.Refresh();
        }

        private static void paintCoverText(PaintEventArgs e)
        {
            int xCenter = Canvas.Width / 2;
            int yCenter = Canvas.Height / 2;

            // paint title & author
            e.Graphics.DrawString(titleCoverText_t.text, titleCoverText_t.font, Brushes.Black, xCenter + titleCoverText_t.xOff, yCenter + titleCoverText_t.yOff, titleCoverText_t.format);
            e.Graphics.DrawString(authorCoverText_t.text, authorCoverText_t.font, Brushes.Black, xCenter + authorCoverText_t.xOff, yCenter + authorCoverText_t.yOff, authorCoverText_t.format);
        }

        public static void processSplineText(string tag)
        {
            int size = tag.Equals("title") ? 33 : 25;
            string text = tag.Equals("title") ? titleTextBox.Text : authorTextBox.Text;
            text = ReplaceWhitespace(text, " ");
            int textWidth, textHeight;
            Font font;
            Graphics g = Canvas.CreateGraphics();

            do
            {
                font = getFont("Arial", --size);
                textWidth = (int)g.MeasureString(text, font).Width; 
                textHeight = (int)g.MeasureString(text, font).Height;
            } while (textWidth > bookHeight / 2);

            StringFormat textFormat = new StringFormat();
            textFormat.Alignment = StringAlignment.Near;

            int xOff = -textHeight / 2;
            int yOff = ((bookHeight / 4) * (tag.Equals("title") ? 1 : -1)) + textWidth / 2;

            if (tag.Equals("title"))
                titleSplineText_t = new text_t { text = text, xOff = xOff, yOff = yOff, font = font, format = textFormat };
            else if (tag.Equals("author"))
                authorSplineText_t = new text_t { text = text, xOff = xOff, yOff = yOff, font = font, format = textFormat };

            Canvas.Refresh();
        }

        private static void paintSplineText(PaintEventArgs e)
        {
            int xCenter = Canvas.Width / 2;
            int yCenter = Canvas.Height / 2;            

            // paint title
            e.Graphics.RotateTransform(270);
            e.Graphics.TranslateTransform(xCenter + titleSplineText_t.xOff, yCenter + titleSplineText_t.yOff, MatrixOrder.Append);
            e.Graphics.DrawString(titleSplineText_t.text, titleSplineText_t.font, Brushes.Black, 0, 0, titleSplineText_t.format);
            e.Graphics.ResetTransform();

            // paint author
            e.Graphics.RotateTransform(270);
            e.Graphics.TranslateTransform(xCenter + authorSplineText_t.xOff, yCenter + authorSplineText_t.yOff, MatrixOrder.Append);
            e.Graphics.DrawString(authorSplineText_t.text, authorSplineText_t.font, Brushes.Black, 0, 0, authorSplineText_t.format);
            e.Graphics.ResetTransform();
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

                LAddedTexts.Add(new text_t { text = addTextBox.Text, xOff = xPos - xCenter, yOff = yPos - yCenter, font = font, format = format });

                addTextBox.Text = String.Empty;
                addText = false;
                Canvas.Refresh();
            }
        }
        
        public static void paintNewBook(int newWidth, int newHeight, int newSpineWidth)
        {
            bookWidth = newWidth;
            bookHeight = newHeight;
            spineWidth = newSpineWidth;

            LAddedTexts.Clear();
            authorTextBox.Text = String.Empty;
            titleTextBox.Text = String.Empty;            
            addTextBox.Text = String.Empty;
            addText = false;

            Canvas.Refresh();
        }
    }
}
