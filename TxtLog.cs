using System.IO;
using System.Threading;

namespace Project.Utility {
    public class TxtLog {

        public const string PathStr = @"d:\WatchServerLog\log.txt";
        private static ReaderWriterLockSlim _readWriteLock = new ReaderWriterLockSlim();
        public TxtLog() {
            if(!File.Exists(PathStr)) {
                Directory.CreateDirectory(Path.GetDirectoryName(PathStr));
                File.Create(PathStr);
            }
        }
        /// <summary>
        /// 性能测试日志
        /// 打印到TxT文件
        /// </summary>
        /// <param name="text"></param>
        public void Log(string text) {
            _readWriteLock.EnterWriteLock();
            using(StreamWriter sw = new StreamWriter(PathStr, true)) {
                sw.WriteLine(text);
                sw.Close();
            }
            _readWriteLock.ExitWriteLock();
        }
    }
}
