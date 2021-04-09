﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;

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
    
    public class Book // singleton
    {
        private static Book Instance { get; set; }

        private int bookWidth;
        private int bookHeight;
        private int spineWidth;
        private Color textColor;
        private Color backgroundColor;
        private readonly Color defaultTextColor = Color.Black;
        private readonly Color defaultBackgroundColor = Color.MistyRose;
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

        //--------------

        private Book()
        {
            BookWidth = 300;
            BookHeight = 500;
            SpineWidth = 30;
            TextColor = defaultTextColor;
            BackgroundColor = defaultBackgroundColor;
            AddedTexts = new();
        }
        public static Book GetBookInstance()
        {
            if (Instance == null) Instance = new Book();
            return Instance;
        }

        public void CreateNewBook(int newWidth, int newHeight, int newSpineWidth)
        {
            BookWidth = newWidth;
            BookHeight = newHeight;
            SpineWidth = newSpineWidth;
            TextColor = defaultTextColor;
            BackgroundColor = defaultBackgroundColor;
            Title = String.Empty;
            Author = String.Empty;
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
            using (FileStream fileStream = new FileStream($"{filename}", FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                bookData_t bookData = new();

                bookData.title = title;
                bookData.author = author;
                bookData.bookWidth = bookWidth;
                bookData.bookHeight = bookHeight;
                bookData.spineWidth = spineWidth;
                bookData.textColor = textColor.ToArgb();
                bookData.backgroundColor = backgroundColor.ToArgb();
                bookData.addedTexts = addedTexts;

                XmlSerializer s = new XmlSerializer(typeof(bookData_t));
                s.Serialize(fileStream, bookData);
            }
        }
        public void LoadBookFromFile(string filename, out bool bookLoaded)
        {
            using (FileStream fileStream = new FileStream($"{filename}", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                XmlSerializer s = new XmlSerializer(typeof(bookData_t));

                try
                {
                    bookData_t loadedBook = (bookData_t)s.Deserialize(fileStream);

                    title = loadedBook.title;
                    author = loadedBook.author;
                    bookWidth = loadedBook.bookWidth;
                    bookHeight = loadedBook.bookHeight;
                    spineWidth = loadedBook.spineWidth;
                    textColor = Color.FromArgb(loadedBook.textColor);
                    backgroundColor = Color.FromArgb(loadedBook.backgroundColor);
                    addedTexts = loadedBook.addedTexts;

                    bookLoaded = true;
                }
                catch (InvalidOperationException)
                {
                    bookLoaded = false;
                }
            }
        }
    }
}
