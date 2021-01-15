using System.Runtime.InteropServices;

namespace WindowsAPI.dlls
{
    public class wininet
    {
        const string wininet_dll = "wininet.dll";

        /// <summary>偵測網路連線狀態</summary>
        /// <param name="Description">回傳連線類別</param>
        /// <param name="ReservedValue">保留參數(傳入0即可)</param>
        /// <returns>是否連線</returns>
        [DllImport(wininet_dll)]
        public static extern bool InternetGetConnectedState(int Description, int ReservedValue);
    }
}
