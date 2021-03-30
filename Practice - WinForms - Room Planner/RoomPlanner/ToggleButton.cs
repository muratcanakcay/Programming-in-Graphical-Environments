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
        public ButtonState State                                
        {
            get => this.state;
            set
            {
                this.state = value;
                if (State.Equals(ButtonState.off)) BackColor = Color.White;
                else BackColor = Color.Salmon; 
            }
        }

        public Point BeginPt(Point cursorPos) // calculates image's upper left corner coordinates based on mouse cursor position
        {
            return new Point(cursorPos.X - this.BackgroundImage.Width / 2, cursorPos.Y - this.BackgroundImage.Height / 2);
        }
    }
}
