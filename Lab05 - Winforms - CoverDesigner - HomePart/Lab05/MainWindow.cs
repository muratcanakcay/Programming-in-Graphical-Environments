using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;

namespace Lab05
{
    public partial class MainWindow : Form
    {
        private Book Book { get; set; }
        private Painter Painter { get; set; }
        private text_t PreparedText { get; set; }
        private Graphics g { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            g = Canvas.CreateGraphics();
            Book = new();
            Painter = new(Book, new Point(Canvas.Width/2, Canvas.Height/2));
        }
        
        private void CoverTextChanged(object sender, EventArgs e)
        {
            TextBox box = (TextBox)sender;
            string tag = box.Tag.ToString();
            Book.ChangeCoverTexts(tag, box.Text);
            Painter.processTexts(g, tag);
            Canvas.Refresh();
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
            Painter.changeCenter(new Point(Canvas.Width / 2, Canvas.Height / 2));
            Canvas.Refresh();
        }
        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            Painter.paintCanvas(e.Graphics);
        }

        private void cmdNew_Click(object sender, EventArgs e)
        {
            using (NewDialog newDialog = new NewDialog())
            {
                if (newDialog.ShowDialog() == DialogResult.OK)
                {
                    Book.NewBook(newDialog.NewWidth, newDialog.NewHeight, newDialog.NewSpineWidth);
                    Painter.selectText(-1);
                    titleTextBox.Text = String.Empty;
                    authorTextBox.Text = String.Empty;
                    Canvas.Refresh();
                }
            }
        }
        private void cmdOpen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            { 
                openFileDialog.Filter = "XML Save File|*.xml";

                if (openFileDialog.ShowDialog() == DialogResult.OK && !openFileDialog.FileName.Equals(""))
                {
                    using (FileStream fileStream = new FileStream($"{openFileDialog.FileName}", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        XmlSerializer s = new XmlSerializer(typeof(bookData_t));
                        
                        try
                        { 
                            bookData_t loadedBook = (bookData_t)s.Deserialize(fileStream);
                            Book.LoadBook(loadedBook);

                            titleTextBox.Text = loadedBook.title;
                            authorTextBox.Text = loadedBook.author;

                            Canvas.Refresh();
                        }
                        catch (InvalidOperationException)
                        {
                            MessageBox.Show("Error in file!", "Error!");
                        }
                    }
                }
            }
        }
        private void cmdSave_Click(object sender, EventArgs e)
        {
            Book.SaveBook();

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "XML Save File|*.xml";
                if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK && !saveFileDialog.FileName.Equals(""))
                {
                    using (System.IO.FileStream fileStream = (System.IO.FileStream)saveFileDialog.OpenFile())
                    {
                        XmlSerializer s = new XmlSerializer(typeof(bookData_t));
                        s.Serialize(fileStream, Book.bookData);
                    }
                }
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

        private void addTextButton_Click(object sender, EventArgs e)
        {
            using (AddTextDialog addTextDialog = new AddTextDialog())
            {
                if (addTextDialog.ShowDialog() == DialogResult.OK)
                {
                    PreparedText = addTextDialog.PreparedText;
                    if (addTextDialog.PreparedText.text != String.Empty) Painter.addTextOn();
                }
            }
        }

        private void Canvas_DoubleClick(object sender, MouseEventArgs e)
        {
            if (Painter.findText(e.Location, out text_t foundText, out int idx))
            {
                using (AddTextDialog modifyTextDialog = new AddTextDialog())
                {
                    modifyTextDialog.ImportText(foundText);
                    if (modifyTextDialog.ShowDialog() == DialogResult.OK)
                    {
                        PreparedText = modifyTextDialog.PreparedText;
                        Painter.modifyOldText(g, idx, PreparedText);
                        Canvas.Refresh();
                    }
                }
            }
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
            if (Painter.AddTextOn && e.Button == MouseButtons.Left)
            {
                Painter.addNewText(g, e.Location, PreparedText);
                Cursor = Cursors.Arrow;
                Canvas.Refresh();
            }
            else if (Painter.TextSelected && e.Button == MouseButtons.Middle)
            {
                Painter.prepareMoveText(e.Location);
                Painter.moveTextOn();
            }
        }
        private void Canvas_Click(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            
            if (Painter.findText(e.Location, out text_t foundText, out int idx))
            {
                Painter.selectText(idx);
            }
            else Painter.selectText(-1);

            Canvas.Refresh();
        }
        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                Painter.moveTextOff();
            }
        }
        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (Painter.MoveTextOn)
            {
                Painter.moveSelectedText(e.Location);
                Canvas.Refresh();
            }
        }
        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (Painter.TextSelected && e.KeyCode == Keys.Delete)
            {
                Painter.deleteSelectedText();
                Canvas.Refresh();
            }
        }
    }

    
}
