using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab05
{
    public partial class AddTextDialog : Form
    {
        public TextT PreparedText { get; set; }
        public StringFormat Format { get; set; }

        public AddTextDialog()
        {
            InitializeComponent();
            Format = new();
            Format.Alignment = StringAlignment.Near;
        }

        public void ImportText(TextT importText)
        {
            addTextBox.Text = importText.text;
            numericUpDownFontSize.Value = importText.fontSize;
            Format = (StringFormat)importText.format.Clone();

            switch (Format.Alignment)
            {
                case StringAlignment.Center:
                    addTextBox.TextAlign = HorizontalAlignment.Center;
                    radioLeft.Checked = false;
                    radioCenter.Checked = true;
                    break;
                case StringAlignment.Far:
                    addTextBox.TextAlign = HorizontalAlignment.Right;
                    radioLeft.Checked = false;
                    radioRight.Checked = true;
                    break;
            }
        }
        
        private void radioButton_Click(object sender, EventArgs e)
        {
            switch (((RadioButton)sender).Tag.ToString())
            {
                case "left":
                    addTextBox.TextAlign = HorizontalAlignment.Left;
                    Format.Alignment = StringAlignment.Near;
                    break;
                case "center":
                    addTextBox.TextAlign = HorizontalAlignment.Center;
                    Format.Alignment = StringAlignment.Center;
                    break;
                case "right":
                    addTextBox.TextAlign = HorizontalAlignment.Right;
                    Format.Alignment = StringAlignment.Far;
                    break;
            }
        }

        private void buttonAddTextOK_Click(object sender, EventArgs e)
        {
            PreparedText = new TextT { text = addTextBox.Text, fontSize = (int)numericUpDownFontSize.Value, format = Format };
        }
    }
}
