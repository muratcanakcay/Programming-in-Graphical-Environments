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
    public partial class AddTextDialog : Form
    {
        public text_t PreparedText { get; set; }
        public StringFormat format { get; set; }

        public AddTextDialog()
        {
            InitializeComponent();
            format = new();
            format.Alignment = StringAlignment.Near;
        }

        public void ImportText(text_t importText)
        {
            addTextBox.Text = importText.text;
            numericUpDownFontSize.Value = importText.fontSize;
            format = (StringFormat)importText.format.Clone();

            switch (format.Alignment)
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
                    format.Alignment = StringAlignment.Near;
                    break;
                case "center":
                    addTextBox.TextAlign = HorizontalAlignment.Center;
                    format.Alignment = StringAlignment.Center;
                    break;
                case "right":
                    addTextBox.TextAlign = HorizontalAlignment.Right;
                    format.Alignment = StringAlignment.Far;
                    break;
            }
        }

        private void buttonAddTextOK_Click(object sender, EventArgs e)
        {
            Debug.Print($"add : final alignment: {format.Alignment.ToString()}\n");
            PreparedText = new text_t { text = addTextBox.Text, fontSize = (int)numericUpDownFontSize.Value, format = format };
        }
    }
}
