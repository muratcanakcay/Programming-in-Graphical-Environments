using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Diagnostics;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace Lab09___WPF___Fourier_Plotter___HomePart
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private long tickCount = 0;
        private bool drawLines = true;
        private bool drawCircles = true;
        private Point CanvasCenter = new Point();
        private Stopwatch Stopwatch = new Stopwatch();
        private CircleList CircleList = new CircleList();
        private readonly Polyline TrailPoly = new Polyline();
        private readonly PointCollection TrailPoints = new PointCollection();
        private readonly DispatcherTimer Timer = new DispatcherTimer(DispatcherPriority.Render);
        private readonly ObservableCollection<UIElement> DrawnLines = new ObservableCollection<UIElement>();
        private readonly ObservableCollection<UIElement> DrawnCircles = new ObservableCollection<UIElement>();
        
        //-----------------
        
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CanvasCenter = new Point(theCanvas.ActualWidth / 2, theCanvas.ActualHeight / 2);
            Timer.Interval = TimeSpan.FromMilliseconds(10);
            Timer.Tick += new EventHandler(Tick);

            DataContext = CircleList.circles;
            InitializePlotter();
        }

        private void InitializePlotter()
        {
            Timer.Stop();
            Stopwatch.Stop();
            Stopwatch.Reset();
            tickCount = 0;
            progressBar.Value = 0;
            
            CalculateTipsAndCenters();
            InitializeCirclesAndLines();
            InitializeTrailAndPen();
        }
        private void CalculateTipsAndCenters()
        {
            if (CircleList.circles.Count == 0) return;
            
            CircleList.circles[0].StartingCenter = new Point(0, 0);
            CircleList.circles[0].tip = new Point(CircleList.circles[0].radius, 0);

            for (int i = 1; i < CircleList.circles.Count; i++)
            {
                CircleList.circles[i].StartingCenter = CircleList.circles[i - 1].tip;
                CircleList.circles[i].tip = new Point(CircleList.circles[i].StartingCenter.X + CircleList.circles[i].radius, CircleList.circles[i].StartingCenter.Y);
            }
        }
        private void InitializeCirclesAndLines()
        {
            theCanvas.Children.Clear();
            DrawnCircles.Clear();
            DrawnLines.Clear();
            
            foreach (var c in CircleList.circles)
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
            if (CircleList.circles.Count == 0) tipPoint = CanvasCenter; 
            else tipPoint = CircleList.circles.Last().tip;

            TrailPoints.Add(tipPoint);
            TrailPoly.Points = TrailPoints;
            
            Canvas.SetLeft(TrailPoly, CanvasCenter.X);
            Canvas.SetTop(TrailPoly, CanvasCenter.Y);
            theCanvas.Children.Add(TrailPoly);

            DrawTrailPen();
        }

        private void Tick(object sender, EventArgs e)
        {
            long elapsedTime = Stopwatch.ElapsedMilliseconds;
            
            if (elapsedTime > 9999) elapsedTime = 10000;
            
            tickCount = elapsedTime;
            progressBar.Value = elapsedTime;
                
            ClearCirclesAndLines(); // also clears the TrailPen
            DrawCirclesAndLines();
            DrawTrailPen();
            DrawTrail();

            if(elapsedTime == 10000)
            {
                Timer.Stop();
                Stopwatch.Stop();
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
            foreach (Circle c in CircleList.circles)
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
            if (!drawCircles) Circle.Visibility = Visibility.Hidden;

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
            if (!drawLines) Line.Visibility = Visibility.Hidden;

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
            if (CircleList.circles.Count > 0) penPos = CircleList.circles.Last().tip;
            else penPos = new Point(0, 0);

            Canvas.SetLeft(Pen, CanvasCenter.X + penPos.X - 2);
            Canvas.SetTop(Pen, CanvasCenter.Y + penPos.Y - 2);
            theCanvas.Children.Add(Pen);
        }
        private void DrawTrail()
        {
            if (CircleList.circles.Count > 0) TrailPoints.Add(CircleList.circles.Last().tip);
        }
        
        // ----------

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            CircleList.Reset();
            InitializePlotter();
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
                CircleList = (CircleList)deserializer.Deserialize(fileStream);
            }
            catch (Exception)
            {
                MessageBox.Show("Error in file!", "Error!");
                return;
            }

            DataContext = CircleList.circles;
            InitializePlotter();
        }
        private void SaveButon_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML Save File|*.xml";

            if (saveFileDialog.ShowDialog() != true || saveFileDialog.FileName.Equals("")) return;

            using var fileStream = new FileStream($"{saveFileDialog.FileName}", FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            XmlSerializer serializer = new XmlSerializer(typeof(CircleList));
            serializer.Serialize(fileStream, CircleList);
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to leave?", "Exit", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                Close();
        }
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            Timer.Start();
            Stopwatch.Start();
        }
        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            Timer.Stop();
            Stopwatch.Stop();;
        }
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            InitializePlotter();
        }
        
        private void DrawCircles_OnChecked(object sender, RoutedEventArgs e)
        {
            drawCircles = true;
            
            foreach(var circle in DrawnCircles)
                circle.Visibility = Visibility.Visible;
        }
        private void DrawCircles_OnUnchecked(object sender, RoutedEventArgs e)
        {
            drawCircles = false;

            foreach(var circle in DrawnCircles)
                circle.Visibility = Visibility.Hidden;
            DrawnCircles.Last().Visibility = Visibility.Visible;
        }
        private void DrawLines_OnChecked(object sender, RoutedEventArgs e)
        {
            drawLines = true;
            
            foreach(var line in DrawnLines)
                line.Visibility = Visibility.Visible;
        }
        private void DrawLines_OnUnchecked(object sender, RoutedEventArgs e)
        {
            drawLines = false;
             
            foreach(var line in DrawnLines)
                line.Visibility = Visibility.Hidden;
        }
        
        private void DataGrid_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete) InitializePlotter();
        }

        private void Circles_Modified(object sender, System.Windows.Data.DataTransferEventArgs e)
        {
            InitializePlotter();
        }
    }
}