using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PracticeWinForms
{
    
    
    public partial class PracticeWindow : Form
    {
        //RadioButton selectedColor { get; set; } = null;

        public PracticeWindow()
        {
            InitializeComponent();
            colorListViewHeader(ref listView, Color.LightSteelBlue, Color.Black);
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren())
                MessageBox.Show("Welcome " + nameTextBox.Text);
        }

        private void nameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(this.ValidateChildren() && e.KeyChar.Equals((char)Keys.Return))
                MessageBox.Show("Welcome " + nameTextBox.Text);
        }

        private void nameTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (nameTextBox.Text.Length == 0 || nameTextBox.Text.Length >6)
            {
                nameErrorProvider.SetError(nameTextBox, "Name must be 1 to 6 characters");
                e.Cancel = true;
                return;
            }

            nameErrorProvider.SetError(nameTextBox, string.Empty);
            e.Cancel = false;
        }

        private void selectedColor(object sender, EventArgs e)
        {
            RadioButton button = (RadioButton)sender;
            if (button.Checked)
            {
                switch (button.Text)
                {
                    case "Red":
                        colorBox.BackColor = Color.Red;
                        break;
                    case "Blue":
                        colorBox.BackColor = Color.Blue;
                        break;
                    case "Green":
                        colorBox.BackColor = Color.Green;
                        break;
                }

                colorBox.Refresh();
            }
        }

        private void resetBtn_Click(object sender, EventArgs e)
        {
            Graphics g = Canvas.CreateGraphics();
            g.Clear(Color.White);
            Canvas.Refresh();
        }

        private void circleBtn_Click(object sender, EventArgs e)
        {
            Graphics g = Canvas.CreateGraphics();
            g.DrawEllipse(Pens.Black, 10, 10, Canvas.Size.Width - 20 , Canvas.Size.Height - 20);
        }

        private void lineBtn_Click(object sender, EventArgs e)
        {
            Graphics g = Canvas.CreateGraphics();
            g.DrawLine(Pens.Black, new Point(0, 0), new Point(Canvas.Size.Width / 2, Canvas.Size.Height / 2));
        }

        private void crossBtn_Click(object sender, EventArgs e)
        {
            Region r1 = new Region(new Rectangle(10, Canvas.Size.Height / 2 - 5, Canvas.Size.Width - 20, 10));
            Region r2 = new Region(new Rectangle(Canvas.Size.Width / 2 - 5, 10, 10, Canvas.Size.Height - 20));
            r1.Union(r2);
            
            Graphics g = Canvas.CreateGraphics();
            g.FillRegion(Brushes.Red, r1);
        }

        private void txtBtn_Click(object sender, EventArgs e)
        {
            FontFamily myFontFamily = new FontFamily("Times New Roman");
            Font myFont = DefaultFont;
            StringFormat myStringFormat = new StringFormat();

            Graphics g = Canvas.CreateGraphics();
            g.DrawString(textBox.Text, myFont, Brushes.Black, 10, Canvas.Size.Height - 30, myStringFormat);
        }

        private void listButton_Click(object sender, EventArgs e)
        {
            ListViewItem newItem = new ListViewItem(listTextBox.Text);
            listView.Items.Add(newItem);
            listTextBox.Text = "";
        }

        private void listTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals((char)Keys.Return))
            {
                ListViewItem newItem = new ListViewItem(listTextBox.Text);
                listView.Items.Add(newItem);
                listTextBox.Text = "";
            }

        }

        public static void colorListViewHeader(ref ListView list, Color backColor, Color foreColor)
        {
            list.OwnerDraw = true;
            list.DrawColumnHeader +=
                new DrawListViewColumnHeaderEventHandler
                (
                    (sender, e) => headerDraw(sender, e, backColor, foreColor)
                );
            list.DrawItem += new DrawListViewItemEventHandler(bodyDraw);
        }

        private static void headerDraw(object sender, DrawListViewColumnHeaderEventArgs e, Color backColor, Color foreColor)
        {
            using (SolidBrush backBrush = new SolidBrush(backColor))
            {
                e.Graphics.FillRectangle(backBrush, e.Bounds);
            }

            using (SolidBrush foreBrush = new SolidBrush(foreColor))
            {
                e.Graphics.DrawString(e.Header.Text, e.Font, foreBrush, e.Bounds);
            }
        }

        private static void bodyDraw(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }
    }
}
