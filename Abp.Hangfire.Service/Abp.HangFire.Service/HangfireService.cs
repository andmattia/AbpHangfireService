using System.ServiceProcess;
using Abp.Castle.Logging.Log4Net;
using Castle.Facilities.Logging;
using Hangfire;


namespace Abp.HangFire.Service
{
    public partial class Service1 : ServiceBase
    {
        private readonly AbpBootstrapper _bootstrapper;

        public Service1()
        {
            InitializeComponent();
            _bootstrapper = AbpBootstrapper.Create<AbpHangFireService>();
            
                _bootstrapper.IocManager.IocContainer
                    .AddFacility<LoggingFacility>(f => f.UseAbpLog4Net()
                        .WithConfig("log4net.config")
                    );
            
        }

        protected override void OnStart(string[] args)
        {

            _bootstrapper.Initialize();
        }

        protected override void OnStop()
        {
            _bootstrapper.Dispose();
        }
    }
}
