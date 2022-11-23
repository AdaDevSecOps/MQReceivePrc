using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MQReceivePrc.Class;
using MQReceivePrc.Class.Standard;
using System;

namespace MQReceivePrc
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    try
        //    {
        //        cConsole.DisableQuickEdit(); //*Net 63-08-18 ป้องกันไม่ให้คลิกเมาส์
        //        cVB.tVB_UniqueTimeCre = DateTime.Now.ToString("yyyyMMddHHmmssfff"); //*Net 63-09-02 วันเวลาที่เปิดโปรแกรม
        //        while (true) //*Net 63-09-02 สำหรับ Restart
        //        {
        //            new cMQReceiver().C_MQRxProcess();
        //        }
        //    }
        //    catch (Exception)
        //    {

        //    }
        //}
        public static void Main(string[] args)
        {
            cVB.tVB_UniqueTimeCre = DateTime.Now.ToString("yyyyMMddHHmmssfff"); //*Net 63-09-02 วันเวลาที่เปิดโปรแกรม
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                //.UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    services.Configure<ConsoleLifetimeOptions>(options =>
                        options.SuppressStatusMessages = true);// Hide ConsoleLife message info
                    services.AddHostedService<cMQReceiverBackgroudService>();
                });
    }
}
