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
        private readonly DispatcherTimer timer = new DispatcherTimer(DispatcherPriority.Render);
        private int tickCount = 0;
        private Point CanvasCenter = new Point();
        private CircleList circleList = new CircleList();
        private readonly ObservableCollection<UIElement> DrawnCircles = new ObservableCollection<UIElement>();
        private readonly ObservableCollection<UIElement> DrawnLines = new ObservableCollection<UIElement>();
        private readonly Polyline TrailPoly = new Polyline();
        private readonly PointCollection TrailPoints = new PointCollection();
        
        private bool DrawCircles = true;
        private bool DrawLines = true;

        public static MainWindow MainWin;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CanvasCenter = new Point(theCanvas.ActualWidth / 2, theCanvas.ActualHeight / 2);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            timer.Tick += new EventHandler(Tick);

            MainWin = this;

            // ----------------

            XmlSerializer deserializer = new XmlSerializer(typeof(CircleList));

            TextReader reader = new StreamReader("test2.xml");

            circleList = (CircleList)deserializer.Deserialize(reader);

            reader.Close();

            foreach (var c in circleList.circles)
            {
                Debug.WriteLine($"{c.radius}, {c.frequency}");
            }

            CalculateTipsAndCenters();
            InitializeCirclesAndLines();
            InitializeTrailAndPen();

            DataContext = circleList.circles;




            // -----------

        }

        public void InitializePlot()
        {
            timer.Stop();
            tickCount = 0;
            progressBar.Value = 0;
            
            CalculateTipsAndCenters();
            InitializeCirclesAndLines();
            InitializeTrailAndPen();
        }
        
        private void CalculateTipsAndCenters()
        {
            if (circleList.circles.Count == 0) return;
            
            circleList.circles[0].StartingCenter = new Point(0, 0);
            circleList.circles[0].tip = new Point(circleList.circles[0].radius, 0);

            for (int i = 1; i < circleList.circles.Count; i++)
            {
                circleList.circles[i].StartingCenter = circleList.circles[i - 1].tip;
                circleList.circles[i].tip = new Point(circleList.circles[i].StartingCenter.X + circleList.circles[i].radius, circleList.circles[i].StartingCenter.Y);
            }
        }

        private void InitializeCirclesAndLines()
        {
            theCanvas.Children.Clear();
            DrawnCircles.Clear();
            DrawnLines.Clear();
            
            foreach (var c in circleList.circles)
            {
                c.Reset();
                DrawCircle(c);
                DrawLine(c);
            }
        }

        public void InitializeTrailAndPen()
        {
            TrailPoints.Clear();
            TrailPoly.Points.Clear();
            
            TrailPoly.Stroke = new SolidColorBrush(Colors.Blue);
            TrailPoly.StrokeThickness = 1;

            Point tipPoint;
            if (circleList.circles.Count == 0) tipPoint = CanvasCenter; 
            else tipPoint = circleList.circles.Last().tip;

            TrailPoints.Add(tipPoint);
            TrailPoly.Points = TrailPoints;
            
            Canvas.SetLeft(TrailPoly, CanvasCenter.X);
            Canvas.SetTop(TrailPoly, CanvasCenter.Y);
            theCanvas.Children.Add(TrailPoly);

            DrawTrailPen();
        }

        private void ExitButtonClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public class CircleList
        {
            public ObservableCollection<Circle> circles;
        }

        public class Circle : INotifyPropertyChanged
        {
            public Point center;
            public Point tip; // tip of the line
            
            private int _radius;
            public int radius
            {
                get =>_radius;
                set
                {
                    _radius = value;
                    OnPropertyRaised("radius");
                }
            }
            private int _frequency;
            public int frequency
            {
                get => _frequency;
                set
                {
                    _frequency = value;
                    OnPropertyRaised("frequency");
                }
            }
            [XmlIgnore]
            private Point _StartingCenter;
            public Point StartingCenter
            {
                get => _StartingCenter;                
                set
                {
                    _StartingCenter = value;
                    center = StartingCenter;
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            private void OnPropertyRaised(string propertyname)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
                    MainWin.InitializePlot();
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

        public void Tick(object sender, EventArgs e)
        {
            if (++tickCount > 666)
            {
                timer.Stop();
                return;
            }

            ++progressBar.Value;

            ClearCirclesAndLines(); // also clears the TrailPen
            DrawCirclesAndLines();
            DrawTrailPen();
            DrawTrail();
        }

        private void ClearCirclesAndLines()
        {
            foreach (var circle in DrawnCircles)
                theCanvas.Children.Remove(circle);

            foreach (var line in DrawnLines)
                theCanvas.Children.Remove(line);
        }

        private void DrawCirclesAndLines()
        {
            Circle lastCircle = null;
            foreach (Circle c in circleList.circles)
            {
                if (lastCircle != null) c.center = lastCircle.tip;

                c.Rotate(tickCount);
                DrawCircle(c);
                DrawLine(c);

                lastCircle = c;
            }
        }

        public void DrawTrail()
        {
            if (circleList.circles.Count > 0) TrailPoints.Add(circleList.circles.Last().tip);
        }

        public void DrawTrailPen()
        {
            Ellipse Pen = new Ellipse();
            Pen.Stroke = new SolidColorBrush(Colors.Red);
            Pen.Fill = new SolidColorBrush(Colors.Red);
            Pen.Width = 4;
            Pen.Height = 4;

            DrawnCircles.Add(Pen);

            Point penPos;
            if (circleList.circles.Count > 0) penPos = circleList.circles.Last().tip;
            else penPos = new Point(0, 0);

            Canvas.SetLeft(Pen, CanvasCenter.X + penPos.X - 2);
            Canvas.SetTop(Pen, CanvasCenter.Y + penPos.Y - 2);
            theCanvas.Children.Add(Pen);
        }
        
        public void DrawCircle(Circle c)
        {
            Ellipse Circle = new Ellipse();
            Circle.Stroke = new SolidColorBrush(Colors.Black);
            Circle.Width = 2 * c.radius;
            Circle.Height = 2 * c.radius;
            if (!DrawCircles) Circle.Visibility = Visibility.Hidden;

            DrawnCircles.Add(Circle);

            Canvas.SetLeft(Circle, CanvasCenter.X + c.center.X - c.radius);
            Canvas.SetTop(Circle, CanvasCenter.Y + c.center.Y - c.radius);
            theCanvas.Children.Add(Circle);
        }
        
        public void DrawLine(Circle c)
        {
            Line Line = new Line();
            Line.Stroke = new SolidColorBrush(Colors.Black);
            Line.X1 = c.center.X;
            Line.Y1 = c.center.Y;
            Line.X2 = c.tip.X;
            Line.Y2 = c.tip.Y;
            if (!DrawLines) Line.Visibility = Visibility.Hidden;

            DrawnLines.Add(Line);

            Canvas.SetLeft(Line, CanvasCenter.X);
            Canvas.SetTop(Line, CanvasCenter.Y);
            theCanvas.Children.Add(Line);
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
            InitializePlot();
        }

        private void dataGrid_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete) InitializePlot();
        }

        private void DrawCircles_OnChecked(object sender, RoutedEventArgs e)
        {
            DrawCircles = true;
            
            foreach(var circle in DrawnCircles)
                circle.Visibility = Visibility.Visible;
        }

        private void DrawCircles_OnUnchecked(object sender, RoutedEventArgs e)
        {
            DrawCircles = false;

            foreach(var circle in DrawnCircles)
                circle.Visibility = Visibility.Hidden;
            DrawnCircles.Last().Visibility = Visibility.Visible;
        }

        private void DrawLines_OnChecked(object sender, RoutedEventArgs e)
        {
            DrawLines = true;
            
            foreach(var line in DrawnLines)
                line.Visibility = Visibility.Visible;
        }

        private void DrawLines_OnUnchecked(object sender, RoutedEventArgs e)
        {
            DrawLines = false;
             
            foreach(var line in DrawnLines)
                line.Visibility = Visibility.Hidden;
        }
    }
}