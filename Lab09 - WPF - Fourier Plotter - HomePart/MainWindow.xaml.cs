﻿using System;
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

            foreach(var c in circleList.circleList)
            {
                Debug.WriteLine($"{c.radius}, {c.frequency}");
            }

            CalculateTipsAndCenters();

            DataContext = circleList.circleList;

            foreach(var c in circleList.circleList) 
            {
                DrawCircle(c);
                DrawLine(c);
            }


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
            progressBar.Value=0;
            theCanvas.Children.Clear();
            
            foreach(var c in circleList.circleList) 
            {
                c.Reset();
                DrawCircle(c);
                DrawLine(c);
            }
        }

        public void Tick(object sender, EventArgs e)
        {
            

            if(++tickCount >= 666)
            {
                timer.Stop();
                return;
            }
            
            ++progressBar.Value;
            
            foreach(var circle in DrawnCircles)
                theCanvas.Children.Remove(circle);

            foreach (var line in DrawnLines)
                theCanvas.Children.Remove(line);

            Circle lastCircle = (Circle)null;
            foreach (Circle c in circleList.circleList)
            {
                if (lastCircle==null)
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

            DrawTrail(lastCircle);
        }

        public void DrawTrail(Circle c)
        {
                Ellipse trailEllipse = new Ellipse();
                //myEllipse.Cursor = Cursors.Hand;
                SolidColorBrush brush = new SolidColorBrush();
                brush.Color = Color.FromArgb(255, 255, 0, 0);
                trailEllipse.Stroke = brush;
                trailEllipse.Width = 3;
                trailEllipse.Height = 3;
                
                Canvas.SetLeft(trailEllipse, CanvasCenter.X + c.tip.X) ;
                Canvas.SetTop(trailEllipse, CanvasCenter.Y + c.tip.Y) ;
                theCanvas.Children.Add(trailEllipse);
        }
        
        public void DrawCircle(Circle c)
        {
                Ellipse myEllipse = new Ellipse();
                //myEllipse.Cursor = Cursors.Hand;
                SolidColorBrush brush = new SolidColorBrush();
                brush.Color = Color.FromArgb(255, 0, 0, 0);
                myEllipse.Stroke = brush;
                myEllipse.Width = 2 * c.radius;
                myEllipse.Height = 2 * c.radius;
            
                DrawnCircles.Add(myEllipse);
                
                Canvas.SetLeft(myEllipse, CanvasCenter.X + c.center.X - c.radius) ;
                Canvas.SetTop(myEllipse, CanvasCenter.Y + c.center.Y - c.radius) ;
                theCanvas.Children.Add(myEllipse);
        }

        public void DrawLine(Circle c)
        {
                Line myLine = new Line();
                SolidColorBrush brush = new SolidColorBrush();
                brush.Color = Color.FromArgb(255, 0, 0, 0);
                myLine.Stroke = brush;
                myLine.X1 = c.center.X;
                myLine.Y1 = c.center.Y;
                myLine.X2 = c.tip.X;
                myLine.Y2 = c.tip.Y;
            
                DrawnLines.Add(myLine);
                
                Canvas.SetLeft(myLine, CanvasCenter.X) ;
                Canvas.SetTop(myLine, CanvasCenter.Y) ;
                theCanvas.Children.Add(myLine);
        }
    }




    

}