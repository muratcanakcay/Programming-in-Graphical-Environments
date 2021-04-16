using System;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;

namespace Lab05
{
    public partial class MainWindow : Form
    {
        private Book Book { get; }
        private Painter Painter { get; }
        private TextT PreparedText { get; set; }
        private Graphics g { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            // get singletons
            Book = Book.GetBookInstance();
            Painter = Painter.GetPainterInstance(new Point(Canvas.Width/2, Canvas.Height/2));

            g = Canvas.CreateGraphics();            
        }
        
        private void CoverTextChanged(object sender, EventArgs e)
        {
            var box = (TextBox)sender;
            var tag = box.Tag.ToString();
            Book.ChangeCoverTexts(tag, box.Text);
            Painter.ProcessTexts(g, tag);
            Canvas.Refresh();
        }
        private void ColorsChanged(object sender, EventArgs e)
        {
            var colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() != DialogResult.OK) return;
            
            var button = (Button)sender;
            Book.ChangeBookColors(button.Tag.ToString(), colorDialog.Color);
            Canvas.Refresh();
            
        }
        private void Canvas_SizeChanged(object sender, EventArgs e)
        {
            Painter.UpdateCenter(new Point(Canvas.Width / 2, Canvas.Height / 2));
            Canvas.Refresh();
        }
        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            Painter.PaintCanvas(e.Graphics);
        }

        private void CmdNew_Click(object sender, EventArgs e)
        {
            using var newDialog = new NewDialog();
            if (newDialog.ShowDialog() != DialogResult.OK) return;

            Book.CreateNewBook(newDialog.NewWidth, newDialog.NewHeight, newDialog.NewSpineWidth);
            Painter.SelectText(-1);
            titleTextBox.Text = string.Empty;
            authorTextBox.Text = string.Empty;
            Canvas.Refresh();
        }
        private void CmdOpen_Click(object sender, EventArgs e)
        {
            using var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML Save File|*.xml";

            if (openFileDialog.ShowDialog() != DialogResult.OK || openFileDialog.FileName.Equals("")) return;
            
            Book.LoadBookFromFile(openFileDialog.FileName, out bool bookLoaded);

            if (bookLoaded)
            {
                titleTextBox.Text = Book.Title;
                authorTextBox.Text = Book.Author;
                Canvas.Refresh();
            }
            else MessageBox.Show("Error in file!", "Error!"); // how to localize these?
            
        }
        private void CmdSave_Click(object sender, EventArgs e)
        {
            using var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML Save File|*.xml";      // how to localize these?

            if (saveFileDialog.ShowDialog() != DialogResult.OK || saveFileDialog.FileName.Equals("")) return;
            
            Book.SaveBookToFile(saveFileDialog.FileName);
            
        }
        private void CmdEnglish_Click(object sender, EventArgs e)
        {
            if (cmdEnglish.Checked) return;
            
            ChangeLanguage(""); 
            cmdTurkish.Checked = false;
            cmdEnglish.Checked = true;
        }
        private void CmdTurkish_Click(object sender, EventArgs e)
        {
            if (cmdTurkish.Checked) return;
            
            ChangeLanguage("tr-TR");                
            cmdEnglish.Checked = false;
            cmdTurkish.Checked = true;
        }
        private void ChangeLanguage(string language)
        {
            CultureInfo.CurrentUICulture = new CultureInfo(language);
            var oldSize = Size;
            var oldLocation = Location;
            var (oldTitle, oldAuthor) = (titleTextBox.Text, (authorTextBox.Text));
            Controls.Clear();
            InitializeComponent();
            Size = oldSize;
            Location = oldLocation;
            (titleTextBox.Text, (authorTextBox.Text)) = (oldTitle, oldAuthor);
        }       

        private void AddTextButton_Click(object sender, EventArgs e)
        {
            using var addTextDialog = new AddTextDialog();
            if (addTextDialog.ShowDialog() != DialogResult.OK) return;
            
            PreparedText = addTextDialog.PreparedText;
            if (addTextDialog.PreparedText.text != string.Empty) Painter.SetAddTextOn();
        }

        private void Canvas_DoubleClick(object sender, MouseEventArgs e)
        {
            if (!Painter.FindText(e.Location, out var foundText, out var idx)) return;

            using var modifyTextDialog = new AddTextDialog();
            modifyTextDialog.ImportText(foundText);
            
            if (modifyTextDialog.ShowDialog() != DialogResult.OK) return;
            
            PreparedText = modifyTextDialog.PreparedText;
            Painter.ModifyOldText(g, idx, PreparedText);
            Canvas.Refresh();
        }
        private void Canvas_MouseEnter(object sender, EventArgs e)
        {
            if (PreparedText.text != string.Empty && Painter.AddTextOn) Cursor = Cursors.Cross;
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
                Painter.AddNewText(g, e.Location, PreparedText);
                Cursor = Cursors.Arrow;
                Canvas.Refresh();
            }
            else if (Painter.IsTextSelected && e.Button == MouseButtons.Middle)
            {
                Painter.PrepareMoveText(e.Location);
                Painter.SetMoveTextOn();
            }
        }
        private void Canvas_Click(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            
            if (Painter.FindText(e.Location, out var foundText, out var idx)) Painter.SelectText(idx);
            else Painter.SelectText(-1);

            Canvas.Refresh();
        }
        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle) Painter.SetMoveTextOff();
        }
        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!Painter.MoveTextOn) return;
            
            Painter.MoveSelectedText(e.Location);
            Canvas.Refresh();
        }
        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (!Painter.IsTextSelected || e.KeyCode != Keys.Delete) return;
            
            Painter.DeleteSelectedText();
            Canvas.Refresh();
        }
    }    
}
