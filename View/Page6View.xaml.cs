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
    /// Page6View.xaml 的交互逻辑
    /// </summary>
    public partial class Page6View : UserControl
    {
        public Page6View()
        {
            InitializeComponent();
            #region 配置文件setgame
            if (ViewModel.IndexViewModel.ini.IniReadValue("window", "主题") == "幻影白")
            {
                zhuti1.IsChecked = true;
            }
            if (ViewModel.IndexViewModel.ini.IniReadValue("window", "主题") == "苹果绿")
            {
                zhuti2.IsChecked = true;
            }
            if (ViewModel.IndexViewModel.ini.IniReadValue("window", "主题") == "深空灰")
            {
                zhuti3.IsChecked = true;
            }
            if (ViewModel.IndexViewModel.ini.IniReadValue("window", "Yuanjiao") == "1")
            {
                yuanjiao.IsChecked = true;
            }
            if (ViewModel.IndexViewModel.ini.IniReadValue("window", "DPI") == "1")
            {
                dpi.IsChecked = true;
            }
            #endregion
        }


        private void Yuanjiao(object sender, RoutedEventArgs e)
        {
            if (yuanjiao.IsChecked == true)
            {
                ViewModel.IndexViewModel.IndexModel.Yuanjiao = "10";
                ViewModel.IndexViewModel.IndexModel.Yuanjiao2 = "10,10,0,0";
                ViewModel.IndexViewModel.IndexModel.Yuanjiao3 = "0,10,0,0";
                ViewModel.IndexViewModel.IndexModel.Yuanjiao4 = "10,0,0,0";
                ViewModel.IndexViewModel.ini.IniWriteValue("window", "Yuanjiao", "1");
            }
            else
            {
                ViewModel.IndexViewModel.IndexModel.Yuanjiao = "0";
                ViewModel.IndexViewModel.IndexModel.Yuanjiao2 = "0,0,0,0";
                ViewModel.IndexViewModel.IndexModel.Yuanjiao3 = "0,0,0,0";
                ViewModel.IndexViewModel.IndexModel.Yuanjiao4 = "0,0,0,0";
                ViewModel.IndexViewModel.ini.IniWriteValue("window", "Yuanjiao", "0");
            }
        }
        private void Zhuti1(object sender, RoutedEventArgs e)
        {
            if (zhuti1.IsChecked == true)
            {
                #region 主题
                ViewModel.IndexViewModel.IndexModel.Line = "#DDD";
                ViewModel.IndexViewModel.IndexModel.Line2 = "#A4A4A4";
                ViewModel.IndexViewModel.IndexModel.Whites2 = "#EDEDED";
                ViewModel.IndexViewModel.IndexModel.Whitem = "#DFDFDF";
                ViewModel.IndexViewModel.IndexModel.WhitesFF = "#FFEDEDED";
                ViewModel.IndexViewModel.IndexModel.Whites00 = "#00EDEDED";
                ViewModel.IndexViewModel.IndexModel.Whites33 = "#33EDEDED";
                ViewModel.IndexViewModel.IndexModel.Whites66 = "#66EDEDED";
                ViewModel.IndexViewModel.IndexModel.WhitesAA = "#AAEDEDED";
                ViewModel.IndexViewModel.IndexModel.Darks66 = "#6600A8F3";
                ViewModel.IndexViewModel.IndexModel.DarksAA = "#AA00A8F3";
                ViewModel.IndexViewModel.IndexModel.DarksFF = "#FF00A8F3";
                ViewModel.IndexViewModel.IndexModel.Playdarks1 = "#FF00A8F3";
                ViewModel.IndexViewModel.IndexModel.Playdarks2 = "#FF20BAFF";
                ViewModel.IndexViewModel.IndexModel.Close66 = "#66EDEDED";
                ViewModel.IndexViewModel.IndexModel.CloseAA = "#AAEDEDED";
                #endregion
                ViewModel.IndexViewModel.ini.IniWriteValue("window", "主题", "幻影白");
            }
        }
        private void Zhuti2(object sender, RoutedEventArgs e)
        {
            if (zhuti2.IsChecked == true)
            {
                #region 主题
                ViewModel.IndexViewModel.IndexModel.Whites2 = "#CCE8CF";
                ViewModel.IndexViewModel.IndexModel.WhitesFF = "#FFCCE8CF";
                ViewModel.IndexViewModel.IndexModel.Whites00 = "#00CCE8CF";
                ViewModel.IndexViewModel.IndexModel.Whites33 = "#33CCE8CF";
                ViewModel.IndexViewModel.IndexModel.Whites66 = "#66CCE8CF";
                ViewModel.IndexViewModel.IndexModel.WhitesAA = "#AACCE8CF";

                ViewModel.IndexViewModel.IndexModel.Whitem = "#B6E2BB";
                ViewModel.IndexViewModel.IndexModel.Line = "#D4E2D5";
                ViewModel.IndexViewModel.IndexModel.Line2 = "#90A692";

                ViewModel.IndexViewModel.IndexModel.DarksFF = "#FFB6E2BB";
                ViewModel.IndexViewModel.IndexModel.Darks66 = "#66B6E2BB";
                ViewModel.IndexViewModel.IndexModel.DarksAA = "#AAB6E2BB";
                ViewModel.IndexViewModel.IndexModel.Close66 = "#22CCE8CF";
                ViewModel.IndexViewModel.IndexModel.CloseAA = "#FFCCE8CF";
                #endregion
                ViewModel.IndexViewModel.ini.IniWriteValue("window", "主题", "苹果绿");
            }
        }
        private void Zhuti3(object sender, RoutedEventArgs e)
        {
            if (zhuti3.IsChecked == true)
            {
                #region 主题
                
                ViewModel.IndexViewModel.IndexModel.Whites2 = "#2A2C37";
                ViewModel.IndexViewModel.IndexModel.WhitesFF = "#FF2A2C37";
                ViewModel.IndexViewModel.IndexModel.Whites00 = "#002A2C37";
                ViewModel.IndexViewModel.IndexModel.Whites33 = "#332A2C37";
                ViewModel.IndexViewModel.IndexModel.Whites66 = "#662A2C37";
                ViewModel.IndexViewModel.IndexModel.WhitesAA = "#AA2A2C37";

                ViewModel.IndexViewModel.IndexModel.Whitem = "#393C4A";
                ViewModel.IndexViewModel.IndexModel.Line = "#838489";
                ViewModel.IndexViewModel.IndexModel.Line2 = "#51535A";

                ViewModel.IndexViewModel.IndexModel.DarksFF = "#FF3F3F46";
                ViewModel.IndexViewModel.IndexModel.Darks66 = "#663F3F46";
                ViewModel.IndexViewModel.IndexModel.DarksAA = "#AA3F3F46";
                ViewModel.IndexViewModel.IndexModel.Close66 = "#992A2C37";
                ViewModel.IndexViewModel.IndexModel.CloseAA = "#BB2A2C37";
                #endregion
                ViewModel.IndexViewModel.ini.IniWriteValue("window", "主题", "深空灰");
            }
        }

        private void DPI(object sender, RoutedEventArgs e)
        {
            if (dpi.IsChecked == true)
            {
                ViewModel.IndexViewModel.ini.IniWriteValue("window", "DPI", "1");
            }
            else
            {
                ViewModel.IndexViewModel.ini.IniWriteValue("window", "DPI", "0");
            }
        }
    }
}
