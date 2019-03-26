using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
namespace Utility
{
    public enum TimerManagerType
    { 
        Run,
        Stop
    }
    public class TimerManager:IDisposable
    {        
        /// <summary>
        /// 定时器
        /// </summary>
        private Timer iTimer;
        /// <summary>
        /// 设置启动时间间隔
        /// </summary>
        public int DueTime
        {
            set
            {
                if (iTimer == null)
                {
                    iTimer.Stop();
                    iTimer.Interval = value;
                    iTimer.Start();
                }
            }
        }
        Action ac;
        /// <summary>
        /// 构造函数
        /// </summary>
        public TimerManager(int dueTime,Action _ac)
        {
            ac = _ac;
            if (iTimer == null && dueTime>0)
            {
                iTimer = new System.Timers.Timer();
                iTimer.Interval = dueTime;
                iTimer.Elapsed += timer_Elapsed;
            }
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            iTimer.Stop();
            ac();
        }
        /// <summary>
        /// 开始
        /// </summary>
        public void Start()
        {
            if (iTimer != null)
            {
                iTimer.Start();
            }
        }
        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            if (iTimer != null)
            {
                iTimer.Stop();
            }
        }

        public void Dispose()
        {
            if (iTimer != null)
            {
                iTimer.Dispose();
            }
        }
    }
}
