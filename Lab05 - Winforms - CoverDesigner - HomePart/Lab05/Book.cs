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
        public Brush brush;
    }
    
    public static class Book
    {
        private static int bookWidth = 300;
        private static int bookHeight= 500;
        private static int spineWidth  = 30;
        private static Color textColor= Color.Black;
        private static Color backgroundColor = Color.White;
        private static string title;
        private static string author;
        private static List<text_t> addedTexts = new();

        public static int BookWidth { get => bookWidth; set { bookWidth = value; } } 
        public static int BookHeight { get => bookHeight; set { bookHeight = value; } }
        public static int SpineWidth { get => spineWidth; set { spineWidth = value; } } 
        public static Color TextColor { get => textColor; set { textColor = value; } }
        public static Color BackgroundColor { get => backgroundColor; set { backgroundColor = value; } }
        public static string Title { get => title; set => title = value; }
        public static string Author { get => author; set => author = value; }
        public static List<text_t> AddedTexts { get => addedTexts; set { addedTexts = value; } } 

        public static void NewBook(int newWidth, int newHeight, int newSpineWidth)
        {
            BookWidth = newWidth;
            BookHeight = newHeight;
            SpineWidth = newSpineWidth;
            TextColor = Color.Black;
            BackgroundColor = Color.White;
            Title = String.Empty;
            Author = String.Empty;
            AddedTexts.Clear();
            
            Painter.paintNewBook();
        }

        public static void ChangeCoverText(string tag, string newText)
        {
            if (tag.Equals("title")) Title = newText;
            else Author = newText;
            
            Painter.processTexts(tag);
        }




    }
}
