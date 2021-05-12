using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace Lab09___WPF___Fourier_Plotter___LabPart
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer = new DispatcherTimer();
        private int tickCount = 0;
        private ObservableCollection<UIElement> UIElements = new ObservableCollection<UIElement>();
        
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new List<RowData>{new RowData() { col1=50, col2=80, col3=100 } };

            timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            timer.Tick += new EventHandler(Tick);
        }


        private void ExitButtonClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }


        // dataGrid data
        public class RowData
        {
            public int col1 {get; set;}
            public int col2 {get; set;}
            public int col3 {get; set;}
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
            
            //foreach(var element  in UIElements)
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
                myEllipse.Width = ((List<RowData>)this.DataContext)[0].col1;
                myEllipse.Height = ((List<RowData>)this.DataContext)[0].col1;
            
                UIElements.Add(myEllipse);
                
                Canvas.SetLeft(myEllipse, (theCanvas.ActualWidth - myEllipse.Width) / 2) ;
                Canvas.SetTop(myEllipse, (theCanvas.ActualHeight - myEllipse.Height) / 2);

                theCanvas.Children.Add(myEllipse);
                UIElements.Add(myEllipse);
            }
        }
    }




    

}