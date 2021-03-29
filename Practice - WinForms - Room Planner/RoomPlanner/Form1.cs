using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoomPlanner
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private bool coffeeTableButtonClicked { get; set; }
        private bool tableButtonClicked { get; set; }
        private bool sofaButtonClicked { get; set; }
        private bool bedButtonClicked { get; set; }

        private void coffeeTableButton_Click(object sender, EventArgs e)
        {
            if (!coffeeTableButtonClicked)
            {
                coffeeTableButtonClicked = true;
                coffeeTableButton.BackColor = Color.FromArgb(240, 220, 170);
            }
            else
            {
                coffeeTableButtonClicked = false;
                coffeeTableButton.BackColor = Color.FromArgb(255, 255, 255);
            }

            tableButton.BackColor = sofaButton.BackColor = bedButton.BackColor = Color.FromArgb(255, 255, 255);
        }

        private void tableButton_Click(object sender, EventArgs e)
        {
            if (!tableButtonClicked)
            {
                tableButtonClicked = true;
                tableButton.BackColor = Color.FromArgb(240, 220, 170);
            }
            else
            {
                tableButtonClicked = false;
                tableButton.BackColor = Color.FromArgb(255, 255, 255);
            }

            coffeeTableButton.BackColor = sofaButton.BackColor = bedButton.BackColor = Color.FromArgb(255, 255, 255);
        }

        private void sofaButton_Click(object sender, EventArgs e)
        {
            if (!sofaButtonClicked)
            {
                sofaButtonClicked = true;
                sofaButton.BackColor = Color.FromArgb(240, 220, 170);
            }
            else
            {
                sofaButtonClicked = false;
                sofaButton.BackColor = Color.FromArgb(255, 255, 255);
            }

            tableButton.BackColor = coffeeTableButton.BackColor = bedButton.BackColor = Color.FromArgb(255, 255, 255);
        }

        private void bedButton_Click(object sender, EventArgs e)
        {
            if (!bedButtonClicked)
            {
                bedButtonClicked = true;
                bedButton.BackColor = Color.FromArgb(240, 220, 170);
            }
            else
            {
                bedButtonClicked = false;
                bedButton.BackColor = Color.FromArgb(255, 255, 255);
            }

            tableButton.BackColor = sofaButton.BackColor = coffeeTableButton.BackColor = Color.FromArgb(255, 255, 255);
        }

        private void coffeeTableButton_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor= Cursors.Hand;
        }

        private void coffeeTableButton_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        
    }
}
