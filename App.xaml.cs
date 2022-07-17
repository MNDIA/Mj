using Mj.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace Mj
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region 窗口被动传参总开关IndexView().ShowDialog() == true
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            if (new View.IndexView().ShowDialog() == true)
            {
                ;
            }
            else
            {
                //Application.Current.Shutdown();
            }

        }
       
        #endregion
        /// <summary>  
        /// App.xaml 的交互逻辑  
        /// </summary>  




        #region 单开"window", "ManySame") == "1"

        System.Threading.Mutex mutex;

        public App()
        {
            this.Startup += new StartupEventHandler(App_Startup);
        }

        void App_Startup(object sender, StartupEventArgs e)
        {
            bool ret;
            mutex = new System.Threading.Mutex(true, "Mj", out ret);

            if (!ret)
            {
                Mj.ViewModel.IndexViewModel IndexViewModel = new IndexViewModel();
                if (IndexViewModel.ini.IniReadValue("window", "ManySame") == "1")
                {
                    ;
                }
                else
                {
                    MessageBox.Show("嘤嘤嘤，太多了电脑会疼的");
                    Environment.Exit(0);
                }

            }

        } 
        #endregion

    }
}
