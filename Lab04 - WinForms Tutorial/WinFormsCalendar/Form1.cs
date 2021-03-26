using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsCalendar
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            string date = monthCalendar.SelectionStart.ToShortDateString();
            string note = noteTextBox.Text;
            ListViewItem newItem = new ListViewItem(new[] { date, note });
            calendarListView.Items.Add(newItem);
            noteTextBox.Text = "";
            notifyIcon.ShowBalloonTip(1000, "Item added", "Added new event", ToolTipIcon.Info);
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in calendarListView.SelectedItems)
                calendarListView.Items.Remove(item);
        }

        private void calendarListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            removeButton.Enabled = calendarListView.SelectedItems.Count > 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            notifyIconContextMenu.Items.Add("Exit", null, ExitContextMenu_Click);
        }

        private void ExitContextMenu_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
