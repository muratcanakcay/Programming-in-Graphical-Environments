using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab08___WPF
{
    public class Flight
    {
        public string Number { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public TimeSpan Duration { get; set; }
        public List<Crew> Members { get; set; }
    }

    public enum Role
    {
        Capitan, Officer, Steward
    }

    public class Crew
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public Role Role { get; set; }
    }
}
