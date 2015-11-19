using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.NotificationHubs;

namespace SensorWebJob.Models
{
    public class Notifications
    {
        public static Notifications Instance = new Notifications();

        public NotificationHubClient Hub { get; set; }

        private Notifications()
        {
            Hub = NotificationHubClient.CreateClientFromConnectionString("Endpoint=sb://iot-devmgmthub-ns.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=Y6YiQ/Egvsfam3cqvd/fIs/lUI1unkk/bw5+2uCreDs=",
                                                                         "iot-devmgmthub");
        }
    }
}
