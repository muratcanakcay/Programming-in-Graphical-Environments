using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Lab05
{
    public partial class MainWindow : Form
    {
        private Book Book { get; set; }
        private Painter Painter { get; set; }
        private text_t PreparedText { get; set; }

        public MainWindow()
        {
            InitializeComponent();           
            
            Book = new();
            Painter = new(Book, Canvas, titleTextBox, authorTextBox);
            //Painter.addTextBox = addTextBox;
        }

        private void MainWindowLoad(object sender, EventArgs e)
        {
            //splitContainer.SplitterDistance = Convert.ToInt32(this.ClientSize.Width * 0.66);
            //Canvas.Width = splitContainer.Panel1.Width;
            //Canvas.Height = splitContainer.Panel1.Height;
        }
        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            Painter.paintCanvas(e);
        }
        private void ColorsChanged(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                Button button = (Button)sender;
                Book.ChangeColors(button.Tag.ToString(), colorDialog.Color);
                Canvas.Refresh();
            }
        }
        private void Canvas_SizeChanged(object sender, EventArgs e)
        {
            Refresh();
        }
        private void CoverTextChanged(object sender, EventArgs e)
        {
            TextBox box = (TextBox)sender;
            string tag = box.Tag.ToString();
            Book.ChangeCoverTexts(tag, box.Text);
            Painter.processTexts(tag);
        }

        private void Canvas_MouseEnter(object sender, EventArgs e)
        {
            if (PreparedText.text != String.Empty && Painter.AddTextOn) Cursor = Cursors.Cross;
            else Cursor = Cursors.Arrow;
        }
        private void Canvas_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Arrow;
        }
        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (Painter.AddTextOn)
            {
                Painter.addNewText(e.X, e.Y, PreparedText);
                Cursor = Cursors.Arrow;
            }
        }

        private void cmdEnglish_Click(object sender, EventArgs e)
        {
            if (cmdPolish.Checked) cmdPolish.Checked = false;
        }
        private void cmdPolish_Click(object sender, EventArgs e)
        {
            if (cmdEnglish.Checked) cmdEnglish.Checked = false;
        }
        private void cmdNew_Click(object sender, EventArgs e)
        {
            using (NewDialog newDialog = new NewDialog())
            {
                if (newDialog.ShowDialog() == DialogResult.OK)
                {
                    Book.NewBook(newDialog.NewWidth, newDialog.NewHeight, newDialog.NewSpineWidth);
                    Painter.paintNewBook();
                }
            }
        }

        private void addTextButton_Click(object sender, EventArgs e)
        {
            using (AddTextDialog addTextDialog = new AddTextDialog())
            {
                if (addTextDialog.ShowDialog() == DialogResult.OK)
                {
                    if (addTextDialog.PreparedText.text != String.Empty)
                    {
                        PreparedText = addTextDialog.PreparedText;
                        Painter.addTextOn();
                    }                    
                }
            }
        }
    }

    
}
