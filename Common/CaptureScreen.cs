using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Drawing;

namespace Mj.Common
{
    class CaptureScreen
    {

        public const int SRCCOPY = 13369376;

        [DllImport("gdi32.dll", EntryPoint = "DeleteDC")]
        public static extern IntPtr DeleteDC(IntPtr hDc);

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        public static extern IntPtr DeleteObject(IntPtr hDc);

        [DllImport("gdi32.dll", EntryPoint = "BitBlt")]
        public static extern bool BitBlt(IntPtr hdcDest, int xDest, int yDest, int wDest, int hDest, IntPtr hdcSource, int xSrc, int ySrc, int RasterOp);

        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleBitmap")]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleDC")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("gdi32.dll", EntryPoint = "SelectObject")]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr bmp);
        public CaptureScreen()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public const int SM_CXSCREEN = 0;
        public const int SM_CYSCREEN = 1;
        [DllImport("user32.dll", EntryPoint = "GetDesktopWindow")]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll", EntryPoint = "GetDC")]
        public static extern IntPtr GetDC(IntPtr ptr);

        [DllImport("user32.dll", EntryPoint = "GetSystemMetrics")]
        public static extern int GetSystemMetrics(int abc);

        [DllImport("user32.dll", EntryPoint = "GetWindowDC")]
        public static extern IntPtr GetWindowDC(Int32 ptr);

        [DllImport("user32.dll", EntryPoint = "ReleaseDC")]
        public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDc);

        public static Bitmap GetDesktopImage()
        {
            //保存截屏的尺寸
            SIZE size;
            //指向bitmap的句柄
            IntPtr hBitmap;
            //通过GetDC函数获得指向桌面设备上下文的句柄
            IntPtr hDC = CaptureScreen.GetDC(CaptureScreen.GetDesktopWindow());
            //在内存中创建一个兼容的设备上下文
            IntPtr hMemDC = CaptureScreen.CreateCompatibleDC(hDC);
            //传递一个常数到GetSystemMetrics，并返回屏幕的x坐标
            size.cx = CaptureScreen.GetSystemMetrics(0);
            //传递一个常数到GetSystemMetrics，并返回屏幕的y坐标
            size.cy = CaptureScreen.GetSystemMetrics(1);
            //创建与指定的设备环境相关的设备兼容的位图。
            hBitmap = CaptureScreen.CreateCompatibleBitmap(hDC, size.cx, size.cy);
            //As hBitmap is IntPtr we can not check it against null. For this purspose IntPtr.Zero is used.
            if (hBitmap != IntPtr.Zero)
            {
                //Here we select the compatible bitmap in memeory device context and keeps the refrence to Old bitmap.
                IntPtr hOld = (IntPtr)CaptureScreen.SelectObject(hMemDC, hBitmap);
                //We copy the Bitmap to the memory device context.
                CaptureScreen.BitBlt(hMemDC, 0, 0, size.cx, size.cy, hDC, 0, 0, CaptureScreen.SRCCOPY);
                //We select the old bitmap back to the memory device context.
                CaptureScreen.SelectObject(hMemDC, hOld);
                //We delete the memory device context.
                CaptureScreen.DeleteDC(hMemDC);
                //We release the screen device context.
                CaptureScreen.ReleaseDC(CaptureScreen.GetDesktopWindow(), hDC);
                //Image is created by Image bitmap handle and stored in local variable.
                Bitmap bmp = System.Drawing.Image.FromHbitmap(hBitmap);
                //Release the memory for compatible bitmap.
                CaptureScreen.DeleteObject(hBitmap);
                //This statement runs the garbage collector manually.
                GC.Collect();
                //Return the bitmap
                return bmp;
            }
            //If hBitmap is null retunrn null.
            return null;
        }
    }
    //This structure shall be used to keep the size of the screen.
    public struct SIZE
    {
        public int cx;
        public int cy;
    }

}
