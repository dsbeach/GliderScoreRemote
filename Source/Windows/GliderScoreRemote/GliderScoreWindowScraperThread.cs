using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading;

namespace GliderScoreRemote
{

    class GliderScoreWindowScraperThread
    {
        #region WIN32API stuff
        // imported win32 functions
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)] //
        public static extern IntPtr SendMessageStringBuilder(IntPtr hWnd, uint Msg, IntPtr wParam, StringBuilder lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        protected static extern int GetWindowText(IntPtr hWnd, StringBuilder strText, int maxCount);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        protected static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll")]
        protected static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);

        [DllImport("user32")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool EnumChildWindows(IntPtr window, EnumWindowsProc enumChildProc, IntPtr lParam);

        [DllImport("user32.dll")]
        protected static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        static extern int GetClassName (IntPtr hwnd, StringBuilder lpClassName, int nMaxCount);

        // win32 API constants
        private const int WM_GETTEXT = 13;
        private const int LB_GETCURSEL = 0x0188;
        private const int LB_GETTEXT = 0x0189;
        private const int LB_GETTEXTLEN = 0x018a;
        private const int LB_ERR = -1;
        #endregion

        // win32 api helper delegates
        protected delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        // private static members
        private static IntPtr hWndGliderScoreTimer = IntPtr.Zero;
        private static string sTime;
        private static string sLBSelectedText;
        private static Regex timeRegex = new Regex(@"[0-5][0-9]:[0-5][0-9]");
        private static Thread scraperThread;
        private static object waitIntervalLock = new object();
        private static int waitInterval;

        public delegate void TimeCallback(string sTime);
        public static TimeCallback timeCallback;

        public delegate void LBSelectedTextCallback(string sLBSelectedText);
        public static LBSelectedTextCallback lbSelectedTextCallback;

        // public methods and members
        public static int WaitInterval
        {
            get
            {
                int result = 0;
                lock (waitIntervalLock)
                {
                    result = waitInterval;
                }
                return result;
            }
            set
            {
                lock (waitIntervalLock)
                {
                    waitInterval = value;
                }
            }
        }

        public static void Start(TimeCallback timeCallback, LBSelectedTextCallback lbSelectedTextCallback)
        {
            GliderScoreWindowScraperThread.timeCallback = timeCallback;
            GliderScoreWindowScraperThread.lbSelectedTextCallback = lbSelectedTextCallback;
            sTime = "";
            sLBSelectedText = "";
            scraperThread = new Thread(ScrapeWindow);
            scraperThread.Start();
        }

        public static void Stop()
        {
            if ((scraperThread != null) && (scraperThread.IsAlive))
            {
                scraperThread.Abort();
                scraperThread.Join();
                scraperThread = null;
            }

        }

        protected static string GetClassNameOfWindow(IntPtr hwnd)
        {
            string className = "";
            StringBuilder classText;
            try
            {
                classText = new StringBuilder(256);
                GetClassName(hwnd, classText, classText.Capacity);
                if (!String.IsNullOrEmpty(classText.ToString()) && !String.IsNullOrWhiteSpace(classText.ToString()))
                    className = classText.ToString();
            }
            catch (Exception ex)
            {
                className = ex.Message;
            }
            finally
            {
                classText = null;
            }
            return className;
        }

        protected static string GetWindowText(IntPtr hWnd)
        {
            string result = "";
            int size = GetWindowTextLength(hWnd);
            if (size++ > 0 && IsWindowVisible(hWnd))
            {
                StringBuilder sb = new StringBuilder(size);
                GetWindowText(hWnd, sb, size);
                result = sb.ToString();
            }
            return result;
        }

        protected static bool EnumTheWindows(IntPtr hWnd, IntPtr lParam)
        {
            string windowText = GetWindowText(hWnd);
            if (windowText.StartsWith("GliderScore DigitalTimer"))
            {
                hWndGliderScoreTimer = hWnd;
                return false; // stop the enumeration - we are done!
            }
            // Debug.WriteLine(windowText);
            return true;
        }

        protected static int LB_GetCurSel(IntPtr hWnd)
        {
            IntPtr curSel = SendMessage(hWnd, LB_GETCURSEL, IntPtr.Zero, IntPtr.Zero);
            return (int)curSel;
        }

        protected static bool EnumTheChildWindows(IntPtr hWnd, IntPtr lParam)
        {
            string windowText = GetWindowText(hWnd);
            string windowClass = GetClassNameOfWindow(hWnd);
            //Debug.WriteLine("Class: " + windowClass + " Text: " + windowText);
            if (windowClass.Contains("STATIC"))
            {
                Match m = timeRegex.Match(windowText);
                if (m.Success)
                {
                    if (!sTime.Equals(m.Value))
                    {
                        sTime = m.Value;
                        timeCallback(sTime);
                    }
                }
            }
            if (windowClass.Contains("LISTBOX"))
            {
                // can we get the currently selected item?
                int curSel = LB_GetCurSel(hWnd);
                if (curSel != LB_ERR)
                {
                    // now get the text of the selected item!
                    int textLength = (int)SendMessage(hWnd, LB_GETTEXTLEN, (IntPtr)curSel, IntPtr.Zero);
                    StringBuilder sb = new StringBuilder(textLength);
                    int lbResult = (int)SendMessageStringBuilder(hWnd, LB_GETTEXT, (IntPtr)curSel, sb);
                    if (lbResult != LB_ERR)
                    {
                        string sTemp = sb.ToString();
                        if (!sTemp.Equals(sLBSelectedText))
                        {
                            sLBSelectedText = sTemp;
                            lbSelectedTextCallback(sLBSelectedText);
                        }
                    }
                }
            }
            return true;
        }

        public static void ScrapeWindow()
        {
            while (true)
            {
                try
                {
                    EnumWindows(new EnumWindowsProc(EnumTheWindows), IntPtr.Zero);

                    if (hWndGliderScoreTimer != IntPtr.Zero)
                    {
                        // Debug.WriteLine("Now enumerate the child windows");
                        EnumChildWindows(hWndGliderScoreTimer, new EnumWindowsProc(EnumTheChildWindows), IntPtr.Zero);
                    }
                    else
                    {
                        if (!sTime.Equals("n/a"))
                        {
                            sTime = "n/a";
                            timeCallback(sTime);
                        }
                    }
                    Thread.Sleep(WaitInterval);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("ScrapeWindow() exiting due to exception: " + ex.Message);
                    break;
                }
            }
        }
    }
}
