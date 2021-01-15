using System;
using System.Text;
using System.Runtime.InteropServices;

namespace WindowsAPI.dlls
{
    public class kernel32
    {
        const string kernel32_dll = "kernel32.dll";

        /// <summary>取得開機至今經過的ms(誤差15ms左右)</summary>
        /// <returns>ms</returns>
        [DllImport(kernel32_dll)]
        public static extern uint GetTickCount();

        /// <summary>取得現行程序</summary>
        /// <returns>Process</returns>
        [DllImport(kernel32_dll)]
        public static extern IntPtr GetCurrentProcess();

        /// <summary>
        /// MoveFileEx
        /// </summary>
        /// <param name="lpExistingFileName"></param>
        /// <param name="lpNewFileName"></param>
        /// <param name="dwFlags"></param>
        /// <returns></returns>
        [DllImport(kernel32_dll)]
        public static extern bool MoveFileEx(string lpExistingFileName, string lpNewFileName, MoveFileFlags dwFlags);

        #region INI檔案操作


        #region 讀取檔案中所有SectionName

        //以下數種方法,目前測試使用IntPtr操作效能較佳
        /// <summary>讀取檔案中所有SectionName</summary>
        /// <param name="lpszReturnBuffer">接收暫存(IntPtr)</param>
        /// <param name="nSize">接收暫存大小(sizeof(byte))</param>
        /// <param name="lpFileName">檔案路徑</param>
        /// <returns>接收的資料大小(sizeof(byte))</returns>
        [DllImport(kernel32_dll)]
        public static extern uint GetPrivateProfileSectionNames(IntPtr lpszReturnBuffer, uint nSize, string lpFileName);

        /// <summary>讀取檔案中所有SectionName 回傳接收到的byte大小</summary>
        /// <param name="lpszReturnBuffer">接收暫存(byte[])</param>
        /// <param name="nSize">接收暫存大小(sizeof(byte))</param>
        /// <param name="lpFileName">檔案路徑</param>
        /// <returns>接收的資料大小(sizeof(byte))</returns>
        [DllImport(kernel32_dll)]
        public static extern uint GetPrivateProfileSectionNames(byte[] lpszReturnBuffer, uint nSize, string lpFileName);

        #endregion

        #region 讀取指定Section資料內容

        //以下數種方法,目前測試使用IntPtr操作效能較佳
        /// <summary>讀取指定Section資料內容</summary>
        /// <param name="lpAppName">section</param>
        /// <param name="lpszReturnBuffer">接收暫存(IntPtr)</param>
        /// <param name="nSize">接收暫存大小(sizeof(byte))</param>
        /// <param name="lpFileName">檔案路徑</param>
        /// <returns>接收的資料大小(sizeof(byte))</returns>
        [DllImport(kernel32_dll)]
        public static extern uint GetPrivateProfileSection(string lpAppName, IntPtr lpReturnedString, uint nSize, string lpFileName);

        /// <summary>讀取指定Section資料內容</summary>
        /// <param name="lpAppName">section</param>
        /// <param name="lpszReturnBuffer">接收暫存(byte[])</param>
        /// <param name="nSize">接收暫存大小(sizeof(byte))</param>
        /// <param name="lpFileName">檔案路徑</param>
        /// <returns>接收的資料大小(sizeof(byte))</returns>
        [DllImport(kernel32_dll, CharSet = CharSet.Unicode)]
        public static extern uint GetPrivateProfileSection(string lpAppName, byte[] lpReturnedString, uint nSize, string lpFileName);

        /// <summary>讀取指定Section資料內容</summary>
        /// <param name="lpAppName">section</param>
        /// <param name="lpszReturnBuffer">接收暫存(string)</param>
        /// <param name="nSize">接收暫存大小(sizeof(byte))</param>
        /// <param name="lpFileName">檔案路徑</param>
        /// <returns>接收的資料大小(sizeof(byte))</returns>
        [DllImport(kernel32_dll, CharSet = CharSet.Unicode)]
        public static extern uint GetPrivateProfileSection(string lpAppName, string lpReturnedString, uint nSize, string lpFileName);

        #endregion

        #region 寫入指定Section資料內容

        /// <summary>
        /// WritePrivateProfileSection
        /// 一次寫入整個section資料;
        /// 資料格式為"key1=value1'\0'key2=value2'\0'key3=value3";
        /// lpString傳入""或null可清空或刪除指定section
        /// </summary>
        /// <param name="lpAppName">section()</param>
        /// <param name="lpString">data 傳入""或null可清空或刪除指定section</param>
        /// <param name="lpFileName">檔案路徑</param>
        /// <returns>是否寫入成功</returns>
        [DllImport(kernel32_dll, CharSet = CharSet.Unicode)]
        public static extern bool WritePrivateProfileSection(string lpAppName, string lpString, string lpFileName);

        #endregion

        #region 讀寫指定value

        /// <summary>讀取int</summary>
        /// <param name="lpAppName">section</param>
        /// <param name="lpKeyName">key</param>
        /// <param name="nDefault">預設的回傳值(發生錯誤時回傳此參數)</param>
        /// <param name="lpFileName">檔案路徑</param>
        /// <returns>接收的數值資料</returns>
        [DllImport(kernel32_dll, CharSet = CharSet.Unicode)]
        public static extern int GetPrivateProfileInt(string lpAppName, string lpKeyName, int nDefault, string lpFileName);

        /// <summary>讀取string</summary>
        /// <param name="lpAppName">section</param>
        /// <param name="lpKeyName">key</param>
        /// <param name="lpDefault">預設的回傳值(發生錯誤時回傳此參數)</param>
        /// <param name="lpReturnedString">接收暫存(StringBuilder)</param>
        /// <param name="nSize">接收暫存大小</param>
        /// <param name="lpFileName">檔案路徑</param>
        /// <returns>接收的字串長度</returns>
        [DllImport(kernel32_dll, CharSet = CharSet.Unicode)]
        public static extern uint GetPrivateProfileString(
            string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, uint nSize, string lpFileName);

        /// <summary>
        /// WritePrivateProfileString
        /// 寫入string
        /// </summary>
        /// <param name="lpAppName">section</param>
        /// <param name="lpKeyName">key</param>
        /// <param name="lpString">value</param>
        /// <param name="lpFileName">檔案路徑</param>
        /// <returns>是否寫入成功</returns>
        [DllImport(kernel32_dll, CharSet = CharSet.Unicode)]
        public static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);

        #endregion


        #endregion
    }
}
