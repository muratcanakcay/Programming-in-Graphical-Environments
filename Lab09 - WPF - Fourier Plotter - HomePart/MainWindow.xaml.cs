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
        private GeometryGroup circlesGeometry = new GeometryGroup();
        private Point CanvasCenter = new Point();
        private GeometryDrawing CircleGeometries = new GeometryDrawing();
       
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

            CircleGeometries.Geometry = circlesGeometry;
            CircleGeometries.Pen = new Pen(new SolidColorBrush(Colors.Black), 1);
            DrawingImage DrawnCircles = new DrawingImage(CircleGeometries);
            DrawnCircles.Drawing = CircleGeometries;
            theCanvas.Source = DrawnCircles;

            //mw = this;


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

            cl.circleList[0].center = CanvasCenter;
            cl.circleList[0].tip = new Point(CanvasCenter.X + cl.circleList[0].radius, CanvasCenter.Y);
            
            for (int i=1; i< cl.circleList.Count; i++)
            {
                cl.circleList[i].center = cl.circleList[i-1].tip;
                cl.circleList[i].tip = new Point(cl.circleList[i].center.X + cl.circleList[i].radius, cl.circleList[i].center.Y);
            }
            
            DataContext = cl.circleList;

            foreach(var c in cl.circleList)
                AddCircleToGeometryGroup(c);


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
            public EllipseGeometry geometry = new EllipseGeometry();
            

            public event PropertyChangedEventHandler PropertyChanged;
            
            private void OnPropertyRaised(string propertyname)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
                    
                    // is it needed?
                    if(propertyname.Equals("radius"))
                    {
                        geometry = new EllipseGeometry(new Rect(new Size(radius, radius)));
                    }
                    
                    //mw.Tick();
                }
            }
        }

         private void AddCircleToGeometryGroup(Circle c)
        {
            if (c != null)
            {
                CanvasCenter.X += c.radius - c.radius/2;
                c.geometry = new EllipseGeometry(c.center, c.radius, c.radius);
                circlesGeometry.Children.Add(c.geometry);
            }

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
            //theCanvas.Children.Clear();            
        }

        public void Tick(object sender, EventArgs e)
        {
            if(++tickCount > 101) return;
            ++progressBar.Value;
            
            
            //if (progressBar.Value == 100)
            //{
            //    var previousCircle = (Circle)null;
                
            //    foreach(var item in dataGrid.Items.SourceCollection)
            //    {
            //        var circle = item as Circle;

            //        Ellipse myEllipse = new Ellipse();
            //        myEllipse.Cursor = Cursors.Hand;
            //        SolidColorBrush brush = new SolidColorBrush();
            //        brush.Color = Color.FromArgb(255, 0, 0, 0);
            //        myEllipse.Stroke = brush;
            //        myEllipse.Width = circle.Radius;
            //        myEllipse.Height = circle.Radius;
                
            //        Canvas.SetLeft(myEllipse, CanvasCenter.X - myEllipse.Width / 2) ;
            //        Canvas.SetTop(myEllipse, CanvasCenter.Y - myEllipse.Height / 2) ;

            //        theCanvas.Children.Add(myEllipse);
            //        UIElements.Add(myEllipse);
            //    }
            //}
        }

        
    }




    

}