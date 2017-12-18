using System;
using System.Configuration;
using Creeper.WindowsService.Comm;
using System.ServiceProcess;
using System.Threading;
using Timer = System.Timers.Timer;

namespace Creeper.WindowsService
{
    public partial class CreeperService : ServiceBase
    {
        System.Timers.Timer timer1;
        System.Timers.Timer timer2;

        public CreeperService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //抓取数据
            if (timer1 == null)
            {
                int runInterval = Convert.ToInt32(ConfigurationManager.AppSettings["RunInterval"]);
                timer1 = new System.Timers.Timer();
                timer1.Interval = runInterval; // 运行时间间隔 ( 60 * 1000 = 1 分钟)
                timer1.Elapsed += new System.Timers.ElapsedEventHandler(timer1_Elapsed);
                timer1.Enabled = true;
                timer1.Start();
            }
            //隔日返利
            if (timer2 == null)
            {
                timer2 = new System.Timers.Timer();
                timer2.Elapsed += new System.Timers.ElapsedEventHandler(timer2_Elapsed);
                timer2.Enabled = true;
                timer2.Start();
            }
        }
        
        protected override void OnStop()
        {
            timer1.Stop();
            timer2.Stop();
            // 程序停止时需要执行的逻辑，比如关闭订单
        }

        private static int _inTimer1 = 0;
        private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (Interlocked.Exchange(ref _inTimer1, 1) == 0)
            {
                CreeperCapture.DoCapture();
                Interlocked.Exchange(ref _inTimer1, 0);
            }
        }

        private static int _inTimer2 = 0;
        private void timer2_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //判断当前时间是否是配置文件中服务要执行的时间
            if (DateTime.Now.ToString("HH:mm:ss") == ConfigurationManager.AppSettings["RunProfitTime"].ToString())
            {
                // 将时间间隔改为23小时，23小时后重新发生timer2_Elapsed事件。
                ((Timer) sender).Interval = 23 * 60 * 60 * 1000;

                if (Interlocked.Exchange(ref _inTimer2, 1) == 0)
                {
                    //隔日返利
                    CreeperCapture.NextDayRebate();
                    Interlocked.Exchange(ref _inTimer2, 0);
                }
            }
            else
            {
                ((Timer)sender).Interval = 1000;//时间间隔为1秒。 
            }
        }
    }
}
