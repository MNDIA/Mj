using System;
using System.Collections.Generic;
using System.Text;

namespace Mj.Common
{
    public static class CommonSTA // static 不是必须
    {
        public static double dpi = 1;
        public static byte Doping = 1;
        public static byte Doping2 = 1;
        public static byte Noping = 0;
        public static byte Noping2 = 0;
        public static byte Noping3 = 0;
        public static byte XDS = 0;
        public static byte Leisile = 0;
        public static bool PING = false;
        public static bool IsPlayStart = false;
        public static bool Danji = false;
        public static bool IsOK = false;
        public static bool IsDownload = false;
        public static bool Stop = false;
        public static string _ip = "10.10.0.1";
        public static string LujingFCN = Environment.CurrentDirectory + @"\FCN.exe";
        public static string LujingPSP = Environment.CurrentDirectory + @"\";
        public static string LujingISO = Environment.CurrentDirectory + @"\memstick\PSP\GAME\";
        public static string PSPName = @"";
        public static System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
        public static System.Net.NetworkInformation.Ping ping2 = new System.Net.NetworkInformation.Ping();
    }

}
