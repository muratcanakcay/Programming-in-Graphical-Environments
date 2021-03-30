using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RoomPlanner
{
    public interface IPicture
    {
        Point BeginPt { get; set; }
        Image Img { get; set; }
        string Name { get; set; }
    }
}
