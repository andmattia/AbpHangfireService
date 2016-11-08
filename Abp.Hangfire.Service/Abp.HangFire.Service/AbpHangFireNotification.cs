using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Notifications;
using Castle.Core.Logging;

namespace abp.hangfire.service
{
    /// <summary>
    /// Class used to catch notification
    /// </summary>
    public class AbpHangFireNotification : IRealTimeNotifier, ISingletonDependency
    {
        /// <summary>
        /// Reference to the logger.
        /// </summary>
        public ILogger Logger { get; set; }


        public AbpHangFireNotification()
        {
            Logger = NullLogger.Instance;
        }

        /// <inheritdoc/>
        public Task SendNotificationsAsync(UserNotification[] userNotifications)
        {
            Logger.Info("send notification");

            return Task.FromResult(0);
        }
    }
}