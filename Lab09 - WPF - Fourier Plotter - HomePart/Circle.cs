﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab09___WPF___Fourier_Plotter___HomePart
{
      public class CircleList
        {
            public ObservableCollection<Circle> circles = new ObservableCollection<Circle>();

            public void Reset()
            {
                circles.Clear();
            }
        }
        public class Circle : INotifyPropertyChanged
        {
            [XmlIgnore]
            public Point center;
            [XmlIgnore]
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
            [XmlIgnore]
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
                    //MainWin.InitializePlot();
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
}
