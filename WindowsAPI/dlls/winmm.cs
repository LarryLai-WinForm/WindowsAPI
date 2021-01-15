using System.Runtime.InteropServices;

namespace WindowsAPI.dlls
{
    public class winmm
    {
        const string winmm_dll = "winmm.dll";

        /// <summary>取得開機至今經過的ms(誤差可達1ms)</summary>
        /// <returns>ms</returns>
        [DllImport(winmm_dll)]
        public static extern uint timeGetTime();

        /// <summary>開始自訂timeGetTime()精準度</summary>
        /// <param name="t">精準度(ms)</param>
        [DllImport(winmm_dll)]
        public static extern void timeBeginPeriod(int t);

        /// <summary>結束自訂timeGetTime()精準度</summary>
        /// <param name="t">精準度(ms)</param>
        [DllImport(winmm_dll)]
        public static extern void timeEndPeriod(int t);
    }
}
