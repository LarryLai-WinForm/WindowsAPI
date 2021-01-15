using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

using WindowsAPI.dlls;
using static WindowsAPI.dlls.user32;

namespace WindowsAPI
{
    #region 參數列舉

    /// <summary>
    /// Windows Handle
    /// </summary>
    public enum HWND
    {
        /// <summary>Set Window On the Top</summary>
        HWND_TOP,
    }

    /// <summary>
    /// SetWindowPos
    /// </summary>
    public enum SWP
    {
        /// <summary>Show Windows 0x0040</summary>
        SWP_SHOWWINDOW = 0x0040,
        /// <summary>Hides the window 0x0080</summary>
        SWP_HIDEWINDOW = 0x0080,
    }

    /// <summary>
    /// Windows Style
    /// </summary>
    public enum WS
    {
        WS_BORDER = 0x00800000,
        WS_CAPTION = 0x00C00000,
        WS_CHILD = 0x40000000,
        WS_CHILDWINDOW = 0x40000000,
        WS_CLIPCHILDREN = 0x02000000,
        WS_CLIPSIBLINGS = 0x04000000,
        WS_DISABLED = 0x08000000,
        WS_DLGFRAME = 0x00400000,
        WS_GROUP = 0x00020000,
        WS_HSCROLL = 0x00100000,
        WS_ICONIC = 0x20000000,
        WS_MAXIMIZE = 0x01000000,
        WS_MAXIMIZEBOX = 0x00010000,
        WS_MINIMIZE = 0x20000000,
        WS_MINIMIZEBOX = 0x00020000,
        WS_OVERLAPPED = 0x00000000,
        WS_OVERLAPPEDWINDOW = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX,
        WS_POPUP = unchecked((int)0x80000000),
        WS_POPUPWINDOW = WS_POPUP | WS_BORDER | WS_SYSMENU,
        WS_SIZEBOX = 0x00040000,
        WS_SYSMENU = 0x00080000,
        WS_TABSTOP = 0x00010000,
        WS_THICKFRAME = 0x00040000,
        WS_TILED = 0x00000000,
        WS_TILEDWINDOW = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX,
        WS_VISIBLE = 0x10000000,
        WS_VSCROLL = 0x00200000,
    }

    /// <summary>
    /// GetWindowLong
    /// </summary>
    public enum GWL
    {
        GWL_EXSTYLE = -20,
        GWL_HINSTANCE = -6,
        GWL_HWNDPARENT = -8,
        GWL_ID = -12,
        GWL_STYLE = -16,
        GWL_USERDATA = -21,
        GWL_WNDPROC = -4,
    }

    /// <summary>
    /// ShowWindow
    /// </summary>
    public enum SW
    {
        SW_HIDE = 0,
        SW_SHOWNORMAL = 1,
        SW_SHOWMINIMIZED = 2,
        SW_SHOWMAXIMIZED = 3,
        SW_MAXIMIZE = 3,
        SW_SHOWNOACTIVATE = 4,
        SW_SHOW = 5,
        SW_MINIMIZE = 6,
        SW_SHOWMINNOACTIVE = 7,
        SW_SHOWNA = 8,
        SW_RESTORE = 9,
        SW_SHOWDEFAULT = 10,
        SW_FORCEMINIMIZE = 11,
    }

    /// <summary>
    /// SystemMetrics
    /// </summary>
    public enum SM
    {
        SM_CXSCREEN,
        SM_CYSCREEN,
    }

    /// <summary>
    /// 熱鍵
    /// </summary>
    public enum KeyModifiers
    {
        None = 0,

        Alt = 1,
        Ctrl = 2,
        Shift = 4,
        WindowsKey = 8,
    }

    /// <summary>
    /// ExitWindowsEx uFlag
    /// </summary>
    public enum EWX_uFlag : uint
    {
        /// <summary>關閉程序然後登出</summary>
        EWX_LOGOFF = 0x00000000,
        /// <summary>關機</summary>
        EWX_SHUTDOWN = 0x00000001,
        /// <summary>重啟</summary>
        EWX_REBOOT = 0x00000002,
        /// <summary>強制執行</summary>
        EWX_FORCE = 0x00000004,
        /// <summary>關閉系統後關閉電源,需系統支援電源關閉功能</summary>
        EWX_POWEROFF = 0x00000008,
        /// <summary>0x00000010</summary>
        FORCEIFHUNG = 0x00000010,
        /// <summary>0x00000040</summary>
        EWX_RESTARTAPPS = 0x00000040,
        /// <summary>0x00400000</summary>
        EWX_HYBRID_SHUTDOWN = 0x00400000,
    }
    /// <summary>
    /// System Shutdown Reason Codes
    /// </summary>
    public enum SystemShutdownReasonCodes
    {
        /// <summary>Other issue.</summary>
        SHTDN_REASON_MAJOR_OTHER = 0x00000000,
    }

    public enum WindowsMessage : int
    {
        WM_NULL = 0x0000,
        WM_CREATE = 0x0001,
        WM_DESTROY = 0x0002,
        WM_MOVE = 0x0003,
        WM_SIZE = 0x0005,
        WM_ACTIVATE = 0x0006,
        WM_SETFOCUS = 0x0007,
        WM_KILLFOCUS = 0x0008,
        WM_ENABLE = 0x000A,
        WM_SETREDRAW = 0x000B,
        WM_SETTEXT = 0x000C,
        WM_GETTEXT = 0x000D,
        WM_GETTEXTLENGTH = 0x000E,
        WM_PAINT = 0x000F,
        WM_CLOSE = 0x0010,
        WM_QUERYENDSESSION = 0x0011,
        WM_QUERYOPEN = 0x0013,
        WM_ENDSESSION = 0x0016,
        WM_QUIT = 0x0012,
        WM_ERASEBKGND = 0x0014,
        WM_SYSCOLORCHANGE = 0x0015,
        WM_SHOWWINDOW = 0x0018,
        WM_WININICHANGE = 0x001A,
        WM_SETTINGCHANGE = WM_WININICHANGE,
        WM_DEVMODECHANGE = 0x001B,
        WM_ACTIVATEAPP = 0x001C,
        WM_FONTCHANGE = 0x001D,
        WM_TIMECHANGE = 0x001E,
        WM_CANCELMODE = 0x001F,
        WM_SETCURSOR = 0x0020,
        WM_MOUSEACTIVATE = 0x0021,
        WM_CHILDACTIVATE = 0x0022,
        WM_QUEUESYNC = 0x0023,
        WM_GETMINMAXINFO = 0x0024,
        WM_PAINTICON = 0x0026,
        WM_ICONERASEBKGND = 0x0027,
        WM_NEXTDLGCTL = 0x0028,
        WM_SPOOLERSTATUS = 0x002A,
        WM_DRAWITEM = 0x002B,
        WM_MEASUREITEM = 0x002C,
        WM_DELETEITEM = 0x002D,
        WM_VKEYTOITEM = 0x002E,
        WM_CHARTOITEM = 0x002F,
        WM_SETFONT = 0x0030,
        WM_GETFONT = 0x0031,
        WM_SETHOTKEY = 0x0032,
        WM_GETHOTKEY = 0x0033,
        WM_QUERYDRAGICON = 0x0037,
        WM_COMPAREITEM = 0x0039,
        WM_GETOBJECT = 0x003D,
        WM_COMPACTING = 0x0041,
        WM_COMMNOTIFY = 0x0044,
        WM_WINDOWPOSCHANGING = 0x0046,
        WM_WINDOWPOSCHANGED = 0x0047,
        WM_POWER = 0x0048,
        WM_COPYDATA = 0x004A,
        WM_CANCELJOURNAL = 0x004B,
        WM_NOTIFY = 0x004E,
        WM_INPUTLANGCHANGEREQUEST = 0x0050,
        WM_INPUTLANGCHANGE = 0x0051,
        WM_TCARD = 0x0052,
        WM_HELP = 0x0053,
        WM_USERCHANGED = 0x0054,
        WM_NOTIFYFORMAT = 0x0055,
        WM_CONTEXTMENU = 0x007B,
        WM_STYLECHANGING = 0x007C,
        WM_STYLECHANGED = 0x007D,
        WM_DISPLAYCHANGE = 0x007E,
        WM_GETICON = 0x007F,
        WM_SETICON = 0x0080,
        WM_NCCREATE = 0x0081,
        WM_NCDESTROY = 0x0082,
        WM_NCCALCSIZE = 0x0083,
        WM_NCHITTEST = 0x0084,
        WM_NCPAINT = 0x0085,
        WM_NCACTIVATE = 0x0086,
        WM_GETDLGCODE = 0x0087,
        WM_SYNCPAINT = 0x0088,


        WM_NCMOUSEMOVE = 0x00A0,
        WM_NCLBUTTONDOWN = 0x00A1,
        WM_NCLBUTTONUP = 0x00A2,
        WM_NCLBUTTONDBLCLK = 0x00A3,
        WM_NCRBUTTONDOWN = 0x00A4,
        WM_NCRBUTTONUP = 0x00A5,
        WM_NCRBUTTONDBLCLK = 0x00A6,
        WM_NCMBUTTONDOWN = 0x00A7,
        WM_NCMBUTTONUP = 0x00A8,
        WM_NCMBUTTONDBLCLK = 0x00A9,
        WM_NCXBUTTONDOWN = 0x00AB,
        WM_NCXBUTTONUP = 0x00AC,
        WM_NCXBUTTONDBLCLK = 0x00AD,

        WM_INPUT_DEVICE_CHANGE = 0x00FE,
        WM_INPUT = 0x00FF,

        WM_KEYFIRST = 0x0100,
        WM_KEYDOWN = 0x0100,
        WM_KEYUP = 0x0101,
        WM_CHAR = 0x0102,
        WM_DEADCHAR = 0x0103,
        WM_SYSKEYDOWN = 0x0104,
        WM_SYSKEYUP = 0x0105,
        WM_SYSCHAR = 0x0106,
        WM_SYSDEADCHAR = 0x0107,
        WM_UNICHAR = 0x0109,
        WM_KEYLAST = 0x0109,

        WM_IME_STARTCOMPOSITION = 0x010D,
        WM_IME_ENDCOMPOSITION = 0x010E,
        WM_IME_COMPOSITION = 0x010F,
        WM_IME_KEYLAST = 0x010F,

        WM_INITDIALOG = 0x0110,
        WM_COMMAND = 0x0111,
        WM_SYSCOMMAND = 0x0112,
        WM_TIMER = 0x0113,
        WM_HSCROLL = 0x0114,
        WM_VSCROLL = 0x0115,
        WM_INITMENU = 0x0116,
        WM_INITMENUPOPUP = 0x0117,
        WM_MENUSELECT = 0x011F,
        WM_MENUCHAR = 0x0120,
        WM_ENTERIDLE = 0x0121,
        WM_MENURBUTTONUP = 0x0122,
        WM_MENUDRAG = 0x0123,
        WM_MENUGETOBJECT = 0x0124,
        WM_UNINITMENUPOPUP = 0x0125,
        WM_MENUCOMMAND = 0x0126,

        WM_CHANGEUISTATE = 0x0127,
        WM_UPDATEUISTATE = 0x0128,
        WM_QUERYUISTATE = 0x0129,

        WM_CTLCOLORMSGBOX = 0x0132,
        WM_CTLCOLOREDIT = 0x0133,
        WM_CTLCOLORLISTBOX = 0x0134,
        WM_CTLCOLORBTN = 0x0135,
        WM_CTLCOLORDLG = 0x0136,
        WM_CTLCOLORSCROLLBAR = 0x0137,
        WM_CTLCOLORSTATIC = 0x0138,
        MN_GETHMENU = 0x01E1,

        WM_MOUSEFIRST = 0x0200,
        WM_MOUSEMOVE = 0x0200,
        WM_LBUTTONDOWN = 0x0201,
        WM_LBUTTONUP = 0x0202,
        WM_LBUTTONDBLCLK = 0x0203,
        WM_RBUTTONDOWN = 0x0204,
        WM_RBUTTONUP = 0x0205,
        WM_RBUTTONDBLCLK = 0x0206,
        WM_MBUTTONDOWN = 0x0207,
        WM_MBUTTONUP = 0x0208,
        WM_MBUTTONDBLCLK = 0x0209,
        WM_MOUSEWHEEL = 0x020A,
        WM_XBUTTONDOWN = 0x020B,
        WM_XBUTTONUP = 0x020C,
        WM_XBUTTONDBLCLK = 0x020D,
        WM_MOUSEHWHEEL = 0x020E,

        WM_PARENTNOTIFY = 0x0210,
        WM_ENTERMENULOOP = 0x0211,
        WM_EXITMENULOOP = 0x0212,

        WM_NEXTMENU = 0x0213,
        WM_SIZING = 0x0214,
        WM_CAPTURECHANGED = 0x0215,
        WM_MOVING = 0x0216,

        WM_POWERBROADCAST = 0x0218,

        WM_DEVICECHANGE = 0x0219,

        WM_MDICREATE = 0x0220,
        WM_MDIDESTROY = 0x0221,
        WM_MDIACTIVATE = 0x0222,
        WM_MDIRESTORE = 0x0223,
        WM_MDINEXT = 0x0224,
        WM_MDIMAXIMIZE = 0x0225,
        WM_MDITILE = 0x0226,
        WM_MDICASCADE = 0x0227,
        WM_MDIICONARRANGE = 0x0228,
        WM_MDIGETACTIVE = 0x0229,


        WM_MDISETMENU = 0x0230,
        WM_ENTERSIZEMOVE = 0x0231,
        WM_EXITSIZEMOVE = 0x0232,
        WM_DROPFILES = 0x0233,
        WM_MDIREFRESHMENU = 0x0234,

        WM_IME_SETCONTEXT = 0x0281,
        WM_IME_NOTIFY = 0x0282,
        WM_IME_CONTROL = 0x0283,
        WM_IME_COMPOSITIONFULL = 0x0284,
        WM_IME_SELECT = 0x0285,
        WM_IME_CHAR = 0x0286,
        WM_IME_REQUEST = 0x0288,
        WM_IME_KEYDOWN = 0x0290,
        WM_IME_KEYUP = 0x0291,

        WM_MOUSEHOVER = 0x02A1,
        WM_MOUSELEAVE = 0x02A3,
        WM_NCMOUSEHOVER = 0x02A0,
        WM_NCMOUSELEAVE = 0x02A2,

        WM_WTSSESSION_CHANGE = 0x02B1,

        WM_TABLET_FIRST = 0x02c0,
        WM_TABLET_LAST = 0x02df,

        WM_CUT = 0x0300,
        WM_COPY = 0x0301,
        WM_PASTE = 0x0302,
        WM_CLEAR = 0x0303,
        WM_UNDO = 0x0304,
        WM_RENDERFORMAT = 0x0305,
        WM_RENDERALLFORMATS = 0x0306,
        WM_DESTROYCLIPBOARD = 0x0307,
        WM_DRAWCLIPBOARD = 0x0308,
        WM_PAINTCLIPBOARD = 0x0309,
        WM_VSCROLLCLIPBOARD = 0x030A,
        WM_SIZECLIPBOARD = 0x030B,
        WM_ASKCBFORMATNAME = 0x030C,
        WM_CHANGECBCHAIN = 0x030D,
        WM_HSCROLLCLIPBOARD = 0x030E,
        WM_QUERYNEWPALETTE = 0x030F,
        WM_PALETTEISCHANGING = 0x0310,
        WM_PALETTECHANGED = 0x0311,
        WM_HOTKEY = 0x0312,

        WM_PRINT = 0x0317,
        WM_PRINTCLIENT = 0x0318,

        WM_APPCOMMAND = 0x0319,

        WM_THEMECHANGED = 0x031A,

        WM_CLIPBOARDUPDATE = 0x031D,

        WM_DWMCOMPOSITIONCHANGED = 0x031E,
        WM_DWMNCRENDERINGCHANGED = 0x031F,
        WM_DWMCOLORIZATIONCOLORCHANGED = 0x0320,
        WM_DWMWINDOWMAXIMIZEDCHANGE = 0x0321,

        WM_GETTITLEBARINFOEX = 0x033F,

        WM_HANDHELDFIRST = 0x0358,
        WM_HANDHELDLAST = 0x035F,

        WM_AFXFIRST = 0x0360,
        WM_AFXLAST = 0x037F,

        WM_PENWINFIRST = 0x0380,
        WM_PENWINLAST = 0x038F,

        WM_APP = 0x8000,

        WM_USER = 0x0400,

        WM_REFLECT = WM_USER + 0x1C00,
    }

    /// <summary>
    /// MoveFileFlags
    /// </summary>
    public enum MoveFileFlags
    {
        /// <summary>
        /// 覆盖已存在的目标文件，如果来源文件和目标文件指定的是一个目录，则不能使用此标记。
        /// </summary>
        MOVEFILE_0x01_REPLACE_EXISTING = 0x01,
        /// <summary>
        /// 如果目标文件被移动到不同的卷上，则函数通过拷贝后删除来源文件的方法来模拟移动文件操作。
        /// </summary>
        MOVEFILE_0x02_COPY_ALLOWED = 0x02,
        /// <summary>
        /// 在系统重新启动前，不执行移动操作，
        /// 直到系统启动后，磁盘检测完毕后，创建页面文件之前，执行移动操作。
        /// 因此，这个参数可以删除系统之前启用的页面文件。
        /// 这个参数只能被拥有 管理员权限 或 LocalSystem权限 的程序使用。
        /// 这个参数不能和 MOVEFILE_COPY_ALLOWED 一起使用。
        /// </summary>
        MOVEFILE_0x04_DELAY_UNTIL_REBOOT = 0x04,
        /// <summary>
        /// 这个标记允许函数在执行完文件移动操作后才返回，否者不等文件移动完毕就直接返回。
        /// 如果设置了 MOVEFILE_DELAY_UNTIL_REBOOT 标记，则 MOVEFILE_WRITE_THROUGH 标记将被忽略。
        /// </summary>
        MOVEFILE_0x08_WRITE_THROUGH = 0x08,
        /// <summary>
        /// 系统保留，以供将来使用
        /// </summary>
        MOVEFILE_0x10_CREATE_HARDLINK = 0x10,
        /// <summary>
        /// 如果来源文件是一个 LINK 文件，但是文件在移动后不能够被 TRACKED，则函数执行失败。
        /// 如果目标文件在一个 FAT 格式的文件系统上，则上述情况可以发生。
        /// 这个参数不支持 NT 系统。
        /// (我想这里说的可能是移动快捷方式的情况，
        /// 如果快捷方式指定的目标文件不存在或无法定位，则操作失败，
        /// 由于没有时间测试，暂时这样理解。)
        /// </summary>
        MOVEFILE_0x20_FAIL_IF_NOT_TRACKABLE = 0x20,
    }

    #endregion

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TokPriv1Luid
    {
        public int PrivilegeCount;
        public long Luid;
        public int Attr;
    }

    /// <summary>
    /// Windows API
    /// </summary>
    public class Func
    {
        /// <summary>
        /// 取得程序實例之窗體,復原其視窗大小並移至前景
        /// </summary>
        /// <param name="instance">程序實例</param>
        static public void HandleRunningInstance(Process instance)
        {
            if (instance == null)
            {
                return;
            }

            IntPtr Handle = instance.MainWindowHandle;


            if (Handle == IntPtr.Zero)
            {
                Handle = FindWindow(null, instance.ProcessName);
            }

            //Make sure the window is not minimized or maximized   
            ShowWindowAsync(Handle, SW.SW_SHOWNORMAL);
            //Set the real intance to foreground window
            SetForegroundWindow(Handle);
        }

        /// <summary>設定是否顯示工作列</summary>
        /// <param name="visible">是否顯示</param>
        static public void SetTaskBarVisible(bool visible)
        {
            SW sw;

            if (visible)
            {
                sw = SW.SW_SHOWNORMAL;
            }
            else
            {
                sw = SW.SW_HIDE;
            }

            IntPtr TaskBar = FindWindow("Shell_TrayWnd", "");
            ShowWindow(TaskBar, sw);

            IntPtr start = FindWindowEx(IntPtr.Zero, IntPtr.Zero, "Button", null);
            ShowWindow(start, sw);
        }

        /// <summary>視窗系統離開功能</summary>
        /// <param name="uFlags">功能參數</param>
        /// <param name="dwReason">保留,預設0</param>
        /// <returns>回傳0為成功,其餘為失敗</returns>
        static public int ExitWindowsEx_adv(EWX_uFlag uFlags, uint dwReason = (uint)SystemShutdownReasonCodes.SHTDN_REASON_MAJOR_OTHER)
        {

            const int TOKEN_QUERY = 0x00000008;
            const int TOKEN_ADJUST_PRIVILEGES = 0x00000020;
            const int SE_PRIVILEGE_ENABLED = 0x00000002;
            const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";

            TokPriv1Luid tp;
            IntPtr hproc = kernel32.GetCurrentProcess();
            IntPtr htoken = IntPtr.Zero;
            bool retVal = advapi32.OpenProcessToken(hproc, TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, ref htoken);

            tp.PrivilegeCount = 1;
            tp.Luid = 0;
            tp.Attr = SE_PRIVILEGE_ENABLED;
            retVal = advapi32.LookupPrivilegeValue(null, SE_SHUTDOWN_NAME, ref tp.Luid);
            retVal = advapi32.AdjustTokenPrivileges(htoken, false, ref tp, 0, IntPtr.Zero, IntPtr.Zero);


            return user32.ExitWindowsEx((uint)uFlags, dwReason);

        }


        ///// <summary>Determine whether the operating system is running on 64bit</summary> 
        ///// <returns>Whether for 64bit operating system</returns> 
        //static bool Is64bit
        //{
        //    get
        //    {
        //        const int bitCountForByte = 8;
        //        const int bitCountFor64Bit = 64;

        //        return IntPtr.Size == (bitCountFor64Bit / bitCountForByte);
        //    }
        //}


        ///// <summary>在窗體內嵌外部程序</summary>
        ///// <param name="MainForm">窗體</param>
        ///// <param name="ProcessPath">程序路徑</param>
        ///// <param name="ProcessName">程序名稱</param>
        ///// <returns>回傳是否成功</returns>
        //static public bool CallProcessEmbed(Form MainForm, string Path)
        //{
        //    try
        //    {

        //        Process process = new Process();
        //        process.StartInfo.FileName = Path;
        //        process.StartInfo.UseShellExecute = false;
        //        process.Start();

        //        if (process == null)
        //        {
        //            return false;
        //        }

        //        do
        //        {
        //            if (process.MainWindowHandle != IntPtr.Zero)
        //            {
        //                break;
        //            }
        //        }
        //        while (true);

        //        SetWindowLong(process.MainWindowHandle, GWL.GWL_STYLE, WS.WS_VISIBLE);
        //        SetParent(process.MainWindowHandle, MainForm.Handle);
        //        MoveWindow(process.MainWindowHandle, 0, 0, MainForm.Width, MainForm.Height, true);

        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //    return true;
        //}

    }
}