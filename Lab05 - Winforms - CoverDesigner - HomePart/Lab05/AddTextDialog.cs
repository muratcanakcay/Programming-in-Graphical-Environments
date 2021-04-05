using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab05
{
    public partial class AddTextDialog : Form
    {
        public AddTextDialog()
        {
            InitializeComponent();
        }

        private void radioButton_Click(object sender, EventArgs e)
        {
            switch (((RadioButton)sender).Tag.ToString())
            {
                case "left":
                    addTextBox.TextAlign = HorizontalAlignment.Left;
                    break;
                case "center":
                    addTextBox.TextAlign = HorizontalAlignment.Center;
                    break;
                case "right":
                    addTextBox.TextAlign = HorizontalAlignment.Right;
                    break;
            }
        }


        private void buttonAddTextOK_Click(object sender, EventArgs e)
        {
            text_t addedText;
            



        }
    }
}
