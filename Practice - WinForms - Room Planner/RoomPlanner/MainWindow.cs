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

namespace RoomPlanner
{
    public partial class MainWindow : Form
    {
        public ToggleButton SelectedButton { get; set; } = null;
        public BindingList<IPicture> pictureList { get; set; } = new BindingList<IPicture>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindowLoad(object sender, EventArgs e)
        {
            splitContainer.SplitterDistance = Convert.ToInt32(this.ClientSize.Width * 0.66);
            Canvas.Width = splitContainer.Panel1.Width;
            Canvas.Height = splitContainer.Panel1.Height;

            listBox.DataSource = pictureList;
            listBox.DisplayMember = "ListBoxDisplay";
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

        private void NewBluePrint(object sender, EventArgs e)
        {
            Canvas.Width = splitContainer.Panel1.Width;
            Canvas.Height = splitContainer.Panel1.Height;
        }

        private void CanvasMouseDown(object sender, MouseEventArgs e)
        {
            if (SelectedButton != null)
            {
                Picture newPicture = new Picture
                {
                    Name = SelectedButton.Tag.ToString(),
                    BeginPt = SelectedButton.BeginPt(e.Location),
                    Img = SelectedButton.BackgroundImage
                };
                
                Debug.WriteLine(newPicture.ToString());

                pictureList.Add(newPicture);
                
                SelectedButton.State = ButtonState.off;
                SelectedButton = null;
            }

            Canvas.Refresh();
        }

        private void CanvasPaint(object sender, PaintEventArgs e)
        {
            Debug.WriteLine("Canvas Paint Event"); 
            
            foreach (var picture in pictureList)
            {
                e.Graphics.DrawImage(picture.Img, picture.BeginPt);
            }

            System.GC.Collect();
        }

        private void listBoxFormat(object sender, ListControlConvertEventArgs e)
        {
            e.Value = e.ListItem.ToString();
        }
    }
}
