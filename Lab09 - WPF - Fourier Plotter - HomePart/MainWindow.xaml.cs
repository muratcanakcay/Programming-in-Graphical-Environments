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
        private Point CanvasCenter = new Point();
        private CircleList circleList = new CircleList();
        private ObservableCollection<UIElement> DrawnCircles = new ObservableCollection<UIElement>();
        private ObservableCollection<UIElement> DrawnLines = new ObservableCollection<UIElement>();
        private Polyline TrailPoly = new Polyline();
        private PointCollection TrailPoints = new PointCollection();

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

            mw = this;

            // ----------------

            XmlSerializer deserializer = new XmlSerializer(typeof(CircleList));

            TextReader reader = new StreamReader("test2.xml");

            circleList = (CircleList)deserializer.Deserialize(reader);

            reader.Close();

            foreach (var c in circleList.circleList)
            {
                Debug.WriteLine($"{c.radius}, {c.frequency}");
            }

            CalculateTipsAndCenters();
            InitializeTrail();

            DataContext = circleList.circleList;




            // -----------

        }

        private void CalculateTipsAndCenters()
        {
            circleList.circleList[0].StartingCenter = new Point(0, 0);
            circleList.circleList[0].tip = new Point(circleList.circleList[0].radius, 0);

            for (int i = 1; i < circleList.circleList.Count; i++)
            {
                circleList.circleList[i].StartingCenter = circleList.circleList[i - 1].tip;
                circleList.circleList[i].tip = new Point(circleList.circleList[i].StartingCenter.X + circleList.circleList[i].radius, circleList.circleList[i].StartingCenter.Y);
            }

            foreach (var c in circleList.circleList)
            {
                DrawCircle(c);
                DrawLine(c);
            }
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
                }
            }

            private Point _StartingCenter;
            public Point StartingCenter
            {
                get
                {
                    return _StartingCenter;
                }
                set
                {
                    _StartingCenter = value;
                    center = StartingCenter;
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

                    Debug.WriteLine($"Property changed");
                    mw.CalculateTipsAndCenters();
                }
            }

            public void Rotate(int tickCount)
            {
                tip.X = center.X + radius * Math.Cos(tickCount * frequency * 2 * Math.PI / 666);
                tip.Y = center.Y + radius * Math.Sin(tickCount * frequency * 2 * Math.PI / 666);
            }

            public void Reset()
            {
                center = StartingCenter;
                tip.X = center.X + radius;
                tip.Y = center.Y;
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
            progressBar.Value = 0;
            theCanvas.Children.Clear();

            foreach (var c in circleList.circleList)
            {
                c.Reset();
                DrawCircle(c);
                DrawLine(c);
            }

            InitializeTrail();
        }



        public void Tick(object sender, EventArgs e)
        {


            if (++tickCount >= 666)
            {
                timer.Stop();
                return;
            }

            ++progressBar.Value;

            foreach (var circle in DrawnCircles)
                theCanvas.Children.Remove(circle);

            foreach (var line in DrawnLines)
                theCanvas.Children.Remove(line);

            Circle lastCircle = (Circle)null;
            foreach (Circle c in circleList.circleList)
            {
                if (lastCircle == null)
                {
                    c.Rotate(tickCount);
                    lastCircle = c;

                    DrawCircle(c);
                    DrawLine(c);

                    continue;
                }
                c.center = lastCircle.tip;
                c.Rotate(tickCount);
                lastCircle = c;

                DrawCircle(c);
                DrawLine(c);
            }

            //DrawPlot(lastCircle);
            DrawTrail(lastCircle);
        }

        public void DrawPlot(Circle c)
        {
            Ellipse trailEllipse = new Ellipse();
            //myEllipse.Cursor = Cursors.Hand;
            TrailPoly.Stroke = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
            trailEllipse.Width = 3;
            trailEllipse.Height = 3;

            Canvas.SetLeft(trailEllipse, CanvasCenter.X + c.tip.X);
            Canvas.SetTop(trailEllipse, CanvasCenter.Y + c.tip.Y);
            theCanvas.Children.Add(trailEllipse);
        }

        public void InitializeTrail()
        {
            TrailPoints.Clear();
            TrailPoly.Points.Clear();
            
            TrailPoly.Stroke = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
            TrailPoly.StrokeThickness = 3;

            Point tipPoint;
            if (circleList.circleList.Count == 0) tipPoint = CanvasCenter; 
            else tipPoint = circleList.circleList.Last().tip;

            TrailPoints.Add(tipPoint);
            TrailPoly.Points = TrailPoints;
            
            Canvas.SetLeft(TrailPoly, CanvasCenter.X);
            Canvas.SetTop(TrailPoly, CanvasCenter.Y);
            theCanvas.Children.Add(TrailPoly);
        }

        public void ResetTrail()
        {
            TrailPoints.Clear();
            TrailPoly.Points.Clear();
            
            Point tipPoint;
            if (circleList.circleList.Count == 0) tipPoint = CanvasCenter; 
            else tipPoint = circleList.circleList.Last().tip;

            TrailPoints.Add(tipPoint);
            TrailPoly.Points = TrailPoints;

            theCanvas.Children.Add(TrailPoly);
        }

        public void DrawTrail(Circle c)
        {
            //theCanvas.Children.Remove(TrailPoly);
            TrailPoints.Add(c.tip);
            TrailPoly.Points = TrailPoints;
            //theCanvas.Children.Add(TrailPoly);
        }

        public void DrawCircle(Circle c)
        {
            Ellipse Circle = new Ellipse();
            //myEllipse.Cursor = Cursors.Hand;
            Circle.Stroke = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
            Circle.Width = 2 * c.radius;
            Circle.Height = 2 * c.radius;

            DrawnCircles.Add(Circle);

            Canvas.SetLeft(Circle, CanvasCenter.X + c.center.X - c.radius);
            Canvas.SetTop(Circle, CanvasCenter.Y + c.center.Y - c.radius);
            theCanvas.Children.Add(Circle);
        }

        public void DrawLine(Circle c)
        {
            Line Line = new Line();
            Line.Stroke = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
            Line.X1 = c.center.X;
            Line.Y1 = c.center.Y;
            Line.X2 = c.tip.X;
            Line.Y2 = c.tip.Y;

            DrawnLines.Add(Line);

            Canvas.SetLeft(Line, CanvasCenter.X);
            Canvas.SetTop(Line, CanvasCenter.Y);
            theCanvas.Children.Add(Line);
        }
    }






}