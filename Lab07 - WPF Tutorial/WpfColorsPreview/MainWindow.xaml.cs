using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace WpfColorsPreview
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var props = typeof(Colors).GetProperties(BindingFlags.Static | BindingFlags.Public);
            var colorInfos = props.Select(prop =>
            {
                var color = (Color)prop.GetValue(null, null);
                return new ColorInfo()
                {
                    Name = prop.Name,
                    Rgb = color,
                    RgbInfo = $"R:{ color.R} G:{ color.G}, B:{ color.B}"
                };
            });
            
            this.DataContext = colorInfos;
        }
    }
}
