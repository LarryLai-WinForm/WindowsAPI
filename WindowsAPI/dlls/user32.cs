using System;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;

namespace WindowsAPI.dlls
{
    public class user32
    {
        const string user32_dll = "user32.dll";

        /// <summary>取得視窗指標</summary>
        /// <param name="className">視窗類別名稱</param>
        /// <param name="windowText">視窗名稱</param>
        /// <returns>視窗指標</returns>
        [DllImport(user32_dll)]
        public static extern IntPtr FindWindow(string className, string windowText);

        /// <summary>取得元件指標</summary>
        /// <param name="parentHandle">父窗體Handle
        /// (從此窗體下之子窗體開始搜尋 為0時以桌面為父窗體)</param>
        /// <param name="childAfter">子窗體Handle
        /// (從此窗體之後的窗體開始搜尋 以A~Z排序 為0時搜尋所有子窗體)</param>
        /// <param name="lclassName">指定類別名稱</param>
        /// <param name="windowTitle">指定窗體名稱</param>
        /// <returns>元件指標</returns>
        [DllImport(user32_dll)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string lclassName, string windowTitle);

        /// <summary>取得滑鼠座標</summary>
        /// <param name="p">回傳滑鼠座標</param>
        /// <returns>回傳是否成功</returns>
        [DllImport(user32_dll)]
        public extern static bool GetCursorPos(out Point p);

        /// <summary>設定元件座標</summary>
        /// <param name="hWnd">Handle</param>
        /// <param name="hWndInsertAfter">0</param>
        /// <param name="X">座標X</param>
        /// <param name="Y">座標Y</param>
        /// <param name="cx">寬</param>
        /// <param name="cy">高</param>
        /// <param name="uFlags">SWP</param>
        /// <returns>成功回傳0</returns>
        [DllImport(user32_dll)]
        public static extern bool SetWindowPos(IntPtr hWnd, HWND hWndInsertAfter, int X, int Y, int cx, int cy, SWP uFlags);

        /// <summary>設定前景視窗</summary>
        /// <param name="hWnd">Handle</param>
        /// <returns>成功回傳0</returns>
        [DllImport(user32_dll)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>設定視窗狀態</summary>
        /// <param name="hWnd">Handle</param>
        /// <param name="cmdShow">SW</param>
        /// <returns>成功回傳0</returns>
        [DllImport(user32_dll)]
        public static extern bool ShowWindowAsync(IntPtr hWnd, SW cmdShow);

        /// <summary>設定父視窗</summary>
        /// <param name="Child">子視窗</param>
        /// <param name="Parent">父視窗</param>
        /// <returns>If the function succeeds, the return value is a handle to the previous parent window.
        /// If the function fails, the return value is NULL. To get extended error information, call GetLastError.</returns>
        [DllImport(user32_dll)]
        public static extern IntPtr SetParent(IntPtr Child, IntPtr Parent);

        /// <summary>移動視窗</summary>
        /// <param name="hwnd">A handle to the window.</param>
        /// <param name="x">The new position of the left side of the window.</param>
        /// <param name="y">The new position of the top of the window.</param>
        /// <param name="nWidth">The new width of the window.</param>
        /// <param name="nHeight">The new height of the window.</param>
        /// <param name="bRepaint">是否重繪</param>
        /// <returns>If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero. To get extended error information, call GetLastError.</returns>
        [DllImport(user32_dll)]
        public static extern bool MoveWindow(IntPtr hwnd, int x, int y, int nWidth, int nHeight, bool bRepaint);

        /// <summary>設定視窗屬性</summary>
        /// <param name="hwnd">A handle to the window</param>
        /// <param name="gwl">GetWindowLong</param>
        /// <param name="ws">Windows Style</param>
        /// <returns>If the function succeeds, the return value is the previous value of the specified 32-bit integer.
        /// If the function fails, the return value is zero. To get extended error information, call GetLastError.</returns>
        [DllImport(user32_dll)]
        public static extern long SetWindowLong(IntPtr hwnd, GWL gwl, WS ws);

        /// <summary>設定視窗顯示狀態</summary>
        /// <param name="hwnd">A handle to the window.</param>
        /// <param name="command">ShowWindow cmd</param>
        /// <returns>If the window was previously visible, the return value is nonzero.
        /// If the window was previously hidden, the return value is zero.</returns>
        [DllImport(user32_dll)]
        public static extern Int32 ShowWindow(IntPtr hwnd, SW command);

        /// <summary>取得系統數據</summary>
        /// <param name="sm">system metric</param>
        /// <returns>If the function succeeds, the return value is the requested system metric or configuration setting.
        /// If the function fails, the return value is 0. GetLastError does not provide extended error information.</returns>
        [DllImport(user32_dll)]
        public static extern int GetSystemMetrics(SM sm);

        /// <summary>取得虛擬鍵狀態</summary>
        /// <param name="vKey">Virtual Key Codes.</param>
        /// <returns>
        /// If the function succeeds, the return value specifies whether the key was pressed since the last call to GetAsyncKeyState,
        /// and whether the key is currently up or down. If the most significant bit is set, the key is down,
        /// and if the least significant bit is set, the key was pressed after the previous call to GetAsyncKeyState.
        /// However, you should not rely on this last behavior; for more information, see the Remarks.The return value is zero for the following cases:
        /// The current desktop is not the active desktopThe foreground thread belongs to another process and the desktop does not allow the hook or the journal record.
        /// </returns>
        [DllImport(user32_dll)]
        public static extern ushort GetAsyncKeyState(ushort vKey);

        /// <summary>熱鍵註冊</summary>
        /// <param name="hWnd">A handle to the window</param>
        /// <param name="id">The identifier of the hot key. </param>
        /// <param name="fsModifiers">The keys that must be pressed in combination with the key specified by the uVirtKey parameter in order to generate the WM_HOTKEY message.
        /// The fsModifiers parameter can be a combination of the following values.</param>
        /// <param name="vk">The virtual-key code of the hot key. </param>
        /// <returns>Type: BOOL
        /// If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero. To get extended error information, call GetLastError.
        /// </returns>
        [DllImport(user32_dll)]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, KeyModifiers fsModifiers, ushort vk);

        /// <summary>熱鍵解除註冊</summary>
        /// <param name="hWnd">A handle to the window</param>
        /// <param name="id">The identifier of the hot key. </param>
        /// <returns>Type: BOOL
        /// If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero. To get extended error information, call GetLastError.
        /// </returns>
        [DllImport(user32_dll)]
        static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        /// <summary>退出系統 (需變更程序權限才可生效)</summary>
        /// <param name="uFlags">ExitWindowsEx uFlag</param>
        /// <param name="dwReason"></param>
        /// <returns></returns>
        [DllImport(user32_dll)]
        public static extern int ExitWindowsEx(uint uFlags, uint dwReason);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="text"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [DllImport(user32_dll)]
        public static extern IntPtr GetWindowText(IntPtr hWnd, StringBuilder text, int count = 256);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport(user32_dll)]
        public static extern IntPtr SetFocus(IntPtr hWnd);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport(user32_dll)]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="Msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport(user32_dll)]
        public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
    }
}
