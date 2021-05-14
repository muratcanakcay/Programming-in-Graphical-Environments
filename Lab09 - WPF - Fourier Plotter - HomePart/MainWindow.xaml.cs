using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using System.Xml.Serialization;

namespace Lab09___WPF___Fourier_Plotter___HomePart
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer = new DispatcherTimer();
        private int tickCount = 0;
        private ObservableCollection<UIElement> UIElements = new ObservableCollection<UIElement>();
        public static MainWindow mw;
       

        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            timer.Tick += new EventHandler(Tick);

            mw = this;


            // ----------------

            XmlSerializer deserializer = new XmlSerializer(typeof(CircleList));

            TextReader reader = new StreamReader("test.xml");

            CircleList cl = new CircleList();
            cl = (CircleList)deserializer.Deserialize(reader);

            reader.Close();

            foreach(var c in cl.circleList)
            {
                Debug.WriteLine($"{c.radius}, {c.frequency}");
            }

            DataContext = cl.circleList;


            // -----------

        }

        public class CircleList
        {
            public ObservableCollection<Circle> circleList;
        }

       



        private void ExitButtonClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }


        // dataGrid data
        public class Circle : INotifyPropertyChanged
        {
            private int _radius;
            public int radius
            {
                get
                {
                    return _radius;
                }
                set
                {
                    _radius = value;
                    OnPropertyRaised("col1");
                }
            }
            private int _frequency;
            public int frequency
            {
                get
                {
                    return _frequency;
                }
                set
                {
                    _frequency = value;
                    OnPropertyRaised("col1");
                }
            }
            

            public event PropertyChangedEventHandler PropertyChanged;
            
            private void OnPropertyRaised(string propertyname)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
                    mw.Tick();
                }
            }
        }

        public void Tick()
        {
            
            
            
            
                Ellipse myEllipse = new Ellipse();
                myEllipse.Cursor = Cursors.Hand;
                SolidColorBrush brush = new SolidColorBrush();
                brush.Color = Color.FromArgb(255, 0, 0, 0);
                myEllipse.Stroke = brush;
                myEllipse.Width = ((ObservableCollection<Circle>)this.DataContext)[0].radius;
                myEllipse.Height = ((ObservableCollection<Circle>)this.DataContext)[0].radius;
            
                UIElements.Add(myEllipse);
                
                Canvas.SetLeft(myEllipse, (theCanvas.ActualWidth - myEllipse.Width) / 2) ;
                Canvas.SetTop(myEllipse, (theCanvas.ActualHeight - myEllipse.Height) / 2);

                theCanvas.Children.Add(myEllipse);
                UIElements.Add(myEllipse);
            
        }

        private void StartButtonClicked(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }

        private void PauseButtonClicked(object sender, RoutedEventArgs e)
        {
            timer.Stop();
        }

        private void ResetButtonClicked(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            tickCount = 0;
            progressBar.Value=0;
            theCanvas.Children.Clear();            
        }

        public void Tick(object sender, EventArgs e)
        {
            if(++tickCount > 101) return;
            
            if (++progressBar.Value == 100)
            {
                Ellipse myEllipse = new Ellipse();
                myEllipse.Cursor = Cursors.Hand;
                SolidColorBrush brush = new SolidColorBrush();
                brush.Color = Color.FromArgb(255, 0, 0, 0);
                myEllipse.Stroke = brush;
                myEllipse.Width = ((ObservableCollection<Circle>)this.DataContext)[0].radius;
                myEllipse.Height = ((ObservableCollection<Circle>)this.DataContext)[0].radius;
            
                UIElements.Add(myEllipse);
                
                Canvas.SetLeft(myEllipse, (theCanvas.ActualWidth - myEllipse.Width) / 2) ;
                Canvas.SetTop(myEllipse, (theCanvas.ActualHeight - myEllipse.Height) / 2);

                theCanvas.Children.Add(myEllipse);
                UIElements.Add(myEllipse);
            }
        }
    }




    

}