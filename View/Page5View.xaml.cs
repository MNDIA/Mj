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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mj.View
{
    /// <summary>
    /// Page5View.xaml 的交互逻辑
    /// </summary>
    public partial class Page5View : UserControl
    {
        public Page5View()
        {
            InitializeComponent();
            lujingiso.Text = Common.CommonSTA.LujingISO;
            lujingpsp.Text = Common.CommonSTA.LujingPSP;
            lujingfcn.Text = Common.CommonSTA.LujingFCN;
       
        }

        private void ISOLiulan(object sender, MouseButtonEventArgs e)
        {

        }
        private void ISOMoren(object sender, RoutedEventArgs e)
        {
            ViewModel.IndexViewModel.ini.IniWriteValue("setgame", "ISO路径", "");
            Common.CommonSTA.LujingISO = Environment.CurrentDirectory + @"\memstick\PSP\GAME\";
            lujingiso.Text = Common.CommonSTA.LujingISO;
        }
        private void ISOYES(object sender, RoutedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(lujingiso.Text, @"^(?:[a-zA-Z]:\\)(?:[^\/|<>?*:""]*\\)*$"))
            {
                Common.CommonSTA.LujingISO = lujingiso.Text;
                ViewModel.IndexViewModel.ini.IniWriteValue("setgame", "ISO路径", Common.CommonSTA.LujingISO);
                lujingiso.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#008000"));
            }
            else
            {
                lujingiso.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#fb1b1b"));
            }
           
        }

        private void PSPLiulan(object sender, MouseButtonEventArgs e)
        {

        }
        private void PSPMoren(object sender, RoutedEventArgs e)
        {
            ViewModel.IndexViewModel.ini.IniWriteValue("setgame", "PSP路径", "");
            Common.CommonSTA.LujingPSP = Environment.CurrentDirectory + @"\";
            lujingpsp.Text = Common.CommonSTA.LujingPSP;
        }
        private void PSPYES(object sender, RoutedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(lujingiso.Text, @"^(?:[a-zA-Z]:\\)(?:[^\/|<>?*:""]*\\)*$"))
            {
                Common.CommonSTA.LujingPSP = lujingpsp.Text;
                ViewModel.IndexViewModel.ini.IniWriteValue("setgame", "PSP路径", Common.CommonSTA.LujingPSP);
                lujingpsp.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#008000"));
            }
            else
            {
                lujingpsp.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#fb1b1b"));
            }
        }

        private void FCNLiulan(object sender, MouseButtonEventArgs e)
        {
            
        }
        private void FCNMoren(object sender, RoutedEventArgs e)
        {
            ViewModel.IndexViewModel.ini.IniWriteValue("setgame", "FCN路径", "");
            Common.CommonSTA.LujingFCN = Environment.CurrentDirectory + @"\FCN.exe";
            lujingfcn.Text = Common.CommonSTA.LujingFCN;
        }
        private void FCNYES(object sender, RoutedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(lujingiso.Text, @"^(?:[a-zA-Z]:\\)(?:[^\/|<>?*:""]*\\)*(?:[^\/|<>?*:""]*\.exe)*$"))
            {
                Common.CommonSTA.LujingFCN = lujingfcn.Text;
                ViewModel.IndexViewModel.ini.IniWriteValue("setgame", "FCN路径", Common.CommonSTA.LujingFCN);
                lujingfcn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#008000"));
            }
            else
            {
                lujingfcn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#fb1b1b"));
            }
        }
    }
}
