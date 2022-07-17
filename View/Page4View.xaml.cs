using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
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
    /// Page4View.xaml 的交互逻辑
    /// </summary>
    public partial class Page4View : UserControl
    {
        private Common.IniBase ini = new Common.IniBase(System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\IndexOfMagic\ppsspp\set");//启用 ini
        public Page4View()
        {
            InitializeComponent();
            #region 配置文件setgame 底包模型剧情材质 10
            if (ini.IniReadValue("setgame", "自启") == "1")
            {
                autoplay.IsChecked = true;
            }
            if (ini.IniReadValue("setgame", "联机") == "0")
            {
                danjiplay.IsChecked = true;
            }
            if (ini.IniReadValue("setgame", "不提示") != "1")
            {
                baoxianplay.IsChecked = true;
            }
            if (ini.IniReadValue("setgame", "UE5") == "1")
            {
                ue5play.IsChecked = true;
            }
            if (ini.IniReadValue("window", "禁用托盘") == "0")
            {
                tuopan.IsChecked = true;
            }
            #endregion
           
            if (Common.CommonSTA.PSPName == "")
            {
                pspnameback.Visibility = Visibility.Visible;
            }
            else
            {
                pspname.Text = Common.CommonSTA.PSPName;
            }
            if (ini.IniReadValue("setgame", "金手指") == "1")
            {
                jinshouzhi.IsChecked = true;
                jinshouzhi.IsEnabled = true;
            }
        }

        #region 按钮
        private void Autoplay(object sender, RoutedEventArgs e)
        {
            if (autoplay.IsChecked == true)
            {
                ini.IniWriteValue("setgame", "自启", "1");
            }
            else
            {
                ini.IniWriteValue("setgame", "自启", "0");
            }
        }
        private void DanjiPlay(object sender, RoutedEventArgs e)
        {
            if (danjiplay.IsChecked == true)
            {
                ini.IniWriteValue("setgame", "联机", "0");
            }
            else
            {
                ini.IniWriteValue("setgame", "联机", "1");
            }
        }
        private void BaoxianPlay(object sender, RoutedEventArgs e)
        {
            if (baoxianplay.IsChecked == true)
            {
                ini.IniWriteValue("setgame", "不提示", "0");
            }
            else
            {
                ini.IniWriteValue("setgame", "不提示", "1");
            }
        }
        private void UE5Play(object sender, RoutedEventArgs e)
        {
            if (ue5play.IsChecked == true)
            {
                ini.IniWriteValue("setgame", "UE5", "1");
            }
            else
            {
                ini.IniWriteValue("setgame", "UE5", "0");
            }
        }
        private void Tuopan(object sender, RoutedEventArgs e)
        {
            if (tuopan.IsChecked == true)
            {
                ini.IniWriteValue("window", "禁用托盘", "0");
                ViewModel.IndexViewModel.IndexModel.Tuopan = "Visible";
            }
            else
            {
                ini.IniWriteValue("window", "禁用托盘", "1");
                ViewModel.IndexViewModel.IndexModel.Tuopan = "Collapsed";
            }
        }
        #endregion

        #region 名字
        private void PSPNameYES(object sender, RoutedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(pspname.Text, "[A-Za-z0-9а-яА-Я一-龠ぁ-んァ-ヴー\u4e00-\u9fa5 -]+"))
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(pspname.Text, "[\u4e00-\u9fa5]+"))
                {
                    MessageBox.Show("存在中文会显示异常");
                }
                if (System.Text.RegularExpressions.Regex.Match(pspname.Text, "[A-Za-z0-9а-яА-Я一-龠ぁ-んァ-ヴー\u4e00-\u9fa5 -]+").Value.Length < 12)
                {
                    pspnameback.Visibility = Visibility.Collapsed;
                    Common.CommonSTA.PSPName = System.Text.RegularExpressions.Regex.Match(pspname.Text, "[A-Za-z0-9а-яА-Я一-龠ぁ-んァ-ヴー\u4e00-\u9fa5 -]+").Value;
                    ViewModel.IndexViewModel.ini.IniWriteValue("setgame", "游戏名", Common.CommonSTA.PSPName);
                    pspname.Text = Common.CommonSTA.PSPName;
                }
                else
                {
                    MessageBox.Show("名称字符过长", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("名称字符不允许：\n[" + pspname.Text + "]", "", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        #endregion

        private void Chushihua(object sender, RoutedEventArgs e)
        {
            #region 加载图标切换
            autoplay.IsEnabled = false;
            danjiplay.IsEnabled = false;
            baoxianplay.IsEnabled = false;
            tuopan.IsEnabled = false;
            chushihua.Visibility = Visibility.Hidden;
            Shezhidonghua.Visibility = Visibility.Visible;
            #endregion
            LoadingView loading = new LoadingView(); loading.Show();
            #region 启动提示
            MessageBoxResult set = MessageBox.Show("将会初始化全部资源\n不建议放在根目录或桌面进行", "请确认", MessageBoxButton.OKCancel, MessageBoxImage.Warning, MessageBoxResult.Cancel);
            if (set != MessageBoxResult.OK)
            {
                View.IndexView.mainThreadSynContext.Post(new System.Threading.SendOrPostCallback(s =>
                loading.Close()), null);
                #region 加载切换回来
                View.IndexView.mainThreadSynContext.Post(new SendOrPostCallback(s =>
                Shezhidonghua.Visibility = Visibility.Hidden), null);
                View.IndexView.mainThreadSynContext.Post(new SendOrPostCallback(s =>
                autoplay.IsEnabled = true), null);
                View.IndexView.mainThreadSynContext.Post(new SendOrPostCallback(s =>
                danjiplay.IsEnabled = true), null);
                View.IndexView.mainThreadSynContext.Post(new SendOrPostCallback(s =>
                baoxianplay.IsEnabled = true), null);
                View.IndexView.mainThreadSynContext.Post(new SendOrPostCallback(s =>
                tuopan.IsEnabled = true), null);
                View.IndexView.mainThreadSynContext.Post(new SendOrPostCallback(s =>
                chushihua.Visibility = Visibility.Visible), null);
                #endregion
                return;
            }
            #endregion
            else
            {
                if (System.IO.File.Exists(System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\IndexOfMagic\ppsspp\set"))
                {
                    try
                    {
                        System.IO.File.Delete(System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\IndexOfMagic\ppsspp\set");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("操作被禁止\n配置文件被占用");
                        return;
                    }
                }
                ini.IniWriteValue("window", "Runed", "0");
                ini.IniWriteValue("window", "ManySame", "0");
                ini.IniWriteValue("window", "Yuanjiao", "0");
                ini.IniWriteValue("window", "主题", "幻影白");
                ini.IniWriteValue("window", "DPI", "0");
                ini.IniWriteValue("window", "禁用托盘", "0");
                ini.IniWriteValue("xfc", "big", "0.2");
                ini.IniWriteValue("setgame", "FCN路径", Environment.CurrentDirectory + @"\FCN.exe");
                ini.IniWriteValue("setgame", "PPSSPP路径", Environment.CurrentDirectory + @"\");
                ini.IniWriteValue("setgame", "ISO路径", Environment.CurrentDirectory + @"\memstick\PSP\GAME\");
                autoplay.IsChecked = false;
                danjiplay.IsChecked = false;
                baoxianplay.IsChecked = true;
                ue5play.IsChecked = false;
                tuopan.IsChecked = true;
                jinshouzhi.IsChecked = false;
                jinshouzhi.IsEnabled = false;
                ViewModel.IndexViewModel.IndexModel.Yuanjiao = "0";
                ViewModel.IndexViewModel.IndexModel.Yuanjiao2 = "0,0,0,0";
                ViewModel.IndexViewModel.IndexModel.Yuanjiao3 = "0,0,0,0";
                ViewModel.IndexViewModel.IndexModel.Yuanjiao4 = "0,0,0,0";
                Common.CommonSTA._ip = "10.10.0.1";
                Common.CommonSTA.LujingFCN = Environment.CurrentDirectory + @"\FCN.exe";
                Common.CommonSTA.LujingPSP = Environment.CurrentDirectory + @"\";
                Common.CommonSTA.LujingISO = Environment.CurrentDirectory + @"\memstick\PSP\GAME\";
                Common.CommonSTA.PSPName = @"";
                pspnameback.Visibility = Visibility.Visible;
                pspname.Text = "";
                ViewModel.IndexViewModel.IndexModel.Tuopan = "Visible";
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

                #region 初始化2
                System.Threading.Tasks.Task.Run(() =>
                {
                    System.Diagnostics.Process[] pProcess;
                    pProcess = System.Diagnostics.Process.GetProcesses();
                    for (int i = 1; i <= pProcess.Length - 1; i++)
                    {
                        if (pProcess[i].ProcessName == "PPSSPPWindows64")
                        {
                            try
                            {
                                pProcess[i].Kill();
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("操作被禁止\n模拟器未关闭");
                            }
                            break;
                        }
                        if (pProcess[i].ProcessName == "FCN")
                        {
                            try
                            {
                                pProcess[i].Kill();
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("操作被禁止\n联机模块未关闭");
                            }
                            break;
                        }
                    }
                    try
                    {
                        if (System.IO.Directory.Exists(Common.CommonSTA.LujingPSP + @"memstick"))
                        {
                            System.IO.Directory.Delete(Common.CommonSTA.LujingPSP + @"memstick", true);
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("操作被禁止\n" + Common.CommonSTA.LujingPSP + @"memstick");
                    }
                    try
                    {
                        //System.IO.File.WriteAllBytes(Environment.CurrentDirectory + @"\" + @"d3dcompiler_47.dll", Properties.Resources.d3dcompiler_47);
                        System.IO.File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + @"mfjsml.top", Properties.Resources.Release);
                        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                        System.IO.Compression.ZipFile.ExtractToDirectory(AppDomain.CurrentDomain.BaseDirectory + @"mfjsml.top", Environment.CurrentDirectory + @"\", Encoding.GetEncoding("GB2312"), true);
                        View.IndexView.mainThreadSynContext.Post(new System.Threading.SendOrPostCallback(s =>
                        loading.Close()), null);
                        ini.IniWriteValue("window", "Runed", "1");
                        #region 加载切换回来
                        View.IndexView.mainThreadSynContext.Post(new SendOrPostCallback(s =>
                        Shezhidonghua.Visibility = Visibility.Hidden), null);
                        View.IndexView.mainThreadSynContext.Post(new SendOrPostCallback(s =>
                        autoplay.IsEnabled = true), null);
                        View.IndexView.mainThreadSynContext.Post(new SendOrPostCallback(s =>
                        danjiplay.IsEnabled = true), null);
                        View.IndexView.mainThreadSynContext.Post(new SendOrPostCallback(s =>
                        baoxianplay.IsEnabled = true), null);
                        View.IndexView.mainThreadSynContext.Post(new SendOrPostCallback(s =>
                        tuopan.IsEnabled = true), null);
                        View.IndexView.mainThreadSynContext.Post(new SendOrPostCallback(s =>
                        chushihua.Visibility = Visibility.Visible), null);
                        #endregion
                    }
                    catch (Exception)
                    {
                        View.IndexView.mainThreadSynContext.Post(new System.Threading.SendOrPostCallback(s =>
                        loading.Close()), null);
                        #region 加载切换回来
                        View.IndexView.mainThreadSynContext.Post(new SendOrPostCallback(s =>
                        Shezhidonghua.Visibility = Visibility.Hidden), null);
                        View.IndexView.mainThreadSynContext.Post(new SendOrPostCallback(s =>
                        autoplay.IsEnabled = true), null);
                        View.IndexView.mainThreadSynContext.Post(new SendOrPostCallback(s =>
                        danjiplay.IsEnabled = true), null);
                        View.IndexView.mainThreadSynContext.Post(new SendOrPostCallback(s =>
                        baoxianplay.IsEnabled = true), null);
                        View.IndexView.mainThreadSynContext.Post(new SendOrPostCallback(s =>
                        tuopan.IsEnabled = true), null);
                        View.IndexView.mainThreadSynContext.Post(new SendOrPostCallback(s =>
                        chushihua.Visibility = Visibility.Visible), null);
                        #endregion
                        MessageBox.Show("操作被禁止\n资源:" + Environment.CurrentDirectory + @"\" + "\n请检查杀软自动拦截d3dcompiler_47.dll" + "如有疑问请咨询群内管理员");
                    }
                }).ContinueWith<int>((t) =>
                {
                    return 1;
                });
                #endregion
            }

        }
        private void Hidetishi(object sender, MouseButtonEventArgs e)
        {
            pspnameback.Visibility = Visibility.Collapsed;
        }

        private void Jinshouzhi(object sender, RoutedEventArgs e)
        {
            if (jinshouzhi.IsChecked == true)
            {
                ini.IniWriteValue("setgame", "金手指", "1");
            }
            else
            {
                ini.IniWriteValue("setgame", "金手指", "0");
            }
        }



        //private void YingyongShezhi(object sender, RoutedEventArgs e)
        //{
        //    #region 加载图标切换
        //    autoplay.IsEnabled = false;
        //    danjiplay.IsEnabled = false;
        //    baoxianplay.IsEnabled = false;
        //    Yingyong.Visibility = Visibility.Hidden;
        //    chushihua.Visibility = Visibility.Hidden;
        //    Shezhidonghua.Visibility = Visibility.Visible;
        //    #endregion
        //    #region 加载切换回来
        //    View.IndexView.mainThreadSynContext.Post(new SendOrPostCallback(s =>
        //    Shezhidonghua.Visibility = Visibility.Hidden), null);
        //    View.IndexView.mainThreadSynContext.Post(new SendOrPostCallback(s =>
        //    autoplay.IsEnabled = true), null);
        //    View.IndexView.mainThreadSynContext.Post(new SendOrPostCallback(s =>
        //    danjiplay.IsEnabled = true), null);
        //    View.IndexView.mainThreadSynContext.Post(new SendOrPostCallback(s =>
        //    baoxianplay.IsEnabled = true), null);
        //    View.IndexView.mainThreadSynContext.Post(new SendOrPostCallback(s =>
        //    Yingyong.Visibility = Visibility.Visible), null);
        //    View.IndexView.mainThreadSynContext.Post(new SendOrPostCallback(s =>
        //    chushihua.Visibility = Visibility.Visible), null);
        //    #endregion
        //}


    }
}
