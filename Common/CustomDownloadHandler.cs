using System;
using System.Collections.Generic;
using System.Text;
using CefSharp;
using HandyControl.Tools;

namespace Mj.Common
{
    public class CustomDownloadHandler : IDownloadHandler
    {
        public bool CanDownload(IWebBrowser chromiumWebBrowser, IBrowser browser, string url, string requestMethod)
        {
            return true;
        }
        public void OnBeforeDownload(IWebBrowser chromiumWebBrowser, IBrowser browser, DownloadItem downloadItem, IBeforeDownloadCallback callback)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                if (!CommonSTA.IsDownload)
                {
                    CommonSTA.IsDownload = true;

                    Aliyundownload(downloadItem.SuggestedFileName);
                }


            }).ContinueWith<int>((t) =>
            {
                return 1;
            });//新线程
        }


        public void OnDownloadUpdated(IWebBrowser chromiumWebBrowser, IBrowser browser, DownloadItem downloadItem, IDownloadItemCallback callback)
        {
        }

        #region 阿里云材质工坊
        private void Aliyundownload(string SuggestedFileName)
        {
            if ((!System.IO.Directory.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\ULJS00329")) || System.IO.File.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\ULJS00329\本文件不可删除_本文件夹材质不可编辑保存"))
            {
                ViewModel.IndexViewModel.IndexModel.WebIP = "";
                ViewModel.IndexViewModel.IndexModel.IndexVisibility = "Visible";
                ViewModel.IndexViewModel.IndexModel.WebVisibility = "Collapsed";
                View.IndexView.mainThreadSynContext.Post(new System.Threading.SendOrPostCallback(s =>
                ViewModel.IndexViewModel.IndexModel.MainContent = (System.Windows.FrameworkElement)Type.GetType("Mj.View.Page2View").GetConstructor(System.Type.EmptyTypes).Invoke(null)), null);

                ViewModel.IndexViewModel.IndexModel.Select1 = 1; ViewModel.IndexViewModel.IndexModel.Select2 = 0; ViewModel.IndexViewModel.IndexModel.Select3 = 0;
                View.IndexView.mainThreadSynContext.Post(new System.Threading.SendOrPostCallback(s =>
                System.Windows.MessageBox.Show("您未启用自定义材质，材质工坊不可用")), null);

                return;
            }
            if (System.IO.File.Exists(Common.CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\Download\" + SuggestedFileName))
            {
                try
                {
                    System.IO.File.Delete(Common.CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\Download\" + SuggestedFileName);
                }
                catch (Exception)
                {
                    View.IndexView.mainThreadSynContext.Post(new System.Threading.SendOrPostCallback(s =>
                    System.Windows.MessageBox.Show("操作被禁止")), null);
                }
            }
            #region 下载参数
            var endpoint = "oss-cn-beijing.aliyuncs.com";
            var accessKeyId = ;
            var accessKeySecret = ;
            var bucketName = "textruesdata";
            var objectName = "scattered/" + SuggestedFileName;
            var downloadFilename = Common.CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\Download\" + SuggestedFileName;
            #endregion
            var client = new Aliyun.OSS.OssClient(endpoint, accessKeyId, accessKeySecret);
            try
            {
                var metadata = client.GetObjectMetadata(bucketName, objectName);
                var etag = metadata.ETag;
                // 生成签名URL
                var req = new Aliyun.OSS.GeneratePresignedUriRequest(bucketName, objectName, Aliyun.OSS.SignHttpMethod.Get);
                var uri = client.GeneratePresignedUri(req);
                //url +"";
                ViewModel.IndexViewModel.IndexModel.DownloadVisibility = "Visible";//Collapsed  Visible
                ViewModel.IndexViewModel.IndexModel.DownloadFileSize = "0" + @" MB/" + Math.Round(metadata.ContentLength / 1048576.0, 2).ToString() + " MB";
                ViewModel.IndexViewModel.IndexModel.DownloadFileSpeed = "1" + "*";//0-10000
                #region 使用签名URL下载文件
                Aliyun.OSS.OssObject ossObject = client.GetObject(uri);
                using (var file = System.IO.File.Open(downloadFilename, System.IO.FileMode.OpenOrCreate))
                {
                    using (System.IO.Stream stream = ossObject.Content)
                    {
                        int length = 4 * 1024;
                        var buf = new byte[length];
                        double showlenth = 0;
                        do
                        {
                            length = stream.Read(buf, 0, length);
                            file.Write(buf, 0, length);
                            ViewModel.IndexViewModel.IndexModel.DownloadFileSize = Math.Round(new System.IO.FileInfo(Common.CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\Download\" + SuggestedFileName).Length / 1048576.0, 2).ToString() + @" MB/" + Math.Round(metadata.ContentLength / 1048576.0, 2).ToString() + " MB";
                            showlenth = showlenth + (1.0 * new System.IO.FileInfo(Common.CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\Download\" + SuggestedFileName).Length / (metadata.ContentLength - new System.IO.FileInfo(Common.CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\Download\" + SuggestedFileName).Length) - showlenth) * 0.8;
                            ViewModel.IndexViewModel.IndexModel.DownloadFileSpeed = showlenth + "*";
                        } while (length != 0);
                    }
                }
                ViewModel.IndexViewModel.IndexModel.DownloadFileSize = "解压中";
                if (System.IO.Directory.Exists(Common.CommonSTA.LujingPSP + @"memstick"))
                {
                    try
                    {
                        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                        System.IO.Compression.ZipFile.ExtractToDirectory(Common.CommonSTA.LujingPSP + @"memstick\PSP\TEXTURES\Download\" + SuggestedFileName, Common.CommonSTA.LujingPSP + @"memstick\", Encoding.GetEncoding("GB2312"), true);
                    }
                    catch (Exception)
                    {
                        View.IndexView.mainThreadSynContext.Post(new System.Threading.SendOrPostCallback(s =>
                        System.Windows.MessageBox.Show("解压材质被禁止")), null);
                    }
                }
                ViewModel.IndexViewModel.IndexModel.DownloadVisibility = "Collapsed";//Collapsed  Visible
                ViewModel.IndexViewModel.IndexModel.DownloadFileSpeed = "0" + "*";//0-10000
                CommonSTA.IsDownload = false;
                #endregion
            }
            catch (Aliyun.OSS.Common.OssException ex)
            {
                Console.WriteLine("Failed with error code: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}",
                    ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed with error info: {0}", ex.Message);
            }
        }
        #endregion
    }
}
