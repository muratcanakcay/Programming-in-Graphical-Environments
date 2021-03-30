using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RoomPlanner
{
    public class Picture : IPicture
    {
        public Point BeginPt { get; set; }
        public Image Img { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Name} {{X={BeginPt.X}, Y={BeginPt.Y}}}";
        }
    }
}
