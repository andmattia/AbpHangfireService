using System.Configuration;
using System.Data.Entity;
using System.Reflection;
using Abp.Hangfire;
using Abp.Hangfire.Configuration;
using Abp.Modules;
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
    }
}