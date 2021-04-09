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
        private readonly System.Text.RegularExpressions.Regex sWhitespace = new System.Text.RegularExpressions.Regex(@"\s+");
                
        private Book Book { get; }
        private Point CanvasCenter { get; set; }
        private Point CursorStartLoc { get; set; }
        private Point TextStartLoc { get; set; }
        private text_t TitleCoverText_t { get; set; } = new text_t { fontSize = 1 };
        private text_t AuthorCoverText_t { get; set; } = new text_t { fontSize = 1 };
        private text_t TitleSpineText_t { get; set; } = new text_t { fontSize = 1 };
        private text_t AuthorSpineText_t { get; set; } = new text_t { fontSize = 1 };

        public bool IsAddTextOn { get => addText; }
        public bool IsMoveTextOn { get => moveText; }
        public bool IsTextSelected { get => selectedText > -1; }

        //------------ c-tor

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

        public void PaintCanvas(Graphics g)
        {
            PaintBorders(g);
            PaintCoverText(g);
            PaintSpineText(g);
            PaintAddedTexts(g);
        }

        public void ProcessTexts(Graphics g, string tag)
        {
            ProcessCoverText(g, tag);
            ProcessSpineText(g, tag);
        }
        public void AddNewText(Graphics g, Point cursor, text_t newText)
        {
            SizeF textSize = g.MeasureString(newText.text, GetFont("Arial", newText.fontSize));

            // calculate offset from canvas center using cursor position and text alignment
            int yOff = cursor.Y - (int)textSize.Height / 2 - CanvasCenter.Y;
            int xOff = cursor.X - (int)textSize.Width / 2 - CanvasCenter.X;
            if (newText.format.Alignment.Equals(StringAlignment.Center)) xOff += (int)textSize.Width / 2;
            else if (newText.format.Alignment.Equals(StringAlignment.Far)) xOff += (int)textSize.Width;

            // add text to list
            Book.AddedTexts.Add(new text_t { text = newText.text, height = (int)textSize.Height, width = (int)textSize.Width, xOff = xOff, yOff = yOff, fontSize = newText.fontSize, format = newText.format });
            AddTextOff();
        }
        public bool FindText(Point cursor, out text_t foundText, out int idx)
        {
            Rectangle rect;

            for (int i = 0; i < Book.AddedTexts.Count; i++)
            {
                text_t t = Book.AddedTexts[i];
                rect = GetTextRect(t);

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
        public void ModifyOldText(Graphics g, int idx, text_t newText)
        {
            // calculate original cursor position using old text's offset
            text_t oldText = Book.AddedTexts[idx];
            Point cursorPos = GetCursorfromOffset(oldText);

            // remove old text and add new text
            Book.AddedTexts.RemoveAt(idx);
            AddNewText(g, cursorPos, newText);
        }

        public void PrepareMoveText(Point cursor)
        {
            // remember initial position of cursor and text
            CursorStartLoc = cursor;
            TextStartLoc = new Point(Book.AddedTexts[selectedText].xOff, Book.AddedTexts[selectedText].yOff);
        }
        public void MoveSelectedText(Point cursor)
        {
            text_t movedText = Book.AddedTexts[selectedText];
            movedText.xOff = TextStartLoc.X + cursor.X - CursorStartLoc.X;
            movedText.yOff = TextStartLoc.Y + cursor.Y - CursorStartLoc.Y;
            Book.AddedTexts[selectedText] = movedText;
        }
        public void DeleteSelectedText()
        {
            Book.AddedTexts.RemoveAt(selectedText);
            selectedText = -1;
        }

        public void AddTextOn() => addText = true;
        public void AddTextOff() => addText = false;
        public void MoveTextOn() => moveText = true;
        public void MoveTextOff() => moveText = false;
        public void SelectText(int idx) => selectedText = idx;
        public void UpdateCenter(Point newCenter) => CanvasCenter = newCenter;

        //------------ private methods

        private void ProcessCoverText(Graphics g, string tag)
        {
            int fontSize = tag.Equals("title") ? 33 : 25;
            int quotient = tag.Equals("title") ? 3 : 6;
            string text = tag.Equals("title") ? Book.Title : Book.Author;
            SizeF textSize;

            // find right font size
            do { textSize = g.MeasureString(text, GetFont("Arial", --fontSize)); }
            while ((int)textSize.Height > Book.BookHeight / quotient || (int)textSize.Width > Book.BookWidth);

            StringFormat textFormat = new StringFormat();
            textFormat.Alignment = StringAlignment.Near;

            // calculate offset from canvas center
            int xOff = (Book.BookWidth + Book.SpineWidth - (int)textSize.Width) / 2;
            int yOff = Book.BookHeight / 10 - Book.BookHeight / 2;
            if (tag.Equals("author")) yOff += TitleCoverText_t.height + Book.BookHeight / 20; // author text should not overlap title text

            text_t newText_t = new text_t { text = text, xOff = xOff, yOff = yOff, width = (int)textSize.Width, height = (int)textSize.Height, fontSize = fontSize, format = textFormat };

            // assign text to its field
            if (tag.Equals("title"))
            {
                TitleCoverText_t = newText_t;
                ProcessCoverText(g, "author"); // also process author text so that it moves down if needed
            }
            else AuthorCoverText_t = newText_t;
        }
        private void ProcessSpineText(Graphics g, string tag)
        {
            int fontSize = tag.Equals("title") ? 33 : 25;
            string text = tag.Equals("title") ? Book.Title : Book.Author;
            text = ReduceWhitespace(text);
            SizeF textSize;

            // find right font size
            do { textSize = g.MeasureString(text, GetFont("Arial", --fontSize)); }
            while (textSize.Width > Book.BookHeight / 2 || textSize.Height > Book.SpineWidth);

            StringFormat textFormat = new StringFormat();
            textFormat.Alignment = StringAlignment.Near;

            // calculate offset from canvas center
            int xOff = -(int)textSize.Height / 2;
            int yOff = ((Book.BookHeight / 4) * (tag.Equals("title") ? 1 : -1)) + (int)textSize.Width / 2;

            text_t newText_t = new text_t { text = text, xOff = xOff, yOff = yOff, width = (int)textSize.Width, height = (int)textSize.Height, fontSize = fontSize, format = textFormat };

            // assign text to its field
            if (tag.Equals("title"))
                TitleSpineText_t = newText_t;
            else if (tag.Equals("author"))
                AuthorSpineText_t = newText_t;
        }
        private void PaintCoverText(Graphics g)
        {
            // paint title & author on cover
            g.DrawString(TitleCoverText_t.text, GetFont("Arial", TitleCoverText_t.fontSize), new SolidBrush(Book.TextColor), CanvasCenter.X + TitleCoverText_t.xOff, CanvasCenter.Y + TitleCoverText_t.yOff, TitleCoverText_t.format);
            g.DrawString(AuthorCoverText_t.text, GetFont("Arial", AuthorCoverText_t.fontSize), new SolidBrush(Book.TextColor), CanvasCenter.X + AuthorCoverText_t.xOff, CanvasCenter.Y + AuthorCoverText_t.yOff, AuthorCoverText_t.format);
        }
        private void PaintSpineText(Graphics g)
        {
            // paint title on spine
            g.RotateTransform(270);
            g.TranslateTransform(CanvasCenter.X + TitleSpineText_t.xOff, CanvasCenter.Y + TitleSpineText_t.yOff, MatrixOrder.Append);
            g.DrawString(TitleSpineText_t.text, GetFont("Arial", TitleSpineText_t.fontSize), new SolidBrush(Book.TextColor), 0, 0, TitleSpineText_t.format);
            g.ResetTransform();

            // paint author on spine
            g.RotateTransform(270);
            g.TranslateTransform(CanvasCenter.X + AuthorSpineText_t.xOff, CanvasCenter.Y + AuthorSpineText_t.yOff, MatrixOrder.Append);
            g.DrawString(AuthorSpineText_t.text, GetFont("Arial", AuthorSpineText_t.fontSize), new SolidBrush(Book.TextColor), 0, 0, AuthorSpineText_t.format);
            g.ResetTransform();
        }
        private void PaintAddedTexts(Graphics g)
        {
            foreach (var t in Book.AddedTexts)
                g.DrawString(t.text, GetFont("Arial", t.fontSize), new SolidBrush(Book.TextColor), CanvasCenter.X + t.xOff, CanvasCenter.Y + t.yOff, t.format);
        }
        private void PaintBorders(Graphics g)
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

            if (selectedText > -1) g.DrawRectangle(selectedTextPen, GetTextRect(Book.AddedTexts[selectedText]));
        }

        private Font GetFont(string type, int size)
        {
            FontFamily fontFamily = new FontFamily(type);
            Font font = new Font(
                       fontFamily,
                       size,
                       FontStyle.Regular,
                       GraphicsUnit.Pixel);
            return font;
        }
        private Rectangle GetTextRect(text_t text)
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
        private Point GetCursorfromOffset(text_t text)
        {
            int yCursor = text.yOff + text.height / 2 + CanvasCenter.Y;
            int xCursor = text.xOff + text.width / 2 + CanvasCenter.X;

            if (text.format.Alignment.Equals(StringAlignment.Center)) xCursor -= text.width / 2;
            else if (text.format.Alignment.Equals(StringAlignment.Far)) xCursor -= text.width;

            return new Point(xCursor, yCursor);
        }
        private string ReduceWhitespace(string input) { return sWhitespace.Replace(input, " "); }
    }
}
