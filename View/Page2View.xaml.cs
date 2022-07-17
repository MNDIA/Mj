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
    /// Page2View.xaml 的交互逻辑
    /// </summary>
    public partial class Page2View : UserControl
    {
        private static System.Threading.SynchronizationContext mainThreadSynContext2;
        private Common.IniBase ini = new Common.IniBase(System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\IndexOfMagic\ppsspp\set");//启用 ini
        public Page2View()
        {
            InitializeComponent();
            #region 配置文件setgame 底包模型剧情材质 10
            if (ini.IniReadValue("setgame", "底包") == "1")
            {
                Dibao.IsChecked = true;
                _caizhi[0] = 1;
            }
            if (ini.IniReadValue("setgame", "模型") == "1")
            {
                Moxing.IsChecked = true;
                _caizhi[1] = 1;
            }
            if (ini.IniReadValue("setgame", "剧情") == "1")
            {
                Juqing.IsChecked = true;
                _caizhi[2] = 1;
            }
            if (ini.IniReadValue("setgame", "地图") == "1")
            {
                Ditu.IsChecked = true;
                _caizhi[3] = 1;
            }
            if (ini.IniReadValue("setgame", "自定义材质") == "1")
            {
                Zidingyi.IsChecked = true;
                _caizhi[4] = 1;
            }
            #endregion
            mainThreadSynContext2 = System.Threading.SynchronizationContext.Current;
        }
        #region 按钮和临时寄存
        private byte[] _caizhi = new byte[5] { 0, 0, 0, 0, 0 };
        private void CaizhiDibao(object sender, RoutedEventArgs e)
        {
            if (Dibao.IsChecked == true)
            {
                _caizhi[0] = 1;
                Zidingyi.IsChecked = false;
                _caizhi[4] = 0;
            }
            else
            {
                _caizhi[0] = 0;
                Moxing.IsChecked = false;
                _caizhi[1] = 0;
                Juqing.IsChecked = false;
                _caizhi[2] = 0;
                Ditu.IsChecked = false;
                _caizhi[3] = 0;
            }
        }

        private void CaizhiMoxing(object sender, RoutedEventArgs e)
        {
            if (Moxing.IsChecked == true)
            {
                _caizhi[1] = 1;
                Dibao.IsChecked = true;
                _caizhi[0] = 1;
                Zidingyi.IsChecked = false;
                _caizhi[4] = 0;
            }
            else
            {
                _caizhi[1] = 0;
            }
        }

        private void CaizhiJuqing(object sender, RoutedEventArgs e)
        {
            if (Juqing.IsChecked == true)
            {
                _caizhi[2] = 1;
                Dibao.IsChecked = true;
                _caizhi[0] = 1;
                Zidingyi.IsChecked = false;
                _caizhi[4] = 0;
            }
            else
            {
                _caizhi[2] = 0;
            }
        }

        private void CaizhiDitu(object sender, RoutedEventArgs e)
        {
            if (Ditu.IsChecked == true)
            {
                _caizhi[3] = 1;
                Dibao.IsChecked = true;
                _caizhi[0] = 1;
                Zidingyi.IsChecked = false;
                _caizhi[4] = 0;
            }
            else
            {
                _caizhi[3] = 0;
            }
        }

        private void CaizhiZidingyi(object sender, RoutedEventArgs e)
        {
            if (Zidingyi.IsChecked == true)
            {
                _caizhi[4] = 1;
                Dibao.IsChecked = false;
                _caizhi[0] = 0;
                Moxing.IsChecked = false;
                _caizhi[1] = 0;
                Juqing.IsChecked = false;
                _caizhi[2] = 0;
                Ditu.IsChecked = false;
                _caizhi[3] = 0;
                Dibao.IsEnabled = false;
                Moxing.IsEnabled = false;
                Juqing.IsEnabled = false;
                Ditu.IsEnabled = false;
            }
            else
            {
                _caizhi[4] = 0;
                Dibao.IsEnabled = true;
                Moxing.IsEnabled = true;
                Juqing.IsEnabled = true;
                Ditu.IsEnabled = true;
            }
        }

        #endregion
        private void YingyongCaizhi(object sender, RoutedEventArgs e)
        {
            #region 加载图标切换
            Dibao.IsEnabled = false;
            Moxing.IsEnabled = false;
            Juqing.IsEnabled = false;
            Ditu.IsEnabled = false;
            Zidingyi.IsEnabled = false;
            Yingyong.Visibility = Visibility.Hidden;
            Huazhidonghua.Visibility = Visibility.Visible;
            #endregion
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            if (_caizhi[4] == 1)
            {
                if (System.IO.Directory.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\ULJS00329") && System.IO.File.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\ULJS00329\本文件不可删除_本文件夹材质不可编辑保存"))
                {
                    try
                    {
                        System.IO.Directory.Delete(Common.CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\ULJS00329", true);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("操作被禁止\n" + Common.CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\ULJS00329");
                    }
                }
                if (System.IO.Directory.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\勿改本名_ULJS00329_您的自定义未启用文件"))
                {
                    try
                    {
                        #region 自己的改回
                        #region CMD初始化
                        System.Diagnostics.Process p = new System.Diagnostics.Process();
                        p.StartInfo.FileName = "cmd.exe";
                        p.StartInfo.UseShellExecute = false;    //不使用shell启动
                        p.StartInfo.RedirectStandardInput = true;//喊cmd接受标准输入
                        p.StartInfo.RedirectStandardOutput = false;//不想听cmd讲话所以不要他输出
                        p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
                        p.StartInfo.CreateNoWindow = true;//不显示窗口
                        p.Start();
                        #endregion
                        p.StandardInput.WriteLine("ren " + Common.CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\勿改本名_ULJS00329_您的自定义未启用文件 " + @"ULJS00329" + "&exit");
                        #region CMD结束
                        p.StandardInput.AutoFlush = true;
                        p.WaitForExit();//等待程序执行完退出进程
                        p.Close();
                        #endregion
                        #endregion
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("操作被禁止");
                    }

                    #region 记录配置设置
                    if (_caizhi[0] == 1)
                    {
                        ini.IniWriteValue("setgame", "底包", "1");
                    }
                    else
                    {
                        ini.IniWriteValue("setgame", "底包", "0");
                    }
                    if (_caizhi[1] == 1)
                    {
                        ini.IniWriteValue("setgame", "模型", "1");
                    }
                    else
                    {
                        ini.IniWriteValue("setgame", "模型", "0");
                    }
                    if (_caizhi[2] == 1)
                    {
                        ini.IniWriteValue("setgame", "剧情", "1");
                    }
                    else
                    {
                        ini.IniWriteValue("setgame", "剧情", "0");
                    }
                    if (_caizhi[3] == 1)
                    {
                        ini.IniWriteValue("setgame", "地图", "1");
                    }
                    else
                    {
                        ini.IniWriteValue("setgame", "地图", "0");
                    }
                    if (_caizhi[4] == 1)
                    {
                        ini.IniWriteValue("setgame", "自定义材质", "1");
                    }
                    else
                    {
                        ini.IniWriteValue("setgame", "自定义材质", "0");
                    }
                    #endregion
                    #region 加载切换回来
                    View.Page2View.mainThreadSynContext2.Post(new SendOrPostCallback(s =>
                    Huazhidonghua.Visibility = Visibility.Hidden), null);
                    View.Page2View.mainThreadSynContext2.Post(new SendOrPostCallback(s =>
                    Dibao.IsEnabled = true), null);
                    View.Page2View.mainThreadSynContext2.Post(new SendOrPostCallback(s =>
                    Moxing.IsEnabled = true), null);
                    View.Page2View.mainThreadSynContext2.Post(new SendOrPostCallback(s =>
                    Juqing.IsEnabled = true), null);
                    View.Page2View.mainThreadSynContext2.Post(new SendOrPostCallback(s =>
                    Ditu.IsEnabled = true), null);
                    View.Page2View.mainThreadSynContext2.Post(new SendOrPostCallback(s =>
                    Zidingyi.IsEnabled = true), null);
                    View.Page2View.mainThreadSynContext2.Post(new SendOrPostCallback(s =>
                    Yingyong.Visibility = Visibility.Visible), null);
                    #endregion
                }
                else
                {
                    try
                    {
                        System.IO.Directory.CreateDirectory(Common.CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\ULJS00329");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("操作被禁止\n" + "您还没有自定义材质文件资源，为您创建自定义材质目录时文件夹拒绝访问");
                    }
                    MessageBoxResult set2 = MessageBox.Show("您还没有自定义材质文件资源，已为您创建自定义材质目录\n是否根据原始材质包为您创建一份初始相同的自定义材质资源供您编辑", "请确认", MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
                    if (set2 == MessageBoxResult.OK)
                    {
                        System.Threading.Tasks.Task.Run(() =>
                        {
                            #region 底包材质写入
                            try
                            {
                                if (!System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"mfjsml.top.0"))
                                {
                                    System.IO.File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + @"mfjsml.top.0", Properties.Resources._0);
                                }
                                System.IO.Compression.ZipFile.ExtractToDirectory(AppDomain.CurrentDomain.BaseDirectory + @"mfjsml.top.0", Common.CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\", Encoding.GetEncoding("GB2312"), true);
                                
                                if (!System.IO.File.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\ULJS00329\本文件夹用于自定义材质编辑_是为您复刻的原装材质包_本文件帮助用户识别_可删除"))
                                {
                                    try
                                    {
                                        System.IO.File.Create(Common.CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\ULJS00329\本文件夹用于自定义材质编辑_是为您复刻的原装材质包_本文件帮助用户识别_可删除").WriteByte(1);
                                    }
                                    catch (Exception)
                                    {
                                        MessageBox.Show("操作被禁止");
                                    }
                                }
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("操作被禁止");
                                Application.Current.Shutdown();
                            }
                            #endregion
                            #region 模型材质写入
                            try
                            {
                                if (!System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"mfjsml.top.1"))
                                {
                                    System.IO.File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + @"mfjsml.top.1", Properties.Resources._1);
                                }
                                System.IO.Compression.ZipFile.ExtractToDirectory(AppDomain.CurrentDomain.BaseDirectory + @"mfjsml.top.1", Common.CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\ULJS00329\", Encoding.GetEncoding("GB2312"), true);
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("操作被禁止");
                                Application.Current.Shutdown();
                            }
                            #endregion
                            #region 剧情写入
                            try
                            {
                                if (!System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"mfjsml.top.3"))
                                {
                                    System.IO.File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + @"mfjsml.top.3", Properties.Resources._3);
                                }
                                System.IO.Compression.ZipFile.ExtractToDirectory(AppDomain.CurrentDomain.BaseDirectory + @"mfjsml.top.3", Common.CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\ULJS00329\可删除\", Encoding.GetEncoding("GB2312"), true);
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("操作被禁止");
                                Application.Current.Shutdown();
                            }
                            #endregion
                            #region 地图材质写入
                            try
                            {
                                if (!System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"mfjsml.top.2"))
                                {
                                    System.IO.File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + @"mfjsml.top.2", Properties.Resources._2);
                                }
                                System.IO.Compression.ZipFile.ExtractToDirectory(AppDomain.CurrentDomain.BaseDirectory + @"mfjsml.top.2", Common.CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\ULJS00329\可删除\", Encoding.GetEncoding("GB2312"), true);
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("操作被禁止");
                                Application.Current.Shutdown();
                            }
                            #endregion

                            #region 记录配置设置
                            if (_caizhi[0] == 1)
                            {
                                ini.IniWriteValue("setgame", "底包", "1");
                            }
                            else
                            {
                                ini.IniWriteValue("setgame", "底包", "0");
                            }
                            if (_caizhi[1] == 1)
                            {
                                ini.IniWriteValue("setgame", "模型", "1");
                            }
                            else
                            {
                                ini.IniWriteValue("setgame", "模型", "0");
                            }
                            if (_caizhi[2] == 1)
                            {
                                ini.IniWriteValue("setgame", "剧情", "1");
                            }
                            else
                            {
                                ini.IniWriteValue("setgame", "剧情", "0");
                            }
                            if (_caizhi[3] == 1)
                            {
                                ini.IniWriteValue("setgame", "地图", "1");
                            }
                            else
                            {
                                ini.IniWriteValue("setgame", "地图", "0");
                            }
                            if (_caizhi[4] == 1)
                            {
                                ini.IniWriteValue("setgame", "自定义材质", "1");
                            }
                            else
                            {
                                ini.IniWriteValue("setgame", "自定义材质", "0");
                            }
                            #endregion
                            #region 加载切换回来
                            View.Page2View.mainThreadSynContext2.Post(new SendOrPostCallback(s =>
                            Huazhidonghua.Visibility = Visibility.Hidden), null);
                            View.Page2View.mainThreadSynContext2.Post(new SendOrPostCallback(s =>
                            Dibao.IsEnabled = true), null);
                            View.Page2View.mainThreadSynContext2.Post(new SendOrPostCallback(s =>
                            Moxing.IsEnabled = true), null);
                            View.Page2View.mainThreadSynContext2.Post(new SendOrPostCallback(s =>
                            Juqing.IsEnabled = true), null);
                            View.Page2View.mainThreadSynContext2.Post(new SendOrPostCallback(s =>
                            Ditu.IsEnabled = true), null);
                            View.Page2View.mainThreadSynContext2.Post(new SendOrPostCallback(s =>
                            Zidingyi.IsEnabled = true), null);
                            View.Page2View.mainThreadSynContext2.Post(new SendOrPostCallback(s =>
                            Yingyong.Visibility = Visibility.Visible), null);
                            #endregion
                            try
                            {
                                #region 打开文件夹
                                #region CMD初始化
                                System.Diagnostics.Process p = new System.Diagnostics.Process();
                                p.StartInfo.FileName = "cmd.exe";
                                p.StartInfo.UseShellExecute = false;    //不使用shell启动
                                p.StartInfo.RedirectStandardInput = true;//喊cmd接受标准输入
                                p.StartInfo.RedirectStandardOutput = false;//不想听cmd讲话所以不要他输出
                                p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
                                p.StartInfo.CreateNoWindow = true;//不显示窗口
                                p.Start();
                                #endregion
                                p.StandardInput.WriteLine("start " + Common.CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\ULJS00329" + "&exit");//ISO位置
                                #region CMD结束
                                p.StandardInput.AutoFlush = true;
                                p.WaitForExit();//等待程序执行完退出进程
                                p.Close();
                                #endregion
                                #endregion
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("操作被禁止\n" + "您还没有自定义材质文件资源，为您创建自定义材质目录时文件夹拒绝访问");
                            }
                        }).ContinueWith<int>((t) =>
                        {
                            return 1;
                        });

                    }
                    else
                    {
                        #region 记录配置设置
                        if (_caizhi[0] == 1)
                        {
                            ini.IniWriteValue("setgame", "底包", "1");
                        }
                        else
                        {
                            ini.IniWriteValue("setgame", "底包", "0");
                        }
                        if (_caizhi[1] == 1)
                        {
                            ini.IniWriteValue("setgame", "模型", "1");
                        }
                        else
                        {
                            ini.IniWriteValue("setgame", "模型", "0");
                        }
                        if (_caizhi[2] == 1)
                        {
                            ini.IniWriteValue("setgame", "剧情", "1");
                        }
                        else
                        {
                            ini.IniWriteValue("setgame", "剧情", "0");
                        }
                        if (_caizhi[3] == 1)
                        {
                            ini.IniWriteValue("setgame", "地图", "1");
                        }
                        else
                        {
                            ini.IniWriteValue("setgame", "地图", "0");
                        }
                        if (_caizhi[4] == 1)
                        {
                            ini.IniWriteValue("setgame", "自定义材质", "1");
                        }
                        else
                        {
                            ini.IniWriteValue("setgame", "自定义材质", "0");
                        }
                        #endregion
                        #region 加载切换回来
                        View.Page2View.mainThreadSynContext2.Post(new SendOrPostCallback(s =>
                            Huazhidonghua.Visibility = Visibility.Hidden), null);
                        View.Page2View.mainThreadSynContext2.Post(new SendOrPostCallback(s =>
                        Dibao.IsEnabled = true), null);
                        View.Page2View.mainThreadSynContext2.Post(new SendOrPostCallback(s =>
                        Moxing.IsEnabled = true), null);
                        View.Page2View.mainThreadSynContext2.Post(new SendOrPostCallback(s =>
                        Juqing.IsEnabled = true), null);
                        View.Page2View.mainThreadSynContext2.Post(new SendOrPostCallback(s =>
                        Ditu.IsEnabled = true), null);
                        View.Page2View.mainThreadSynContext2.Post(new SendOrPostCallback(s =>
                        Zidingyi.IsEnabled = true), null);
                        View.Page2View.mainThreadSynContext2.Post(new SendOrPostCallback(s =>
                        Yingyong.Visibility = Visibility.Visible), null);
                        #endregion
                        try
                        {
                            #region 打开文件夹
                            #region CMD初始化
                            System.Diagnostics.Process p = new System.Diagnostics.Process();
                            p.StartInfo.FileName = "cmd.exe";
                            p.StartInfo.UseShellExecute = false;    //不使用shell启动
                            p.StartInfo.RedirectStandardInput = true;//喊cmd接受标准输入
                            p.StartInfo.RedirectStandardOutput = false;//不想听cmd讲话所以不要他输出
                            p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
                            p.StartInfo.CreateNoWindow = true;//不显示窗口
                            p.Start();
                            #endregion
                            p.StandardInput.WriteLine("start " + Common.CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\ULJS00329" + "&exit");//ISO位置
                            #region CMD结束
                            p.StandardInput.AutoFlush = true;
                            p.WaitForExit();//等待程序执行完退出进程
                            p.Close();
                            #endregion
                            #endregion
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("操作被禁止\n" + "您还没有自定义材质文件资源，为您创建自定义材质目录时文件夹拒绝访问");
                        }
                    }
                }
            }
            else
            {
                try
                {
                    #region 自己的改名掉
                    if (System.IO.Directory.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\ULJS00329") && (!System.IO.File.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\ULJS00329\本文件不可删除_本文件夹材质不可编辑保存")))
                    {
                        #region CMD初始化
                        System.Diagnostics.Process p = new System.Diagnostics.Process();
                        p.StartInfo.FileName = "cmd.exe";
                        p.StartInfo.UseShellExecute = false;    //不使用shell启动
                        p.StartInfo.RedirectStandardInput = true;//喊cmd接受标准输入
                        p.StartInfo.RedirectStandardOutput = false;//不想听cmd讲话所以不要他输出
                        p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
                        p.StartInfo.CreateNoWindow = true;//不显示窗口
                        p.Start();
                        #endregion
                        p.StandardInput.WriteLine("ren " + Common.CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\ULJS00329 " + @"勿改本名_ULJS00329_您的自定义未启用文件" + "&exit");
                        #region CMD结束
                        p.StandardInput.AutoFlush = true;
                        p.WaitForExit();//等待程序执行完退出进程
                        p.Close();
                        #endregion
                    }
                    #endregion
                }
                catch (Exception)
                {
                    MessageBox.Show("操作被禁止");
                }
               
                System.Threading.Tasks.Task.Run(() =>
                {
                    #region 原版材质包操作
                    if (_caizhi[0] == 1)
                    {
                        #region 底包材质写入
                        try
                        {
                            if (!System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"mfjsml.top.0"))
                            {
                                System.IO.File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + @"mfjsml.top.0", Properties.Resources._0);
                            }
                            System.IO.Compression.ZipFile.ExtractToDirectory(AppDomain.CurrentDomain.BaseDirectory + @"mfjsml.top.0", Common.CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\", Encoding.GetEncoding("GB2312"), true);
                            
                            if (!System.IO.File.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\ULJS00329\本文件不可删除_本文件夹材质不可编辑保存"))
                            {
                                try
                                {
                                    System.IO.File.Create(Common.CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\ULJS00329\本文件不可删除_本文件夹材质不可编辑保存").WriteByte(1);
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show("操作被禁止");
                                }
                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("操作被禁止");
                            Application.Current.Shutdown();
                        }
                        #endregion
                        if (_caizhi[1] == 1)
                        {
                            #region 模型材质写入
                            try
                            {
                                if (!System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"mfjsml.top.1"))
                                {
                                    System.IO.File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + @"mfjsml.top.1", Properties.Resources._1);
                                }
                                System.IO.Compression.ZipFile.ExtractToDirectory(AppDomain.CurrentDomain.BaseDirectory + @"mfjsml.top.1", Common.CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\ULJS00329\", Encoding.GetEncoding("GB2312"), true);
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("操作被禁止");
                                Application.Current.Shutdown();
                            }
                            #endregion
                        }
                        else
                        {
                            if (System.IO.Directory.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\ULJS00329\模型"))
                            {
                                try
                                {
                                    System.IO.Directory.Delete(Common.CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\ULJS00329\模型", true);
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show("操作被禁止\n" + Common.CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\ULJS00329\模型");
                                }
                            }
                        }
                        if (_caizhi[2] == 1)
                        {
                            #region 剧情写入
                            try
                            {
                                if (!System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"mfjsml.top.3"))
                                {
                                    System.IO.File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + @"mfjsml.top.3", Properties.Resources._3);
                                }
                                System.IO.Compression.ZipFile.ExtractToDirectory(AppDomain.CurrentDomain.BaseDirectory + @"mfjsml.top.3", Common.CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\ULJS00329\可删除\", Encoding.GetEncoding("GB2312"), true);
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("操作被禁止");
                                Application.Current.Shutdown();
                            }
                            #endregion
                        }
                        else
                        {
                            if (System.IO.Directory.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\ULJS00329\剧情"))
                            {
                                try
                                {
                                    System.IO.Directory.Delete(Common.CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\ULJS00329\剧情", true);
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show("操作被禁止\n" + Common.CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\ULJS00329\剧情");
                                }
                            }

                            if (System.IO.Directory.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\ULJS00329\战斗指南"))
                            {
                                try
                                {
                                    System.IO.Directory.Delete(Common.CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\ULJS00329\战斗指南", true);
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show("操作被禁止\n" + Common.CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\ULJS00329\战斗指南");
                                }
                            }
                        }
                        if (_caizhi[3] == 1)
                        {
                            #region 地图材质写入
                            try
                            {
                                if (!System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"mfjsml.top.2"))
                                {
                                    System.IO.File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + @"mfjsml.top.2", Properties.Resources._2);
                                }
                                System.IO.Compression.ZipFile.ExtractToDirectory(AppDomain.CurrentDomain.BaseDirectory + @"mfjsml.top.2", Common.CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\ULJS00329\可删除\", Encoding.GetEncoding("GB2312"), true);
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("操作被禁止");
                                Application.Current.Shutdown();
                            }
                            #endregion
                        }
                        else
                        {
                            if (System.IO.Directory.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\ULJS00329\地图"))
                            {
                                try
                                {
                                    System.IO.Directory.Delete(Common.CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\ULJS00329\地图", true);
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show("操作被禁止\n" + Common.CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\ULJS00329\地图");
                                }
                            }
                        }
                    }
                    else
                    {
                        if (System.IO.Directory.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\ULJS00329"))
                        {
                            try
                            {
                                System.IO.Directory.Delete(Common.CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\ULJS00329", true);
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("操作被禁止\n" + Common.CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\ULJS00329");
                            }
                        }
                    }

                    #endregion

                    #region 记录配置设置
                    if (_caizhi[0] == 1)
                    {
                        ini.IniWriteValue("setgame", "底包", "1");
                    }
                    else
                    {
                        ini.IniWriteValue("setgame", "底包", "0");
                    }
                    if (_caizhi[1] == 1)
                    {
                        ini.IniWriteValue("setgame", "模型", "1");
                    }
                    else
                    {
                        ini.IniWriteValue("setgame", "模型", "0");
                    }
                    if (_caizhi[2] == 1)
                    {
                        ini.IniWriteValue("setgame", "剧情", "1");
                    }
                    else
                    {
                        ini.IniWriteValue("setgame", "剧情", "0");
                    }
                    if (_caizhi[3] == 1)
                    {
                        ini.IniWriteValue("setgame", "地图", "1");
                    }
                    else
                    {
                        ini.IniWriteValue("setgame", "地图", "0");
                    }
                    if (_caizhi[4] == 1)
                    {
                        ini.IniWriteValue("setgame", "自定义材质", "1");
                    }
                    else
                    {
                        ini.IniWriteValue("setgame", "自定义材质", "0");
                    }
                    #endregion
                    #region 加载切换回来
                    View.Page2View.mainThreadSynContext2.Post(new SendOrPostCallback(s =>
                    Huazhidonghua.Visibility = Visibility.Hidden), null);
                    View.Page2View.mainThreadSynContext2.Post(new SendOrPostCallback(s =>
                    Dibao.IsEnabled = true), null);
                    View.Page2View.mainThreadSynContext2.Post(new SendOrPostCallback(s =>
                    Moxing.IsEnabled = true), null);
                    View.Page2View.mainThreadSynContext2.Post(new SendOrPostCallback(s =>
                    Juqing.IsEnabled = true), null);
                    View.Page2View.mainThreadSynContext2.Post(new SendOrPostCallback(s =>
                    Ditu.IsEnabled = true), null);
                    View.Page2View.mainThreadSynContext2.Post(new SendOrPostCallback(s =>
                    Zidingyi.IsEnabled = true), null);
                    View.Page2View.mainThreadSynContext2.Post(new SendOrPostCallback(s =>
                    Yingyong.Visibility = Visibility.Visible), null);
                    #endregion
                }).ContinueWith<int>((t) =>
                {
                    return 1;
                });
            }
        }
    }
}
