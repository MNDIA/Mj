using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Mj.Common
{
    public class SizeHelper
    {
        public static readonly DependencyProperty SizeProperty = DependencyProperty.RegisterAttached("Width", typeof(double), typeof(SizeHelper), new FrameworkPropertyMetadata("",new PropertyChangedCallback(OnPropertyChanged)));
        public static double GetWidth(DependencyObject d)
        {
            return double.Parse(d.GetValue(SizeProperty).ToString());
        }
        public static void SetWidth(DependencyObject d,double value)
        {
            d.SetValue(SizeProperty, value);
        }
        private static void OnPropertyChanged(DependencyObject d,DependencyPropertyChangedEventArgs e)
        {
           
        }
    }
}
