using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Abp.Castle.Logging.Log4Net;
using Abp.Dependency;
using Abp.Events.Bus;
using Castle.Facilities.Logging;
using Hangfire;
using Hangfire.Windsor;
using Microsoft.Owin;
using Microsoft.Owin.Hosting;


namespace Abp.HangFire.Service
{
    public partial class Service1 : ServiceBase
    {
        private readonly AbpBootstrapper bootstrapper;
        private BackgroundJobServer _server;
        public Service1()
        {
            InitializeComponent();
            bootstrapper = AbpBootstrapper.Create<AbpHangFireService>();
            
                bootstrapper.IocManager.IocContainer
                    .AddFacility<LoggingFacility>(f => f.UseAbpLog4Net()
                        .WithConfig("log4net.config")
                    );
            
        }

        protected override void OnStart(string[] args)
        {

            bootstrapper.Initialize();
        }

        protected override void OnStop()
        {
            bootstrapper.Dispose();
        }
    }
}
