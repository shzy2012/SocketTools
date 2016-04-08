using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
namespace MappingBytesArrayToStructure {

    [StructLayout(LayoutKind.Sequential,Pack = 1)]
    struct Message {
        public int id;
        [MarshalAs(UnmanagedType.ByValTStr,SizeConst = 6)]
        public string text;
    }
    class Program {

        /// <summary>
        /// 将struct转化为bytes
        /// </summary>
        /// <param name="packet"></param>
        static void ConvertBytesToObject(byte[] packet) {
            GCHandle pinnedPacket = GCHandle.Alloc(packet,GCHandleType.Pinned);
            Message msg = (Message)Marshal.PtrToStructure(pinnedPacket.AddrOfPinnedObject(),typeof(Message));
            pinnedPacket.Free();
        }

        static void Main(string[] args) {
            var id = BitConverter.GetBytes(123456);
            var msg = Encoding.ASCII.GetBytes("abcde");
            byte[] packet = id.Concat(msg).ToArray();
            ConvertBytesToObject(packet);

            // 将bytes转化为 struct
            Message p = (Message)Marshal.PtrToStructure(Marshal.UnsafeAddrOfPinnedArrayElement(packet,10),typeof(Message));
        }
    }
}
