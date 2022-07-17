using Mj.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Mj.Model
{
    public class IndexModel : NotifyBase
    {
        //————————————————————————————————————————————————————————————
        //封装列表[+DoNotify();实时更新],数据驱动
        //————————————————————————————————————————————————————————————
        #region 封装📃提示
        //private int myVar;

        //public int MyProperty
        //{
        //    get { return myVar; }
        //    set { myVar = value; this.DoNotify(); }
        //} 
        #endregion

        //
        //未知类型（我太菜了
        //
        #region 跨窗口传参，未使用
        private List<View.XfcView> _xfcs = new List<View.XfcView>();
        public List<View.XfcView> XFCS
        {
            get { return _xfcs; }
            set { _xfcs = value; }
        }
        #endregion
        #region 悬浮窗窗口实例化XfcView
        private View.XfcView _xfc = new View.XfcView();
        public View.XfcView XfcView
        {
            get { return _xfc; }
            set { _xfc = value; }
        }
        #endregion
        #region 提示窗口实例化XfcView
        private View.UMessageBox _tishi = new View.UMessageBox();
        public View.UMessageBox Tishi
        {
            get { return _tishi; }
            set { _tishi = value; }
        }
        #endregion
        #region 改变主页MainContent
        private FrameworkElement _maincontent;
        public FrameworkElement MainContent
        {
            get { return _maincontent; }
            set { _maincontent = value; this.DoNotify(); }
        }
        private FrameworkElement _maincontent2;
        public FrameworkElement MainContent2
        {
            get { return _maincontent2; }
            set { _maincontent2 = value; this.DoNotify(); }
        }
        #endregion
        #region WebContent
        private FrameworkElement _webcontent;
        public FrameworkElement WebContent
        {
            get { return _webcontent; }
            set { _webcontent = value; this.DoNotify(); }
        }
        #endregion

        //
        //int
        //
        #region 右侧栏横线参数SelectN####
        private int _select1 = 1;
        private int _select2 = 0;
        private int _select3 = 0;
        public int Select1
        {
            get { return _select1; }
            set
            {
                _select1 = value;
                this.DoNotify();
            }
        }
        public int Select2
        {
            get { return _select2; }
            set
            {
                _select2 = value;
                this.DoNotify();
            }
        }
        public int Select3
        {
            get { return _select3; }
            set
            {
                _select3 = value;
                this.DoNotify();
            }
        }
        #endregion
     



        //
        //Double
        //
        #region 窗体高Gao
        private double _gao = 701;

        public double Gao
        {
            get { return 701; }
            set
            {
                _gao = value;

            }
        }
        #endregion
        #region 窗体宽Kuan
        private double _kuan = 1162;
        public double Kuan
        {
            get { return 1162; }
            set
            {
                _kuan = value;
                if (_kuan < 980 && _kuan > 652)
                {
                    _kuan = 730;
                }
                if (_kuan < 335 && _kuan > 286)
                {
                    _kuan = 286;
                }
                if (_gao == 390)
                {
                    _kuan = 286;
                }

            }
        }
        #endregion
        #region 设置栏动画偏移SetGrid
        private double _setgrid = 0;
        public double SetGrid
        {
            get
            { return _setgrid; }
            set
            {
                if (_setgrid == 0)
                {
                    _setgrid = 1;
                }
                else
                {
                    _setgrid = 0;
                }
                this.DoNotify();
            }
        }
        #endregion
      



        //
        //String
        //
        //全局
        #region IP栏内容IP
        //private string _ip = "10.10.0.1";
        public string IP
        {
            get { return CommonSTA._ip; }
            set
            {
                CommonSTA._ip = value.Trim();
                this.DoNotify();
                
            }
        }
        #endregion

        //非全局
        #region 随机启动标题Biaoti
        private string _biaoti = "魔法禁书目录";

        public string Biaoti
        {
            get { return _biaoti; }
            set
            { //_biaoti = value;
                Random rd = new Random();
                int a = rd.Next(1, 24);
                _biaoti = a switch
                {
                    1 => "魔法禁书目录：GaodaGG",
                    2 => "魔法禁书目录：ywrc",
                    3 => "魔法禁书目录：战术后仰",
                    4 => "魔法禁书目录：击剑！",
                    5 => "魔法禁书目录：呱",
                    6 => "魔法禁书目录：wasabi~",
                    7 => "魔法禁书目录：哼哼，哼啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊",
                    8 => "魔法禁书目录：晚安",
                    9 => "魔法禁书目录：歪比巴卜",
                    10 => "魔法禁书目录：歪比歪比",
                    11 => "魔法禁书目录：菜菜子是什么",
                    12 => "魔法禁书目录：锟斤拷",
                    13 => "魔法禁书目录：尝尝这个！",
                    14 => "魔法禁书目录：也许我应该防反",
                    15 => "魔法禁书目录：今天你寄了吗",
                    16 => "魔法禁书目录：砍他大动脉！",
                    17 => "魔法禁书目录：寄汤来喽",
                    18 => "魔法禁书目录：要来点萝莉吗",
                    19 => "魔法禁书目录：Alt+F4",
                    20 => "魔法禁书目录：小电视蹦起来了",
                    21 => "魔法禁书目录：啊~夜色",
                    22 => "魔法禁书目录：再来一刀！",
                    23 => "魔法禁书目录：Alt+I",
                    _ => "魔法禁书目录：麻了",
                };
                this.DoNotify();
            }
        }
        #endregion
        #region MS栏内容MS
        private string _ms = "检查中";
        public string MS
        {
            get { return _ms; }
            set
            {
                _ms = value;
                this.DoNotify();
            }
        }
        #endregion
        #region IP栏IPVisible
        private string _ipvisible = "Visible";
        public string IPVisible
        {
            get { return _ipvisible; }
            set { _ipvisible = value; this.DoNotify(); }
        }
        #endregion
        #region MS栏MSVisible
        private string _msvisible = "Collapsed";
        public string MSVisible
        {
            get { return _msvisible; }
            set { _msvisible = value; this.DoNotify(); }
        }
        #endregion
        #region MS栏MSColor
        private string _mscolor = "Black";
        public string MSColor
        {
            get { return _mscolor; }
            set { _mscolor = value; this.DoNotify(); }
        }
        #endregion
        #region PING栏内容小提示IPtishi
        private string _iptishi;

        public string IPtishi
        {
            get { return _iptishi; }
            set
            {
                _iptishi = value;
                this.DoNotify();
            }
        }
        #endregion
        #region 悬浮窗图片IPtupian
        private string _iptupian = "../Assets/ip.png";
        public string IPtupian
        {
            get { return _iptupian; }
            set { _iptupian = value; this.DoNotify(); }
        }
        #endregion
        #region PING搜索图片Searchtupian
        private string _searchtupian = "../Assets/search1.png";

        public string Searchtupian
        {
            get { return _searchtupian; }
            set { _searchtupian = value; this.DoNotify(); }
        }
        #endregion
        #region 托盘属性Tuopan
        private string _tuopan = "Visible";

        public string Tuopan
        {
            get { return _tuopan; }
            set { _tuopan = value; this.DoNotify(); }
        }
        #endregion
        #region 立即开始文字PlayContent
        private string _playcontent = "立即开始";
        public string PlayContent
        {
            get { return _playcontent; }
            set { _playcontent = value; this.DoNotify(); }
        }
        #endregion
        #region 按键按钮图标Anjian
        private string _anjian = "../Assets/anjian1.png";
        public string Anjian
        {
            get { return _anjian; }
            set { _anjian = value; this.DoNotify(); }
        }
        #endregion
        #region ICO
        private string _ico = "../Assets/logo128.ico";
        public string ICO
        {
            get { return _ico; }
            set { _ico = value; this.DoNotify(); }
        }
        #endregion

        #region IndexVisibility
        private string _indexvisibility = "Visible";//"Hidden";
        public string IndexVisibility
        {
            get { return _indexvisibility; }
            set { _indexvisibility = value; this.DoNotify(); }
        }
        #endregion
        #region WebVisibility
        private string _webvisibility = "Collapsed";//"Visible";
        public string WebVisibility
        {
            get { return _webvisibility; }
            set { _webvisibility = value; this.DoNotify(); }
        }
        #endregion
        #region WebIP
        private string _webip = "";//http://8.131.60.137/
        public string WebIP
        {
            get { return _webip; }
            set { _webip = value; this.DoNotify(); }
        }
        #endregion
        #region 下载文件DownloadFileSize
        private string _downloadfilesize = "";

        public string DownloadFileSize
        {
            get { return _downloadfilesize; }
            set { _downloadfilesize = value; this.DoNotify(); }
        }
        #endregion
        #region 文件完成度DownloadFileSpeed
        private string _downloadfilespeed = "0";//0-10000

        public string DownloadFileSpeed
        {
            get { return _downloadfilespeed; }
            set { _downloadfilespeed = value; this.DoNotify(); }
        }
        #endregion
        #region 下载可见DownloadVisibility
        private string _downloadvisibility = "Collapsed";//Collapsed  Visible

        public string DownloadVisibility
        {
            get { return _downloadvisibility; }
            set { _downloadvisibility = value; this.DoNotify(); }
        }
        #endregion
        #region 下载文字DownloadText
        private string _downloadtext = "";

        public string DownloadText
        {
            get { return _downloadtext; }
            set { _downloadtext = value; this.DoNotify(); }
        }
        #endregion
        #region 下载文字可见DownloadTextVisibility
        private string _downloadtextvisibility = "Collapsed";//Collapsed  Visible

        public string DownloadTextVisibility
        {
            get { return _downloadtextvisibility; }
            set { _downloadtextvisibility = value; this.DoNotify(); }
        }
        #endregion

        #region JianpanVisibility
        private string _jianpanvisibility = "Collapsed";//"Visible";
        public string JianpanVisibility
        {
            get { return _jianpanvisibility; }
            set { _jianpanvisibility = value; this.DoNotify(); }
        }
        #endregion



        #region 圆角Yuanjiao
        private string _yuanjiao = "0";
        public string Yuanjiao
        {
            get { return _yuanjiao; }
            set { _yuanjiao = value; this.DoNotify(); }
        }
        #endregion
        #region 圆角2Yuanjiao2
        private string _yuanjiao2 = "0";
        public string Yuanjiao2
        {
            get { return _yuanjiao2; }
            set { _yuanjiao2 = value; this.DoNotify(); }
        }
        #endregion
        #region 圆角3Yuanjiao3
        private string _yuanjiao3 = "0";
        public string Yuanjiao3
        {
            get { return _yuanjiao3; }
            set { _yuanjiao3 = value; this.DoNotify(); }
        }
        #endregion
        #region 圆角3Yuanjiao4
        private string _yuanjiao4 = "0";
        public string Yuanjiao4
        {
            get { return _yuanjiao4; }
            set { _yuanjiao4 = value; this.DoNotify(); }
        }
        #endregion

        #region 主题色值####

        private string _line = "#DDD";
        public string Line
        {
            get { return _line; }
            set { _line = value; this.DoNotify(); }
        }

        private string _line2 = "#A4A4A4";
        public string Line2
        {
            get { return _line2; }
            set { _line2 = value; this.DoNotify(); }
        }

        private string _whites2 = "#EDEDED";
        public string Whites2
        {
            get { return _whites2; }
            set { _whites2 = value; this.DoNotify(); }
        }

        private string _whitem = "#DFDFDF";
        public string Whitem
        {
            get { return _whitem; }
            set { _whitem = value; this.DoNotify(); }
        }

       
        private string _whitesFF = "#FFEDEDED";
        public string WhitesFF
        {
            get { return _whitesFF; }
            set { _whitesFF = value; this.DoNotify(); }
        }

        private string _whites00 = "#00EDEDED";
        public string Whites00
        {
            get { return _whites00; }
            set { _whites00 = value; this.DoNotify(); }
        }

        private string _whites33 = "#33EDEDED";
        public string Whites33
        {
            get { return _whites33; }
            set { _whites33 = value; this.DoNotify(); }
        }

        private string _whites66 = "#66EDEDED";
        public string Whites66
        {
            get { return _whites66; }
            set { _whites66 = value; this.DoNotify(); }
        }

        private string _whitesAA = "#AAEDEDED";
        public string WhitesAA
        {
            get { return _whitesAA; }
            set { _whitesAA = value; this.DoNotify(); }
        }

    
        private string _darks66 = "#6600A8F3";
        public string Darks66
        {
            get { return _darks66; }
            set { _darks66 = value; this.DoNotify(); }
        }

        private string _darksAA = "#AA00A8F3";
        public string DarksAA
        {
            get { return _darksAA; }
            set { _darksAA = value; this.DoNotify(); }
        }

        private string _darksFF = "#FF00A8F3";
        public string DarksFF
        {
            get { return _darksFF; }
            set { _darksFF = value; this.DoNotify(); }
        }

        private string _playdarks1 = "#FF00A8F3";
        public string Playdarks1
        {
            get { return _playdarks1; }
            set { _playdarks1 = value; this.DoNotify(); }
        }

        private string _playdarks2 = "#FF20BAFF";
        public string Playdarks2
        {
            get { return _playdarks2; }
            set { _playdarks2 = value; this.DoNotify(); }
        }

        private string _close66 = "#66EDEDED";
        public string Close66
        {
            get { return _close66; }
            set { _close66 = value; this.DoNotify(); }
        }

        private string _closeAA = "#AAEDEDED";
        public string CloseAA
        {
            get { return _closeAA; }
            set { _closeAA = value; this.DoNotify(); }
        }
        #endregion
        #region 主题字体Ziti
        private string _ziti = "Microsoft YaHei";

        public string Ziti
        {
            get { return _ziti; }
            set { _ziti = value; this.DoNotify(); }
        }
        #endregion



        //————————————————————————————————————————————————————————————
    }
}
