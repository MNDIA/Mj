using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Mj.ViewModel
{
    public class XfcViewModel : INotifyPropertyChanged
    {
        private Common.IniBase ini = new Common.IniBase(System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\IndexOfMagic\ppsspp\set");//启用 ini
        public event PropertyChangedEventHandler PropertyChanged;


        #region 图标转换MSpng
        private string _mspng = "../Assets/ip.png";
        public string MSpng
        {
            get { return _mspng; }
            set { _mspng = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MSpng")); }
        }
        #endregion

        #region MS颜色MScolor
        private string _mscolor = "#183139";

        public string MScolor
        {
            get { return _mscolor; }
            set { _mscolor = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MSColor")); }
        }
        #endregion

        #region MS内容MStxt
        private string _mstxt = "检查中";
        public string MStxt
        {
            get { return _mstxt; }
            set
            {
                _mstxt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MStxt"));
            }
        }
        #endregion

        #region MS可见MSsee
        private string _mssee = "Hidden";

        public string MSsee
        {
            get { return _mssee; }
            set { _mssee = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MSsee")); }
        }
        #endregion

        #region MS可见MSseeback
        private string _msseeback = "Hidden";

        public string MSseeback
        {
            get { return _msseeback; }
            set { _msseeback = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MSseeback")); }
        }
        #endregion

        #region GIF次数GIFmany
        private string _gifmany = "5x";

        public string GIFmany
        {
            get { return _gifmany; }
            set { _gifmany = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GIFmany")); }
        }
        #endregion

        #region 自定义大小Zidingyibig
        private double _zidingyibig = 0.2;
        public double Zidingyibig
        {
            get { return _zidingyibig; }
            set
            {
                _zidingyibig = Math.Round(value, 4);
                ini.IniWriteValue("xfc", "big", _zidingyibig.ToString());
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Zidingyibig"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Ziti1"));
            }
        }
        private double _ziti1 = 85;
        public double Ziti1
        {
            get
            {
                return _ziti1 * _zidingyibig;
            }
            set { _ziti1 = value; }
        }


        private string _msfont = "Heavy";
        public string MSFont

        {
            get { return _msfont; }
            set
            {
                _msfont = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MSFont"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Ziti1"));
            }
        }
        #endregion


        #region IP
        //private string _ip = "10.10.0.1";
        public string IP
        {
            get { return Common.CommonSTA._ip; }
            set { Common.CommonSTA._ip = value.Trim(); PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IP")); }
        }
        #endregion

        #region IPtishi
        private string _iptishi = "";

        public string IPtishi
        {
            get { return _iptishi; }
            set { _iptishi = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IPtishi")); }
        }
        #endregion

        //PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(""));
    }
}
