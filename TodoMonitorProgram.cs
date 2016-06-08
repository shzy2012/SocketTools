using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TodoMonitorProgram {
    class Program {

        static int count = 0;
        static void Main(string[] args) {

            //定时器
            Timer timer;
            timer = new Timer();
            timer.Interval = 10000;
            timer.Elapsed += new ElapsedEventHandler(TimerEventProcessor);
            timer.Start();

            //防止程序结束
            while(true) {

            }
        }

        /// <summary>
        /// 判断程序是否开启
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool IsProcessOpen(string name) {
            foreach(Process clsProcess in Process.GetProcesses()) {
                if(clsProcess.ProcessName.Contains(name)) {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 开启程序
        /// </summary>
        /// <param name="prgrameName"></param>
        /// <param name="programePath"></param>
        /// <returns></returns>
        public static bool MonitorProgram(string prgrameName, string programePath) {
            bool re = IsProcessOpen(prgrameName);
            if(!re) {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = programePath;
                Process.Start(startInfo);
            }

            return true;
        }

        /// <summary>
        /// 定时器执行事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void TimerEventProcessor(object sender, ElapsedEventArgs e) {
            string path = @"C:\Users\joey\Source\Repos\TodoNN\TodoGetDataFromWeb\TodoHttp\bin\Release\TodoHttp.exe";
            if(MonitorProgram("TodoHttp", path)) {
                Console.WriteLine(count++);
            }
        }
    }
}
