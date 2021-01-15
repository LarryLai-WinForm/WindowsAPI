using System;
using System.Runtime.InteropServices;

namespace WindowsAPI.dlls
{
    public class advapi32
    {
        const string advapi32_dll = "advapi32.dll";

        [DllImport(advapi32_dll)]
        public static extern bool OpenProcessToken(IntPtr h, int acc, ref IntPtr phtok);

        [DllImport(advapi32_dll, CharSet = CharSet.Unicode)]
        public static extern bool LookupPrivilegeValue(string host, string name, ref long pluid);

        [DllImport(advapi32_dll)]
        public static extern bool AdjustTokenPrivileges
            (IntPtr htok, bool disall, ref TokPriv1Luid newst, int len, IntPtr prev, IntPtr relen);
    }
}
