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
        private bool moveText = false;
        private int selectedText = -1;

        private Book Book { get; }

        private Point CanvasCenter { get; set; }
        private Point cursorStartLoc { get; set; }
        private Point textStartLoc { get; set; }

        private text_t titleCoverText_t { get; set; } = new text_t { fontSize = 1 };
        private text_t authorCoverText_t { get; set; } = new text_t { fontSize = 1 };
        private text_t titleSplineText_t { get; set; } = new text_t { fontSize = 1 };
        private text_t authorSplineText_t { get; set; } = new text_t { fontSize = 1 };

        private readonly System.Text.RegularExpressions.Regex sWhitespace = new System.Text.RegularExpressions.Regex(@"\s+");
        private string ReplaceWhitespace(string input, string replacement) { return sWhitespace.Replace(input, replacement); }

        public bool AddTextOn { get => addText; }
        public bool MoveTextOn { get => moveText; }
        public bool TextSelected { get => selectedText > -1; }

        public Painter(Book _book, Point _CanvasCenter)
        {
            Book = _book;
            CanvasCenter = _CanvasCenter;
        }

        //------------ public methods

        public void paintCanvas(PaintEventArgs e)
        {
            paintBorders(e);
            paintCoverText(e);
            paintSpineText(e);
            paintAddedTexts(e);
        }

        public void processTexts(Graphics g, string tag)
        {
            processCoverText(g, tag);
            processSplineText(g, tag);
        }
        public void addNewText(Graphics g, int xCursor, int yCursor, text_t newText)
        {
            SizeF textSize = g.MeasureString(newText.text, getFont("Arial", newText.fontSize));

            // calculate offset from canvas center
            int yOff = yCursor - (int)textSize.Height / 2 - CanvasCenter.Y;
            int xOff = xCursor - (int)textSize.Width / 2 - CanvasCenter.X;

            if (newText.format.Alignment == StringAlignment.Center) xOff += (int)textSize.Width / 2;
            else if (newText.format.Alignment == StringAlignment.Far) xOff += (int)textSize.Width;

            // add text to list
            Book.AddedTexts.Add(new text_t { text = newText.text, height = (int)textSize.Height, width = (int)textSize.Width, xOff = xOff, yOff = yOff, fontSize = newText.fontSize, format = newText.format });
            addTextOff();
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
        public void modifyOldText(Graphics g, int idx, text_t newText)
        {
            text_t oldText = Book.AddedTexts[idx];
            
            // calculate cursor position from text offset
            int xCursor = oldText.xOff + CanvasCenter.X + oldText.width / 2;
            int yCursor = oldText.yOff + CanvasCenter.Y + oldText.height / 2;
            if (oldText.format.Alignment == StringAlignment.Center) xCursor -= oldText.width / 2;
            else if (oldText.format.Alignment == StringAlignment.Far) xCursor -= oldText.width;

            // remove old text and add new text
            Book.AddedTexts.RemoveAt(idx);
            addNewText(g, xCursor, yCursor, newText);
        }
        public void prepareMoveText(MouseEventArgs e)
        {
            // remember initial position of text
            cursorStartLoc = e.Location;
            textStartLoc = new Point(Book.AddedTexts[selectedText].xOff, Book.AddedTexts[selectedText].yOff);
        }
        public void moveSelectedText(MouseEventArgs e)
        {
            text_t movedText = Book.AddedTexts[selectedText];
            movedText.xOff = textStartLoc.X + e.X - cursorStartLoc.X;
            movedText.yOff = textStartLoc.Y + e.Y - cursorStartLoc.Y;
            Book.AddedTexts[selectedText] = movedText;
        }
        public void deleteSelectedText()
        {
            Book.AddedTexts.RemoveAt(selectedText);
            selectedText = -1;
        }

        public void addTextOn() => addText = true;
        public void addTextOff() => addText = false;
        public void moveTextOn() => moveText = true;
        public void moveTextOff() => moveText = false;
        public void selectText(int idx) => selectedText = idx;
        public void changeCenter(Point newCenter) => CanvasCenter = newCenter;

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
            e.Graphics.FillRectangle(new SolidBrush(Book.BackgroundColor), CanvasCenter.X - Book.BookWidth - Book.SpineWidth / 2, CanvasCenter.Y - Book.BookHeight / 2,
                2 * Book.BookWidth + Book.SpineWidth, Book.BookHeight
                );

            e.Graphics.DrawRectangle(Pens.DarkGray,
                CanvasCenter.X - Book.BookWidth - Book.SpineWidth / 2, CanvasCenter.Y - Book.BookHeight / 2,
                2 * Book.BookWidth + Book.SpineWidth, Book.BookHeight
                );

            e.Graphics.DrawLine(Pens.DarkGray,
                CanvasCenter.X - Book.SpineWidth / 2, CanvasCenter.Y - Book.BookHeight / 2,
                CanvasCenter.X - Book.SpineWidth / 2, CanvasCenter.Y + Book.BookHeight / 2
                );

            e.Graphics.DrawLine(Pens.DarkGray,
                CanvasCenter.X + Book.SpineWidth / 2, CanvasCenter.Y - Book.BookHeight / 2,
                CanvasCenter.X + Book.SpineWidth / 2, CanvasCenter.Y + Book.BookHeight / 2
                );

            SolidBrush selectedTextBrush = new SolidBrush(Color.FromArgb(255 - Book.BackgroundColor.R, 255 - Book.BackgroundColor.G, 255 - Book.BackgroundColor.B));
            Pen selectedTextPen = new Pen(selectedTextBrush);

            if (selectedText > -1) e.Graphics.DrawRectangle(selectedTextPen, getTextRect(Book.AddedTexts[selectedText]));
        }
        private void processCoverText(Graphics g, string tag)
        {
            int fontSize = tag.Equals("title") ? 33 : 25;
            int quotient = tag.Equals("title") ? 3 : 6;
            string text = tag.Equals("title") ? Book.Title : Book.Author;
            SizeF textSize;

            // find right font size
            do { textSize = g.MeasureString(text, getFont("Arial", --fontSize)); }
            while ((int)textSize.Height > Book.BookHeight / quotient || (int)textSize.Width > Book.BookWidth);

            StringFormat textFormat = new StringFormat();
            textFormat.Alignment = StringAlignment.Near;

            // calculate offset from canvas center
            int xOff = (Book.BookWidth + Book.SpineWidth - (int)textSize.Width) / 2;
            int yOff = Book.BookHeight / 10 - Book.BookHeight / 2;
            if (tag.Equals("author")) yOff += titleCoverText_t.height + Book.BookHeight / 20; // author text should not overlap title text

            text_t newText_t = new text_t { text = text, xOff = xOff, yOff = yOff, width = (int)textSize.Width, height = (int)textSize.Height, fontSize = fontSize, format = textFormat };

            // assign text to its field
            if (tag.Equals("title"))
            {
                titleCoverText_t = newText_t;
                processCoverText(g, "author"); // also process author text so that it moves down if needed
            }
            else authorCoverText_t = newText_t;
        }
        private void paintCoverText(PaintEventArgs e)
        {
            // paint title & author on cover
            e.Graphics.DrawString(titleCoverText_t.text, getFont("Arial", titleCoverText_t.fontSize), new SolidBrush(Book.TextColor), CanvasCenter.X + titleCoverText_t.xOff, CanvasCenter.Y + titleCoverText_t.yOff, titleCoverText_t.format);
            e.Graphics.DrawString(authorCoverText_t.text, getFont("Arial", authorCoverText_t.fontSize), new SolidBrush(Book.TextColor), CanvasCenter.X + authorCoverText_t.xOff, CanvasCenter.Y + authorCoverText_t.yOff, authorCoverText_t.format);
        }
        private void processSplineText(Graphics g, string tag)
        {
            int fontSize = tag.Equals("title") ? 33 : 25;
            string text = tag.Equals("title") ? Book.Title : Book.Author;
            text = ReplaceWhitespace(text, " ");
            SizeF textSize;

            // find right font size
            do { textSize = g.MeasureString(text, getFont("Arial", --fontSize)); } 
            while (textSize.Width > Book.BookHeight / 2 || textSize.Height > Book.SpineWidth);

            StringFormat textFormat = new StringFormat();
            textFormat.Alignment = StringAlignment.Near;

            // calculate offset from canvas center
            int xOff = -(int)textSize.Height / 2;
            int yOff = ((Book.BookHeight / 4) * (tag.Equals("title") ? 1 : -1)) + (int)textSize.Width / 2;

            text_t newText_t = new text_t { text = text, xOff = xOff, yOff = yOff, width = (int)textSize.Width, height = (int)textSize.Height, fontSize = fontSize, format = textFormat };

            // assign text to its field
            if (tag.Equals("title"))
                titleSplineText_t = newText_t;
            else if (tag.Equals("author"))
                authorSplineText_t = newText_t;
        }
        private void paintSpineText(PaintEventArgs e)
        {
            // paint title on spine
            e.Graphics.RotateTransform(270);
            e.Graphics.TranslateTransform(CanvasCenter.X + titleSplineText_t.xOff, CanvasCenter.Y + titleSplineText_t.yOff, MatrixOrder.Append);
            e.Graphics.DrawString(titleSplineText_t.text, getFont("Arial", titleSplineText_t.fontSize), new SolidBrush(Book.TextColor), 0, 0, titleSplineText_t.format);
            e.Graphics.ResetTransform();

            // paint author on spine
            e.Graphics.RotateTransform(270);
            e.Graphics.TranslateTransform(CanvasCenter.X + authorSplineText_t.xOff, CanvasCenter.Y + authorSplineText_t.yOff, MatrixOrder.Append);
            e.Graphics.DrawString(authorSplineText_t.text, getFont("Arial", authorSplineText_t.fontSize), new SolidBrush(Book.TextColor), 0, 0, authorSplineText_t.format);
            e.Graphics.ResetTransform();
        }
        private void paintAddedTexts(PaintEventArgs e)
        {
            foreach (var t in Book.AddedTexts)
                e.Graphics.DrawString(t.text, getFont("Arial", t.fontSize), Brushes.Black, CanvasCenter.X + t.xOff, CanvasCenter.Y + t.yOff, t.format);
        }
        private Rectangle getTextRect(text_t text)
        {
            Rectangle rect = new();

            rect.X = CanvasCenter.X + text.xOff;
            rect.Y = CanvasCenter.Y + text.yOff;
            rect.Width = text.width;
            rect.Height = text.height;

            if (text.format.Alignment == StringAlignment.Center) rect.X -= text.width / 2;
            if (text.format.Alignment == StringAlignment.Far) rect.X -= text.width;

            return rect;
        }
    }
}
