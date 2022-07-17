using System;
using Mj.ViewModel;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Drawing;


namespace Mj.View
{
    /// <summary>
    /// IndexView.xaml 的交互逻辑
    /// </summary>
  
    public partial class IndexView : Window
    {
        #region SynchronizationContext
        public static System.Threading.SynchronizationContext mainThreadSynContext;
        #endregion
        #region DPI所需接口
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
            var g = Graphics.FromHwnd(IntPtr.Zero);
            IntPtr desktop = g.GetHdc();
            var physicalScreenHeight = GetDeviceCaps(desktop, (int)DeviceCap.DESKTOPVERTRES);
            return physicalScreenHeight;
        }
        #endregion
        #region DataContext(其他事件要用
        public IndexViewModel IndexViewModel = new IndexViewModel();
        #endregion
        #region 启动运行
        public IndexView()
        {
            bool NomalExe = true;
            //MessageBox.Show(Environment.CurrentDirectory);
            #region 创建配置文件目录
            if (!Directory.Exists(System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\IndexOfMagic"))
            {
                Directory.CreateDirectory(System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\IndexOfMagic\ppsspp");
                Directory.CreateDirectory(System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\IndexOfMagic\unreal5");
            }
            #endregion
            #region 配置文件初始化set
            if (IndexViewModel.ini.IniReadValue("window", "Runed") != "1")
            {
                NomalExe = false;
                LoadingView loading = new LoadingView();
                loading.Show();
                #region 启动提示
                MessageBoxResult set = MessageBox.Show("是否在此启动，程序将会在此目录准备游戏\n首次运行不建议放在根目录或桌面", "请确认", MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
                if (set != MessageBoxResult.OK)
                {
                    Application.Current.Shutdown();
                }
                #endregion
                else
                {
                    IndexViewModel.ini.IniWriteValue("window", "Runed", "0");
                    IndexViewModel.ini5.IniWriteValue("UE5Beta", "activation", "0");
                    IndexViewModel.ini.IniWriteValue("window", "ManySame", "0");
                    IndexViewModel.ini.IniWriteValue("window", "Yuanjiao", "0");
                    IndexViewModel.ini.IniWriteValue("window", "主题", "幻影白");
                    IndexViewModel.ini.IniWriteValue("window", "DPI", "0");
                    IndexViewModel.ini.IniWriteValue("window", "禁用托盘", "0");
                    IndexViewModel.ini.IniWriteValue("xfc", "big", "0.2");
                    IndexViewModel.ini.IniWriteValue("setgame", "FCN路径", Environment.CurrentDirectory + @"\FCN.exe");
                    IndexViewModel.ini.IniWriteValue("setgame", "PPSSPP路径", Environment.CurrentDirectory + @"\");
                    IndexViewModel.ini.IniWriteValue("setgame", "ISO路径", Environment.CurrentDirectory + @"\memstick\PSP\GAME\");


                    #region 创建桌面快捷方式
                    if (System.IO.File.Exists(string.Format(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\魔法禁书目录.lnk")))
                    {
                        ;
                    }
                    else
                    {
                        try
                        {
                            IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShell();
                            IWshRuntimeLibrary.IWshShortcut shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\" + "魔法禁书目录.lnk");
                            shortcut.TargetPath = Environment.CurrentDirectory + @"\" + System.Diagnostics.Process.GetCurrentProcess().ProcessName + @".exe";         //目标文件
                            shortcut.WorkingDirectory = Environment.CurrentDirectory;    //目标文件夹
                            shortcut.Description = "魔禁多人联动委员会";   //描述
                            shortcut.Arguments = "";
                            shortcut.Save();
                        }
                        catch (Exception)
                        {
                        }

                    }
                    #endregion
                    #region 文件初始化
                    System.Threading.Tasks.Task.Run(() =>
                    {
                        try
                        {
                            //System.IO.File.WriteAllBytes(Environment.CurrentDirectory + @"\" + @"d3dcompiler_47.dll", Properties.Resources.d3dcompiler_47);
                            System.IO.File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + @"mfjsml.top", Properties.Resources.Release);
                            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                            System.IO.Compression.ZipFile.ExtractToDirectory(AppDomain.CurrentDomain.BaseDirectory + @"mfjsml.top", Environment.CurrentDirectory + @"\", Encoding.GetEncoding("GB2312"), true);

                            View.IndexView.mainThreadSynContext.Post(new System.Threading.SendOrPostCallback(s =>
                            loading.Close()), null);
                            IndexViewModel.ini.IniWriteValue("window", "Runed", "1");
                            View.IndexView.mainThreadSynContext.Post(new System.Threading.SendOrPostCallback(s =>
                            {
                                window.Show();
                                #region 是否DPI禁用（主窗口大小调整
                                window.MaxHeight = 10000 * Common.CommonSTA.dpi;
                                window.Width = (SystemParameters.PrimaryScreenWidth * 0.6 * Common.CommonSTA.dpi * Common.CommonSTA.dpi) + 5;
                                window.Height = (SystemParameters.PrimaryScreenHeight * 0.64 * Common.CommonSTA.dpi * Common.CommonSTA.dpi) + 10;
                                window.Top = (SystemParameters.PrimaryScreenHeight * (0.5 - 0.32 * Common.CommonSTA.dpi) - 20) * Common.CommonSTA.dpi - 5;
                                window.Left = SystemParameters.PrimaryScreenWidth * Common.CommonSTA.dpi * (0.5 - 0.3 * Common.CommonSTA.dpi) - 2.5;
                                window.MinHeight = 390 * Common.CommonSTA.dpi;
                                window.MinWidth = 281 * Common.CommonSTA.dpi;
                                #endregion
                            }
                            ), null);
                        }
                        catch (Exception)
                        {
                            View.IndexView.mainThreadSynContext.Post(new System.Threading.SendOrPostCallback(s =>
                            loading.Close()), null);
                            MessageBox.Show("操作被禁止\n资源:" + Environment.CurrentDirectory + @"\" + "\n请检查杀软自动拦截d3dcompiler_47.dll" + "如有疑问请咨询群内管理员");
                            View.IndexView.mainThreadSynContext.Post(new System.Threading.SendOrPostCallback(s =>
                            Application.Current.Shutdown()), null);
                        }
                        return 1;
                    });
                    #endregion
                }
            }
            #endregion
            //————————————————————————————————————————————————————————
            InitializeComponent();
            #region SynchronizationContext
            mainThreadSynContext = System.Threading.SynchronizationContext.Current;
            #endregion
            #region DataContext
            this.DataContext = IndexViewModel;
            #endregion
            #region 获取DPI(其他窗口也用到
            Common.CommonSTA.dpi = double.Parse(GetScreenScalingFactor().ToString(), 0) / double.Parse(SystemParameters.PrimaryScreenHeight.ToString(), 0);
            #endregion
            #region 是否DPI禁用（主窗口大小调整
            window.Top = (SystemParameters.PrimaryScreenHeight * (0.5 - 0.32 * Common.CommonSTA.dpi) - 20) - 5;
            window.Left = SystemParameters.PrimaryScreenWidth * (0.5 - 0.3 * Common.CommonSTA.dpi) - 2.5;
            //if (IndexViewModel.ini.IniReadValue("window", "DPI") != "1" && NomalExe)
            //{
            //    //window.MaxHeight = 10000 * Common.CommonSTA.dpi;
            //    //window.Width = (SystemParameters.PrimaryScreenWidth * 0.6 * Common.CommonSTA.dpi * Common.CommonSTA.dpi) + 5;
            //    //window.Height = (SystemParameters.PrimaryScreenHeight * 0.64 * Common.CommonSTA.dpi * Common.CommonSTA.dpi) + 10;
            //    window.Top = (SystemParameters.PrimaryScreenHeight * (0.5 - 0.32 * Common.CommonSTA.dpi) - 20) * Common.CommonSTA.dpi - 5;
            //    window.Left = SystemParameters.PrimaryScreenWidth * Common.CommonSTA.dpi * (0.5 - 0.3 * Common.CommonSTA.dpi) - 2.5;
            //    //window.MinHeight = 390 * Common.CommonSTA.dpi;
            //    //window.MinWidth = 281 * Common.CommonSTA.dpi;
            //}
            #endregion
            #region 是否第一次启动，不显示
            if (IndexViewModel.ini.IniReadValue("window", "Runed") != "1")
            {
                window.Visibility = Visibility.Hidden;
            }
            #endregion
            #region 小概率启动
            if (IndexViewModel.ini.IniReadValue("setgame", "菜菜子") == "1" || (new Random().Next(0, 1000) == 521 && NomalExe))
            {
                View.BloodView blood = new BloodView();
                View.IndexView.mainThreadSynContext.Post(new System.Threading.SendOrPostCallback(s =>
                blood.Show()), null);
                window.Title = "ƒé›†å›¢¸Šæµ·duå»ºå·¥æŠ•èµ„æµ·å»";
                IndexViewModel.IndexModel.ICO = "../Assets/logoccz.ico";
                //BitmapSource bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(Common.CaptureScreen.GetDesktopImage().GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
            }
            #endregion
            #region 浏览器初始化
            WebBrowser.DownloadHandler = new Common.CustomDownloadHandler();
            #endregion
            #region 自启动
            if (IndexViewModel.ini.IniReadValue("setgame", "自启") == "1")
            {
                if (IndexViewModel.ini.IniReadValue("setgame", "游戏名") == "")
                {
                    IndexViewModel.IndexModel.MainContent = (FrameworkElement)Type.GetType("Mj.View.Page4View").GetConstructor(System.Type.EmptyTypes).Invoke(null);
                    IndexViewModel.IndexModel.Select1 = 1; IndexViewModel.IndexModel.Select2 = 0; IndexViewModel.IndexModel.Select3 = 0;
                    MessageBox.Show("请修改您的游戏昵称");
                    return;
                }
                if (!System.IO.File.Exists(Common.CommonSTA.LujingISO + "UJS00329.iso"))
                {
                    IndexViewModel.IndexModel.MainContent = (FrameworkElement)Type.GetType("Mj.View.Page4View").GetConstructor(System.Type.EmptyTypes).Invoke(null);
                    IndexViewModel.IndexModel.Select1 = 1; IndexViewModel.IndexModel.Select2 = 0; IndexViewModel.IndexModel.Select3 = 0;
                    MessageBox.Show("未找到游戏文件目录，请重新初始化\n\r\n\r或请前往设置中指定您自己[UJS00329.iso]的位置");
                    return;
                }
                if (IndexViewModel.IndexModel.PlayContent == "等待选服/取消")
                {
                    Common.CommonSTA.Stop = true;
                }
                else
                {
                    IndexViewModel.IndexModel.PlayContent = "启动中";
                    Common.CommonSTA.IsPlayStart = true;
                    Common.CommonSTA.Stop = false;
                    IndexViewModel.IndexModel.ICO = "../Assets/ico.ico";
                    #region 隐藏启动器
                    if (IndexViewModel.ini.IniReadValue("window", "禁用托盘") != "1")
                    {
                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            if (this != null)
                            {
                                this.Hide();
                            }
                        }));
                    }
                    #endregion
                    if (!System.IO.File.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\SYSTEM\isppsspp"))
                    {
                        #region PPSSPP初始化
                        if (System.IO.File.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\SYSTEM\ppsspp.ini"))
                        {
                            System.IO.File.Delete(Common.CommonSTA.LujingPSP + @"memstick\PSP\SYSTEM\ppsspp.ini");
                        }
                        try
                        {
                            System.Diagnostics.Process.Start(Common.CommonSTA.LujingPSP + @"PPSSPPWindows64.exe");
                        }
                        catch (Exception)
                        {
                            IndexViewModel.IndexModel.MainContent = (FrameworkElement)Type.GetType("Mj.View.Page5View").GetConstructor(System.Type.EmptyTypes).Invoke(null);
                            IndexViewModel.IndexModel.Select1 = 1; IndexViewModel.IndexModel.Select2 = 0; IndexViewModel.IndexModel.Select3 = 0;
                            MessageBox.Show("模拟器损坏，请重新初始化\n\r\n\r或请前往设置中指定您自己[PPSSPP]的位置");
                            Common.CommonSTA.IsPlayStart = false;
                            IndexViewModel.IndexModel.ICO = "../Assets/logo128.ico";
                            IndexViewModel.IndexModel.PlayContent = "立即开始";
                            return;
                        }
                        System.Threading.Tasks.Task.Run(() =>
                        {
                            System.Diagnostics.Process[] pProcess;
                            pProcess = System.Diagnostics.Process.GetProcesses();
                            while (true)
                            {
                                if (System.IO.File.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\SYSTEM\ppsspp.ini"))
                                {
                                    System.Threading.Thread.Sleep(500);
                                    for (int i = 1; i <= pProcess.Length - 1; i++)
                                    {
                                        if (pProcess[i].ProcessName == "PPSSPPWindows64")
                                        {
                                            pProcess[i].Kill();
                                            break;
                                        }
                                    }
                                    break;
                                }
                                System.Threading.Thread.Sleep(500);
                            }
                            #endregion
                            //设置改变
                            #region 默认
                            IndexViewModel.psp.IniWriteValue("General", "CheckForNewVersion ", " False");
                            IndexViewModel.psp.IniWriteValue("General", "ForceLagSync2 ", " True");
                            IndexViewModel.psp.IniWriteValue("Graphics", "AnisotropyLevel ", " 0");
                            IndexViewModel.psp.IniWriteValue("Graphics", "ReplaceTextures ", " True");
                            IndexViewModel.psp.IniWriteValue("Graphics", "TextureBackoffCache ", " True");
                            IndexViewModel.psp.IniWriteValue("Graphics", "FullScreen ", " True");
                            IndexViewModel.psp.IniWriteValue("Graphics", "SplineBezierQuality ", " 0");
                            IndexViewModel.psp.IniWriteValue("Graphics", "DisableSlowFramebufEffects ", " True");
                            IndexViewModel.psp.IniWriteValue("Network", "ForcedFirstConnect ", " True");
                            IndexViewModel.psp.IniWriteValue("Network", "EnableNetworkChat ", " True");
                            IndexViewModel.psp.IniWriteValue("Network", "ChatButtonPosition ", " 3");
                            IndexViewModel.psp.IniWriteValue("Network", "ChatScreenPosition ", " 3");
                            #endregion


                            if (!System.IO.File.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\SYSTEM\isppsspp"))
                            {
                                try
                                {
                                    System.IO.File.Create(Common.CommonSTA.LujingPSP + @"memstick\PSP\SYSTEM\isppsspp").WriteByte(1);
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show("操作被禁止");
                                }
                            }

                            #region 正常启动
                            if (IndexViewModel.ini.IniReadValue("setgame", "联机") != "0")
                            {
                                #region 悬浮窗总执行
                                if (IndexViewModel.IndexModel.IPtupian == "../Assets/ping-close.png")//AAA窗口开启的话
                                {
                                    View.IndexView.mainThreadSynContext.Post(new System.Threading.SendOrPostCallback(s =>
                                    IndexViewModel.IndexModel.XfcView.Hide()), null);
                                    Common.CommonSTA.Noping3 = 1;
                                    Common.CommonSTA.XDS = 0;
                                    //AAA窗口关闭
                                }
                                else
                                {
                                    View.IndexView.mainThreadSynContext.Post(new System.Threading.SendOrPostCallback(s =>
                                    IndexViewModel.IndexModel.XfcView.Owner = this), null);
                                    try
                                    {
                                        View.IndexView.mainThreadSynContext.Post(new System.Threading.SendOrPostCallback(s =>
                                        IndexViewModel.IndexModel.XfcView.Show()), null);
                                        Common.CommonSTA.XDS = 1;
                                        Common.CommonSTA.Noping3 = 0;
                                    }
                                    catch (Exception)
                                    {
                                        MessageBox.Show("要复活小电视嘛 = = ");
                                        View.IndexView.mainThreadSynContext.Post(new System.Threading.SendOrPostCallback(s =>
                                        IndexViewModel.IndexModel.XfcView = new View.XfcView()), null);
                                        View.IndexView.mainThreadSynContext.Post(new System.Threading.SendOrPostCallback(s =>
                                        IndexViewModel.IndexModel.XfcView.Show()), null);
                                        Common.CommonSTA.XDS = 1;
                                        Common.CommonSTA.Noping3 = 0;
                                    }
                                }
                                #endregion
                                #region 有网模式
                                try
                                {
                                    System.Diagnostics.Process.Start(Common.CommonSTA.LujingFCN);//FCN
                                }
                                catch (Exception)
                                {
                                    View.IndexView.mainThreadSynContext.Post(new System.Threading.SendOrPostCallback(s =>
                                    IndexViewModel.IndexModel.MainContent = (FrameworkElement)Type.GetType("Mj.View.Page5View").GetConstructor(System.Type.EmptyTypes).Invoke(null)), null);
                                    IndexViewModel.IndexModel.Select1 = 1; IndexViewModel.IndexModel.Select2 = 0; IndexViewModel.IndexModel.Select3 = 0;
                                    MessageBox.Show("联机模块损坏，请重新初始化\n\r\n\r或请前往设置中指定您自己[联机模块]的位置");
                                    IndexViewModel.IndexModel.ICO = "../Assets/logo128.ico";
                                    IndexViewModel.IndexModel.PlayContent = "立即开始";
                                    Common.CommonSTA.IsPlayStart = false;
                                    return;
                                }
                                Common.CommonSTA.Danji = false;
                                Common.CommonSTA.IsOK = false;
                                if (IndexViewModel.ini.IniReadValue("setgame", "不提示") != "1")
                                {
                                    #region 显示弹窗
                                    try
                                    {
                                        View.IndexView.mainThreadSynContext.Post(new System.Threading.SendOrPostCallback(s =>
                                        IndexViewModel.IndexModel.Tishi.Show()), null);
                                    }
                                    catch (Exception)
                                    {
                                        View.IndexView.mainThreadSynContext.Post(new System.Threading.SendOrPostCallback(s =>
                                        IndexViewModel.IndexModel.Tishi = new View.UMessageBox()), null);
                                        View.IndexView.mainThreadSynContext.Post(new System.Threading.SendOrPostCallback(s =>
                                        IndexViewModel.IndexModel.Tishi.Show()), null);
                                    }
                                    #endregion
                                    #region 倒计时
                                    System.Threading.Tasks.Task.Run(() =>
                                    {
                                        for (int i = 5; i > 0; i--)
                                        {
                                            View.IndexView.mainThreadSynContext.Post(new System.Threading.SendOrPostCallback(s =>
                                            IndexViewModel.IndexModel.Tishi.ZDL = "知道了(" + i + ")"), null);
                                            System.Threading.Thread.Sleep(1000);
                                        }
                                        #region 自动关闭弹窗
                                        try
                                        {
                                            View.IndexView.mainThreadSynContext.Post(new System.Threading.SendOrPostCallback(s =>
                                            IndexViewModel.IndexModel.Tishi.Hide()), null);
                                        }
                                        catch (Exception)
                                        {
                                            ;
                                        }
                                        #endregion
                                        Common.CommonSTA.IsOK = true;
                                    }).ContinueWith<int>((t) =>
                                    {

                                        return 1;
                                    });
                                    #endregion
                                }
                                System.Threading.Tasks.Task.Run(() =>
                                {
                                    IndexViewModel.IndexModel.PlayContent = "等待选服/取消";
                                    while (true)
                                    {
                                        if (Common.CommonSTA.Stop == true)
                                        {
                                            Common.CommonSTA.Noping2 = 1;
                                            break;
                                        }
                                        if (Common.CommonSTA.Danji == true)
                                        {
                                            #region 悬浮窗关闭
                                            View.IndexView.mainThreadSynContext.Post(new System.Threading.SendOrPostCallback(s =>
                                                IndexViewModel.IndexModel.XfcView.Hide()), null);
                                            Common.CommonSTA.Noping3 = 1;
                                            Common.CommonSTA.XDS = 0;
                                            #endregion
                                            #region 关闭网络
                                            IndexViewModel.psp.IniWriteValue("Network", "EnableWlan", "False");
                                            #endregion
                                            #region 保险
                                            IndexViewModel.psp.IniWriteValue("SystemParam", "PSPModel", "1");
                                            if (IndexViewModel.ini.IniReadValue("setgame", "游戏名") != "")
                                            {
                                                IndexViewModel.psp.IniWriteValue("SystemParam", "NickName", Common.CommonSTA.PSPName);
                                            }
                                            if (IndexViewModel.ini.IniReadValue("setgame", "金手指") != "1")
                                            {
                                                IndexViewModel.psp.IniWriteValue("General", "EnableCheats", "False");
                                                if (System.IO.File.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\Cheats\ULJS00329.ini"))
                                                {
                                                    System.IO.File.Delete(Common.CommonSTA.LujingPSP + @"memstick\PSP\Cheats\ULJS00329.ini");
                                                }
                                            }
                                            else
                                            {
                                                IndexViewModel.psp.IniWriteValue("General", "EnableCheats", "True");
                                                try
                                                {
                                                    if (!System.IO.File.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\Cheats\ULJS00329.ini"))
                                                    {
                                                        System.IO.File.WriteAllBytes(Common.CommonSTA.LujingPSP + @"memstick\PSP\Cheats\ULJS00329.ini", Properties.Resources.ULJS00329);
                                                    }
                                                }
                                                catch (Exception)
                                                {
                                                    MessageBox.Show("操作被禁止");
                                                }
                                            }
                                            if (System.IO.File.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\SYSTEM\ULJS00329_ppsspp.ini"))
                                            {
                                                System.IO.File.Delete(Common.CommonSTA.LujingPSP + @"memstick\PSP\SYSTEM\ULJS00329_ppsspp.ini");
                                            }
                                            IndexViewModel.psp.IniWriteValue("Graphics", "FullScreen ", " True");
                                            IndexViewModel.psp.IniWriteValue("CPU", "IOTimingMethod ", " 0");
                                            IndexViewModel.psp.IniWriteValue("Network", "PortOffset ", " 5000");
                                            IndexViewModel.psp.IniWriteValue("Network", "proAdhocServer ", " " + Common.CommonSTA._ip);
                                            int[] mac = new int[] { new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16) };
                                            IndexViewModel.psp.IniWriteValue("SystemParam", "MacAddress ", " " + mac[0] + mac[1] + ":" + mac[2] + mac[3] + ":" + mac[4] + mac[5] + ":" + mac[6] + mac[7] + ":" + mac[8] + mac[9] + ":" + mac[10] + mac[11]);
                                            #endregion
                                            #region 启动游戏
                                            try
                                            {
                                                System.Diagnostics.Process.Start(Common.CommonSTA.LujingPSP + @"PPSSPPWindows64.exe", Common.CommonSTA.LujingISO + @"UJS00329.iso");
                                            }
                                            catch (Exception)
                                            {
                                                View.IndexView.mainThreadSynContext.Post(new System.Threading.SendOrPostCallback(s =>
                                                IndexViewModel.IndexModel.MainContent = (FrameworkElement)Type.GetType("Mj.View.Page5View").GetConstructor(System.Type.EmptyTypes).Invoke(null)), null);
                                                IndexViewModel.IndexModel.Select1 = 1; IndexViewModel.IndexModel.Select2 = 0; IndexViewModel.IndexModel.Select3 = 0;
                                                MessageBox.Show("模拟器损坏，请重新初始化\n\r\n\r或请前往设置中指定您自己[PPSSPP]的位置");
                                            }
                                            #endregion
                                            break;
                                        }
                                        try
                                        {
                                            System.Net.NetworkInformation.PingReply pingReply = Common.CommonSTA.ping2.Send(Common.CommonSTA._ip);
                                            if (pingReply.Status == System.Net.NetworkInformation.IPStatus.Success)
                                            {
                                                Common.CommonSTA.PING = true;
                                            }
                                        }
                                        catch (Exception)
                                        {
                                            MessageBox.Show("网络异常"); break;
                                        }

                                        if (Common.CommonSTA.PING == true && Common.CommonSTA.IsOK == true)
                                        {
                                            #region 启用网络
                                            IndexViewModel.psp.IniWriteValue("Network", "EnableWlan", "True");
                                            #endregion
                                            #region 保险
                                            IndexViewModel.psp.IniWriteValue("SystemParam", "PSPModel", "1");
                                            if (IndexViewModel.ini.IniReadValue("setgame", "游戏名") != "")
                                            {
                                                IndexViewModel.psp.IniWriteValue("SystemParam", "NickName", Common.CommonSTA.PSPName);
                                            }
                                            if (IndexViewModel.ini.IniReadValue("setgame", "金手指") != "1")
                                            {
                                                IndexViewModel.psp.IniWriteValue("General", "EnableCheats", "False");
                                                if (System.IO.File.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\Cheats\ULJS00329.ini"))
                                                {
                                                    System.IO.File.Delete(Common.CommonSTA.LujingPSP + @"memstick\PSP\Cheats\ULJS00329.ini");
                                                }
                                            }
                                            else
                                            {
                                                IndexViewModel.psp.IniWriteValue("General", "EnableCheats", "True");
                                                try
                                                {
                                                    if (!System.IO.File.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\Cheats\ULJS00329.ini"))
                                                    {
                                                        System.IO.File.WriteAllBytes(Common.CommonSTA.LujingPSP + @"memstick\PSP\Cheats\ULJS00329.ini", Properties.Resources.ULJS00329);
                                                    }
                                                }
                                                catch (Exception)
                                                {
                                                    MessageBox.Show("操作被禁止");
                                                }
                                            }
                                            if (System.IO.File.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\SYSTEM\ULJS00329_ppsspp.ini"))
                                            {
                                                System.IO.File.Delete(Common.CommonSTA.LujingPSP + @"memstick\PSP\SYSTEM\ULJS00329_ppsspp.ini");
                                            }
                                            IndexViewModel.psp.IniWriteValue("Graphics", "FullScreen ", " True");
                                            IndexViewModel.psp.IniWriteValue("CPU", "IOTimingMethod ", " 0");
                                            IndexViewModel.psp.IniWriteValue("Network", "PortOffset ", " 5000");
                                            IndexViewModel.psp.IniWriteValue("Network", "proAdhocServer ", " " + Common.CommonSTA._ip);
                                            int[] mac = new int[] { new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16) };
                                            IndexViewModel.psp.IniWriteValue("SystemParam", "MacAddress ", " " + mac[0] + mac[1] + ":" + mac[2] + mac[3] + ":" + mac[4] + mac[5] + ":" + mac[6] + mac[7] + ":" + mac[8] + mac[9] + ":" + mac[10] + mac[11]);
                                            #endregion
                                            #region 启动游戏
                                            try
                                            {
                                                System.Diagnostics.Process.Start(Common.CommonSTA.LujingPSP + @"PPSSPPWindows64.exe", Common.CommonSTA.LujingISO + @"UJS00329.iso");
                                            }
                                            catch (Exception)
                                            {
                                                View.IndexView.mainThreadSynContext.Post(new System.Threading.SendOrPostCallback(s =>
                                                IndexViewModel.IndexModel.MainContent = (FrameworkElement)Type.GetType("Mj.View.Page5View").GetConstructor(System.Type.EmptyTypes).Invoke(null)), null);
                                                IndexViewModel.IndexModel.Select1 = 1; IndexViewModel.IndexModel.Select2 = 0; IndexViewModel.IndexModel.Select3 = 0;
                                                MessageBox.Show("模拟器损坏，请重新初始化\n\r\n\r或请前往设置中指定您自己[PPSSPP]的位置");
                                            }
                                            #endregion
                                            break;
                                        }
                                        System.Threading.Thread.Sleep(1000);
                                    }
                                }).ContinueWith<int>((t) =>
                                {
                                    IndexViewModel.IndexModel.PlayContent = "立即开始";
                                    Common.CommonSTA.PING = false;
                                    Common.CommonSTA.IsPlayStart = false;
                                    IndexViewModel.IndexModel.ICO = "../Assets/logo128.ico";
                                    return 1;
                                });
                                #endregion
                            }
                            else
                            {
                                #region 无网模式
                                #region 悬浮窗关闭
                                View.IndexView.mainThreadSynContext.Post(new System.Threading.SendOrPostCallback(s =>
                                    IndexViewModel.IndexModel.XfcView.Hide()), null);
                                Common.CommonSTA.Noping3 = 1;
                                Common.CommonSTA.XDS = 0;
                                #endregion
                                #region 关闭网络
                                IndexViewModel.psp.IniWriteValue("Network", "EnableWlan", "False");
                                #endregion
                                #region 保险
                                IndexViewModel.psp.IniWriteValue("SystemParam", "PSPModel", "1");
                                if (IndexViewModel.ini.IniReadValue("setgame", "游戏名") != "")
                                {
                                    IndexViewModel.psp.IniWriteValue("SystemParam", "NickName", Common.CommonSTA.PSPName);
                                }
                                if (IndexViewModel.ini.IniReadValue("setgame", "金手指") != "1")
                                {
                                    IndexViewModel.psp.IniWriteValue("General", "EnableCheats", "False");
                                    if (System.IO.File.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\Cheats\ULJS00329.ini"))
                                    {
                                        System.IO.File.Delete(Common.CommonSTA.LujingPSP + @"memstick\PSP\Cheats\ULJS00329.ini");
                                    }
                                }
                                else
                                {
                                    IndexViewModel.psp.IniWriteValue("General", "EnableCheats", "True");
                                    try
                                    {
                                        if (!System.IO.File.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\Cheats\ULJS00329.ini"))
                                        {
                                            System.IO.File.WriteAllBytes(Common.CommonSTA.LujingPSP + @"memstick\PSP\Cheats\ULJS00329.ini", Properties.Resources.ULJS00329);
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        MessageBox.Show("操作被禁止");
                                    }
                                }
                                if (System.IO.File.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\SYSTEM\ULJS00329_ppsspp.ini"))
                                {
                                    System.IO.File.Delete(Common.CommonSTA.LujingPSP + @"memstick\PSP\SYSTEM\ULJS00329_ppsspp.ini");
                                }
                                IndexViewModel.psp.IniWriteValue("Graphics", "FullScreen ", " True");
                                IndexViewModel.psp.IniWriteValue("CPU", "IOTimingMethod ", " 0");
                                IndexViewModel.psp.IniWriteValue("Network", "PortOffset ", " 5000");
                                IndexViewModel.psp.IniWriteValue("Network", "proAdhocServer ", " " + Common.CommonSTA._ip);
                                int[] mac = new int[] { new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16) };
                                IndexViewModel.psp.IniWriteValue("SystemParam", "MacAddress ", " " + mac[0] + mac[1] + ":" + mac[2] + mac[3] + ":" + mac[4] + mac[5] + ":" + mac[6] + mac[7] + ":" + mac[8] + mac[9] + ":" + mac[10] + mac[11]);
                                #endregion
                                #region 启动游戏
                                try
                                {
                                    System.Diagnostics.Process.Start(Common.CommonSTA.LujingPSP + @"PPSSPPWindows64.exe", Common.CommonSTA.LujingISO + @"UJS00329.iso");
                                }
                                catch (Exception)
                                {
                                    View.IndexView.mainThreadSynContext.Post(new System.Threading.SendOrPostCallback(s =>
                                    IndexViewModel.IndexModel.MainContent = (FrameworkElement)Type.GetType("Mj.View.Page5View").GetConstructor(System.Type.EmptyTypes).Invoke(null)), null);
                                    IndexViewModel.IndexModel.Select1 = 1; IndexViewModel.IndexModel.Select2 = 0; IndexViewModel.IndexModel.Select3 = 0;
                                    MessageBox.Show("模拟器损坏，请重新初始化\n\r\n\r或请前往设置中指定您自己[PPSSPP]的位置");
                                }
                                #endregion
                                #endregion
                                Common.CommonSTA.IsPlayStart = false;
                                IndexViewModel.IndexModel.ICO = "../Assets/logo128.ico";
                            }
                            #endregion

                        }).ContinueWith<int>((t) =>
                        {
                            return 1;
                        });

                    }
                    else
                    {
                        #region 正常启动
                        if (IndexViewModel.ini.IniReadValue("setgame", "联机") != "0")
                        {
                            #region 悬浮窗总执行
                            if (IndexViewModel.IndexModel.IPtupian == "../Assets/ping-close.png")//AAA窗口开启的话
                            {
                                IndexViewModel.IndexModel.XfcView.Hide();
                                Common.CommonSTA.Noping3 = 1;
                                Common.CommonSTA.XDS = 0;
                                //AAA窗口关闭
                            }
                            else
                            {
                                IndexViewModel.IndexModel.XfcView.Owner = this;
                                try
                                {
                                    IndexViewModel.IndexModel.XfcView.Show();
                                    Common.CommonSTA.XDS = 1;
                                    Common.CommonSTA.Noping3 = 0;
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show("要复活小电视嘛 = = ");
                                    IndexViewModel.IndexModel.XfcView = new View.XfcView();
                                    IndexViewModel.IndexModel.XfcView.Show();
                                    Common.CommonSTA.XDS = 1;
                                    Common.CommonSTA.Noping3 = 0;
                                }
                            }
                            #endregion
                            #region 有网模式
                            try
                            {
                                System.Diagnostics.Process.Start(Common.CommonSTA.LujingFCN);//FCN
                            }
                            catch (Exception)
                            {
                                IndexViewModel.IndexModel.MainContent = (FrameworkElement)Type.GetType("Mj.View.Page5View").GetConstructor(System.Type.EmptyTypes).Invoke(null);
                                IndexViewModel.IndexModel.Select1 = 1; IndexViewModel.IndexModel.Select2 = 0; IndexViewModel.IndexModel.Select3 = 0;
                                MessageBox.Show("联机模块损坏，请重新初始化\n\r\n\r或请前往设置中指定您自己[联机模块]的位置");
                                IndexViewModel.IndexModel.ICO = "../Assets/logo128.ico";
                                IndexViewModel.IndexModel.PlayContent = "立即开始";
                                Common.CommonSTA.IsPlayStart = false;
                                return;
                            }
                            Common.CommonSTA.Danji = false;
                            Common.CommonSTA.IsOK = false;
                            if (IndexViewModel.ini.IniReadValue("setgame", "不提示") != "1")
                            {
                                #region 显示弹窗
                                try
                                {
                                    IndexViewModel.IndexModel.Tishi.Show();
                                }
                                catch (Exception)
                                {
                                    IndexViewModel.IndexModel.Tishi = new View.UMessageBox();
                                    IndexViewModel.IndexModel.Tishi.Show();
                                }
                                #endregion
                                #region 倒计时
                                System.Threading.Tasks.Task.Run(() =>
                                {
                                    for (int i = 5; i > 0; i--)
                                    {
                                        View.IndexView.mainThreadSynContext.Post(new System.Threading.SendOrPostCallback(s =>
                                        IndexViewModel.IndexModel.Tishi.ZDL = "知道了(" + i + ")"), null);
                                        System.Threading.Thread.Sleep(1000);
                                    }
                                    #region 自动关闭弹窗
                                    try
                                    {
                                        View.IndexView.mainThreadSynContext.Post(new System.Threading.SendOrPostCallback(s =>
                                        IndexViewModel.IndexModel.Tishi.Hide()), null);
                                    }
                                    catch (Exception)
                                    {
                                        ;
                                    }
                                    #endregion
                                    Common.CommonSTA.IsOK = true;
                                }).ContinueWith<int>((t) =>
                                {

                                    return 1;
                                });
                                #endregion
                            }
                            System.Threading.Tasks.Task.Run(() =>
                            {
                                IndexViewModel.IndexModel.PlayContent = "等待选服/取消";
                                while (true)
                                {
                                    if (Common.CommonSTA.Stop == true)
                                    {
                                        Common.CommonSTA.Noping2 = 1;
                                        break;
                                    }
                                    if (Common.CommonSTA.Danji == true)
                                    {
                                        #region 悬浮窗关闭
                                        View.IndexView.mainThreadSynContext.Post(new System.Threading.SendOrPostCallback(s =>
                                            IndexViewModel.IndexModel.XfcView.Hide()), null);
                                        Common.CommonSTA.Noping3 = 1;
                                        Common.CommonSTA.XDS = 0;
                                        #endregion
                                        #region 关闭网络
                                        IndexViewModel.psp.IniWriteValue("Network", "EnableWlan", "False");
                                        #endregion
                                        #region 保险
                                        IndexViewModel.psp.IniWriteValue("SystemParam", "PSPModel", "1");
                                        if (IndexViewModel.ini.IniReadValue("setgame", "游戏名") != "")
                                        {
                                            IndexViewModel.psp.IniWriteValue("SystemParam", "NickName", Common.CommonSTA.PSPName);
                                        }
                                        if (IndexViewModel.ini.IniReadValue("setgame", "金手指") != "1")
                                        {
                                            IndexViewModel.psp.IniWriteValue("General", "EnableCheats", "False");
                                            if (System.IO.File.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\Cheats\ULJS00329.ini"))
                                            {
                                                System.IO.File.Delete(Common.CommonSTA.LujingPSP + @"memstick\PSP\Cheats\ULJS00329.ini");
                                            }
                                        }
                                        else
                                        {
                                            IndexViewModel.psp.IniWriteValue("General", "EnableCheats", "True");
                                            try
                                            {
                                                if (!System.IO.File.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\Cheats\ULJS00329.ini"))
                                                {
                                                    System.IO.File.WriteAllBytes(Common.CommonSTA.LujingPSP + @"memstick\PSP\Cheats\ULJS00329.ini", Properties.Resources.ULJS00329);
                                                }
                                            }
                                            catch (Exception)
                                            {
                                                MessageBox.Show("操作被禁止");
                                            }
                                        }
                                        if (System.IO.File.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\SYSTEM\ULJS00329_ppsspp.ini"))
                                        {
                                            System.IO.File.Delete(Common.CommonSTA.LujingPSP + @"memstick\PSP\SYSTEM\ULJS00329_ppsspp.ini");
                                        }
                                        IndexViewModel.psp.IniWriteValue("Graphics", "FullScreen ", " True");
                                        IndexViewModel.psp.IniWriteValue("CPU", "IOTimingMethod ", " 0");
                                        IndexViewModel.psp.IniWriteValue("Network", "PortOffset ", " 5000");
                                        IndexViewModel.psp.IniWriteValue("Network", "proAdhocServer ", " " + Common.CommonSTA._ip);
                                        int[] mac = new int[] { new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16) };
                                        IndexViewModel.psp.IniWriteValue("SystemParam", "MacAddress ", " " + mac[0] + mac[1] + ":" + mac[2] + mac[3] + ":" + mac[4] + mac[5] + ":" + mac[6] + mac[7] + ":" + mac[8] + mac[9] + ":" + mac[10] + mac[11]);
                                        #endregion
                                        #region 启动游戏
                                        try
                                        {
                                            System.Diagnostics.Process.Start(Common.CommonSTA.LujingPSP + @"PPSSPPWindows64.exe", Common.CommonSTA.LujingISO + @"UJS00329.iso");
                                        }
                                        catch (Exception)
                                        {
                                            IndexViewModel.IndexModel.MainContent = (FrameworkElement)Type.GetType("Mj.View.Page5View").GetConstructor(System.Type.EmptyTypes).Invoke(null);
                                            IndexViewModel.IndexModel.Select1 = 1; IndexViewModel.IndexModel.Select2 = 0; IndexViewModel.IndexModel.Select3 = 0;
                                            MessageBox.Show("模拟器损坏，请重新初始化\n\r\n\r或请前往设置中指定您自己[PPSSPP]的位置");
                                        }
                                        #endregion
                                        break;
                                    }

                                    try
                                    {
                                        System.Net.NetworkInformation.PingReply pingReply = Common.CommonSTA.ping2.Send(Common.CommonSTA._ip);
                                        if (pingReply.Status == System.Net.NetworkInformation.IPStatus.Success)
                                        {
                                            Common.CommonSTA.PING = true;
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        MessageBox.Show("网络异常"); break;
                                    }

                                    if (Common.CommonSTA.PING == true && Common.CommonSTA.IsOK == true)
                                    {
                                        #region 启用网络
                                        IndexViewModel.psp.IniWriteValue("Network", "EnableWlan", "True");
                                        #endregion
                                        #region 保险
                                        IndexViewModel.psp.IniWriteValue("SystemParam", "PSPModel", "1");
                                        if (IndexViewModel.ini.IniReadValue("setgame", "游戏名") != "")
                                        {
                                            IndexViewModel.psp.IniWriteValue("SystemParam", "NickName", Common.CommonSTA.PSPName);
                                        }
                                        if (IndexViewModel.ini.IniReadValue("setgame", "金手指") != "1")
                                        {
                                            IndexViewModel.psp.IniWriteValue("General", "EnableCheats", "False");
                                            if (System.IO.File.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\Cheats\ULJS00329.ini"))
                                            {
                                                System.IO.File.Delete(Common.CommonSTA.LujingPSP + @"memstick\PSP\Cheats\ULJS00329.ini");
                                            }
                                        }
                                        else
                                        {
                                            IndexViewModel.psp.IniWriteValue("General", "EnableCheats", "True");
                                            try
                                            {
                                                if (!System.IO.File.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\Cheats\ULJS00329.ini"))
                                                {
                                                    System.IO.File.WriteAllBytes(Common.CommonSTA.LujingPSP + @"memstick\PSP\Cheats\ULJS00329.ini", Properties.Resources.ULJS00329);
                                                }
                                            }
                                            catch (Exception)
                                            {
                                                MessageBox.Show("操作被禁止");
                                            }
                                        }
                                        if (System.IO.File.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\SYSTEM\ULJS00329_ppsspp.ini"))
                                        {
                                            System.IO.File.Delete(Common.CommonSTA.LujingPSP + @"memstick\PSP\SYSTEM\ULJS00329_ppsspp.ini");
                                        }
                                        IndexViewModel.psp.IniWriteValue("Graphics", "FullScreen ", " True");
                                        IndexViewModel.psp.IniWriteValue("CPU", "IOTimingMethod ", " 0");
                                        IndexViewModel.psp.IniWriteValue("Network", "PortOffset ", " 5000");
                                        IndexViewModel.psp.IniWriteValue("Network", "proAdhocServer ", " " + Common.CommonSTA._ip);
                                        int[] mac = new int[] { new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16) };
                                        IndexViewModel.psp.IniWriteValue("SystemParam", "MacAddress ", " " + mac[0] + mac[1] + ":" + mac[2] + mac[3] + ":" + mac[4] + mac[5] + ":" + mac[6] + mac[7] + ":" + mac[8] + mac[9] + ":" + mac[10] + mac[11]);
                                        #endregion
                                        #region 启动游戏
                                        try
                                        {
                                            System.Diagnostics.Process.Start(Common.CommonSTA.LujingPSP + @"PPSSPPWindows64.exe", Common.CommonSTA.LujingISO + @"UJS00329.iso");
                                        }
                                        catch (Exception)
                                        {
                                            IndexViewModel.IndexModel.MainContent = (FrameworkElement)Type.GetType("Mj.View.Page5View").GetConstructor(System.Type.EmptyTypes).Invoke(null);
                                            IndexViewModel.IndexModel.Select1 = 1; IndexViewModel.IndexModel.Select2 = 0; IndexViewModel.IndexModel.Select3 = 0;
                                            MessageBox.Show("模拟器损坏，请重新初始化\n\r\n\r或请前往设置中指定您自己[PPSSPP]的位置");
                                        }
                                        #endregion
                                        break;
                                    }
                                    System.Threading.Thread.Sleep(1000);
                                }
                            }).ContinueWith<int>((t) =>
                            {
                                IndexViewModel.IndexModel.PlayContent = "立即开始";
                                Common.CommonSTA.PING = false;
                                Common.CommonSTA.IsPlayStart = false;
                                IndexViewModel.IndexModel.ICO = "../Assets/logo128.ico";
                                return 1;
                            });
                            #endregion
                        }
                        else
                        {
                            #region 无网模式
                            #region 悬浮窗关闭
                            IndexViewModel.IndexModel.XfcView.Hide();
                            Common.CommonSTA.Noping3 = 1;
                            Common.CommonSTA.XDS = 0;
                            #endregion
                            #region 关闭网络
                            IndexViewModel.psp.IniWriteValue("Network", "EnableWlan", "False");
                            #endregion
                            #region 保险
                            IndexViewModel.psp.IniWriteValue("SystemParam", "PSPModel", "1");
                            if (IndexViewModel.ini.IniReadValue("setgame", "游戏名") != "")
                            {
                                IndexViewModel.psp.IniWriteValue("SystemParam", "NickName", Common.CommonSTA.PSPName);
                            }
                            if (IndexViewModel.ini.IniReadValue("setgame", "金手指") != "1")
                            {
                                IndexViewModel.psp.IniWriteValue("General", "EnableCheats", "False");
                                if (System.IO.File.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\Cheats\ULJS00329.ini"))
                                {
                                    System.IO.File.Delete(Common.CommonSTA.LujingPSP + @"memstick\PSP\Cheats\ULJS00329.ini");
                                }
                            }
                            else
                            {
                                IndexViewModel.psp.IniWriteValue("General", "EnableCheats", "True");
                                try
                                {
                                    if (!System.IO.File.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\Cheats\ULJS00329.ini"))
                                    {
                                        System.IO.File.WriteAllBytes(Common.CommonSTA.LujingPSP + @"memstick\PSP\Cheats\ULJS00329.ini", Properties.Resources.ULJS00329);
                                    }
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show("操作被禁止");
                                }
                            }
                            if (System.IO.File.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\SYSTEM\ULJS00329_ppsspp.ini"))
                            {
                                System.IO.File.Delete(Common.CommonSTA.LujingPSP + @"memstick\PSP\SYSTEM\ULJS00329_ppsspp.ini");
                            }
                            IndexViewModel.psp.IniWriteValue("Graphics", "FullScreen ", " True");
                            IndexViewModel.psp.IniWriteValue("CPU", "IOTimingMethod ", " 0");
                            IndexViewModel.psp.IniWriteValue("Network", "PortOffset ", " 5000");
                            IndexViewModel.psp.IniWriteValue("Network", "proAdhocServer ", " " + Common.CommonSTA._ip);
                            int[] mac = new int[] { new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16) };
                            IndexViewModel.psp.IniWriteValue("SystemParam", "MacAddress ", " " + mac[0] + mac[1] + ":" + mac[2] + mac[3] + ":" + mac[4] + mac[5] + ":" + mac[6] + mac[7] + ":" + mac[8] + mac[9] + ":" + mac[10] + mac[11]);
                            #endregion
                            #region 启动游戏
                            try
                            {
                                System.Diagnostics.Process.Start(Common.CommonSTA.LujingPSP + @"PPSSPPWindows64.exe", Common.CommonSTA.LujingISO + @"UJS00329.iso");
                            }
                            catch (Exception)
                            {
                                IndexViewModel.IndexModel.MainContent = (FrameworkElement)Type.GetType("Mj.View.Page5View").GetConstructor(System.Type.EmptyTypes).Invoke(null);
                                IndexViewModel.IndexModel.Select1 = 1; IndexViewModel.IndexModel.Select2 = 0; IndexViewModel.IndexModel.Select3 = 0;
                                MessageBox.Show("模拟器损坏，请重新初始化\n\r\n\r或请前往设置中指定您自己[PPSSPP]的位置");
                            }
                            #endregion
                            #endregion
                            Common.CommonSTA.IsPlayStart = false;
                            IndexViewModel.IndexModel.ICO = "../Assets/logo128.ico";
                            IndexViewModel.IndexModel.PlayContent = "立即开始";
                        }
                        #endregion
                    }
                }
            }
            #endregion

        }
        #endregion
        //————————————————————————————————————————————————————————————
        //事件驱动
        //————————————————————————————————————————————————————————————
        #region 最小化按钮MinsizeButton_Click
        private void MinsizeButton_Click(object sender, EventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        #endregion
        #region 最大化按钮MaxsizeButton_Click未使用
        private void MaxsizeButton_Click(object sender, EventArgs e)
        {
            WindowState = WindowState.Maximized;
        }
        #endregion
        #region 上栏拖动Shangmove
        private void Shangmove(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
        #endregion
        #region 绝对关闭APPClose
        private void APPClose(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        #endregion
        #region 关闭方式MyClosing
        private void Myclosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IndexViewModel.ini.IniReadValue("window", "禁用托盘") == "1")
            {
                Application.Current.Shutdown();
            }
            else
            {
                this.Hide();
                e.Cancel = true;
            }
        }
        #endregion
        #region 禁用托盘Jinyongtuopan
        private void Jinyongtuopan(object sender, RoutedEventArgs e)
        {
            IndexViewModel.ini.IniWriteValue("window", "禁用托盘", "1");
            tuopan.Visibility = Visibility.Collapsed;
            this.Show();
        }
        #endregion
        #region 右键启动
        private void PlayDanji(object sender, MouseButtonEventArgs e)
        {
            if (IndexViewModel.ini.IniReadValue("setgame", "游戏名") == "")
            {
                IndexViewModel.IndexModel.MainContent = (FrameworkElement)Type.GetType("Mj.View.Page4View").GetConstructor(System.Type.EmptyTypes).Invoke(null);
                IndexViewModel.IndexModel.Select1 = 1; IndexViewModel.IndexModel.Select2 = 0; IndexViewModel.IndexModel.Select3 = 0;
                MessageBox.Show("请修改您的游戏昵称");
                return;
            }
            if (!System.IO.File.Exists(Common.CommonSTA.LujingISO + "UJS00329.iso"))
            {
                IndexViewModel.IndexModel.MainContent = (FrameworkElement)Type.GetType("Mj.View.Page4View").GetConstructor(System.Type.EmptyTypes).Invoke(null);
                IndexViewModel.IndexModel.Select1 = 1; IndexViewModel.IndexModel.Select2 = 0; IndexViewModel.IndexModel.Select3 = 0;
                MessageBox.Show("未找到游戏文件目录，请重新初始化\n\r\n\r或请前往设置中指定您自己[UJS00329.iso]的位置");
                return;
            }
            IndexViewModel.IndexModel.PlayContent = "启动中";
            Common.CommonSTA.IsPlayStart = true;
            IndexViewModel.IndexModel.ICO = "../Assets/ico.ico";
            #region 悬浮窗关闭
            IndexViewModel.IndexModel.XfcView.Hide();
            Common.CommonSTA.Noping3 = 1;
            Common.CommonSTA.XDS = 0;
            #endregion
            #region 隐藏启动器
            if (IndexViewModel.ini.IniReadValue("window", "禁用托盘") != "1")
            {
                this.Hide();
            }
            #endregion
            if (!System.IO.File.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\SYSTEM\isppsspp"))
            {
                #region PPSSPP初始化
                if (System.IO.File.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\SYSTEM\ppsspp.ini"))
                {
                    System.IO.File.Delete(Common.CommonSTA.LujingPSP + @"memstick\PSP\SYSTEM\ppsspp.ini");
                }
                try
                {
                    System.Diagnostics.Process.Start(Common.CommonSTA.LujingPSP + @"PPSSPPWindows64.exe");
                }
                catch (Exception)
                {
                    IndexViewModel.IndexModel.MainContent = (FrameworkElement)Type.GetType("Mj.View.Page5View").GetConstructor(System.Type.EmptyTypes).Invoke(null);
                    IndexViewModel.IndexModel.Select1 = 1; IndexViewModel.IndexModel.Select2 = 0; IndexViewModel.IndexModel.Select3 = 0;
                    MessageBox.Show("模拟器损坏，启动器位置可能没有位于模拟器根目录\n\r\n\r或请前往设置中重新指定游戏文件的位置");
                    Common.CommonSTA.IsPlayStart = false;
                    IndexViewModel.IndexModel.ICO = "../Assets/logo128.ico";
                    return;
                }
                System.Threading.Tasks.Task.Run(() =>
                {
                    System.Diagnostics.Process[] pProcess;
                    pProcess = System.Diagnostics.Process.GetProcesses();
                    while (true)
                    {
                        if (System.IO.File.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\SYSTEM\ppsspp.ini"))
                        {
                            System.Threading.Thread.Sleep(500);
                            for (int i = 1; i <= pProcess.Length - 1; i++)
                            {
                                if (pProcess[i].ProcessName == "PPSSPPWindows64")
                                {
                                    pProcess[i].Kill();
                                    break;
                                }
                            }
                            break;
                        }
                        System.Threading.Thread.Sleep(500);
                    }

                    #endregion
                    //设置改变
                    #region 默认
                    IndexViewModel.psp.IniWriteValue("General", "CheckForNewVersion ", " False");
                    IndexViewModel.psp.IniWriteValue("General", "ForceLagSync2 ", " True");
                    IndexViewModel.psp.IniWriteValue("Graphics", "AnisotropyLevel ", " 0");
                    IndexViewModel.psp.IniWriteValue("Graphics", "ReplaceTextures ", " True");
                    IndexViewModel.psp.IniWriteValue("Graphics", "TextureBackoffCache ", " True");
                    IndexViewModel.psp.IniWriteValue("Graphics", "FullScreen ", " True");
                    IndexViewModel.psp.IniWriteValue("Graphics", "SplineBezierQuality ", " 0");
                    IndexViewModel.psp.IniWriteValue("Graphics", "DisableSlowFramebufEffects ", " True");
                    IndexViewModel.psp.IniWriteValue("Network", "ForcedFirstConnect ", " True");
                    IndexViewModel.psp.IniWriteValue("Network", "EnableNetworkChat ", " True");
                    IndexViewModel.psp.IniWriteValue("Network", "ChatButtonPosition ", " 3");
                    IndexViewModel.psp.IniWriteValue("Network", "ChatScreenPosition ", " 3");

                    #endregion


                    if (!System.IO.File.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\SYSTEM\isppsspp"))
                    {
                        try
                        {
                            System.IO.File.Create(Common.CommonSTA.LujingPSP + @"memstick\PSP\SYSTEM\isppsspp").WriteByte(1);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("操作被禁止");
                        }
                    }

                    #region 无网模式
                    #region 关闭网络
                    IndexViewModel.psp.IniWriteValue("Network", "EnableWlan", "False");
                    #endregion
                    #region 保险
                    IndexViewModel.psp.IniWriteValue("SystemParam", "PSPModel", "1");
                    if (IndexViewModel.ini.IniReadValue("setgame", "游戏名") != "")
                    {
                        IndexViewModel.psp.IniWriteValue("SystemParam", "NickName", Common.CommonSTA.PSPName);
                    }
                    if (IndexViewModel.ini.IniReadValue("setgame", "金手指") != "1")
                    {
                        IndexViewModel.psp.IniWriteValue("General", "EnableCheats", "False");
                        if (System.IO.File.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\Cheats\ULJS00329.ini"))
                        {
                            System.IO.File.Delete(Common.CommonSTA.LujingPSP + @"memstick\PSP\Cheats\ULJS00329.ini");
                        }
                    }
                    else
                    {
                        IndexViewModel.psp.IniWriteValue("General", "EnableCheats", "True");
                        try
                        {
                            if (!System.IO.File.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\Cheats\ULJS00329.ini"))
                            {
                                System.IO.File.WriteAllBytes(Common.CommonSTA.LujingPSP + @"memstick\PSP\Cheats\ULJS00329.ini", Properties.Resources.ULJS00329);
                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("操作被禁止");
                        }
                    }
                    if (System.IO.File.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\SYSTEM\ULJS00329_ppsspp.ini"))
                    {
                        System.IO.File.Delete(Common.CommonSTA.LujingPSP + @"memstick\PSP\SYSTEM\ULJS00329_ppsspp.ini");
                    }
                    IndexViewModel.psp.IniWriteValue("Graphics", "FullScreen ", " True");
                    IndexViewModel.psp.IniWriteValue("CPU", "IOTimingMethod ", " 0");
                    IndexViewModel.psp.IniWriteValue("Network", "PortOffset ", " 5000");
                    IndexViewModel.psp.IniWriteValue("Network", "proAdhocServer ", " " + Common.CommonSTA._ip);
                    int[] mac = new int[] { new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16) };
                    IndexViewModel.psp.IniWriteValue("SystemParam", "MacAddress ", " " + mac[0] + mac[1] + ":" + mac[2] + mac[3] + ":" + mac[4] + mac[5] + ":" + mac[6] + mac[7] + ":" + mac[8] + mac[9] + ":" + mac[10] + mac[11]);
                    #endregion
                    #region 启动游戏
                    try
                    {
                        System.Diagnostics.Process.Start(Common.CommonSTA.LujingPSP + @"PPSSPPWindows64.exe", Common.CommonSTA.LujingISO + @"UJS00329.iso");
                    }
                    catch (Exception)
                    {
                        IndexViewModel.IndexModel.MainContent = (FrameworkElement)Type.GetType("Mj.View.Page5View").GetConstructor(System.Type.EmptyTypes).Invoke(null);
                        IndexViewModel.IndexModel.Select1 = 1; IndexViewModel.IndexModel.Select2 = 0; IndexViewModel.IndexModel.Select3 = 0;
                        MessageBox.Show("模拟器损坏，启动器位置可能没有位于模拟器根目录\n\r\n\r或请前往设置中重新指定游戏文件的位置");
                    }
                    #endregion
                    #endregion
                    IndexViewModel.IndexModel.ICO = "../Assets/logo128.ico";
                    IndexViewModel.IndexModel.PlayContent = "立即开始";
                    Common.CommonSTA.IsPlayStart = false;
                }).ContinueWith<int>((t) =>
                {
                    return 1;
                });

            }
            else
            {
                #region 无网模式
                #region 关闭网络
                IndexViewModel.psp.IniWriteValue("Network", "EnableWlan", "False");
                #endregion
                #region 保险
                IndexViewModel.psp.IniWriteValue("SystemParam", "PSPModel", "1");
                if (IndexViewModel.ini.IniReadValue("setgame", "游戏名") != "")
                {
                    IndexViewModel.psp.IniWriteValue("SystemParam", "NickName", Common.CommonSTA.PSPName);
                }
                if (IndexViewModel.ini.IniReadValue("setgame", "金手指") != "1")
                {
                    IndexViewModel.psp.IniWriteValue("General", "EnableCheats", "False");
                    if (System.IO.File.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\Cheats\ULJS00329.ini"))
                    {
                        System.IO.File.Delete(Common.CommonSTA.LujingPSP + @"memstick\PSP\Cheats\ULJS00329.ini");
                    }
                }
                else
                {
                    IndexViewModel.psp.IniWriteValue("General", "EnableCheats", "True");
                    try
                    {
                        if (!System.IO.File.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\Cheats\ULJS00329.ini"))
                        {
                            System.IO.File.WriteAllBytes(Common.CommonSTA.LujingPSP + @"memstick\PSP\Cheats\ULJS00329.ini", Properties.Resources.ULJS00329);
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("操作被禁止");
                    }
                }
                if (System.IO.File.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\SYSTEM\ULJS00329_ppsspp.ini"))
                {
                    System.IO.File.Delete(Common.CommonSTA.LujingPSP + @"memstick\PSP\SYSTEM\ULJS00329_ppsspp.ini");
                }
                IndexViewModel.psp.IniWriteValue("Graphics", "FullScreen ", " True");
                IndexViewModel.psp.IniWriteValue("CPU", "IOTimingMethod ", " 0");
                IndexViewModel.psp.IniWriteValue("Network", "PortOffset ", " 5000");
                IndexViewModel.psp.IniWriteValue("Network", "proAdhocServer ", " " + Common.CommonSTA._ip);
                int[] mac = new int[] { new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16) };
                IndexViewModel.psp.IniWriteValue("SystemParam", "MacAddress ", " " + mac[0] + mac[1] + ":" + mac[2] + mac[3] + ":" + mac[4] + mac[5] + ":" + mac[6] + mac[7] + ":" + mac[8] + mac[9] + ":" + mac[10] + mac[11]);
                #endregion
                #region 启动游戏
                try
                {
                    System.Diagnostics.Process.Start(Common.CommonSTA.LujingPSP + @"PPSSPPWindows64.exe", Common.CommonSTA.LujingISO + @"UJS00329.iso");
                }
                catch (Exception)
                {
                    IndexViewModel.IndexModel.MainContent = (FrameworkElement)Type.GetType("Mj.View.Page5View").GetConstructor(System.Type.EmptyTypes).Invoke(null);
                    IndexViewModel.IndexModel.Select1 = 1; IndexViewModel.IndexModel.Select2 = 0; IndexViewModel.IndexModel.Select3 = 0;
                    MessageBox.Show("模拟器损坏，启动器位置可能没有位于模拟器根目录\n\r\n\r或请前往设置中重新指定游戏文件的位置");
                }
                #endregion
                #endregion
                IndexViewModel.IndexModel.ICO = "../Assets/logo128.ico";
                IndexViewModel.IndexModel.PlayContent = "立即开始";
                Common.CommonSTA.IsPlayStart = false;
            }
        }

        #endregion
        #region 键盘切换Jianpanqiehuan
        private void Jianpanqiehuan(object sender, MouseButtonEventArgs e)
        {
            if (System.IO.Directory.Exists(Common.CommonSTA.LujingPSP + @"memstick"))
            {
                if (IndexViewModel.IndexModel.Anjian != "../Assets/anjian2.png")
                {//+
                    IndexViewModel.IndexModel.Anjian = "../Assets/anjian2.png";


                    System.Windows.Media.Animation.Storyboard SB_Handle = (System.Windows.Media.Animation.Storyboard)this.FindResource("SB_Handle");
                    SB_Handle.Begin();
                    #region 手柄提示
                    MessageBoxResult Handleset = MessageBox.Show("是否应用手柄预设键位", "请确认", MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
                    if (Handleset == MessageBoxResult.OK)
                    {
                        IndexViewModel.ini.IniWriteValue("setgame", "游戏按键", "2");
                     


                        if (!System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"mfjsml.top.anjian2"))
                        {
                            System.IO.File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + @"mfjsml.top.anjian2", Properties.Resources.Anjian2);
                        }
                        try
                        {
                            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                            System.IO.Compression.ZipFile.ExtractToDirectory(AppDomain.CurrentDomain.BaseDirectory + @"mfjsml.top.anjian2", Common.CommonSTA.LujingPSP + @"memstick\", Encoding.GetEncoding("GB2312"), true);
                        }
                        catch (Exception)
                        {
                            IndexViewModel.ini.IniWriteValue("setgame", "游戏按键", "1");
                            IndexViewModel.IndexModel.Anjian = "../Assets/anjian1.png";
                            System.Windows.MessageBox.Show("手柄键位被禁止");
                        }
                    }
                    #endregion

                }
                else
                {//+
                    IndexViewModel.IndexModel.Anjian = "../Assets/anjian1.png";
                    System.Windows.Media.Animation.Storyboard SB_Keyboard = (System.Windows.Media.Animation.Storyboard)this.FindResource("SB_Keyboard");
                    SB_Keyboard.Begin();
                    #region 按键提示
                    MessageBoxResult Handleset = MessageBox.Show("是否应用按键预设键位", "请确认", MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
                    if (Handleset == MessageBoxResult.OK)
                    {
                        IndexViewModel.ini.IniWriteValue("setgame", "游戏按键", "1");
                       

                        if (!System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"mfjsml.top.anjian1"))
                        {
                            System.IO.File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + @"mfjsml.top.anjian1", Properties.Resources.Anjian1);
                        }
                        try
                        {
                            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                            System.IO.Compression.ZipFile.ExtractToDirectory(AppDomain.CurrentDomain.BaseDirectory + @"mfjsml.top.anjian1", Common.CommonSTA.LujingPSP + @"memstick\", Encoding.GetEncoding("GB2312"), true);
                        }
                        catch (Exception)
                        {
                            IndexViewModel.ini.IniWriteValue("setgame", "游戏按键", "2");
                            IndexViewModel.IndexModel.Anjian = "../Assets/anjian2.png";
                            System.Windows.MessageBox.Show("键盘键位被禁止");
                        }
                    }
                    #endregion
                }
            }
            else
            {
                IndexViewModel.IndexModel.IndexVisibility = "Visible";
                IndexViewModel.IndexModel.JianpanVisibility = "Collapsed";
                IndexViewModel.IndexModel.MainContent = (FrameworkElement)Type.GetType("Mj.View.Page5View").GetConstructor(System.Type.EmptyTypes).Invoke(null);
                IndexViewModel.IndexModel.Select1 = 1; IndexViewModel.IndexModel.Select2 = 0; IndexViewModel.IndexModel.Select3 = 0;
                MessageBox.Show("未找到游戏文件目录，请重新初始化\n\r\n\r或请前往设置中指定您自己[游戏镜像目录]的位置");
            }
        }
        #endregion

        //————————————————————————————————————————————————————————————
    }
}
