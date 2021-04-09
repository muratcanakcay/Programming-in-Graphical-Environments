using System.Drawing;
using System.Drawing.Drawing2D;

namespace Lab05
{
    public class Painter  // singleton
    {
        private static Painter Instance { get; set; } = null;
        
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
        private string ReduceWhitespace(string input) { return sWhitespace.Replace(input, " "); }

        public bool AddTextOn { get => addText; }
        public bool MoveTextOn { get => moveText; }
        public bool TextSelected { get => selectedText > -1; }

        private Painter(Point _CanvasCenter)
        {
            Book = Book.GetBook();
            CanvasCenter = _CanvasCenter;
        }
        public static Painter GetPainter(Point _CanvasCenter)
        {
            if (Instance == null) Instance = new Painter(_CanvasCenter);
            return Instance;
        }

        //------------ public methods

        public void paintCanvas(Graphics g)
        {
            paintBorders(g);
            paintCoverText(g);
            paintSpineText(g);
            paintAddedTexts(g);
        }

        public void processTexts(Graphics g, string tag)
        {
            processCoverText(g, tag);
            processSpineText(g, tag);
        }
        public void addNewText(Graphics g, Point cursor, text_t newText)
        {
            SizeF textSize = g.MeasureString(newText.text, getFont("Arial", newText.fontSize));

            // calculate offset from canvas center using cursor position and text alignment
            int yOff = cursor.Y - (int)textSize.Height / 2 - CanvasCenter.Y;
            int xOff = cursor.X - (int)textSize.Width / 2 - CanvasCenter.X;
            if (newText.format.Alignment.Equals(StringAlignment.Center)) xOff += (int)textSize.Width / 2;
            else if (newText.format.Alignment.Equals(StringAlignment.Far)) xOff += (int)textSize.Width;

            // add text to list
            Book.AddedTexts.Add(new text_t { text = newText.text, height = (int)textSize.Height, width = (int)textSize.Width, xOff = xOff, yOff = yOff, fontSize = newText.fontSize, format = newText.format });
            addTextOff();
        }
        public bool findText(Point cursor, out text_t foundText, out int idx)
        {
            Rectangle rect;

            for (int i = 0; i < Book.AddedTexts.Count; i++)
            {
                text_t t = Book.AddedTexts[i];
                rect = getTextRect(t);

                if (rect.Contains(cursor))
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

            // calculate original cursor position using old text offset

            int yCursor = oldText.yOff + oldText.height / 2 + CanvasCenter.Y;
            int xCursor = oldText.xOff + oldText.width / 2 + CanvasCenter.X;

            if (oldText.format.Alignment.Equals(StringAlignment.Center)) xCursor -= oldText.width / 2;
            else if (oldText.format.Alignment.Equals(StringAlignment.Far)) xCursor -= oldText.width;

            // remove old text and add new text
            Book.AddedTexts.RemoveAt(idx);
            addNewText(g, new Point(xCursor, yCursor), newText);
        }
        public void prepareMoveText(Point cursor)
        {
            // remember initial position of cursor and text
            cursorStartLoc = cursor;
            textStartLoc = new Point(Book.AddedTexts[selectedText].xOff, Book.AddedTexts[selectedText].yOff);
        }
        public void moveSelectedText(Point cursor)
        {
            text_t movedText = Book.AddedTexts[selectedText];
            movedText.xOff = textStartLoc.X + cursor.X - cursorStartLoc.X;
            movedText.yOff = textStartLoc.Y + cursor.Y - cursorStartLoc.Y;
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
        private void paintBorders(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Book.BackgroundColor), CanvasCenter.X - Book.BookWidth - Book.SpineWidth / 2, CanvasCenter.Y - Book.BookHeight / 2,
                2 * Book.BookWidth + Book.SpineWidth, Book.BookHeight
                );

            g.DrawRectangle(Pens.DarkGray,
                CanvasCenter.X - Book.BookWidth - Book.SpineWidth / 2, CanvasCenter.Y - Book.BookHeight / 2,
                2 * Book.BookWidth + Book.SpineWidth, Book.BookHeight
                );

            g.DrawLine(Pens.DarkGray,
                CanvasCenter.X - Book.SpineWidth / 2, CanvasCenter.Y - Book.BookHeight / 2,
                CanvasCenter.X - Book.SpineWidth / 2, CanvasCenter.Y + Book.BookHeight / 2
                );

            g.DrawLine(Pens.DarkGray,
                CanvasCenter.X + Book.SpineWidth / 2, CanvasCenter.Y - Book.BookHeight / 2,
                CanvasCenter.X + Book.SpineWidth / 2, CanvasCenter.Y + Book.BookHeight / 2
                );

            SolidBrush selectedTextBrush = new SolidBrush(Color.FromArgb(255 - Book.BackgroundColor.R, 255 - Book.BackgroundColor.G, 255 - Book.BackgroundColor.B));
            Pen selectedTextPen = new Pen(selectedTextBrush);

            if (selectedText > -1) g.DrawRectangle(selectedTextPen, getTextRect(Book.AddedTexts[selectedText]));
        }
        private void paintCoverText(Graphics g)
        {
            // paint title & author on cover
            g.DrawString(titleCoverText_t.text, getFont("Arial", titleCoverText_t.fontSize), new SolidBrush(Book.TextColor), CanvasCenter.X + titleCoverText_t.xOff, CanvasCenter.Y + titleCoverText_t.yOff, titleCoverText_t.format);
            g.DrawString(authorCoverText_t.text, getFont("Arial", authorCoverText_t.fontSize), new SolidBrush(Book.TextColor), CanvasCenter.X + authorCoverText_t.xOff, CanvasCenter.Y + authorCoverText_t.yOff, authorCoverText_t.format);
        }
        private void paintSpineText(Graphics g)
        {
            // paint title on spine
            g.RotateTransform(270);
            g.TranslateTransform(CanvasCenter.X + titleSplineText_t.xOff, CanvasCenter.Y + titleSplineText_t.yOff, MatrixOrder.Append);
            g.DrawString(titleSplineText_t.text, getFont("Arial", titleSplineText_t.fontSize), new SolidBrush(Book.TextColor), 0, 0, titleSplineText_t.format);
            g.ResetTransform();

            // paint author on spine
            g.RotateTransform(270);
            g.TranslateTransform(CanvasCenter.X + authorSplineText_t.xOff, CanvasCenter.Y + authorSplineText_t.yOff, MatrixOrder.Append);
            g.DrawString(authorSplineText_t.text, getFont("Arial", authorSplineText_t.fontSize), new SolidBrush(Book.TextColor), 0, 0, authorSplineText_t.format);
            g.ResetTransform();
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
        private void processSpineText(Graphics g, string tag)
        {
            int fontSize = tag.Equals("title") ? 33 : 25;
            string text = tag.Equals("title") ? Book.Title : Book.Author;
            text = ReduceWhitespace(text);
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
        private void paintAddedTexts(Graphics g)
        {
            foreach (var t in Book.AddedTexts)
                g.DrawString(t.text, getFont("Arial", t.fontSize), new SolidBrush(Book.TextColor), CanvasCenter.X + t.xOff, CanvasCenter.Y + t.yOff, t.format);
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
