
using System;
using System.Text;
using System.Security.Cryptography;
using System.Runtime.InteropServices;

namespace Legends.Core.Cryptography
{
    public unsafe class BlowFishCS
    {
        private const string LIB = "libintlib.dll";
        [DllImport(LIB)]
        public static extern BlowFish* BlowFishCreate(byte* ucKey, IntPtr n);
        [DllImport(LIB)]
        public static extern void Encrypt1(BlowFish* handle, byte* buf, IntPtr n, int iMode = (int)BlowfishMode.ECB);

        [DllImport(LIB)]
        public static extern void Decrypt1(BlowFish* handle, byte* buf, IntPtr n, int iMode = (int)BlowfishMode.ECB);
        [DllImport(LIB)]

        public static extern void DestroyHandle(void* handle);
        [DllImport(LIB)]
        public static extern ulong Encrypt2(void* handle, ulong buf);
        [DllImport(LIB)]
        public static extern ulong Decrypt2(void* handle, ulong buf);
        [DllImport(LIB)]
        public static extern void Encrypt3(void* handle, byte* @in, byte* @out, IntPtr n, int iMode = (int)BlowfishMode.ECB);
        [DllImport(LIB)]
        public static extern void Decrypt3(void* handle, byte* @in, byte* @out, IntPtr n, int iMode = (int)BlowfishMode.ECB);
    }

    public struct BlowFish
    {

    }
    public enum BlowfishMode
    {
        ECB = 0,
        CBC = 1,
        CFB = 2
    };
}
