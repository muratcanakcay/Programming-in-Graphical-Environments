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
    public class Painter
    {
        public static bool addText { get; set; } = false;
        public static PictureBox Canvas;
        public static TextBox titleTextBox; 
        public static TextBox authorTextBox;

        private static text_t titleCoverText_t { get; set; } 
        private static text_t authorCoverText_t { get; set; }
        private static text_t titleSplineText_t { get; set; }
        private static text_t authorSplineText_t { get; set; }

        private static readonly System.Text.RegularExpressions.Regex sWhitespace = new System.Text.RegularExpressions.Regex(@"\s+"); 
        private static string ReplaceWhitespace(string input, string replacement) { return sWhitespace.Replace(input, replacement); }
        
        //------------ public methods
        
        public static void paintCanvas(PaintEventArgs e)
        {
            paintBorders(e);
            paintCoverText(e);
            paintSplineText(e);
        }

        public static void processTexts(string tag)
        {
            processCoverText(tag);
            processSplineText(tag);
        }

        public static void paintString(int xCursor, int yCursor)
        {
            if (addText)
            {
                Graphics g = Canvas.CreateGraphics();

                Font font = getFont("Arial", 16);
                StringFormat format = new StringFormat();

                int textWidth = (int)g.MeasureString("text", font).Width;
                int textHeight = (int)g.MeasureString("text", font).Height;
                int xPos = xCursor - textWidth / 2;
                int yPos = yCursor - textHeight / 2;
                int xCenter = Canvas.Width / 2;
                int yCenter = Canvas.Height / 2;

                Book.AddedTexts.Add(new text_t { text = "text", xOff = xPos - xCenter, yOff = yPos - yCenter, font = font, format = format });

                addText = false;
                Canvas.Refresh();
            }
        }

        public static void paintNewBook()
        {
            titleTextBox.Text = String.Empty;
            authorTextBox.Text = String.Empty;
            addText = false;

            Canvas.Refresh();
        }

        //------------ private methods

        private static void paintBorders(PaintEventArgs e)
        {
            int xCenter = Canvas.Width / 2;
            int yCenter = Canvas.Height / 2; 
            
            e.Graphics.FillRectangle(new SolidBrush(Book.BackgroundColor), xCenter - Book.BookWidth - Book.SpineWidth / 2, yCenter - Book.BookHeight / 2,
                2 * Book.BookWidth + Book.SpineWidth, Book.BookHeight 
                );

            e.Graphics.DrawRectangle(Pens.DarkGray,
                xCenter - Book.BookWidth - Book.SpineWidth / 2, yCenter - Book.BookHeight / 2,
                2 * Book.BookWidth + Book.SpineWidth, Book.BookHeight
                );

            e.Graphics.DrawLine(Pens.DarkGray,
                xCenter - Book.SpineWidth / 2, yCenter - Book.BookHeight / 2,
                xCenter - Book.SpineWidth / 2, yCenter + Book.BookHeight / 2
                );

            e.Graphics.DrawLine(Pens.DarkGray,
                xCenter + Book.SpineWidth / 2, yCenter - Book.BookHeight / 2,
                xCenter + Book.SpineWidth / 2, yCenter + Book.BookHeight / 2
                );

            foreach (var t in Book.AddedTexts)
                e.Graphics.DrawString(t.text, t.font, Brushes.Black, xCenter + t.xOff, yCenter + t.yOff, t.format);
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

        private static void processCoverText(string tag)
        {
            int size = tag.Equals("title") ? 33 : 25;
            int quotient = tag.Equals("title") ? 3 : 6;
            string text = tag.Equals("title") ? Book.Title : Book.Author;
            int textWidth, textHeight;
            Font font;
            Graphics g = Canvas.CreateGraphics();

            do
            {
                font = getFont("Arial", --size);
                textWidth = (int)g.MeasureString(text, font).Width;
                textHeight = (int)g.MeasureString(text, font).Height;
            } while (textHeight > Book.BookHeight / quotient || textWidth > Book.BookWidth);

            StringFormat textFormat = new StringFormat();
            textFormat.Alignment = StringAlignment.Near;

            int xOff = (Book.BookWidth + Book.SpineWidth - textWidth) / 2;
            int yOff = -Book.BookHeight / 2 + Book.BookHeight / 10;
            if (tag.Equals("author")) yOff += Book.BookHeight / 5;

            text_t newText_t = new text_t { text = text, xOff = xOff, yOff = yOff, font = font, format = textFormat };

            if (tag.Equals("title"))
                titleCoverText_t = newText_t;
            else if (tag.Equals("author"))
                authorCoverText_t = newText_t;

            Canvas.Refresh();
        }

        private static void paintCoverText(PaintEventArgs e)
        {
            int xCenter = Canvas.Width / 2;
            int yCenter = Canvas.Height / 2;

            // paint title & author
            e.Graphics.DrawString(titleCoverText_t.text, titleCoverText_t.font, new SolidBrush(Book.TextColor), xCenter + titleCoverText_t.xOff, yCenter + titleCoverText_t.yOff, titleCoverText_t.format);
            e.Graphics.DrawString(authorCoverText_t.text, authorCoverText_t.font, new SolidBrush(Book.TextColor), xCenter + authorCoverText_t.xOff, yCenter + authorCoverText_t.yOff, authorCoverText_t.format);
        }

        private static void processSplineText(string tag)
        {
            int size = tag.Equals("title") ? 33 : 25;
            string text = tag.Equals("title") ? Book.Title : Book.Author;
            text = ReplaceWhitespace(text, " ");
            int textWidth, textHeight;
            Font font;
            Graphics g = Canvas.CreateGraphics();

            do
            {
                font = getFont("Arial", --size);
                textWidth = (int)g.MeasureString(text, font).Width;
                textHeight = (int)g.MeasureString(text, font).Height;
            } while (textWidth > Book.BookHeight / 2 || textHeight > Book.SpineWidth);

            StringFormat textFormat = new StringFormat();
            textFormat.Alignment = StringAlignment.Near;

            int xOff = -textHeight / 2;
            int yOff = ((Book.BookHeight / 4) * (tag.Equals("title") ? 1 : -1)) + textWidth / 2;

            text_t newText_t = new text_t { text = text, xOff = xOff, yOff = yOff, font = font, format = textFormat};

            if (tag.Equals("title"))
                titleSplineText_t = newText_t;
            else if (tag.Equals("author"))
                authorSplineText_t = newText_t;

            Canvas.Refresh();
        }

        private static void paintSplineText(PaintEventArgs e)
        {
            int xCenter = Canvas.Width / 2;
            int yCenter = Canvas.Height / 2;

            // paint title
            e.Graphics.RotateTransform(270);
            e.Graphics.TranslateTransform(xCenter + titleSplineText_t.xOff, yCenter + titleSplineText_t.yOff, MatrixOrder.Append);
            e.Graphics.DrawString(titleSplineText_t.text, titleSplineText_t.font, new SolidBrush(Book.TextColor), 0, 0, titleSplineText_t.format);
            e.Graphics.ResetTransform();

            // paint author
            e.Graphics.RotateTransform(270);
            e.Graphics.TranslateTransform(xCenter + authorSplineText_t.xOff, yCenter + authorSplineText_t.yOff, MatrixOrder.Append);
            e.Graphics.DrawString(authorSplineText_t.text, authorSplineText_t.font, new SolidBrush(Book.TextColor), 0, 0, authorSplineText_t.format);
            e.Graphics.ResetTransform();
        }
    }
}
