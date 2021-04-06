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
        private bool addText = false;
        public bool AddTextOn { get => addText; }
        private int selectedText = -1;
        public bool textSelected { get => selectedText > -1; }
        private bool moveText = false;
        public bool MoveTextOn { get => moveText; }

        private int xCenter { get; set; }
        private int yCenter { get; set; }
        private Point cursorStartLoc { get; set; }
        private Point textStartLoc { get; set; }

        private PictureBox Canvas { get; }
        private TextBox titleTextBox { get; }
        private TextBox authorTextBox { get; }
        private Book Book { get; }

        private text_t titleCoverText_t { get; set; } = new text_t { fontSize = 1 };
        private text_t authorCoverText_t { get; set; } = new text_t { fontSize = 1 };
        private text_t titleSplineText_t { get; set; } = new text_t { fontSize = 1 };
        private text_t authorSplineText_t { get; set; } = new text_t { fontSize = 1 };
        
        

        private readonly System.Text.RegularExpressions.Regex sWhitespace = new System.Text.RegularExpressions.Regex(@"\s+"); 
        private string ReplaceWhitespace(string input, string replacement) { return sWhitespace.Replace(input, replacement); }

        public Painter(Book _book, PictureBox _canvas, TextBox _titleTextBox, TextBox _authorTextBox)
        {
            Book = _book;
            Canvas = _canvas;
            authorTextBox = _authorTextBox;
            titleTextBox = _titleTextBox;

            xCenter = Canvas.Width / 2; 
            yCenter = Canvas.Height / 2;
        }

        //------------ public methods

        public void paintCanvas(PaintEventArgs e)
        {
            xCenter = Canvas.Width / 2;
            yCenter = Canvas.Height / 2;

            paintBorders(e);
            paintCoverText(e);
            paintSplineText(e);
            paintAddedTexts(e);
        }

        public void processTexts(string tag)
        {
            processCoverText(tag);
            processSplineText(tag);
        }
        public void addNewText(int xCursor, int yCursor, text_t newText)
        {
            Graphics g = Canvas.CreateGraphics();

            int fontSize = newText.fontSize;
            Font font = getFont("Arial", fontSize);
            StringFormat format = newText.format;
                
            string text = newText.text;

            int textWidth = (int)g.MeasureString(text, font).Width;
            int textHeight = (int)g.MeasureString(text, font).Height;
            int yPos = yCursor - textHeight / 2;

            int xPos = xCursor - textWidth / 2;
            if (format.Alignment == StringAlignment.Center) xPos += textWidth / 2;
            if (format.Alignment == StringAlignment.Far) xPos += textWidth;

            int xOff = xPos - xCenter;
            int yOff = yPos - yCenter;

            Book.AddedTexts.Add(new text_t { text = text, height = textHeight, width = textWidth, xOff = xOff, yOff = yOff, fontSize = fontSize, format = format });

            addTextOff();
            Canvas.Refresh();            
        }

        public bool findText(MouseEventArgs e, out text_t foundText, out int idx)
        {
            Point mouse = e.Location;
            Rectangle rect;

            for (int i = 0; i < Book.AddedTexts.Count; i++)
            {
                text_t t = Book.AddedTexts[i];
                rect = getTextRect(t);
                
                if (rect.Contains(mouse))
                {
                    foundText = t;
                    idx = i;
                    return true;
                }
            }
            
            idx = -1;
            foundText = new text_t();
            return false;
        }
        public void modifyOldText(int idx, text_t newText)
        {
            text_t oldText = Book.AddedTexts[idx];
            int xCursor = oldText.xOff + xCenter + oldText.width / 2;
            if (oldText.format.Alignment == StringAlignment.Center) xCursor -= oldText.width / 2;
            if (oldText.format.Alignment == StringAlignment.Far) xCursor -= oldText.width;
            int yCursor = oldText.yOff + yCenter + oldText.height / 2;
            
            Book.AddedTexts.RemoveAt(idx);
            addNewText(xCursor, yCursor, newText);
        }
        public void selectText(int idx)
        {
            selectedText = idx;
            Canvas.Refresh();
        }
        public void prepareMoveText(MouseEventArgs e)
        {
            cursorStartLoc = e.Location;
            textStartLoc = new Point(Book.AddedTexts[selectedText].xOff, Book.AddedTexts[selectedText].yOff);
        }
        public void moveSelectedText(MouseEventArgs e)
        {
            text_t movedText = Book.AddedTexts[selectedText];
            movedText.xOff = textStartLoc.X + e.X - cursorStartLoc.X;
            movedText.yOff = textStartLoc.Y + e.Y - cursorStartLoc.Y; 
            Book.AddedTexts[selectedText] = movedText;
            Canvas.Refresh();
        }


        public void paintNewBook()
        {
            titleTextBox.Text = String.Empty;
            authorTextBox.Text = String.Empty;
            //addTextOff();

            Canvas.Refresh();
        }
        
        public void addTextOn() { addText = true; }
        public void addTextOff() { addText = false; }
        public void moveTextOn() { moveText = true; }
        public void moveTextOff() { moveText = false; }


        //------------ private methods

        private Font getFont(string type, int size)
        {
            FontFamily fontFamily = new FontFamily(type);
            Font font = new Font(
                       fontFamily,
                       size,
                       FontStyle.Regular,
                       GraphicsUnit.Pixel);
            return font;
        }
        private void paintBorders(PaintEventArgs e)
        {
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

            SolidBrush selectedTextBrush = new SolidBrush(Color.FromArgb(255 - Book.BackgroundColor.R, 255 - Book.BackgroundColor.G, 255 - Book.BackgroundColor.B));
            Pen selectedTextPen = new Pen(selectedTextBrush);
            
            if(selectedText > -1) e.Graphics.DrawRectangle(selectedTextPen, getTextRect(Book.AddedTexts[selectedText]));
        }
        private void processCoverText(string tag)
        {
            int fontSize = tag.Equals("title") ? 33 : 25;
            int quotient = tag.Equals("title") ? 3 : 6;
            string text = tag.Equals("title") ? Book.Title : Book.Author;
            int textWidth, textHeight;
            Font font;
            Graphics g = Canvas.CreateGraphics();

            do
            {
                font = getFont("Arial", --fontSize);
                textWidth = (int)g.MeasureString(text, font).Width;
                textHeight = (int)g.MeasureString(text, font).Height;
            } while (textHeight > Book.BookHeight / quotient || textWidth > Book.BookWidth);

            StringFormat textFormat = new StringFormat();
            textFormat.Alignment = StringAlignment.Near;

            int xOff = (Book.BookWidth + Book.SpineWidth - textWidth) / 2;
            int yOff = -Book.BookHeight / 2 + Book.BookHeight / 10;
            if (tag.Equals("author")) yOff += Book.BookHeight / 5;

            text_t newText_t = new text_t { text = text, xOff = xOff, yOff = yOff, fontSize = fontSize, format = textFormat };

            if (tag.Equals("title"))
                titleCoverText_t = newText_t;
            else if (tag.Equals("author"))
                authorCoverText_t = newText_t;

            Canvas.Refresh();
        }
        private void paintCoverText(PaintEventArgs e)
        {
            // paint title & author
            e.Graphics.DrawString(titleCoverText_t.text, getFont("Arial", titleCoverText_t.fontSize), new SolidBrush(Book.TextColor), xCenter + titleCoverText_t.xOff, yCenter + titleCoverText_t.yOff, titleCoverText_t.format);
            e.Graphics.DrawString(authorCoverText_t.text, getFont("Arial", authorCoverText_t.fontSize), new SolidBrush(Book.TextColor), xCenter + authorCoverText_t.xOff, yCenter + authorCoverText_t.yOff, authorCoverText_t.format);
        }
        private void processSplineText(string tag)
        {
            int fontSize = tag.Equals("title") ? 33 : 25;
            string text = tag.Equals("title") ? Book.Title : Book.Author;
            text = ReplaceWhitespace(text, " ");
            int textWidth, textHeight;
            Font font;
            Graphics g = Canvas.CreateGraphics();

            do
            {
                font = getFont("Arial", --fontSize);
                textWidth = (int)g.MeasureString(text, font).Width;
                textHeight = (int)g.MeasureString(text, font).Height;
            } while (textWidth > Book.BookHeight / 2 || textHeight > Book.SpineWidth);

            StringFormat textFormat = new StringFormat();
            textFormat.Alignment = StringAlignment.Near;

            int xOff = -textHeight / 2;
            int yOff = ((Book.BookHeight / 4) * (tag.Equals("title") ? 1 : -1)) + textWidth / 2;

            text_t newText_t = new text_t { text = text, xOff = xOff, yOff = yOff, fontSize = fontSize, format = textFormat};

            if (tag.Equals("title"))
                titleSplineText_t = newText_t;
            else if (tag.Equals("author"))
                authorSplineText_t = newText_t;

            Canvas.Refresh();
        }
        private void paintSplineText(PaintEventArgs e)
        {
            // paint title
            e.Graphics.RotateTransform(270);
            e.Graphics.TranslateTransform(xCenter + titleSplineText_t.xOff, yCenter + titleSplineText_t.yOff, MatrixOrder.Append);
            e.Graphics.DrawString(titleSplineText_t.text, getFont("Arial", titleSplineText_t.fontSize), new SolidBrush(Book.TextColor), 0, 0, titleSplineText_t.format);
            e.Graphics.ResetTransform();

            // paint author
            e.Graphics.RotateTransform(270);
            e.Graphics.TranslateTransform(xCenter + authorSplineText_t.xOff, yCenter + authorSplineText_t.yOff, MatrixOrder.Append);
            e.Graphics.DrawString(authorSplineText_t.text, getFont("Arial", authorSplineText_t.fontSize), new SolidBrush(Book.TextColor), 0, 0, authorSplineText_t.format);
            e.Graphics.ResetTransform();
        }
        private void paintAddedTexts(PaintEventArgs e)
        {
            foreach (var t in Book.AddedTexts)
                e.Graphics.DrawString(t.text, getFont("Arial", t.fontSize), Brushes.Black, xCenter + t.xOff, yCenter + t.yOff, t.format);
        }
        private Rectangle getTextRect(text_t t)
        {
            Rectangle rect = new(); 
            
            rect.X = xCenter + t.xOff;
            if (t.format.Alignment == StringAlignment.Center) rect.X -= t.width / 2;
            if (t.format.Alignment == StringAlignment.Far) rect.X -= t.width;

            rect.Y = yCenter + t.yOff;
            rect.Width = t.width;
            rect.Height = t.height;

            return rect;
        }





    }
}
