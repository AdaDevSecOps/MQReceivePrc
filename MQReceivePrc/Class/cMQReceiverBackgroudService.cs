using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MQReceivePrc.Class
{
    public class cMQReceiverBackgroudService : BackgroundService
    {
        //private IConnection oC_Connection;
        //private IModel oC_Channel;
        //private string tC_consumerTag;
        //private readonly ILogger<cMQReceiverBackgroudService> oC_Logger;
        private readonly IHostApplicationLifetime oC_Lifetime;
        //public cMQReceiverBackgroudService(ILogger<cMQReceiverBackgroudService> poLogger, IHostApplicationLifetime poLifetime)
        //{
        //    oC_Logger = poLogger;
        //    oC_Lifetime = poLifetime;
        //}
        public cMQReceiverBackgroudService(IHostApplicationLifetime poLifetime)
        {
            oC_Lifetime = poLifetime;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            cMQReceiver oMQReceiver = new cMQReceiver();
            oMQReceiver.C_MQRxProcess();
            return Task.CompletedTask;
        }
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            oC_Lifetime.ApplicationStarted.Register(OnStarted);
            oC_Lifetime.ApplicationStopping.Register(new Action(() => { OnStopping(cancellationToken); }));
            oC_Lifetime.ApplicationStopped.Register(OnStopped);

            return base.StartAsync(cancellationToken);
        }
        //public override Task StopAsync(CancellationToken cancellationToken)
        //{

        //    oC_Channel?.BasicCancel(tC_consumerTag);
        //    oC_Channel?.Close();
        //    oC_Connection?.Close();
        //    return base.StopAsync(cancellationToken);
        //}

        private void OnStarted()
        {
            //_logger.LogInformation("OnStarted has been called.");
            // Perform post-startup activities here
        }

        private void OnStopping(CancellationToken poCancellationToken)
        {
            //_logger.LogInformation("OnStopping has been called.");
            // Perform on-stopping activities here

        }

        private void OnStopped()
        {
            //_logger.LogInformation("OnStopped has been called.");
            // Perform post-stopped activities here
        }
    }
}
