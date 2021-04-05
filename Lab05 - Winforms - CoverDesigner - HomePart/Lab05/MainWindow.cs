﻿using System;
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
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            Painter.Canvas = Canvas;
            Painter.titleTextBox = titleTextBox;
            Painter.authorTextBox = authorTextBox;
            Painter.addTextBox = addTextBox;
        }

        private void MainWindowLoad(object sender, EventArgs e)
        {
            //splitContainer.SplitterDistance = Convert.ToInt32(this.ClientSize.Width * 0.66);
            //Canvas.Width = splitContainer.Panel1.Width;
            //Canvas.Height = splitContainer.Panel1.Height;
        }
        
        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            Painter.paintCanvas(e);
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (addTextBox.Text != String.Empty) Painter.addText = true;
        }

        private void Canvas_MouseEnter(object sender, EventArgs e)
        {
            if (addTextBox.Text != String.Empty && Painter.addText) Cursor = Cursors.Cross;
            else Cursor = Cursors.Arrow;
        }

        private void Canvas_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            Painter.paintString(e.X, e.Y);
            Cursor = Cursors.Arrow;
        }

        private void cmdEnglish_Click(object sender, EventArgs e)
        {
            if (cmdPolish.Checked) cmdPolish.Checked = false;
        }

        private void cmdPolish_Click(object sender, EventArgs e)
        {
            if (cmdEnglish.Checked) cmdEnglish.Checked = false;
        }

        private void cmdNew_Click(object sender, EventArgs e)
        {
            NewDialog newDialog = new NewDialog();
            newDialog.ShowDialog();
        }

        private void CoverTextChanged(object sender, EventArgs e)
        {
            TextBox box = (TextBox)sender;
            Book.ChangeCoverText(box.Tag.ToString(), box.Text);
        }

        private void Canvas_SizeChanged(object sender, EventArgs e)
        {
            Refresh();
        }

        private void addTextBox_TextChanged(object sender, EventArgs e)
        {
            Painter.addText = false;
        }
    }

    
}
