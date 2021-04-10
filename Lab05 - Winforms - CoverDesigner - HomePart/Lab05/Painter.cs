using System.Drawing;
using System.Drawing.Drawing2D;

namespace Lab05
{
    public class Painter  // singleton
    {
        private static Painter Instance { get; set; }
        
        private readonly System.Text.RegularExpressions.Regex sWhitespace = new System.Text.RegularExpressions.Regex(@"\s+");

        public int SelectedText { get; private set; }= -1;
        private Book Book { get; }
        private Point CanvasCenter { get; set; }
        private Point CursorStartLoc { get; set; }
        private Point TextStartLoc { get; set; }
        private TextT TitleCoverTextT { get; set; } = new TextT { fontSize = 1 };
        private TextT AuthorCoverTextT { get; set; } = new TextT { fontSize = 1 };
        private TextT TitleSpineTextT { get; set; } = new TextT { fontSize = 1 };
        private TextT AuthorSpineTextT { get; set; } = new TextT { fontSize = 1 };

        public bool AddTextOn { get; private set; }
        public bool MoveTextOn { get; private set; }
        public bool IsTextSelected => SelectedText > -1;

        //------------ c-tor

        private Painter(Point canvasCenter)
        {
            Book = Book.GetBookInstance();
            CanvasCenter = canvasCenter;
        }
        public static Painter GetPainterInstance(Point canvasCenter)
        {
            Instance ??= new Painter(canvasCenter);
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
        public void AddNewText(Graphics g, Point cursor, TextT newText)
        {
            SizeF textSize = g.MeasureString(newText.text, GetFont("Arial", newText.fontSize));

            // calculate offset from canvas center using cursor position and text alignment
            int yOff = cursor.Y - (int)textSize.Height / 2 - CanvasCenter.Y;
            int xOff = cursor.X - (int)textSize.Width / 2 - CanvasCenter.X;
            if (newText.format.Alignment.Equals(StringAlignment.Center)) xOff += (int)textSize.Width / 2;
            else if (newText.format.Alignment.Equals(StringAlignment.Far)) xOff += (int)textSize.Width;

            // add text to list
            Book.AddedTexts.Add(new TextT { text = newText.text, height = (int)textSize.Height, width = (int)textSize.Width, xOff = xOff, yOff = yOff, fontSize = newText.fontSize, format = newText.format });
            SetAddTextOff();
        }
        public bool FindText(Point cursor, out TextT foundText, out int idx)
        {
            for (var i = 0; i < Book.AddedTexts.Count; i++)
            {
                var t = Book.AddedTexts[i];

                if (!GetTextRect(t).Contains(cursor)) continue;
                
                foundText = t;
                idx = i;
                return true;
            }

            idx = -1;
            foundText = new TextT();
            return false;
        }
        public void ModifyOldText(Graphics g, int idx, TextT newText)
        {
            // calculate original cursor position using old text's offset
            TextT oldText = Book.AddedTexts[idx];
            Point cursorPos = GetCursorFromOffset(oldText);

            // remove old text and add new text
            Book.AddedTexts.RemoveAt(idx);
            AddNewText(g, cursorPos, newText);
        }

        public void PrepareMoveText(Point cursor)
        {
            // remember initial position of cursor and text
            CursorStartLoc = cursor;
            TextStartLoc = new Point(Book.AddedTexts[SelectedText].xOff, Book.AddedTexts[SelectedText].yOff);
        }
        public void MoveSelectedText(Point cursor)
        {
            TextT movedText = Book.AddedTexts[SelectedText];
            movedText.xOff = TextStartLoc.X + cursor.X - CursorStartLoc.X;
            movedText.yOff = TextStartLoc.Y + cursor.Y - CursorStartLoc.Y;
            Book.AddedTexts[SelectedText] = movedText;
        }
        public void DeleteSelectedText()
        {
            Book.AddedTexts.RemoveAt(SelectedText);
            SelectedText = -1;
        }

        public void SetAddTextOn() => AddTextOn = true;
        public void SetAddTextOff() => AddTextOn = false;
        public void SetMoveTextOn() => MoveTextOn = true;
        public void SetMoveTextOff() => MoveTextOn = false;
        public void SelectText(int idx) => SelectedText = idx;
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

            var textFormat = new StringFormat();
            textFormat.Alignment = StringAlignment.Near;

            // calculate offset from canvas center
            int xOff = (Book.BookWidth + Book.SpineWidth - (int)textSize.Width) / 2;
            int yOff = Book.BookHeight / 10 - Book.BookHeight / 2;
            if (tag.Equals("author")) yOff += TitleCoverTextT.height + Book.BookHeight / 20; // author text should not overlap title text

            var newTextT = new TextT { text = text, xOff = xOff, yOff = yOff, width = (int)textSize.Width, height = (int)textSize.Height, fontSize = fontSize, format = textFormat };

            // assign text to its field
            if (tag.Equals("title"))
            {
                TitleCoverTextT = newTextT;
                ProcessCoverText(g, "author"); // also process author text so that it moves down if needed
            }
            else AuthorCoverTextT = newTextT;
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

            var textFormat = new StringFormat();
            textFormat.Alignment = StringAlignment.Near;

            // calculate offset from canvas center
            int xOff = -(int)textSize.Height / 2;
            int yOff = ((Book.BookHeight / 4) * (tag.Equals("title") ? 1 : -1)) + (int)textSize.Width / 2;

            var newTextT = new TextT { text = text, xOff = xOff, yOff = yOff, width = (int)textSize.Width, height = (int)textSize.Height, fontSize = fontSize, format = textFormat };

            // assign text to its field
            if (tag.Equals("title"))
                TitleSpineTextT = newTextT;
            else if (tag.Equals("author"))
                AuthorSpineTextT = newTextT;
        }
        private void PaintCoverText(Graphics g)
        {
            // paint title & author on cover
            g.DrawString(TitleCoverTextT.text, GetFont("Arial", TitleCoverTextT.fontSize), new SolidBrush(Book.TextColor), CanvasCenter.X + TitleCoverTextT.xOff, CanvasCenter.Y + TitleCoverTextT.yOff, TitleCoverTextT.format);
            g.DrawString(AuthorCoverTextT.text, GetFont("Arial", AuthorCoverTextT.fontSize), new SolidBrush(Book.TextColor), CanvasCenter.X + AuthorCoverTextT.xOff, CanvasCenter.Y + AuthorCoverTextT.yOff, AuthorCoverTextT.format);
        }
        private void PaintSpineText(Graphics g)
        {
            // paint title on spine
            g.RotateTransform(270);
            g.TranslateTransform(CanvasCenter.X + TitleSpineTextT.xOff, CanvasCenter.Y + TitleSpineTextT.yOff, MatrixOrder.Append);
            g.DrawString(TitleSpineTextT.text, GetFont("Arial", TitleSpineTextT.fontSize), new SolidBrush(Book.TextColor), 0, 0, TitleSpineTextT.format);
            g.ResetTransform();

            // paint author on spine
            g.RotateTransform(270);
            g.TranslateTransform(CanvasCenter.X + AuthorSpineTextT.xOff, CanvasCenter.Y + AuthorSpineTextT.yOff, MatrixOrder.Append);
            g.DrawString(AuthorSpineTextT.text, GetFont("Arial", AuthorSpineTextT.fontSize), new SolidBrush(Book.TextColor), 0, 0, AuthorSpineTextT.format);
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

            var selectedTextBrush = new SolidBrush(Color.FromArgb(255 - Book.BackgroundColor.R, 255 - Book.BackgroundColor.G, 255 - Book.BackgroundColor.B));
            var selectedTextPen = new Pen(selectedTextBrush);

            if (SelectedText > -1) g.DrawRectangle(selectedTextPen, GetTextRect(Book.AddedTexts[SelectedText]));
        }

        private Font GetFont(string type, int size)
        {
            var fontFamily = new FontFamily(type);
            var font = new Font(
                       fontFamily,
                       size,
                       FontStyle.Regular,
                       GraphicsUnit.Pixel);
            return font;
        }
        private Rectangle GetTextRect(TextT text)
        {
            var rect = new Rectangle();

            rect.X = CanvasCenter.X + text.xOff;
            rect.Y = CanvasCenter.Y + text.yOff;
            rect.Width = text.width;
            rect.Height = text.height;

            if (text.format.Alignment == StringAlignment.Center) rect.X -= text.width / 2;
            else if (text.format.Alignment == StringAlignment.Far) rect.X -= text.width;

            return rect;
        }
        private Point GetCursorFromOffset(TextT text)
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