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
        /// 将byte array转化为struct
        /// </summary>
        /// <param name="packet"></param>
        static void ConvertBytesToObject(byte[] packet) {
            GCHandle pinnedPacket = GCHandle.Alloc(packet,GCHandleType.Pinned);
            Message msg = (Message)Marshal.PtrToStructure(pinnedPacket.AddrOfPinnedObject(),typeof(Message));
            pinnedPacket.Free();
        }

        /// <summary>
        /// 将struct转化为byte array
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        static byte[] ConvertObjectToBytes(Message obj) {
            int size = Marshal.SizeOf(obj);
            byte[] arr = new byte[size];

            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(obj,ptr,true);
            Marshal.Copy(ptr,arr,0,size);
            Marshal.FreeHGlobal(ptr);
            return arr;
        }

        static void Main(string[] args) {
            var msgObj = new Message() {
                id = 10, text = "Hello"
            };

            var getByte = ConvertObjectToBytes(msgObj);
            ConvertBytesToObject(getByte);
        }
    }
}
