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
    /// Page3View.xaml 的交互逻辑
    /// </summary>
    public partial class Page3View : UserControl
    {
        private Common.IniBase ini = new Common.IniBase(System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\IndexOfMagic\ppsspp\set");//启用 ini
        private Common.IniBase psp = new Common.IniBase(ViewModel.IndexViewModel.ini.IniReadValue("setgame", "PPSSPP路径") + @"memstick\PSP\SYSTEM\ppsspp.ini");//启用 ini
        public Page3View()
        {
            InitializeComponent();
            #region 配置文件setgame huazhi 1234
            switch (ini.IniReadValue("setgame", "huahzi"))
            {
                case "1": Di.IsChecked = true; break;
                case "2": Zhong.IsChecked = true; break;
                case "3": Gao.IsChecked = true; break;
                case "4": Feichanggao.IsChecked = true; break;
                default: break;
            }
            #endregion
        }
        #region 按钮和临时寄存
        private byte _huazhi = 0;
        private void HuazhiDi(object sender, RoutedEventArgs e)
        {
            _huazhi = 1;
        }

        private void HuazhiZhong(object sender, RoutedEventArgs e)
        {
            _huazhi = 2;
        }

        private void HuazhiGao(object sender, RoutedEventArgs e)
        {
            _huazhi = 3;
        }

        private void HuazhiFeichanggao(object sender, RoutedEventArgs e)
        {
            _huazhi = 4;
        }

        #endregion
        private void YingyongHuazhi(object sender, RoutedEventArgs e)
        {
            if (_huazhi != 0)
            {
                #region 加载图标切换
                Di.IsEnabled = false;
                Zhong.IsEnabled = false;
                Gao.IsEnabled = false;
                Feichanggao.IsEnabled = false;
                Yingyong.Visibility = Visibility.Hidden;
                Huazhidonghua.Visibility = Visibility.Visible;
                #endregion
                switch (_huazhi)
                {
                    case 1:
                        #region 低画质
                        psp.IniWriteValue("Graphics", "GraphicsBackend ", " 0");//
                        psp.IniWriteValue("Graphics", "RenderingMode ", " 0");//
                        psp.IniWriteValue("Graphics", "VertexDecCache ", " True");//
                        psp.IniWriteValue("Graphics", "MemBlockTransferGPU ", " False");//
                        psp.IniWriteValue("Graphics", "InflightFrames ", " 0");//
                        psp.IniWriteValue("Graphics", "SplineBezierQuality ", " 0");
                        psp.IniWriteValue("Graphics", "InternalResolution ", " 0");
                        psp.IniWriteValue("Graphics", "AnisotropyLevel ", " 0");
                        psp.IniWriteValue("Graphics", "TexScalingLevel ", " 1");
                        psp.IniWriteValue("Graphics", "HardwareTessellation ", " False");
                        #endregion
                        #region 记录设置配置
                        switch (_huazhi)
                        {
                            case 1: ini.IniWriteValue("setgame", "huahzi", "1"); break;
                            case 2: ini.IniWriteValue("setgame", "huahzi", "2"); break;
                            case 3: ini.IniWriteValue("setgame", "huahzi", "3"); break;
                            case 4: ini.IniWriteValue("setgame", "huahzi", "4"); break;
                            default: break;
                        }
                        #endregion
                        #region 加载切换回来
                        Huazhidonghua.Visibility = Visibility.Hidden;
                        Di.IsEnabled = true;
                        Zhong.IsEnabled = true;
                        Gao.IsEnabled = true;
                        Feichanggao.IsEnabled = true;
                        Yingyong.Visibility = Visibility.Visible;
                        #endregion
                        MessageBox.Show("低画质设置完毕");
                        break;
                    case 2:
                        if (System.IO.File.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\SYSTEM\ppsspp.ini"))
                        {
                            System.IO.File.Delete(Common.CommonSTA.LujingPSP + @"memstick\PSP\SYSTEM\ppsspp.ini");
                        }
                        System.Diagnostics.Process.Start(Common.CommonSTA.LujingPSP + @"PPSSPPWindows64.exe");
                        System.Threading.Tasks.Task.Run(() =>
                        {
                            System.Threading.Thread.Sleep(1500);
                            System.Diagnostics.Process[] pProcess;
                            pProcess = System.Diagnostics.Process.GetProcesses();
                            for (int i = 1; i <= pProcess.Length - 1; i++)
                            {
                                if (pProcess[i].ProcessName == "PPSSPPWindows64")
                                {
                                    pProcess[i].Kill();
                                    break;
                                }
                            }
                            System.Threading.Thread.Sleep(500);
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
                            
                       
                        }).ContinueWith<int>((t) =>
                        {
                            #region 记录设置配置
                            switch (_huazhi)
                            {
                                case 1: ini.IniWriteValue("setgame", "huahzi", "1"); break;
                                case 2: ini.IniWriteValue("setgame", "huahzi", "2"); break;
                                case 3: ini.IniWriteValue("setgame", "huahzi", "3"); break;
                                case 4: ini.IniWriteValue("setgame", "huahzi", "4"); break;
                                default: break;
                            }
                            #endregion
                            #region 加载切换回来
                            View.IndexView.mainThreadSynContext.Post(new SendOrPostCallback(s =>
                            Huazhidonghua.Visibility = Visibility.Hidden), null);
                            View.IndexView.mainThreadSynContext.Post(new SendOrPostCallback(s =>
                            Di.IsEnabled = true), null);
                            View.IndexView.mainThreadSynContext.Post(new SendOrPostCallback(s =>
                            Zhong.IsEnabled = true), null);
                            View.IndexView.mainThreadSynContext.Post(new SendOrPostCallback(s =>
                            Gao.IsEnabled = true), null);
                            View.IndexView.mainThreadSynContext.Post(new SendOrPostCallback(s =>
                            Feichanggao.IsEnabled = true), null);
                            View.IndexView.mainThreadSynContext.Post(new SendOrPostCallback(s =>
                            Yingyong.Visibility = Visibility.Visible), null);
                            #endregion
                            MessageBox.Show("您的设置已重新生成\n根据您的硬件采用最优方案");
                            return 1;
                        });
                        break;
                    case 3:
                        #region 高画质
                        psp.IniWriteValue("Graphics", "GraphicsBackend ", " 0");//
                        psp.IniWriteValue("Graphics", "RenderingMode ", " 0");//
                        psp.IniWriteValue("Graphics", "VertexDecCache ", " True");//
                        psp.IniWriteValue("Graphics", "MemBlockTransferGPU ", " False");//
                        psp.IniWriteValue("Graphics", "InflightFrames ", " 0");//
                        psp.IniWriteValue("Graphics", "SplineBezierQuality ", " 2");
                        psp.IniWriteValue("Graphics", "InternalResolution ", " 0");
                        psp.IniWriteValue("Graphics", "AnisotropyLevel ", " 4");
                        psp.IniWriteValue("Graphics", "TexScalingLevel ", " 1");
                        psp.IniWriteValue("Graphics", "HardwareTessellation ", " True");
                        #endregion
                        #region 记录设置配置
                        switch (_huazhi)
                        {
                            case 1: ini.IniWriteValue("setgame", "huahzi", "1"); break;
                            case 2: ini.IniWriteValue("setgame", "huahzi", "2"); break;
                            case 3: ini.IniWriteValue("setgame", "huahzi", "3"); break;
                            case 4: ini.IniWriteValue("setgame", "huahzi", "4"); break;
                            default: break;
                        }
                        #endregion
                        #region 加载切换回来
                        Huazhidonghua.Visibility = Visibility.Hidden;
                        Di.IsEnabled = true;
                        Zhong.IsEnabled = true;
                        Gao.IsEnabled = true;
                        Feichanggao.IsEnabled = true;
                        Yingyong.Visibility = Visibility.Visible;
                        #endregion
                        MessageBox.Show("高画质设置完毕");
                        break;
                    case 4:
                        #region 超高画质
                        psp.IniWriteValue("Graphics", "GraphicsBackend ", " 0");//
                        psp.IniWriteValue("Graphics", "RenderingMode ", " 1");//1
                        psp.IniWriteValue("Graphics", "VertexDecCache ", " True");//
                        psp.IniWriteValue("Graphics", "MemBlockTransferGPU ", " False");//
                        psp.IniWriteValue("Graphics", "InflightFrames ", " 0");//
                        psp.IniWriteValue("Graphics", "SplineBezierQuality ", " 2");
                        psp.IniWriteValue("Graphics", "InternalResolution ", " 10");
                        psp.IniWriteValue("Graphics", "AnisotropyLevel ", " 4");
                        psp.IniWriteValue("Graphics", "TexScalingLevel ", " 3");
                        psp.IniWriteValue("Graphics", "HardwareTessellation ", " True");
                        #endregion
                        #region 记录设置配置
                        switch (_huazhi)
                        {
                            case 1: ini.IniWriteValue("setgame", "huahzi", "1"); break;
                            case 2: ini.IniWriteValue("setgame", "huahzi", "2"); break;
                            case 3: ini.IniWriteValue("setgame", "huahzi", "3"); break;
                            case 4: ini.IniWriteValue("setgame", "huahzi", "4"); break;
                            default: break;
                        }
                        #endregion
                        #region 加载切换回来
                        Huazhidonghua.Visibility = Visibility.Hidden;
                        Di.IsEnabled = true;
                        Zhong.IsEnabled = true;
                        Gao.IsEnabled = true;
                        Feichanggao.IsEnabled = true;
                        Yingyong.Visibility = Visibility.Visible;
                        #endregion
                        MessageBox.Show("非常高画质设置完毕");
                        break;
                    default: break;
                }


              


            }
            else
            {
                MessageBox.Show("请选择画质");
            }

        }
    }
}
