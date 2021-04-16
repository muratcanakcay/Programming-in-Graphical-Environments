using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;

namespace Lab05
{
    public struct TextT
    {
        public string text;
        public int xOff;
        public int yOff;
        public int height;
        public int width;
        public int fontSize;
        public StringFormat format;
    }

    public struct BookDataT
    {
        public int bookWidth;
        public int bookHeight;
        public int spineWidth;
        public int textColor;
        public int backgroundColor;
        public string title;
        public string author;
        public List<TextT> addedTexts;
    }
    
    public class Book // singleton
    {
        private static Book Instance { get; set; }
        private readonly Color defaultTextColor = Color.Black;
        private readonly Color defaultBackgroundColor = Color.MistyRose;

        public int BookWidth { get; private set; } 
        public int BookHeight { get; private set; }
        public int SpineWidth { get; private set; } 
        public Color TextColor { get; private set; }
        public Color BackgroundColor { get; private set; }
        public string Title { get; private set; }
        public string Author { get; private set; }
        public List<TextT> AddedTexts { get; private set; }

        //--------------

        private Book()
        {
            BookWidth = 300;
            BookHeight = 500;
            SpineWidth = 30;
            TextColor = defaultTextColor;
            BackgroundColor = defaultBackgroundColor;
            AddedTexts = new List<TextT>();
        }
        public static Book GetBookInstance()
        {
            Instance ??= new Book();
            return Instance;
        }

        public void CreateNewBook(int newWidth, int newHeight, int newSpineWidth)
        {
            BookWidth = newWidth;
            BookHeight = newHeight;
            SpineWidth = newSpineWidth;
            TextColor = defaultTextColor;
            BackgroundColor = defaultBackgroundColor;
            Title = string.Empty;
            Author = string.Empty;
            AddedTexts.Clear();
        }
        public void ChangeCoverTexts(string tag, string newText)
        {
            if (tag.Equals("title")) Title = newText;
            else Author = newText;
        }
        public void ChangeBookColors(string tag, Color color)
        {
            if (tag.Equals("background")) BackgroundColor = color;
            else TextColor = color;
        }
        public void SaveBookToFile(string filename)
        {
            using FileStream fileStream = new FileStream($"{filename}", FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
        
            var bookData = new BookDataT
            {
                title = Title,
                author = Author,
                bookWidth = BookWidth,
                bookHeight = BookHeight,
                spineWidth = SpineWidth,
                textColor = TextColor.ToArgb(),
                backgroundColor = BackgroundColor.ToArgb(),
                addedTexts = AddedTexts
            };

            var s = new XmlSerializer(typeof(BookDataT));
            s.Serialize(fileStream, bookData);
            
        }
        public void LoadBookFromFile(string filename, out bool bookLoaded)
        {
            
            using var fileStream = new FileStream($"{filename}", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            var s = new XmlSerializer(typeof(BookDataT));

            try
            {
                var loadedBook = (BookDataT)s.Deserialize(fileStream); // should I also try-catch ArgumentNullException here ?

                Title = loadedBook.title;
                Author = loadedBook.author;
                BookWidth = loadedBook.bookWidth;
                BookHeight = loadedBook.bookHeight;
                SpineWidth = loadedBook.spineWidth;
                TextColor = Color.FromArgb(loadedBook.textColor);
                BackgroundColor = Color.FromArgb(loadedBook.backgroundColor);
                AddedTexts = loadedBook.addedTexts;

                bookLoaded = true;
            }
            catch (InvalidOperationException)
            {
                bookLoaded = false;
            }
        }
    }
}
