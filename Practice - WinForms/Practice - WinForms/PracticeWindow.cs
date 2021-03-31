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
        public PracticeWindow()
        {
            InitializeComponent();
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
    }
}
