using System.Runtime.InteropServices;

namespace LukeWarmCorners.Core.Native
{
    [StructLayout(LayoutKind.Sequential)]
    struct POINT
    {
        public Int32 X;
        public Int32 Y;
    }
}
