
using Mj.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// XFC.xaml 的交互逻辑
    /// </summary>
    public partial class XfcView : Window
    {
        private Common.IniBase ini = new Common.IniBase(System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\IndexOfMagic\ppsspp\set");//启用 ini
        public ViewModel.XfcViewModel XfcViewModel = new ViewModel.XfcViewModel();
        #region DPI
        [System.Runtime.InteropServices.DllImport("gdi32.dll", EntryPoint = "GetDeviceCaps", SetLastError = true)]
        public static extern int GetDeviceCaps(IntPtr hdc, int nIndex);
        enum DeviceCap
        {
            VERTRES = 10,
            PHYSICALWIDTH = 110,
            SCALINGFACTORX = 114,
            DESKTOPVERTRES = 117,
        }
        public static double GetScreenScalingFactor()
        {
            var g = System.Drawing.Graphics.FromHwnd(IntPtr.Zero);
            IntPtr desktop = g.GetHdc();
            var physicalScreenHeight = GetDeviceCaps(desktop, (int)DeviceCap.DESKTOPVERTRES);
            return physicalScreenHeight;
        }
        #endregion
        public XfcView()
        {
            InitializeComponent();
            this.DataContext = XfcViewModel;
            //重制悬浮窗位置
            //ini.IniWriteValue("xfc", "自定义位置", "0");
            
            #region 大小
            if (ini.IniReadValue("xfc", "自定义大小") == "1")
            {
                XfcViewModel.Zidingyibig = double.Parse(ini.IniReadValue("xfc", "big"));
                #region DPI禁用
                if (ini.IniReadValue("window", "DPI") != "1")
                {
                    xfcwindow.MaxHeight = 10000 * Common.CommonSTA.dpi;
                    xfcwindow.Height = (SystemParameters.PrimaryScreenHeight * XfcViewModel.Zidingyibig * Common.CommonSTA.dpi * Common.CommonSTA.dpi) + 5;
                }
                else
                {
                    xfcwindow.Height = (SystemParameters.PrimaryScreenHeight * XfcViewModel.Zidingyibig) + 5;
                }
                #endregion
                xfcwindow.Width = xfcwindow.Height * 0.904;
            }
            else
            {
                xfcwindow.Height = (SystemParameters.PrimaryScreenHeight * 0.2 * CommonSTA.dpi) + 5;
            }
            xfcwindow.Width = xfcwindow.Height * 0.904;
            #endregion
            #region 位置
            if (ini.IniReadValue("xfc", "自定义位置") == "1")
            {
                xfcwindow.Top = SystemParameters.PrimaryScreenHeight * double.Parse(ini.IniReadValue("xfc", "top"));
                xfcwindow.Left = SystemParameters.PrimaryScreenWidth * double.Parse(ini.IniReadValue("xfc", "left"));
            }
            else
            {
                xfcwindow.Top = SystemParameters.PrimaryScreenHeight * 0.12;
                xfcwindow.Left = SystemParameters.PrimaryScreenWidth * 0.7;
            }
            #endregion
        }
        #region 左键拖动xfcMove
        private void XfcMove(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
            if (e.LeftButton == MouseButtonState.Released)
            {
                ini.IniWriteValue("xfc", "自定义位置", "1");
                ini.IniWriteValue("xfc", "top", Math.Round(xfcwindow.Top / SystemParameters.PrimaryScreenHeight, 2).ToString());
                ini.IniWriteValue("xfc", "left", Math.Round(xfcwindow.Left / SystemParameters.PrimaryScreenWidth, 2).ToString());
            }
        }
        #endregion
        #region 事件
        #region 图像加载完后转换
        private void Image_AnimationCompleted(object sender, RoutedEventArgs e)
        {
            XfcViewModel.MSseeback = "Visiable";
            pinggif.Visibility = Visibility.Hidden;
            Bs(sender, e);
        }
        #endregion

        #region 开启或关闭悬浮窗PING
        private void Bs(object sender, RoutedEventArgs e)
        {
            
            if (CommonSTA.Doping2 == 1)
            {
                CommonSTA.Doping2 = 0;
                if (CommonSTA.Doping == 0)
                {
                    CommonSTA.Noping = 1;
                }
                CommonSTA.Noping2 = 0;
                XfcViewModel.MSpng = "../Assets/ip-kong.png";
                XfcViewModel.MSsee = "Visiable";
                System.Threading.Tasks.Task.Run(() =>
                {
                    while (true)
                    {
                        if (CommonSTA.Noping2 == 1 || CommonSTA.Noping3 == 1)
                        {
                            break;
                        }
                        try
                        {
                            System.Net.NetworkInformation.PingReply pingReply = CommonSTA.ping2.Send(CommonSTA._ip);
                            if (pingReply.Status == System.Net.NetworkInformation.IPStatus.Success)
                            {
                                if (pingReply.RoundtripTime < 45)
                                {
                                    XfcViewModel.MScolor = "Green";
                                }
                                else if (pingReply.RoundtripTime < 70)
                                {
                                    XfcViewModel.MScolor = "#f27900";
                                }
                                else
                                {
                                    XfcViewModel.MScolor = "Red";
                                }
                                XfcViewModel.MStxt = pingReply.RoundtripTime + "";
                                XfcViewModel.Ziti1 = 170;
                                XfcViewModel.MSFont = "Normal";
                                CommonSTA.PING = true;
                            }
                            else
                            {
                                XfcViewModel.MScolor = "Red";
                                XfcViewModel.MStxt = "超 时";
                                XfcViewModel.Ziti1 = 85;
                                XfcViewModel.MSFont = "Heavy";
                                CommonSTA.PING = false;
                            }
                        }
                        catch (Exception)
                        {
                            XfcViewModel.MScolor = "Red";
                            XfcViewModel.MStxt = "IP错误";
                            XfcViewModel.Ziti1 = 85;
                            XfcViewModel.MSFont = "Heavy";
                            CommonSTA.PING = false;
                            System.Threading.Thread.Sleep(3000); break;
                        }
                        if (CommonSTA.Noping2 == 0)
                        {
                            System.Threading.Thread.Sleep(1000);
                        }
                    }
                }).ContinueWith<int>((t) =>
                {

                    XfcViewModel.MSsee = "Hidden";
                    XfcViewModel.MSpng = "../Assets/ip.png";
                    XfcViewModel.MScolor = "#183139";
                    XfcViewModel.MStxt = "检查中";
                    XfcViewModel.Ziti1 = 85;
                    XfcViewModel.MSFont = "Heavy";
                    CommonSTA.Noping2 = 0;
                    CommonSTA.Doping2 = 1;
                    CommonSTA.PING = false;
                    return 1;
                });
            }
            else
            {
                CommonSTA.Noping2 = 1;
                XfcViewModel.MScolor = "#183139";
                XfcViewModel.MStxt = "关闭中";
                XfcViewModel.Ziti1 = 85;
                XfcViewModel.MSFont = "Heavy";
            }
            
        }
        #endregion
        #region 自定义IP显隐
        private void PING(object sender, RoutedEventArgs e)
        {
            if (ipgrid.Visibility == Visibility.Visible)
            {
                ipgrid.Visibility = Visibility.Hidden;
            }
            else
            {
                biggrid.Visibility = Visibility.Hidden;
                ipgrid.Visibility = Visibility.Visible;
            }
            //if (CommonSTA._ip.Length > 6 && CommonSTA._ip.Length < 15 && CommonSTA._ip.Contains("."))
            //{
            //    CommonSTA._iptishi = "";
            //    //do
            //}
            //else
            //{
            //    CommonSTA._iptishi = "请输入正确的地址";
            //}
        } 
        #endregion

        #region 蹦
        private void Beng(object sender, RoutedEventArgs e)
        {
            CommonSTA.Noping2 = 1;
            Random rd = new Random();
            if (CommonSTA.Leisile > rd.Next(2, 4))
            {
                Hide();
                CommonSTA.XDS = 0;
                CommonSTA.Noping3 = 1;
                MessageBox.Show("小电视累死了(溜");
            }
            else
            {
                XfcViewModel.MSsee = "Hidden";
                XfcViewModel.MSseeback = "Hidden";
                XfcViewModel.GIFmany = "9x";
                pinggif.Visibility = Visibility.Visible;
                XfcViewModel.GIFmany = rd.Next(1, 6) + "x";
                //++CommonSTA.Leisile;
            }
        }
        #endregion

        #region 自定义大小显隐
        private void ZidingyiBig(object sender, RoutedEventArgs e)
        {
            if (biggrid.Visibility == Visibility.Visible)
            {
                biggrid.Visibility = Visibility.Hidden;
            }
            else
            {
                ipgrid.Visibility = Visibility.Hidden;
                biggrid.Visibility = Visibility.Visible;

            }
        } 
        #endregion
        #region 自定义大小重制
        private void Chongzhibig(object sender, RoutedEventArgs e)
        {
            XfcViewModel.Zidingyibig = 0.2;
            xfcwindow.Height = (System.Windows.SystemParameters.PrimaryScreenHeight * 0.2) + 5;
            xfcwindow.Width = xfcwindow.Height * 0.904;
        } 
        #endregion
        #region 自定义大小应用
        private void Yingyongbig(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            xfcwindow.Height = (System.Windows.SystemParameters.PrimaryScreenHeight * XfcViewModel.Zidingyibig) + 5;
            xfcwindow.Width = xfcwindow.Height * 0.904;
            if (XfcViewModel.Zidingyibig != 0.2)
            {
                ini.IniWriteValue("xfc", "自定义大小", "1");
            }
        }
        #endregion
        #region 关闭大小面板
        public void Closebiggrid(object sender, RoutedEventArgs e)
        {
            biggrid.Visibility = Visibility.Hidden;
        }
        #endregion
        #region 确定并关闭IP面板
        public void Closeipgrid(object sender, RoutedEventArgs e)
        {
            if (XfcViewModel.IP.Length > 6 && XfcViewModel.IP.Length < 15 && XfcViewModel.IP.Contains("."))
            {
                XfcViewModel.IPtishi = "";
                ini.IniWriteValue("setgame", "IP", XfcViewModel.IP);
                ipgrid.Visibility = Visibility.Hidden;
            }
            else
            {
                XfcViewModel.IPtishi = "请输入正确的地址";
            }
        }
        #endregion
        #region 关闭
        private void Close(object sender, RoutedEventArgs e)
        {
            this.Hide();
            CommonSTA.Noping3 = 1;
            CommonSTA.XDS = 0;
        }




        #endregion

        #endregion

       
    }
}
