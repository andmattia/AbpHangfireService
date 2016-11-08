using System.Configuration;
using System.Data.Entity;
using System.Reflection;
using abp.hangfire.service;
using Abp.Configuration.Startup;
using Abp.Hangfire;
using Abp.Hangfire.Configuration;
using Abp.Modules;
using Abp.Notifications;
using Hangfire;


namespace Abp.HangFire.Service
{
    [DependsOn(typeof(AbpHangfireModule),
        typeof(AbpHangfireModule)

        )]
    public class AbpHangFireService : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<yourDbContext>(null);

            Configuration.BackgroundJobs.UseHangfire(configuration =>
            {
                configuration.GlobalConfiguration.UseSqlServerStorage("Default");

                var options = new BackgroundJobServerOptions
                {
                    Queues = ConfigurationManager.AppSettings["Queues"].Split(new char[] { ';' }),
                    ServerName = ConfigurationManager.AppSettings["ServerName"]
            };

                configuration.Server = new BackgroundJobServer(options);
            });

        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        public override void PostInitialize()
        {
            Configuration.ReplaceService<IRealTimeNotifier, AbpHangFireNotification>();
        }
    }
}