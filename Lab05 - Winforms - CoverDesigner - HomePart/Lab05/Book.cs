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
        public int height;
        public int width;
        public int fontSize;
        public StringFormat format;
    }

    public struct bookData_t
    {
        public int bookWidth;
        public int bookHeight;
        public int spineWidth;
        public int textColor;
        public int backgroundColor;
        public string title;
        public string author;
        public List<text_t> addedTexts;
    }
    
    public class Book
    {
        private int bookWidth;
        private int bookHeight;
        private int spineWidth;
        private Color textColor;
        private Color backgroundColor;
        private string title;
        private string author;
        private List<text_t> addedTexts;

        public int BookWidth { get => bookWidth; private set { bookWidth = value; } } 
        public int BookHeight { get => bookHeight; private set { bookHeight = value; } }
        public int SpineWidth { get => spineWidth; private set { spineWidth = value; } } 
        public Color TextColor { get => textColor; private set { textColor = value; } }
        public Color BackgroundColor { get => backgroundColor; private set { backgroundColor = value; } }
        public string Title { get => title; private set => title = value; }
        public string Author { get => author; private set => author = value; }
        public List<text_t> AddedTexts { get => addedTexts; private set { addedTexts = value; } }

        public static bookData_t bookData;
        
        public Book()
        {
            BookWidth = 300;
            BookHeight = 500;
            SpineWidth = 30;
            TextColor = Color.Black;
            BackgroundColor = Color.MistyRose;
            AddedTexts = new();
        }

        public void NewBook(int newWidth, int newHeight, int newSpineWidth)
        {
            BookWidth = newWidth;
            BookHeight = newHeight;
            SpineWidth = newSpineWidth;
            TextColor = Color.Black;
            BackgroundColor = Color.White;
            Title = String.Empty;
            Author = String.Empty;
            AddedTexts.Clear();
        }

        public void ChangeCoverTexts(string tag, string newText)
        {
            if (tag.Equals("title")) Title = newText;
            else Author = newText;
        }

        public void ChangeColors(string tag, Color color)
        {
            if (tag.Equals("background")) BackgroundColor = color;
            else TextColor = color;
        }

        public void SaveBook()
        {
            bookData.title = title;
            bookData.author = author;
            bookData.bookWidth = bookWidth;
            bookData.bookHeight = bookHeight;
            bookData.spineWidth = spineWidth;
            bookData.textColor = textColor.ToArgb();
            bookData.backgroundColor = backgroundColor.ToArgb();
            bookData.addedTexts = addedTexts;
        }

        public void LoadBook()
        {
            title = bookData.title;
            author = bookData.author;
            bookWidth = bookData.bookWidth;
            bookHeight = bookData.bookHeight;
            spineWidth = bookData.spineWidth;
            textColor = Color.FromArgb(bookData.textColor);
            backgroundColor = Color.FromArgb(bookData.backgroundColor);
            addedTexts = bookData.addedTexts;
        }
    }
}
