using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SensorGateway.Controllers
{
    public class ConnectController : ApiController
    {
        public string Get()
        {
            try
            {
                var deviceId = this.Request.GetQueryNameValuePairs().First(q => q.Key == "device_id").Value;
                if (deviceId == null)
                {
                    return "Hello from Azure to NULL";
                }
                else
                {
                    return "Hello from Azure to device; " + deviceId;
                }
            } catch(Exception ex)
            {
                return "Hellow from Azure ignoring an error; " + ex.ToString();
            }
        }
        
        public string Get(string category)
        {
            return "Hello from Azure category; " + category;
        }
    }
}
