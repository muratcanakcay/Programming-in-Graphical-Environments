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
    public partial class MainWindow : Form
    {
        public ToggleButton SelectedButton { get; set; } = null;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectButton(object sender, EventArgs e)
        {
            if (SelectedButton == null)
            {
                SelectedButton = (ToggleButton)sender;
                SelectedButton.State = ButtonState.on;
            }
            else if (SelectedButton == (ToggleButton) sender)
            {
                SelectedButton.State = ButtonState.off;
                SelectedButton = null;
            }
            else
            {
                SelectedButton.State = ButtonState.off;
                SelectedButton = (ToggleButton)sender;
                SelectedButton.State = ButtonState.on;
            }
        }

        
    }
}
