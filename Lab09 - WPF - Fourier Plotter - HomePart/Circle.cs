﻿using System;
using System.Windows;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

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
            get => _radius;
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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        public void Rotate(long tickCount)
        {
            tip.X = center.X + radius * Math.Cos(tickCount * frequency * 2 * Math.PI / 10000);
            tip.Y = center.Y + radius * Math.Sin(tickCount * frequency * 2 * Math.PI / 10000);
        }

        public void Reset()
        {
            center = StartingCenter;
            tip.X = center.X + radius;
            tip.Y = center.Y;
        }
    }
}