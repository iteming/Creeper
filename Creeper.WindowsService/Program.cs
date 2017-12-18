using System.ServiceProcess;

namespace Creeper.WindowsService
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new CreeperService() 
            };
            ServiceBase.Run(ServicesToRun);

            //Comm.CreeperCapture.DoCapture();
            //Comm.CreeperCapture.NextDayRebate();
        }
    }
}
