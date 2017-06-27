using WebAzureTestApplication.Models;
using Newtonsoft.Json.Linq;
using PushSharp.Google;
using System;
using System.Linq;

namespace WebAzureTestApplication.Providers
{
    class PushBrokerConfig : GcmConfiguration
    {
        public PushBrokerConfig()
            : base("AIzaSyCsP6EgS3acsYGCkQ1iA9IWNG9Z2joPQGI")
        {
            this.GcmUrl = "https://fcm.googleapis.com/fcm/send";
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class PushNotificationsBroker : GcmServiceBroker
    {
        /// <summary>
        /// 
        /// </summary>
        private static PushNotificationsBroker _PushNotificationsBroker = new PushNotificationsBroker();

        /// <summary>
        /// 
        /// </summary>
        public static PushNotificationsBroker Broker
        {
            get
            {
                return _PushNotificationsBroker;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private PushNotificationsBroker()
            : base(new PushBrokerConfig())
        {
            this.OnNotificationFailed += _gcmBroker_OnNotificationFailed;
            this.OnNotificationSucceeded += _gcmBroker_OnNotificationSucceeded;
        }

        private void _gcmBroker_OnNotificationSucceeded(GcmNotification notification)
        {
            Console.WriteLine(notification.IsDeviceRegistrationIdValid().ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="exception"></param>
        private void _gcmBroker_OnNotificationFailed(GcmNotification notification, AggregateException exception)
        {
            Console.WriteLine(exception.Message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pushNotifications"></param>
        public void Queue(PushNotification pushNotification)
        {
            var notificationData = new
            {
                //Base notification data
                title = pushNotification.Title,
                body = pushNotification.Body,
                sound = "default"
            };

            var data = JObject.FromObject(notificationData);
            var notification = JObject.FromObject(notificationData);
            notification.Add("click_action", "FCM_PLUGIN_ACTIVITY");

            this.QueueNotification(new GcmNotification()
            {
                To = !string.IsNullOrEmpty(pushNotification.Topic) ? string.Format("/topics/{0}", pushNotification.Topic) : null,
                RegistrationIds = pushNotification.Devices != null && pushNotification.Devices.Any() ? pushNotification.Devices : null,
                Notification = notification,
                Data = data
            });
        }
    }
}