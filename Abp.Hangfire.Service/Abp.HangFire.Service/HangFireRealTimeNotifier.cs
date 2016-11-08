using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Json;
using Abp.Notifications;
using Castle.Core.Logging;

namespace abp.hangfire.service
{
    public class HangFireRealTimeNotifier : IRealTimeNotifier, ISingletonDependency
    {
        /// <summary>
        /// Reference to the logger.
        /// </summary>
        public ILogger Logger { get; set; }


        public HangFireRealTimeNotifier()
        {
            Logger = NullLogger.Instance;
        }

        /// <inheritdoc/>
        public Task SendNotificationsAsync(UserNotification[] userNotifications)
        {
            Logger.Info("send notification");
            // Send Notification
            SendNotifcation(userNotifications.ToJsonString());
            return Task.FromResult(0);
        }

        private void SendNotifcation(string userNotifications)
        {
            try
            {
                var urlStr = string.Format(ConfigurationManager.AppSettings["ServerUrl"]);

                // Create a request using a URL that can receive a post.
                var request = (HttpWebRequest)WebRequest.Create(urlStr);

                // Set the Method property of the request to POST.
                request.Method = WebRequestMethods.Http.Post;

                request.ContentType = "application/json; charset=utf-8";
                request.Accept = "application/json; charset=utf-8";

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(userNotifications);
                    streamWriter.Flush();
                    streamWriter.Close();

                    var httpResponse = (HttpWebResponse)request.GetResponse();
                    Logger.Info($"Reuqest status -> {httpResponse.StatusCode}");
                    Console.WriteLine(((HttpWebResponse)httpResponse).StatusDescription);
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        Logger.Debug($"Request result -> {result}");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Error -> {ex.Message}");
            }


        }
    }
}