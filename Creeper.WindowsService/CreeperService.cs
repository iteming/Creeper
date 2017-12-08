using Creeper.WindowsService.Comm;
using System.ServiceProcess;
using System.Threading;

namespace Creeper.WindowsService
{
    public partial class CreeperService : ServiceBase
    {
        System.Timers.Timer timer1;

        public CreeperService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //抓取数据
            if (timer1 == null)
            {
                timer1 = new System.Timers.Timer();
                timer1.Interval = 60 * 60 * 1000; // 运行时间间隔 ( 60 * 1000 = 1 分钟)
                timer1.Elapsed += new System.Timers.ElapsedEventHandler(timer1_Elapsed);
                timer1.Enabled = true;
            }
        }

        public void OnStart()
        {
            CreeperCapture.doCapture();
        }

        protected override void OnStop()
        {
            timer1.Stop();
            // 程序停止时需要执行的逻辑，比如关闭订单
        }

        private static int inTimer1 = 0;
        private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (Interlocked.Exchange(ref inTimer1, 1) == 0)
            {
                CreeperCapture.doCapture();
                Interlocked.Exchange(ref inTimer1, 0);
            }
        }
    }
}
