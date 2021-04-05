﻿using System;
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
        public int NewWidth { get; private set; }
        public int NewHeight { get; private set; }
        public int NewSpineWidth { get; private set; }
        

        public NewDialog()
        {
            InitializeComponent();
        }

        private void okButtonNewDialog_Click(object sender, EventArgs e)
        {
            NewWidth = (int)newWidth.Value;
            NewHeight = (int)newHeight.Value;
            NewSpineWidth = (int)newSpineWidth.Value;
        }
    }

    
}
