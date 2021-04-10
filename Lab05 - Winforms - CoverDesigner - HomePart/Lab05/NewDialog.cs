using System;
using System.Windows.Forms;

namespace Lab05
{
    public partial class NewDialog : Form
    {
        public int NewWidth { get; private set; }
        public int NewHeight { get; private set; }
        public int NewSpineWidth { get; private set; }

        public NewDialog()
        {
            InitializeComponent();
        }

        private void OkButtonNewDialog_Click(object sender, EventArgs e)
        {
            NewWidth = (int)newWidth.Value;
            NewHeight = (int)newHeight.Value;
            NewSpineWidth = (int)newSpineWidth.Value;
        }
    }

    
}
