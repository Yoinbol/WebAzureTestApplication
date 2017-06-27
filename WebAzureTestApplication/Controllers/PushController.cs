using WebAzureTestApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using PushSharp;
using PushSharp.Google;

namespace WebAzureTestApplication.Controllers
{
    public class PushController : ApiController
    {
        [AllowAnonymous]
        public void Post(PushNotification pushNotification)
        {
            try
            {
                Providers.PushNotificationsBroker.Broker.Queue(pushNotification);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
