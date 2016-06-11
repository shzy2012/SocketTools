using System;
using System.Diagnostics;
using System.Timers;

namespace TodoMonitorProgram {
    /// <summary>
    /// 定时监控，开启程序
    /// </summary>
    public class MonitorProgram {
        //定时器
        private static Timer timer;

        //设置默认监视时间
        private readonly int interval = 1000;

        private static int startCounter = 1;

        /// <summary>
        /// 程序名称
        /// </summary>
        public string ProgramName { get; set; }

        /// <summary>
        /// 程序路径
        /// </summary>
        public string ProgramPath { get; set; }

        /// <summary>
        /// 检查程序是否开启
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool IsProcessOpen(string name) {
            foreach(Process clsProcess in Process.GetProcesses()) {
                if(clsProcess.ProcessName.Contains(name)) {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 定时器执行事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerEventProcessor(object sender, ElapsedEventArgs e) {
            if(!Start()) {
                Console.WriteLine("开启 {0} 次", startCounter++);
            }
        }

        public MonitorProgram() {
            //初始化定时器
            if(timer == null) {
                timer = new Timer();
                timer.Interval = interval;
                timer.Elapsed += new ElapsedEventHandler(TimerEventProcessor);
                timer.Start();
            }
        }

        /// <summary>
        /// 初始化定时器
        /// </summary>
        public MonitorProgram(string progrName, string progrPath) {
            this.ProgramName = progrName;
            this.ProgramPath = progrPath;
            //初始化定时器
            if(timer == null) {
                timer = new Timer();
                timer.Interval = interval;
                timer.Elapsed += new ElapsedEventHandler(TimerEventProcessor);
                timer.Start();
            }
        }

        /// <summary>
        /// 开启程序健康
        /// </summary>
        /// <param name="prgrameName"></param>
        /// <param name="programePath"></param>
        /// <returns></returns>
        private bool Start() {
            bool isOpened = IsProcessOpen(this.ProgramName);
            if(!isOpened) {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = this.ProgramPath;
                Process.Start(startInfo);
            }

            return isOpened;
        }

    }

    class Program {
        static void Main(string[] args) {
            string programName = "TotoA";
            string programPath = @"D:\Workplace\VS2013\Projects\TodoRedisCache\TotoA\bin\Debug\TotoA.exe";
            MonitorProgram monitor = new MonitorProgram(programName, programPath);
            //monitor.Start();

            //防止程序结束
            Console.ReadLine();
        }
    }
}
