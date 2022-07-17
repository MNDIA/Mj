using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Mj.View
{
    /// <summary>
    /// UMessageBox.xaml 的交互逻辑
    /// </summary>
    public partial class UMessageBox : Window
    {
        private Common.IniBase ini = new Common.IniBase(System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\IndexOfMagic\ppsspp\set");//启用 ini
        public UMessageBox()
        {
            InitializeComponent();
        }
        public string ZDL { get { return this.zdl.Text; } set { this.zdl.Text = value; } }

        private void OK(object sender, MouseButtonEventArgs e)
        {
            Common.CommonSTA.IsOK = true;
            this.Hide();
        }

        private void Danji(object sender, MouseButtonEventArgs e)
        {
            Common.CommonSTA.Danji = true;
            this.Hide();
        }
        private void Nomore(object sender, MouseButtonEventArgs e)
        {
            ini.IniWriteValue("setgame", "不提示", "1");
            Common.CommonSTA.IsOK = true;
            this.Hide();
        }

        private void OVERRIDECLOSING(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Common.CommonSTA.IsOK = true;
            this.Hide();
            e.Cancel = true;
        }

        private void Color1(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.color1.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffffffff"));
        }
        private void Color2(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.color2.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffffffff"));
        }
        private void Color3(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.color3.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffffffff"));
        }

        private void ColorB(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.color1.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFC7C7D1"));
            this.color2.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFC7C7D1"));
            this.color3.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFC7C7D1"));
        }
    }
}
