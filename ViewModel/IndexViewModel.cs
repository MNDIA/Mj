using Mj.Common;
using Mj.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows;

namespace Mj.ViewModel
{
    public class IndexViewModel
    {
        public static IndexModel IndexModel { get; set; }//基本关系
        //public IniBase ini = new IniBase(AppDomain.CurrentDomain.BaseDirectory + @"1\set");//启用 ini
        public static IniBase ini = new IniBase(System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\IndexOfMagic\ppsspp\set");//启用 ini
        public IniBase ini5 = new IniBase(System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\IndexOfMagic\unreal5\usebeta");//启用 ini
        public IniBase psp = new IniBase(ini.IniReadValue("setgame", "PPSSPP路径") + @"memstick\PSP\SYSTEM\ppsspp.ini");//启用 ini



        //[System.Runtime.InteropServices.DllImport("user32.dll")] public static extern int MessageBoxTimeoutA(IntPtr hWnd, string msg, string Caps, int type, int Id, int time); //引用DLL
        //————————————————————————————————————————————————————————————————
        //命令集列表Command事件驱动
        //————————————————————————————————————————————————————————————————
        #region Command事件驱动📃提示
        //public CommandBase XXXCommand { get; set; } 
        #endregion
        public CommandBase CloseWindowCommand { get; set; }
        public CommandBase PINGButtonCommand { get; set; }
        public CommandBase XFButtonCommand { get; set; }
        public CommandBase NavChangedCommand { get; set; }
        public CommandBase SetGridChangeCommand { get; set; }
        public CommandBase PlayButtonCommand { get; set; }

        public CommandBase ZhumuluCommand { get; set; }
        public CommandBase CaizhimuluCommand { get; set; }
        public CommandBase KuaijiefangshiCommand { get; set; }
        public CommandBase ShenqingmimaCommand { get; set; }
        public CommandBase GengxinjianceCommand { get; set; }
        public CommandBase YouxianjianCommand { get; set; }
        public CommandBase CaizhigongfangCommand { get; set; }
        public CommandBase IndextrueCommand { get; set; }
        public CommandBase XiazaimuluCommand { get; set; }
        public CommandBase LanpingCommand { get; set; }
        public CommandBase JinshouzhiCommand { get; set; }


        //————————————————————————————————————————————————————————————————
        public IndexViewModel()
        {
            IndexModel = new IndexModel();
            //————————————————————————————————————————————————————————————
            //初始化封装
            //————————————————————————————————————————————————————————————
            #region 初始化📃提示
            //进行;分号;可重复!!注意：ViewModel.cs中也含有封装初始化
            //this.IndexModel.IP = "233";//set 
            //this.IndexModel.IP = IndexModel.Biaoti; //set且get 
            #endregion
            IndexModel.Biaoti = "";//set
            if (ini.IniReadValue("setgame", "IP") != "")
            {
                IndexModel.IP = ini.IniReadValue("setgame", "IP");
            }
            IndexModel.MainContent = (FrameworkElement)Type.GetType("Mj.View.Page1View").GetConstructor(System.Type.EmptyTypes).Invoke(null);//首页默认值
            IndexModel.MainContent2 = (FrameworkElement)Type.GetType("Mj.View.Page1View").GetConstructor(System.Type.EmptyTypes).Invoke(null);
            IndexModel.WebContent = (FrameworkElement)Type.GetType("Mj.View.PageWebView").GetConstructor(System.Type.EmptyTypes).Invoke(null);
            if (ini.IniReadValue("window", "Yuanjiao") == "1")
            {
                IndexModel.Yuanjiao = "10";
                IndexModel.Yuanjiao2 = "10,10,0,0";
                IndexModel.Yuanjiao3 = "0,10,0,0";
                IndexModel.Yuanjiao4 = "10,0,0,0";
            }//圆角
            #region 主题
            if (ini.IniReadValue("window", "主题") == "幻影白")
            {
                ;
            }
            else if (ini.IniReadValue("window", "主题") == "深空灰")
            {
                IndexModel.Whites2 = "#2A2C37";
                IndexModel.WhitesFF = "#FF2A2C37";
                IndexModel.Whites00 = "#002A2C37";
                IndexModel.Whites33 = "#332A2C37";
                IndexModel.Whites66 = "#662A2C37";
                IndexModel.WhitesAA = "#AA2A2C37";

                IndexModel.Whitem = "#393C4A";
                IndexModel.Line = "#838489";
                IndexModel.Line2 = "#51535A";

                IndexModel.DarksFF = "#FF3F3F46";
                IndexModel.Darks66 = "#663F3F46";
                IndexModel.DarksAA = "#AA3F3F46";
                IndexModel.Close66 = "#992A2C37";
                IndexModel.CloseAA = "#BB2A2C37";
            }
            else if (ini.IniReadValue("window", "主题") == "苹果绿")
            {
                IndexModel.Whites2 = "#CCE8CF";
                IndexModel.WhitesFF = "#FFCCE8CF";
                IndexModel.Whites00 = "#00CCE8CF";
                IndexModel.Whites33 = "#33CCE8CF";
                IndexModel.Whites66 = "#66CCE8CF";
                IndexModel.WhitesAA = "#AACCE8CF";

                IndexModel.Whitem = "#B6E2BB";
                IndexModel.Line = "#D4E2D5";
                IndexModel.Line2 = "#90A692";

                IndexModel.DarksFF = "#FFB6E2BB";
                IndexModel.Darks66 = "#66B6E2BB";
                IndexModel.DarksAA = "#AAB6E2BB";
                IndexModel.Close66 = "#22CCE8CF";
                IndexModel.CloseAA = "#FFCCE8CF";
            }
            else if (ini.IniReadValue("window", "主题") == "NVIDIA")
            {
                IndexModel.Whites2 = "#76b900";
                IndexModel.WhitesFF = "#FF000000";
                IndexModel.Whites00 = "#00000000";
                IndexModel.Whites33 = "#33000000";
                IndexModel.Whites66 = "#66000000";
                IndexModel.WhitesAA = "#AA000000";

                IndexModel.Whitem = "#76b900";
                IndexModel.Line = "#76b900";
                IndexModel.Line2 = "#76d300";

                IndexModel.DarksFF = "#76b900";
                IndexModel.Darks66 = "#76D300";
                IndexModel.DarksAA = "#76D300";
                IndexModel.Close66 = "#6676b900";
                IndexModel.CloseAA = "#AA76b900";
                IndexModel.Playdarks1 = "#FF76d300";
                IndexModel.Playdarks2 = "#FF76b900";
            }
            #endregion
            #region 托盘
            if (ini.IniReadValue("window", "禁用托盘") == "1")
            {
                IndexModel.Tuopan = "Collapsed";
            }
            #endregion
            #region 悬浮窗初始化
            if (ini.IniReadValue("setgame", "启用悬浮窗") == "0")
            {
                CommonSTA.XDS = 0;
                CommonSTA.Noping3 = 1;
                IndexModel.IPtupian = "../Assets/ping-close.png";
            }
            #endregion
            #region 路径初始化
            if (ini.IniReadValue("setgame", "FCN路径") != "")
            {
                CommonSTA.LujingFCN = ini.IniReadValue("setgame", "FCN路径");
            }
            if (ini.IniReadValue("setgame", "PPSSPP路径") != "")
            {
                CommonSTA.LujingPSP = ini.IniReadValue("setgame", "PPSSPP路径");
            }
            if (ini.IniReadValue("setgame", "ISO路径") != "")
            {
                CommonSTA.LujingISO = ini.IniReadValue("setgame", "ISO路径");
            }
            #endregion
            #region 游戏名始化
            if (ini.IniReadValue("setgame", "游戏名") != "")
            {
                CommonSTA.PSPName = ini.IniReadValue("setgame", "游戏名");
            }
            #endregion
            if (ini.IniReadValue("setgame", "游戏按键") == "2")
            {
                IndexModel.Anjian = "../Assets/anjian2.png";
                
            }


            //————————————————————————————————————————————————————————————
            //命令集Command内容
            //————————————————————————————————————————————————————————————
            #region 命令集内容📃提示
            //this.XXXCommand = new CommandBase();
            //this.XXXCommand.DoExecute = new Action<object>((o) =>
            //{
            //          执行内容，o为传参
            //});
            //this.XXXCommand.DoCanExecute = new Func<object, bool>((o) =>
            //{
            //          执行内容2，o为传参，并
            //    return 是否允许执行;
            //});
            #endregion

            //
            #region 关闭窗口CloseWindowCommand
            this.CloseWindowCommand = new CommandBase();
            this.CloseWindowCommand.DoExecute = new Action<object>((o) =>
            {
                //IndexModel.XFC.Close();
                //(o as Window).Close();
                //Environment.Exit(0);
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    (o as Window).Close();
                }));
            });
            this.CloseWindowCommand.DoCanExecute = new Func<object, bool>((o) =>
            {
                return true;
            });
            #endregion
            //
            #region 显示主页IndextrueCommand
            this.IndextrueCommand = new CommandBase();
            this.IndextrueCommand.DoExecute = new Action<object>((o) =>
            {
                //IndexModel.XFC.Close();
                //(o as Window).Close();
                //Environment.Exit(0);
                try
                {
                    (o as Window).Show();
                }
                catch (Exception)
                {
                    MessageBox.Show("你在干什么？");
                    Application.Current.Shutdown();
                }
            });
            this.IndextrueCommand.DoCanExecute = new Func<object, bool>((o) =>
            {
                return true;
            });
            #endregion
            //
            #region PING执行PINGButtonCommand
            this.PINGButtonCommand = new CommandBase();
            this.PINGButtonCommand.DoExecute = new Action<object>((o) =>
            {
                if (CommonSTA.Doping == 1)
                {
                    CommonSTA.Doping = 0;
                    if (CommonSTA.Doping2 == 0)
                    {
                        CommonSTA.Noping2 = 1;
                    }
                    CommonSTA.Noping = 0;
                    IndexModel.IPVisible = "Collapsed";
                    IndexModel.Searchtupian = "../Assets/noping.png";
                    IndexModel.MSVisible = "Visible";

                    System.Threading.Tasks.Task.Run(() =>
                    {
                        while (true)
                        {
                            if (CommonSTA.Noping == 1)
                            {
                                break;
                            }
                            try
                            {
                                System.Net.NetworkInformation.PingReply pingReply = CommonSTA.ping.Send(IndexModel.IP);
                                if (pingReply.Status == System.Net.NetworkInformation.IPStatus.Success)
                                {
                                    if (pingReply.RoundtripTime < 45)
                                    {
                                        IndexModel.MSColor = "Green";
                                        IndexModel.Searchtupian = "../Assets/greenping.png";
                                    }
                                    else if (pingReply.RoundtripTime < 70)
                                    {
                                        IndexModel.Searchtupian = "../Assets/yellowping.png";
                                        IndexModel.MSColor = "#f27900";
                                    }
                                    else
                                    {
                                        IndexModel.Searchtupian = "../Assets/redping.png";
                                        IndexModel.MSColor = "Red";
                                    }

                                    IndexModel.MS = " " + pingReply.RoundtripTime + " ms";
                                }
                                else
                                {
                                    IndexModel.MSColor = "Red";
                                    IndexModel.MS = "连接超时，请检查是否启用连接";
                                    IndexModel.Searchtupian = "../Assets/noping.png";
                                }
                            }
                            catch (Exception)
                            {
                                IndexModel.IPtishi = "请输入正确的地址"; break;
                            }
                            if (CommonSTA.Noping == 0)
                            {
                                System.Threading.Thread.Sleep(3000);
                            }
                        }
                    }).ContinueWith<int>((t) =>
                    {
                        CommonSTA.Doping = 1;
                        CommonSTA.Noping = 0;
                        IndexModel.MSVisible = "Collapsed";
                        IndexModel.IPVisible = "Visible";
                        IndexModel.MSColor = "Black";
                        IndexModel.MS = "检查中";
                        IndexModel.Searchtupian = "../Assets/search1.png";
                        return 1;
                    });

                }
                else
                {
                    CommonSTA.Noping = 1;
                    IndexModel.MSColor = "Black";
                    IndexModel.MS = "关闭中";
                    IndexModel.Searchtupian = "../Assets/noping.png";
                }
            });
            this.PINGButtonCommand.DoCanExecute = new Func<object, bool>((o) =>
            {
                if (IndexModel.IP.Length > 6 && IndexModel.IP.Length < 15 && IndexModel.IP.Contains("."))
                {
                    IndexModel.IPtishi = "";
                    ini.IniWriteValue("setgame", "IP", IndexModel.IP);
                    return true;
                }
                IndexModel.IPtishi = "请输入正确的地址";
                return false;
            });
            #endregion
            //  
            #region 悬浮窗XFButtonCommand
            this.XFButtonCommand = new CommandBase();
            this.XFButtonCommand.DoExecute = new Action<object>((o) =>
            {

                if (CommonSTA.XDS == 1)//AAA窗口开启的话
                {
                    IndexModel.IPtupian = "../Assets/ping-close.png";
                    IndexModel.XfcView.Hide();
                    CommonSTA.Noping3 = 1;
                    CommonSTA.XDS = 0;
                    ini.IniWriteValue("setgame", "启用悬浮窗", "0");
                    //AAA窗口关闭
                }
                else
                {
                    IndexModel.IPtupian = "../Assets/ip.png";
                    ini.IniWriteValue("setgame", "启用悬浮窗", "1");
                    IndexModel.XfcView.Owner = o as Window;
                    try
                    {
                        IndexModel.XfcView.Show();
                        CommonSTA.XDS = 1;
                        CommonSTA.Noping3 = 0;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("要复活小电视嘛 = = ");
                        IndexModel.XfcView = new View.XfcView();
                        IndexModel.XfcView.Show();
                        CommonSTA.XDS = 1;
                        CommonSTA.Noping3 = 0;
                    }

                    //IndexModel.XFCS.Add(IndexModel.XfcView);
                    //foreach (View.XfcView xFC in IndexModel.XFCS)
                    //{
                    //    xFC.Content = DateTime.Now.ToLongTimeString() + ".";
                    //}
                    //AAA窗口开启
                }

            });
            this.XFButtonCommand.DoCanExecute = new Func<object, bool>((o) =>
            {
                return !CommonSTA.IsPlayStart;
            });
            #endregion
            //
            #region 改变主页NavChangedCommand
            this.NavChangedCommand = new CommandBase();
            this.NavChangedCommand.DoExecute = new Action<object>((o) =>
            {
                Type type = Type.GetType("Mj.View." + o.ToString());
                ConstructorInfo cti = type.GetConstructor(System.Type.EmptyTypes);
                IndexModel.MainContent = (FrameworkElement)cti.Invoke(null);
                switch (o.ToString())
                {
                    case "Page1View": IndexModel.Select1 = 1; IndexModel.Select2 = 0; IndexModel.Select3 = 0; break;
                    case "Page2View": IndexModel.Select1 = 0; IndexModel.Select2 = 1; IndexModel.Select3 = 0; break;
                    case "Page3View": IndexModel.Select1 = 0; IndexModel.Select2 = 0; IndexModel.Select3 = 1; break;
                    default: IndexModel.Select1 = 1; IndexModel.Select2 = 0; IndexModel.Select3 = 0; break;
                }
                System.Threading.Tasks.Task.Run(() =>
                {
                    System.Threading.Thread.Sleep(301);//延时
                    View.IndexView.mainThreadSynContext.Post(new SendOrPostCallback(s => {
                        IndexModel.MainContent2 = (FrameworkElement)cti.Invoke(null);
                    }), null);//主体

                });
            });
            this.NavChangedCommand.DoCanExecute = new Func<object, bool>((o) =>
            {
                return true;
            });
            #endregion
            //
            #region 设置动画改变SetGridChangeCommand
            this.SetGridChangeCommand = new CommandBase();
            this.SetGridChangeCommand.DoExecute = new Action<object>((o) =>
            {
                IndexModel.SetGrid = 0;
            });
            this.SetGridChangeCommand.DoCanExecute = new Func<object, bool>((o) =>
            {
                return true;
            });
            #endregion
            //
            #region 开始按钮PlayButtonCommand
            this.PlayButtonCommand = new CommandBase();
            this.PlayButtonCommand.DoExecute = new Action<object>((o) =>
            {
                if (ini.IniReadValue("setgame", "游戏名") == "")
                {
                    IndexModel.MainContent = (FrameworkElement)Type.GetType("Mj.View.Page4View").GetConstructor(System.Type.EmptyTypes).Invoke(null);
                    IndexModel.Select1 = 1; IndexModel.Select2 = 0; IndexModel.Select3 = 0;
                    MessageBox.Show("请修改您的游戏昵称");
                    return;
                }
                if (!System.IO.File.Exists(CommonSTA.LujingISO + "UJS00329.iso"))
                {
                    IndexModel.MainContent = (FrameworkElement)Type.GetType("Mj.View.Page4View").GetConstructor(System.Type.EmptyTypes).Invoke(null);
                    IndexModel.Select1 = 1; IndexModel.Select2 = 0; IndexModel.Select3 = 0;
                    MessageBox.Show("未找到游戏文件目录，请重新初始化\n\r\n\r或请前往设置中指定您自己[UJS00329.iso]的位置");
                    return;
                }
                if (IndexModel.PlayContent == "等待选服/取消")
                {
                    CommonSTA.Stop = true;
                }
                else
                {
                    IndexModel.PlayContent = "启动中";
                    CommonSTA.IsPlayStart = true;
                    CommonSTA.Stop = false;
                    IndexModel.ICO = "../Assets/ico.ico";
                    #region 隐藏启动器
                    if (ini.IniReadValue("window", "禁用托盘") != "1")
                    {
                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            if ((o as Window) != null)
                            {
                                (o as Window).Hide();
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
                            IndexModel.MainContent = (FrameworkElement)Type.GetType("Mj.View.Page5View").GetConstructor(System.Type.EmptyTypes).Invoke(null);
                            IndexModel.Select1 = 1; IndexModel.Select2 = 0; IndexModel.Select3 = 0;
                            MessageBox.Show("模拟器损坏，请重新初始化\n\r\n\r或请前往设置中指定您自己[PPSSPP]的位置");
                            CommonSTA.IsPlayStart = false;
                            IndexModel.ICO = "../Assets/logo128.ico";
                            IndexModel.PlayContent = "立即开始";
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
                            psp.IniWriteValue("General", "CheckForNewVersion ", " False");
                            psp.IniWriteValue("General", "ForceLagSync2 ", " True");
                            psp.IniWriteValue("Graphics", "AnisotropyLevel ", " 0");
                            psp.IniWriteValue("Graphics", "ReplaceTextures ", " True");
                            psp.IniWriteValue("Graphics", "TextureBackoffCache ", " True");
                            psp.IniWriteValue("Graphics", "FullScreen ", " True");
                            psp.IniWriteValue("Graphics", "SplineBezierQuality ", " 0");
                            psp.IniWriteValue("Graphics", "DisableSlowFramebufEffects ", " True");
                            psp.IniWriteValue("Network", "ForcedFirstConnect ", " True");
                            psp.IniWriteValue("Network", "EnableNetworkChat ", " True");
                            psp.IniWriteValue("Network", "ChatButtonPosition ", " 3");
                            psp.IniWriteValue("Network", "ChatScreenPosition ", " 3");
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
                            if (ini.IniReadValue("setgame", "联机") != "0")
                            {
                                #region 悬浮窗总执行
                                if (IndexModel.IPtupian == "../Assets/ping-close.png")//AAA窗口开启的话
                                {
                                    View.IndexView.mainThreadSynContext.Post(new SendOrPostCallback(s =>
                                    IndexModel.XfcView.Hide()), null);
                                    CommonSTA.Noping3 = 1;
                                    CommonSTA.XDS = 0;
                                    //AAA窗口关闭
                                }
                                else
                                {
                                    View.IndexView.mainThreadSynContext.Post(new SendOrPostCallback(s =>
                                    IndexModel.XfcView.Owner = o as Window), null);
                                    try
                                    {
                                        View.IndexView.mainThreadSynContext.Post(new SendOrPostCallback(s =>
                                        IndexModel.XfcView.Show()), null);
                                        CommonSTA.XDS = 1;
                                        CommonSTA.Noping3 = 0;
                                    }
                                    catch (Exception)
                                    {
                                        MessageBox.Show("要复活小电视嘛 = = ");
                                        View.IndexView.mainThreadSynContext.Post(new SendOrPostCallback(s =>
                                        IndexModel.XfcView = new View.XfcView()), null);
                                        View.IndexView.mainThreadSynContext.Post(new SendOrPostCallback(s =>
                                        IndexModel.XfcView.Show()), null);
                                        CommonSTA.XDS = 1;
                                        CommonSTA.Noping3 = 0;
                                    }
                                }
                                #endregion
                                #region 有网模式
                                try
                                {
                                    System.Diagnostics.Process.Start(CommonSTA.LujingFCN);//FCN
                                }
                                catch (Exception)
                                {
                                    View.IndexView.mainThreadSynContext.Post(new SendOrPostCallback(s =>
                                    IndexModel.MainContent = (FrameworkElement)Type.GetType("Mj.View.Page5View").GetConstructor(System.Type.EmptyTypes).Invoke(null)), null);
                                    IndexModel.Select1 = 1; IndexModel.Select2 = 0; IndexModel.Select3 = 0;
                                    MessageBox.Show("联机模块损坏，请重新初始化\n\r\n\r或请前往设置中指定您自己[联机模块]的位置");
                                    IndexModel.ICO = "../Assets/logo128.ico";
                                    IndexModel.PlayContent = "立即开始";
                                    CommonSTA.IsPlayStart = false;
                                    return;
                                }
                                CommonSTA.Danji = false;
                                CommonSTA.IsOK = false;
                                if (ini.IniReadValue("setgame", "不提示") != "1")
                                {
                                    #region 显示弹窗
                                    try
                                    {
                                        View.IndexView.mainThreadSynContext.Post(new SendOrPostCallback(s =>
                                        IndexModel.Tishi.Show()), null);
                                    }
                                    catch (Exception)
                                    {
                                        View.IndexView.mainThreadSynContext.Post(new SendOrPostCallback(s =>
                                        IndexModel.Tishi = new View.UMessageBox()), null);
                                        View.IndexView.mainThreadSynContext.Post(new SendOrPostCallback(s =>
                                        IndexModel.Tishi.Show()), null);
                                    }
                                    #endregion
                                    #region 倒计时
                                    System.Threading.Tasks.Task.Run(() =>
                                        {
                                            for (int i = 5; i > 0; i--)
                                            {
                                                View.IndexView.mainThreadSynContext.Post(new SendOrPostCallback(s =>
                                                IndexModel.Tishi.ZDL = "知道了(" + i + ")"), null);
                                                System.Threading.Thread.Sleep(1000);
                                            }
                                            #region 自动关闭弹窗
                                            try
                                            {
                                                View.IndexView.mainThreadSynContext.Post(new SendOrPostCallback(s =>
                                                IndexModel.Tishi.Hide()), null);
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
                                    IndexModel.PlayContent = "等待选服/取消";
                                    while (true)
                                    {
                                        if (CommonSTA.Stop == true)
                                        {
                                            CommonSTA.Noping2 = 1;
                                            break;
                                        }
                                        if (CommonSTA.Danji == true)
                                        {
                                            #region 悬浮窗关闭
                                            View.IndexView.mainThreadSynContext.Post(new SendOrPostCallback(s =>
                                                IndexModel.XfcView.Hide()), null);
                                            CommonSTA.Noping3 = 1;
                                            CommonSTA.XDS = 0;
                                            #endregion
                                            #region 关闭网络
                                            psp.IniWriteValue("Network", "EnableWlan", "False");
                                            #endregion
                                            #region 保险
                                            psp.IniWriteValue("SystemParam", "PSPModel", "1");
                                            if (ini.IniReadValue("setgame", "游戏名") != "")
                                            {
                                                psp.IniWriteValue("SystemParam", "NickName", CommonSTA.PSPName);
                                            }
                                            if (ini.IniReadValue("setgame", "金手指") != "1")
                                            {
                                                psp.IniWriteValue("General", "EnableCheats", "False");
                                                if (System.IO.File.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\Cheats\ULJS00329.ini"))
                                                {
                                                    System.IO.File.Delete(Common.CommonSTA.LujingPSP + @"memstick\PSP\Cheats\ULJS00329.ini");
                                                }
                                            }
                                            else
                                            {
                                                psp.IniWriteValue("General", "EnableCheats", "True");
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
                                            psp.IniWriteValue("Graphics", "FullScreen ", " True");
                                            psp.IniWriteValue("CPU", "IOTimingMethod ", " 0");
                                            psp.IniWriteValue("Network", "PortOffset ", " 5000");
                                            psp.IniWriteValue("Network", "proAdhocServer ", " " + Common.CommonSTA._ip);
                                            int[] mac = new int[] { new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16) };
                                            psp.IniWriteValue("SystemParam", "MacAddress ", " " + mac[0] + mac[1] + ":" + mac[2] + mac[3] + ":" + mac[4] + mac[5] + ":" + mac[6] + mac[7] + ":" + mac[8] + mac[9] + ":" + mac[10] + mac[11]);
                                            #endregion
                                            #region 启动游戏
                                            try
                                            {
                                                System.Diagnostics.Process.Start(CommonSTA.LujingPSP + @"PPSSPPWindows64.exe", CommonSTA.LujingISO + @"UJS00329.iso");
                                            }
                                            catch (Exception)
                                            {
                                                View.IndexView.mainThreadSynContext.Post(new SendOrPostCallback(s =>
                                                IndexModel.MainContent = (FrameworkElement)Type.GetType("Mj.View.Page5View").GetConstructor(System.Type.EmptyTypes).Invoke(null)), null);
                                                IndexModel.Select1 = 1; IndexModel.Select2 = 0; IndexModel.Select3 = 0;
                                                MessageBox.Show("模拟器损坏，请重新初始化\n\r\n\r或请前往设置中指定您自己[PPSSPP]的位置");
                                            }
                                            #endregion
                                            break;
                                        }
                                        try
                                        {
                                            System.Net.NetworkInformation.PingReply pingReply = CommonSTA.ping2.Send(CommonSTA._ip);
                                            if (pingReply.Status == System.Net.NetworkInformation.IPStatus.Success)
                                            {
                                                CommonSTA.PING = true;
                                            }
                                        }
                                        catch (Exception)
                                        {
                                            MessageBox.Show("网络异常"); break;
                                        }

                                        if (CommonSTA.PING == true && CommonSTA.IsOK == true)
                                        {
                                            #region 启用网络
                                            psp.IniWriteValue("Network", "EnableWlan", "True");
                                            #endregion
                                            #region 保险
                                            psp.IniWriteValue("SystemParam", "PSPModel", "1");
                                            if (ini.IniReadValue("setgame", "游戏名") != "")
                                            {
                                                psp.IniWriteValue("SystemParam", "NickName", CommonSTA.PSPName);
                                            }
                                            if (ini.IniReadValue("setgame", "金手指") != "1")
                                            {
                                                psp.IniWriteValue("General", "EnableCheats", "False");
                                                if (System.IO.File.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\Cheats\ULJS00329.ini"))
                                                {
                                                    System.IO.File.Delete(Common.CommonSTA.LujingPSP + @"memstick\PSP\Cheats\ULJS00329.ini");
                                                }
                                            }
                                            else
                                            {
                                                psp.IniWriteValue("General", "EnableCheats", "True");
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
                                            psp.IniWriteValue("Graphics", "FullScreen ", " True");
                                            psp.IniWriteValue("CPU", "IOTimingMethod ", " 0");
                                            psp.IniWriteValue("Network", "PortOffset ", " 5000");
                                            psp.IniWriteValue("Network", "proAdhocServer ", " " + Common.CommonSTA._ip);
                                            int[] mac = new int[] { new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16) };
                                            psp.IniWriteValue("SystemParam", "MacAddress ", " " + mac[0] + mac[1] + ":" + mac[2] + mac[3] + ":" + mac[4] + mac[5] + ":" + mac[6] + mac[7] + ":" + mac[8] + mac[9] + ":" + mac[10] + mac[11]);
                                            #endregion
                                            #region 启动游戏
                                            try
                                            {
                                                System.Diagnostics.Process.Start(CommonSTA.LujingPSP + @"PPSSPPWindows64.exe", CommonSTA.LujingISO + @"UJS00329.iso");
                                            }
                                            catch (Exception)
                                            {
                                                View.IndexView.mainThreadSynContext.Post(new SendOrPostCallback(s =>
                                                IndexModel.MainContent = (FrameworkElement)Type.GetType("Mj.View.Page5View").GetConstructor(System.Type.EmptyTypes).Invoke(null)), null);
                                                IndexModel.Select1 = 1; IndexModel.Select2 = 0; IndexModel.Select3 = 0;
                                                MessageBox.Show("模拟器损坏，请重新初始化\n\r\n\r或请前往设置中指定您自己[PPSSPP]的位置");
                                            }
                                            #endregion
                                            break;
                                        }
                                        System.Threading.Thread.Sleep(1000);
                                    }
                                }).ContinueWith<int>((t) =>
                                {
                                    IndexModel.PlayContent = "立即开始";
                                    CommonSTA.PING = false;
                                    CommonSTA.IsPlayStart = false;
                                    IndexModel.ICO = "../Assets/logo128.ico";
                                    return 1;
                                });
                                #endregion
                            }
                            else
                            {
                                #region 无网模式
                                #region 悬浮窗关闭
                                View.IndexView.mainThreadSynContext.Post(new SendOrPostCallback(s =>
                                    IndexModel.XfcView.Hide()), null);
                                CommonSTA.Noping3 = 1;
                                CommonSTA.XDS = 0;
                                #endregion
                                #region 关闭网络
                                psp.IniWriteValue("Network", "EnableWlan", "False");
                                #endregion
                                #region 保险
                                psp.IniWriteValue("SystemParam", "PSPModel", "1");
                                if (ini.IniReadValue("setgame", "游戏名") != "")
                                {
                                    psp.IniWriteValue("SystemParam", "NickName", CommonSTA.PSPName);
                                }
                                if (ini.IniReadValue("setgame", "金手指") != "1")
                                {
                                    psp.IniWriteValue("General", "EnableCheats", "False");
                                    if (System.IO.File.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\Cheats\ULJS00329.ini"))
                                    {
                                        System.IO.File.Delete(Common.CommonSTA.LujingPSP + @"memstick\PSP\Cheats\ULJS00329.ini");
                                    }
                                }
                                else
                                {
                                    psp.IniWriteValue("General", "EnableCheats", "True");
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
                                psp.IniWriteValue("Graphics", "FullScreen ", " True");
                                psp.IniWriteValue("CPU", "IOTimingMethod ", " 0");
                                psp.IniWriteValue("Network", "PortOffset ", " 5000");
                                psp.IniWriteValue("Network", "proAdhocServer ", " " + Common.CommonSTA._ip);
                                int[] mac = new int[] { new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16) };
                                psp.IniWriteValue("SystemParam", "MacAddress ", " " + mac[0] + mac[1] + ":" + mac[2] + mac[3] + ":" + mac[4] + mac[5] + ":" + mac[6] + mac[7] + ":" + mac[8] + mac[9] + ":" + mac[10] + mac[11]);
                                #endregion
                                #region 启动游戏
                                try
                                {
                                    System.Diagnostics.Process.Start(CommonSTA.LujingPSP + @"PPSSPPWindows64.exe", CommonSTA.LujingISO + @"UJS00329.iso");
                                }
                                catch (Exception)
                                {
                                    View.IndexView.mainThreadSynContext.Post(new SendOrPostCallback(s =>
                                    IndexModel.MainContent = (FrameworkElement)Type.GetType("Mj.View.Page5View").GetConstructor(System.Type.EmptyTypes).Invoke(null)), null);
                                    IndexModel.Select1 = 1; IndexModel.Select2 = 0; IndexModel.Select3 = 0;
                                    MessageBox.Show("模拟器损坏，请重新初始化\n\r\n\r或请前往设置中指定您自己[PPSSPP]的位置");
                                }
                                #endregion
                                #endregion
                                CommonSTA.IsPlayStart = false;
                                IndexModel.ICO = "../Assets/logo128.ico";
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
                        if (ini.IniReadValue("setgame", "联机") != "0")
                        {
                            #region 悬浮窗总执行
                            if (IndexModel.IPtupian == "../Assets/ping-close.png")//AAA窗口开启的话
                            {
                                IndexModel.XfcView.Hide();
                                CommonSTA.Noping3 = 1;
                                CommonSTA.XDS = 0;
                                //AAA窗口关闭
                            }
                            else
                            {
                                IndexModel.XfcView.Owner = o as Window;
                                try
                                {
                                    IndexModel.XfcView.Show();
                                    CommonSTA.XDS = 1;
                                    CommonSTA.Noping3 = 0;
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show("要复活小电视嘛 = = ");
                                    IndexModel.XfcView = new View.XfcView();
                                    IndexModel.XfcView.Show();
                                    CommonSTA.XDS = 1;
                                    CommonSTA.Noping3 = 0;
                                }
                            }
                            #endregion
                            #region 有网模式
                            try
                            {
                                System.Diagnostics.Process.Start(CommonSTA.LujingFCN);//FCN
                            }
                            catch (Exception)
                            {
                                IndexModel.MainContent = (FrameworkElement)Type.GetType("Mj.View.Page5View").GetConstructor(System.Type.EmptyTypes).Invoke(null);
                                IndexModel.Select1 = 1; IndexModel.Select2 = 0; IndexModel.Select3 = 0;
                                MessageBox.Show("联机模块损坏，请重新初始化\n\r\n\r或请前往设置中指定您自己[联机模块]的位置");
                                IndexModel.ICO = "../Assets/logo128.ico";
                                IndexModel.PlayContent = "立即开始";
                                CommonSTA.IsPlayStart = false;
                                return;
                            }
                            CommonSTA.Danji = false;
                            CommonSTA.IsOK = false;
                            if (ini.IniReadValue("setgame", "不提示") != "1")
                            {
                                #region 显示弹窗
                                try
                                {
                                    IndexModel.Tishi.Show();
                                }
                                catch (Exception)
                                {
                                    IndexModel.Tishi = new View.UMessageBox();
                                    IndexModel.Tishi.Show();
                                }
                                #endregion
                                #region 倒计时
                                System.Threading.Tasks.Task.Run(() =>
                                {
                                    for (int i = 5; i > 0; i--)
                                    {
                                        View.IndexView.mainThreadSynContext.Post(new SendOrPostCallback(s =>
                                        IndexModel.Tishi.ZDL = "知道了(" + i + ")"), null);
                                        System.Threading.Thread.Sleep(1000);
                                    }
                                    #region 自动关闭弹窗
                                    try
                                    {
                                        View.IndexView.mainThreadSynContext.Post(new SendOrPostCallback(s =>
                                        IndexModel.Tishi.Hide()), null);
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
                                IndexModel.PlayContent = "等待选服/取消";
                                while (true)
                                {
                                    if (CommonSTA.Stop == true)
                                    {
                                        CommonSTA.Noping2 = 1;
                                        break;
                                    }
                                    if (CommonSTA.Danji == true)
                                    {
                                        #region 悬浮窗关闭
                                        View.IndexView.mainThreadSynContext.Post(new SendOrPostCallback(s =>
                                            IndexModel.XfcView.Hide()), null);
                                        CommonSTA.Noping3 = 1;
                                        CommonSTA.XDS = 0;
                                        #endregion
                                        #region 关闭网络
                                        psp.IniWriteValue("Network", "EnableWlan", "False");
                                        #endregion
                                        #region 保险
                                        psp.IniWriteValue("SystemParam", "PSPModel", "1");
                                        if (ini.IniReadValue("setgame", "游戏名") != "")
                                        {
                                            psp.IniWriteValue("SystemParam", "NickName", CommonSTA.PSPName);
                                        }
                                        if (ini.IniReadValue("setgame", "金手指") != "1")
                                        {
                                            psp.IniWriteValue("General", "EnableCheats", "False");
                                            if (System.IO.File.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\Cheats\ULJS00329.ini"))
                                            {
                                                System.IO.File.Delete(Common.CommonSTA.LujingPSP + @"memstick\PSP\Cheats\ULJS00329.ini");
                                            }
                                        }
                                        else
                                        {
                                            psp.IniWriteValue("General", "EnableCheats", "True");
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
                                        psp.IniWriteValue("Graphics", "FullScreen ", " True");
                                        psp.IniWriteValue("CPU", "IOTimingMethod ", " 0");
                                        psp.IniWriteValue("Network", "PortOffset ", " 5000");
                                        psp.IniWriteValue("Network", "proAdhocServer ", " " + Common.CommonSTA._ip);
                                        int[] mac = new int[] { new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16) };
                                        psp.IniWriteValue("SystemParam", "MacAddress ", " " + mac[0] + mac[1] + ":" + mac[2] + mac[3] + ":" + mac[4] + mac[5] + ":" + mac[6] + mac[7] + ":" + mac[8] + mac[9] + ":" + mac[10] + mac[11]);
                                        #endregion
                                        #region 启动游戏
                                        try
                                        {
                                            System.Diagnostics.Process.Start(CommonSTA.LujingPSP + @"PPSSPPWindows64.exe", CommonSTA.LujingISO + @"UJS00329.iso");
                                        }
                                        catch (Exception)
                                        {
                                            IndexModel.MainContent = (FrameworkElement)Type.GetType("Mj.View.Page5View").GetConstructor(System.Type.EmptyTypes).Invoke(null);
                                            IndexModel.Select1 = 1; IndexModel.Select2 = 0; IndexModel.Select3 = 0;
                                            MessageBox.Show("模拟器损坏，请重新初始化\n\r\n\r或请前往设置中指定您自己[PPSSPP]的位置");
                                        }
                                        #endregion
                                        break;
                                    }

                                    try
                                    {
                                        System.Net.NetworkInformation.PingReply pingReply = CommonSTA.ping2.Send(CommonSTA._ip);
                                        if (pingReply.Status == System.Net.NetworkInformation.IPStatus.Success)
                                        {
                                            CommonSTA.PING = true;
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        MessageBox.Show("网络异常"); break;
                                    }

                                    if (CommonSTA.PING == true && CommonSTA.IsOK == true)
                                    {
                                        #region 启用网络
                                        psp.IniWriteValue("Network", "EnableWlan", "True");
                                        #endregion
                                        #region 保险
                                        psp.IniWriteValue("SystemParam", "PSPModel", "1");
                                        if (ini.IniReadValue("setgame", "游戏名") != "")
                                        {
                                            psp.IniWriteValue("SystemParam", "NickName", CommonSTA.PSPName);
                                        }
                                        if (ini.IniReadValue("setgame", "金手指") != "1")
                                        {
                                            psp.IniWriteValue("General", "EnableCheats", "False");
                                            if (System.IO.File.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\Cheats\ULJS00329.ini"))
                                            {
                                                System.IO.File.Delete(Common.CommonSTA.LujingPSP + @"memstick\PSP\Cheats\ULJS00329.ini");
                                            }
                                        }
                                        else
                                        {
                                            psp.IniWriteValue("General", "EnableCheats", "True");
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
                                        psp.IniWriteValue("Graphics", "FullScreen ", " True");
                                        psp.IniWriteValue("CPU", "IOTimingMethod ", " 0");
                                        psp.IniWriteValue("Network", "PortOffset ", " 5000");
                                        psp.IniWriteValue("Network", "proAdhocServer ", " " + Common.CommonSTA._ip);
                                        int[] mac = new int[] { new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16) };
                                        psp.IniWriteValue("SystemParam", "MacAddress ", " " + mac[0] + mac[1] + ":" + mac[2] + mac[3] + ":" + mac[4] + mac[5] + ":" + mac[6] + mac[7] + ":" + mac[8] + mac[9] + ":" + mac[10] + mac[11]);
                                        #endregion
                                        #region 启动游戏
                                        try
                                        {
                                            System.Diagnostics.Process.Start(CommonSTA.LujingPSP + @"PPSSPPWindows64.exe", CommonSTA.LujingISO + @"UJS00329.iso");
                                        }
                                        catch (Exception)
                                        {
                                            IndexModel.MainContent = (FrameworkElement)Type.GetType("Mj.View.Page5View").GetConstructor(System.Type.EmptyTypes).Invoke(null);
                                            IndexModel.Select1 = 1; IndexModel.Select2 = 0; IndexModel.Select3 = 0;
                                            MessageBox.Show("模拟器损坏，请重新初始化\n\r\n\r或请前往设置中指定您自己[PPSSPP]的位置");
                                        }
                                        #endregion
                                        break;
                                    }
                                    System.Threading.Thread.Sleep(1000);
                                }
                            }).ContinueWith<int>((t) =>
                            {
                                IndexModel.PlayContent = "立即开始";
                                CommonSTA.PING = false;
                                CommonSTA.IsPlayStart = false;
                                IndexModel.ICO = "../Assets/logo128.ico";
                                return 1;
                            });
                            #endregion
                        }
                        else
                        {
                            #region 无网模式
                            #region 悬浮窗关闭
                            IndexModel.XfcView.Hide();
                            CommonSTA.Noping3 = 1;
                            CommonSTA.XDS = 0;
                            #endregion
                            #region 关闭网络
                            psp.IniWriteValue("Network", "EnableWlan", "False");
                            #endregion
                            #region 保险
                            psp.IniWriteValue("SystemParam", "PSPModel", "1");
                            if (ini.IniReadValue("setgame", "游戏名") != "")
                            {
                                psp.IniWriteValue("SystemParam", "NickName", CommonSTA.PSPName);
                            }
                            if (ini.IniReadValue("setgame", "金手指") != "1")
                            {
                                psp.IniWriteValue("General", "EnableCheats", "False");
                                if (System.IO.File.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\Cheats\ULJS00329.ini"))
                                {
                                    System.IO.File.Delete(Common.CommonSTA.LujingPSP + @"memstick\PSP\Cheats\ULJS00329.ini");
                                }
                            }
                            else
                            {
                                psp.IniWriteValue("General", "EnableCheats", "True");
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
                            psp.IniWriteValue("Graphics", "FullScreen ", " True");
                            psp.IniWriteValue("CPU", "IOTimingMethod ", " 0");
                            psp.IniWriteValue("Network", "PortOffset ", " 5000");
                            psp.IniWriteValue("Network", "proAdhocServer ", " " + Common.CommonSTA._ip);
                            int[] mac = new int[] { new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16), new Random().Next(0, 16) };
                            psp.IniWriteValue("SystemParam", "MacAddress ", " " + mac[0] + mac[1] + ":" + mac[2] + mac[3] + ":" + mac[4] + mac[5] + ":" + mac[6] + mac[7] + ":" + mac[8] + mac[9] + ":" + mac[10] + mac[11]);
                            #endregion
                            #region 启动游戏
                            try
                            {
                                System.Diagnostics.Process.Start(CommonSTA.LujingPSP + @"PPSSPPWindows64.exe", CommonSTA.LujingISO + @"UJS00329.iso");
                            }
                            catch (Exception)
                            {
                                IndexModel.MainContent = (FrameworkElement)Type.GetType("Mj.View.Page5View").GetConstructor(System.Type.EmptyTypes).Invoke(null);
                                IndexModel.Select1 = 1; IndexModel.Select2 = 0; IndexModel.Select3 = 0;
                                MessageBox.Show("模拟器损坏，请重新初始化\n\r\n\r或请前往设置中指定您自己[PPSSPP]的位置");
                            }
                            #endregion
                            #endregion
                            CommonSTA.IsPlayStart = false;
                            IndexModel.ICO = "../Assets/logo128.ico";
                            IndexModel.PlayContent = "立即开始";
                        }
                        #endregion
                    }
                }
            });
            this.PlayButtonCommand.DoCanExecute = new Func<object, bool>((o) =>
            {
                if (IndexModel.PlayContent == "等待选服/取消")
                {
                    return true;
                }
                else
                {
                    return !CommonSTA.IsPlayStart;
                }

            });
            #endregion
            //
            #region 主目录ZhumuluCommand
            this.ZhumuluCommand = new CommandBase();
            this.ZhumuluCommand.DoExecute = new Action<object>((o) =>
            {
                if (System.IO.Directory.Exists(CommonSTA.LujingISO))
                {
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
                        p.StandardInput.WriteLine("start " + CommonSTA.LujingISO + "&exit");//ISO位置
                        #region CMD结束
                        p.StandardInput.AutoFlush = true;
                        p.WaitForExit();//等待程序执行完退出进程
                        p.Close();
                        #endregion
                        #endregion
                    }
                    catch (Exception)
                    {
                        IndexModel.MainContent = (FrameworkElement)Type.GetType("Mj.View.Page5View").GetConstructor(System.Type.EmptyTypes).Invoke(null);
                        IndexModel.Select1 = 1; IndexModel.Select2 = 0; IndexModel.Select3 = 0;
                        MessageBox.Show("未找到游戏文件目录，请重新初始化\n\r\n\r或请前往设置中指定您自己[游戏镜像目录]的位置");
                    }
                }
                else
                {
                    IndexModel.MainContent = (FrameworkElement)Type.GetType("Mj.View.Page5View").GetConstructor(System.Type.EmptyTypes).Invoke(null);
                    IndexModel.Select1 = 1; IndexModel.Select2 = 0; IndexModel.Select3 = 0;
                    MessageBox.Show("未找到游戏文件目录，请重新初始化\n\r\n\r或请前往设置中指定您自己[游戏镜像目录]的位置");
                }
            });
            this.ZhumuluCommand.DoCanExecute = new Func<object, bool>((o) =>
            {
                return true;
            });
            #endregion
            //
            #region 材质目录CaizhimuluCommand
            this.CaizhimuluCommand = new CommandBase();
            this.CaizhimuluCommand.DoExecute = new Action<object>((o) =>
            {
                if (System.IO.Directory.Exists(CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\ULJS00329\"))
                {
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
                        p.StandardInput.WriteLine("start " + CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\ULJS00329\" + "&exit");//ISO位置
                        #region CMD结束
                        p.StandardInput.AutoFlush = true;
                        p.WaitForExit();//等待程序执行完退出进程
                        p.Close();
                        #endregion
                        #endregion
                    }
                    catch (Exception)
                    {
                        IndexModel.MainContent = (FrameworkElement)Type.GetType("Mj.View.Page5View").GetConstructor(System.Type.EmptyTypes).Invoke(null);
                        IndexModel.Select1 = 1; IndexModel.Select2 = 0; IndexModel.Select3 = 0;
                        MessageBox.Show("未找到游戏材质目录，请重新初始化\n\r\n\r或请前往设置中指定您自己[PPSSPP]的位置");
                    }
                }
                else if (System.IO.Directory.Exists(CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\"))
                {
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
                        p.StandardInput.WriteLine("start " + CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\" + "&exit");//ISO位置
                        #region CMD结束
                        p.StandardInput.AutoFlush = true;
                        p.WaitForExit();//等待程序执行完退出进程
                        p.Close();
                        #endregion
                        #endregion
                    }
                    catch (Exception)
                    {
                        IndexModel.MainContent = (FrameworkElement)Type.GetType("Mj.View.Page5View").GetConstructor(System.Type.EmptyTypes).Invoke(null);
                        IndexModel.Select1 = 1; IndexModel.Select2 = 0; IndexModel.Select3 = 0;
                        MessageBox.Show("未找到游戏材质目录，请重新初始化\n\r\n\r或请前往设置中指定您自己[PPSSPP]的位置");
                    }
                }
                else
                {
                    IndexModel.MainContent = (FrameworkElement)Type.GetType("Mj.View.Page5View").GetConstructor(System.Type.EmptyTypes).Invoke(null);
                    IndexModel.Select1 = 1; IndexModel.Select2 = 0; IndexModel.Select3 = 0;
                    MessageBox.Show("未找到游戏材质目录，请重新初始化\n\r\n\r或请前往设置中指定您自己[PPSSPP]的位置");
                }
            });
            this.CaizhimuluCommand.DoCanExecute = new Func<object, bool>((o) =>
            {
                return true;
            });
            #endregion
            //
            #region 快捷方式KuaijiefangshiCommand
            this.KuaijiefangshiCommand = new CommandBase();
            this.KuaijiefangshiCommand.DoExecute = new Action<object>((o) =>
            {
                #region 创建桌面快捷方式
                if (System.IO.File.Exists(string.Format(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\魔法禁书目录.lnk")))
                {
                    //  System.IO.File.Delete(string.Format(@"{0}\魔法禁书目录.lnk",deskTop));//删除原来的桌面快捷键方式
                    MessageBox.Show("您已经查收了快递，记得给个好评哦("); ;
                }
                else
                {
                    try
                    {
                        IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShell();
                        IWshRuntimeLibrary.IWshShortcut shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\" + "魔法禁书目录.lnk");
                        shortcut.TargetPath = Environment.CurrentDirectory + @"\" + System.Diagnostics.Process.GetCurrentProcess().ProcessName + @".exe";         //目标文件
                        shortcut.WorkingDirectory = Environment.CurrentDirectory;    //目标文件夹
                                                                                     //shortcut.WindowStyle = 1;               //目标应用程序的窗口状态分为普通、最大化、最小化【1,3,7】
                        shortcut.Description = "魔禁多人联动委员会";   //描述
                                                              //shortcut.IconLocation = "../Assets/logo128.ico";  //快捷方式图标
                        shortcut.Arguments = "";
                        //shortcut.Hotkey = "SHIFT+DELETE";              // 快捷键
                        shortcut.Save();
                        MessageBox.Show("您的快递已发送到桌面，请查收");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("您的快递在路上起火了，很遗憾");
                    }
                }
                #endregion
            });
            this.KuaijiefangshiCommand.DoCanExecute = new Func<object, bool>((o) =>
            {
                return true;
            });
            #endregion
            //
            #region 下载目录XiazaimuluCommand
            this.XiazaimuluCommand = new CommandBase();
            this.XiazaimuluCommand.DoExecute = new Action<object>((o) =>
            {
                if (System.IO.Directory.Exists(CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\"))
                {
                    if (System.IO.Directory.Exists(CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\Download"))
                    {
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
                            p.StandardInput.WriteLine("start " + CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\Download" + "&exit");//ISO位置
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
                    }
                    else
                    {
                        MessageBox.Show("您使用的可能是自己的PPSSPP\n\r\n\r将为您创建下载目录文件夹");
                        System.IO.Directory.CreateDirectory(CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\Download");
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
                            p.StandardInput.WriteLine("start " + CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\Download" + "&exit");//ISO位置
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
                    }

                }
                else
                {
                    IndexModel.MainContent = (FrameworkElement)Type.GetType("Mj.View.Page5View").GetConstructor(System.Type.EmptyTypes).Invoke(null);
                    IndexModel.Select1 = 1; IndexModel.Select2 = 0; IndexModel.Select3 = 0;
                    MessageBox.Show("未找到下载目录，请重新初始化\n\r\n\r或请前往设置中指定您自己[PPSSPP]的位置");
                }
            });
            this.XiazaimuluCommand.DoCanExecute = new Func<object, bool>((o) =>
            {
                return true;
            });
            #endregion
            //
            #region 蓝屏LanpingCommand
            this.LanpingCommand = new CommandBase();
            this.LanpingCommand.DoExecute = new Action<object>((o) =>
            {
                try
                {
                    if (!System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"mfjsml.top.yee"))
                    {
                        System.IO.File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + @"mfjsml.top.yee", Properties.Resources.yee);
                    }
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    System.IO.Compression.ZipFile.ExtractToDirectory(AppDomain.CurrentDomain.BaseDirectory + @"mfjsml.top.yee", Common.CommonSTA.LujingPSP, Encoding.GetEncoding("GB2312"), true);
                    System.Diagnostics.Process.Start(Common.CommonSTA.LujingPSP + @"yee.exe");
                }
                catch (Exception)
                {
                    MessageBox.Show("操作被禁止");
                }
            });
            this.LanpingCommand.DoCanExecute = new Func<object, bool>((o) =>
            {
                return true;
            });
            #endregion
            //
            #region 金手指JinshouzhiCommand
            this.JinshouzhiCommand = new CommandBase();
            this.JinshouzhiCommand.DoExecute = new Action<object>((o) =>
            {
                ini.IniWriteValue("setgame", "金手指", "1");
                MessageBoxResult j = MessageBox.Show("金手指已解锁\n是否初始化金手指文件\n(PPSSPP内开关另需开启)", "请确认", MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
                if (j == MessageBoxResult.OK)
                {
                    try
                    {
                        System.IO.File.WriteAllBytes(Common.CommonSTA.LujingPSP + @"memstick\PSP\Cheats\ULJS00329.ini", Properties.Resources.ULJS00329);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("操作被禁止");
                    }
                }
            });
            this.JinshouzhiCommand.DoCanExecute = new Func<object, bool>((o) =>
            {
                return true;
            });
            #endregion
            //
            #region 申请密码ShenqingmimaCommand
            this.ShenqingmimaCommand = new CommandBase();
            this.ShenqingmimaCommand.DoExecute = new Action<object>((o) =>
            {
                try
                {
                    #region 打开网页
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
                    p.StandardInput.WriteLine("start " + "http://8.131.60.137/Web/start.html" + "&exit");//ISO位置
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
            });
            this.ShenqingmimaCommand.DoCanExecute = new Func<object, bool>((o) =>
            {
                return true;
            });
            #endregion
            //
            #region 材质工坊CaizhigongfangCommand
            this.CaizhigongfangCommand = new CommandBase();
            this.CaizhigongfangCommand.DoExecute = new Action<object>((o) =>
            {
                if (IndexModel.WebVisibility == "Collapsed") //Collapsed  Hidden Visible
                {
                    IndexModel.WebIP = "http://8.131.60.137/";
                    IndexModel.WebVisibility = "Visible";
                    IndexModel.IndexVisibility = "Hidden";
                }
                else
                {
                    IndexModel.WebIP = "";
                    IndexModel.IndexVisibility = "Visible";
                    IndexModel.WebVisibility = "Collapsed";
                }
            });
            this.CaizhigongfangCommand.DoCanExecute = new Func<object, bool>((o) =>
            {
                return true;
            });
            #endregion
            //
            #region 更新检测GengxinjianceCommand
            this.GengxinjianceCommand = new CommandBase();
            this.GengxinjianceCommand.DoExecute = new Action<object>((o) =>
            {
                System.Threading.Tasks.Task.Run(() =>
                {
                    System.Threading.Thread.Sleep(800);//延时
                    MessageBox.Show("目前已是最新版2.0");
                }).ContinueWith<int>((t) =>
                {
                    return 1;
                });
            });
            this.GengxinjianceCommand.DoCanExecute = new Func<object, bool>((o) =>
            {
                return true;
            });
            #endregion
            //
            #region 游线按键YouxianjianCommand
            this.YouxianjianCommand = new CommandBase();
            this.YouxianjianCommand.DoExecute = new Action<object>((o) =>
            {
                if (IndexModel.JianpanVisibility == "Collapsed") //Collapsed  Hidden Visible
                {
                    
                    IndexModel.JianpanVisibility = "Visible";
                    IndexModel.IndexVisibility = "Hidden";
                }
                else
                {
                    IndexModel.IndexVisibility = "Visible";
                    IndexModel.JianpanVisibility = "Collapsed";
                }
            });
            this.YouxianjianCommand.DoCanExecute = new Func<object, bool>((o) =>
            {
                return true;
            });
            #endregion
            //


            //————————————————————————————————————————————————————————————
        }
    }
}
