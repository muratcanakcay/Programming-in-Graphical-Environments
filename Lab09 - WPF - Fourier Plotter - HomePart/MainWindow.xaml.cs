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
        private DispatcherTimer timer = new DispatcherTimer(DispatcherPriority.Render);
        private int tickCount = 0;
        private ObservableCollection<UIElement> UIElements = new ObservableCollection<UIElement>();
        private Point CanvasCenter = new Point();
        private CircleList circleList = new CircleList();
        
       public static MainWindow mw;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CanvasCenter = new Point(theCanvas.ActualWidth / 2, theCanvas.ActualHeight / 2);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            timer.Tick += new EventHandler(Tick);

            //mw = this;

            // ----------------

            XmlSerializer deserializer = new XmlSerializer(typeof(CircleList));

            TextReader reader = new StreamReader("test.xml");

            
            circleList = (CircleList)deserializer.Deserialize(reader);

            reader.Close();

            foreach(var c in circleList.circleList)
            {
                Debug.WriteLine($"{c.radius}, {c.frequency}");
            }

            circleList.circleList[0].center = CanvasCenter;
            circleList.circleList[0].tip = new Point(CanvasCenter.X + circleList.circleList[0].radius, CanvasCenter.Y);
            
            for (int i=1; i< circleList.circleList.Count; i++)
            {
                circleList.circleList[i].center = circleList.circleList[i-1].tip;
                circleList.circleList[i].tip = new Point(circleList.circleList[i].center.X + circleList.circleList[i].radius, circleList.circleList[i].center.Y);
            }
            
            DataContext = circleList.circleList;

            foreach(var c in circleList.circleList)
                DrawCircle(c);


            // -----------

        }

        private void ExitButtonClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public class CircleList
        {
            public ObservableCollection<Circle> circleList;
        }
        
        public class Circle : UIElement, INotifyPropertyChanged 
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
                    OnPropertyRaised("radius");
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
                    OnPropertyRaised("frequency");
                }
            }

            public Point center;
            public Point tip;

            public event PropertyChangedEventHandler PropertyChanged;
            
            private void OnPropertyRaised(string propertyname)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
                    
                    //mw.Tick();
                }
            }

            public void Rotate(int tickCount)
            {
                tip.X = center.X + radius * Math.Cos(tickCount * frequency * 2 * Math.PI / 1000);
                tip.Y = center.Y + radius * Math.Sin(tickCount * frequency * 2 * Math.PI / 1000);
            }
        }

        //private void AddCircleToGeometryGroup(Circle c)
        //{
        //    if (c != null)
        //    {
        //        CanvasCenter.X += c.radius - c.radius/2;
        //        c.geometry = new EllipseGeometry(c.center, c.radius, c.radius);
        //        circlesGeometry.Children.Add(c.geometry);
        //    }

        //}

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
            if(++tickCount > 1001) return;
            ++progressBar.Value;
            
            Circle previousCircle = (Circle)null;
            foreach (Circle c in circleList.circleList)
            {
                if (previousCircle==null)
                {
                    c.Rotate(tickCount);
                    DrawCircle(c);
                    previousCircle = c;
                    continue;
                }
                c.center = previousCircle.tip;
                c.Rotate(tickCount);
                previousCircle = c;
                DrawCircle(c);
            }
        }

        public void DrawCircle(Circle c)
        {
                Ellipse myEllipse = new Ellipse();
                //myEllipse.Cursor = Cursors.Hand;
                SolidColorBrush brush = new SolidColorBrush();
                brush.Color = Color.FromArgb(255, 0, 0, 0);
                myEllipse.Stroke = brush;
                myEllipse.Width = c.radius;
                myEllipse.Height = c.radius;
            
                UIElements.Add(myEllipse);
                
                Canvas.SetLeft(myEllipse, CanvasCenter.X / 2 + c.center.X) ;
                Canvas.SetTop(myEllipse, CanvasCenter.Y / 2 + c.center.Y) ;

                theCanvas.Children.Add(myEllipse);
        }

        
    }




    

}