using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab08___WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Random rendomGenerator = new Random();

            List<Crew> crew_1 = new List<Crew>()
            {
                new Crew() {Name = "Raoul", Surname = "Frantsev", Age = rendomGenerator.Next(18, 65), Role = Role.Capitan  },
                new Crew() {Name = "Davin", Surname = "Surgeoner", Age = rendomGenerator.Next(18, 65), Role = Role.Steward },
                new Crew() {Name = "Vivienne", Surname = "Cruddas", Age = rendomGenerator.Next(18, 65), Role = Role.Officer },
            };

            List<Crew> crew_2 = new List<Crew>()
            {
                new Crew() {Name = "Gerianna", Surname = "Dilston", Age = rendomGenerator.Next(18, 65), Role = Role.Steward },
                new Crew() {Name = "Cameron", Surname = "Gullan", Age = rendomGenerator.Next(18, 65), Role = Role.Capitan },
                new Crew() {Name = "Kattie", Surname = "Gullan", Age = rendomGenerator.Next(18, 65), Role = Role.Steward },
            };

            List<Crew> crew_3 = new List<Crew>()
            {
                new Crew() {Name = "Wallas", Surname = "Peron", Age = rendomGenerator.Next(18, 65), Role = Role.Steward },
                new Crew() {Name = "Luther", Surname = "Wythill", Age = rendomGenerator.Next(18, 65), Role = Role.Officer},
                new Crew() {Name = "Lilias", Surname = "Belden", Age = rendomGenerator.Next(18, 65), Role = Role.Officer },
            };

            List<Crew> crew_4 = new List<Crew>()
            {
                new Crew() {Name = "Joelynn", Surname = "Camell", Age = rendomGenerator.Next(18, 65), Role = Role.Steward },
                new Crew() { Name = "Wallas", Surname = "Gamddsev", Age = rendomGenerator.Next(18, 65), Role = Role.Capitan },
            };

            List<Crew> crew_5 = new List<Crew>()
            {
                new Crew() {Name = "Joelynn", Surname = "Dilston", Age = rendomGenerator.Next(18, 65), Role = Role.Steward },
                new Crew() { Name = "Wallas", Surname = "Surgeoner", Age = rendomGenerator.Next(18, 65), Role = Role.Capitan },
            };

            List<Crew> crew_6 = new List<Crew>()
            {
                new Crew() {Name = "Joelynn", Surname = "Peron", Age = rendomGenerator.Next(18, 65), Role = Role.Steward },
                new Crew() { Name = "Wallas", Surname = "Gullan", Age = rendomGenerator.Next(18, 65), Role = Role.Capitan },
            };


            this.DataContext = new List<Flight>()
            {
                new Flight() { Origin="WAW", Destination="BCN", Number="FR-7872", Duration = TimeSpan.FromMinutes(rendomGenerator.Next(18, 265)), Members = crew_1 },
                new Flight() { Origin="BCN", Destination="MAD", Number="FR-4779", Duration = TimeSpan.FromMinutes(rendomGenerator.Next(18, 265)), Members = crew_2 },
                new Flight() { Origin="MAD", Destination="LHR", Number="LH-1835", Duration = TimeSpan.FromMinutes(rendomGenerator.Next(18, 265)), Members = crew_3 },
                new Flight() { Origin="FCO", Destination="CDG", Number="W-63901", Duration = TimeSpan.FromMinutes(rendomGenerator.Next(18, 265)), Members = crew_4 },
                new Flight() { Origin="CGN", Destination="VIE", Number="LO-385", Duration = TimeSpan.FromMinutes(rendomGenerator.Next(18, 265)), Members = crew_5 },
                new Flight() { Origin="KRK", Destination="NAP", Number="KL-921", Duration = TimeSpan.FromMinutes(rendomGenerator.Next(18, 265)), Members = crew_6 },
                new Flight() { Origin="MUC", Destination="BER", Number="EK-39", Duration = TimeSpan.FromMinutes(rendomGenerator.Next(18, 265)), Members = crew_2 },
                new Flight() { Origin="LHR", Destination="EDI", Number="W-63901", Duration = TimeSpan.FromMinutes(rendomGenerator.Next(18, 265)), Members = crew_4 },
            };
        }
    }
}
