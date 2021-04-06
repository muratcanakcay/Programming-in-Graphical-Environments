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
        

        public int BookWidth { get => bookWidth; set { bookWidth = value; } } 
        public int BookHeight { get => bookHeight; set { bookHeight = value; } }
        public int SpineWidth { get => spineWidth; set { spineWidth = value; } } 
        public Color TextColor { get => textColor; set { textColor = value; } }
        public Color BackgroundColor { get => backgroundColor; set { backgroundColor = value; } }
        public string Title { get => title; set => title = value; }
        public string Author { get => author; set => author = value; }
        public List<text_t> AddedTexts { get => addedTexts; set { addedTexts = value; } } 
        

        public Book()
        {
            BookWidth = 300;
            BookHeight = 500;
            SpineWidth = 30;
            TextColor = Color.Black;
            BackgroundColor = Color.White;
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
            
            //Painter.paintNewBook();  ---- do this in MainWindow
        }

        public void ChangeCoverTexts(string tag, string newText)
        {
            if (tag.Equals("title")) Title = newText;
            else Author = newText;

            //Painter.processTexts(tag); ---- do this in MainWindow
        }

        public void ChangeColors(string tag, Color color)
        {
            if (tag.Equals("background")) BackgroundColor = color;
            else TextColor = color;
        }




    }
}
