using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace RoomPlanner
{
    public enum ButtonState { on, off }

    public partial class ToggleButton : System.Windows.Forms.Button 
    {
        // private fields
        private ButtonState state = ButtonState.off;            

        // public properties
        public Image Picture { get; set; }                      // button image
        public ButtonState State                                // button state
        {
            get => this.state;
            set
            {
                this.state = value;
                if (State.Equals(ButtonState.off)) BackColor = Color.White;
                else BackColor = Color.Salmon; 
            }
        }

        // c-tor  IS IT NECESSARY???
        //ToggleButton() { }

        public Point BeginPos(Point cursorPos) // calculates image's upper left corner coordinates based on mouse cursor position
        {
            return new Point(cursorPos.X - this.Image.Width / 2, cursorPos.Y - this.Image.Height / 2);
        }
    }
}
