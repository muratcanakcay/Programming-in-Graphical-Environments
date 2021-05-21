using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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
        private long tickCount = 0;
        private Point CanvasCenter = new Point();
        private CircleList circleList = new CircleList();
        private readonly Polyline TrailPoly = new Polyline();
        private readonly PointCollection TrailPoints = new PointCollection();
        private readonly DispatcherTimer timer = new DispatcherTimer(DispatcherPriority.Render);
        private readonly ObservableCollection<UIElement> DrawnCircles = new ObservableCollection<UIElement>();
        private readonly ObservableCollection<UIElement> DrawnLines = new ObservableCollection<UIElement>();
        
        private bool DrawCircles = true;
        private bool DrawLines = true;

        private Stopwatch stopwatch = new Stopwatch();
        
        //-----------------
        
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CanvasCenter = new Point(theCanvas.ActualWidth / 2, theCanvas.ActualHeight / 2);
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += new EventHandler(Tick);

            DataContext = circleList.circles;
            InitializePlot();
        }

        private void InitializePlot()
        {
            timer.Stop();
            stopwatch.Stop();
            stopwatch.Reset();
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
        private void InitializeTrailAndPen()
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

        private void Tick(object sender, EventArgs e)
        {
            long elapsed = stopwatch.ElapsedMilliseconds;
            
            if (elapsed > 9999) elapsed = 10000;
            
            tickCount = elapsed;
            progressBar.Value = elapsed;
                
            ClearCirclesAndLines(); // also clears the TrailPen
            DrawCirclesAndLines();
            DrawTrailPen();
            DrawTrail();

            if(elapsed == 10000)
            {
                timer.Stop();
                stopwatch.Stop();
                return;
            }
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
        private void DrawCircle(Circle c)
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
        private void DrawLine(Circle c)
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
        private void DrawTrailPen()
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
        private void DrawTrail()
        {
            if (circleList.circles.Count > 0) TrailPoints.Add(circleList.circles.Last().tip);
        }
        
        // ----------

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            circleList.Reset();
            InitializePlot();
        }
        private void OpenButon_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML Save File|*.xml";

            if (openFileDialog.ShowDialog() != true || openFileDialog.FileName.Equals("")) return;
            
            using var fileStream = new FileStream($"{openFileDialog.FileName}", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            try
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(CircleList));
                circleList = (CircleList)deserializer.Deserialize(fileStream);
            }
            catch (Exception)
            {
                MessageBox.Show("Error in file!", "Error!");
                return;
            }

            DataContext = circleList.circles;
            InitializePlot();
        }
        private void SaveButon_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML Save File|*.xml";

            if (saveFileDialog.ShowDialog() != true || saveFileDialog.FileName.Equals("")) return;

            using var fileStream = new FileStream($"{saveFileDialog.FileName}", FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            XmlSerializer serializer = new XmlSerializer(typeof(CircleList));
            serializer.Serialize(fileStream, circleList);
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to leave?", "Exit", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                Close();
        }
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
            stopwatch.Start();
        }
        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            stopwatch.Stop();;
        }
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            InitializePlot();
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
        
        private void DataGrid_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete) InitializePlot();
        }

        private void Circles_Modified(object sender, System.Windows.Data.DataTransferEventArgs e)
        {
            InitializePlot();
        }
    }
}