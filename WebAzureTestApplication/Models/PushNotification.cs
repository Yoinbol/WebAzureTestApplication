using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAzureTestApplication.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class PushNotification
    {
        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Topic { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<string> Devices { get; set; }
    }
}