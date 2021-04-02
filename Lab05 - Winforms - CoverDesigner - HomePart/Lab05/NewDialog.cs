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
    public partial class NewDialog : Form
    {
        public NewDialog()
        {
            InitializeComponent();
        }

        private void cancelButtonNewDialog_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void okButtonNewDialog_Click(object sender, EventArgs e)
        {
            Painter.newBook((int)widthUpDownNewDialog.Value, (int)heightUpDownNewDialog.Value, (int)spineWidthUpDownNewDialog.Value);
            this.Close();
        }
    }

    
}
