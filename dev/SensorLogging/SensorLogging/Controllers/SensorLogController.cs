using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using SensorLogging.DataObjects;
using SensorLogging.Models;
using System.Net.Http;
using System.Net.Http.Headers;

namespace SensorLogging.Controllers
{
    public class SensorLogController : TableController<SensorLog>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<SensorLog>(context, Request, Services);
        }

        // GET tables/SensorLog
        public IQueryable<SensorLog> GetAllSensorLog()
        {
            return Query(); 
        }

        // GET tables/SensorLog/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<SensorLog> GetSensorLog(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/SensorLog/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<SensorLog> PatchSensorLog(string id, Delta<SensorLog> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/SensorLog
        public async Task<IHttpActionResult> PostSensorLog(SensorLog item)
        {
            SensorLog current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/SensorLog/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteSensorLog(string id)
        {
             return DeleteAsync(id);
        }

        public Task<SensorLog> Post(string deviceId)
        {
            var queryPairs = this.Request.GetQueryNameValuePairs();
            string pressure = queryPairs.FirstOrDefault(q => q.Key == "pressure").Value;
            string humidity = queryPairs.FirstOrDefault(q => q.Key == "humidity").Value;
            string temperature = queryPairs.FirstOrDefault(q => q.Key == "temperature").Value;
            string magnetoX = queryPairs.FirstOrDefault(q => q.Key == "magneto_x").Value;
            string magnetoY = queryPairs.FirstOrDefault(q => q.Key == "magneto_y").Value;
            string magnetoZ = queryPairs.FirstOrDefault(q => q.Key == "magneto_z").Value;
            string accelerationX = queryPairs.FirstOrDefault(q => q.Key == "acceleration_x").Value;
            string accelerationY = queryPairs.FirstOrDefault(q => q.Key == "acceleration_y").Value;
            string accelerationZ = queryPairs.FirstOrDefault(q => q.Key == "acceleration_z").Value;
            string gyroX = queryPairs.FirstOrDefault(q => q.Key == "gyro_x").Value;
            string gyroY = queryPairs.FirstOrDefault(q => q.Key == "gyro_y").Value;
            string gyroZ = queryPairs.FirstOrDefault(q => q.Key == "gyro_z").Value;

            SensorLog entity = new SensorLog(deviceId)
            {
                Pressure = (pressure == null) ? (float?)null : float.Parse(pressure),
                Humidity = (pressure == null) ? (float?)null : float.Parse(humidity),
                Temperature = (pressure == null) ? (float?)null : float.Parse(temperature),
                MagnetoX = (pressure == null) ? (int?)null : int.Parse(magnetoX),
                MagnetoY = (pressure == null) ? (int?)null : int.Parse(magnetoY),
                MagnetoZ = (pressure == null) ? (int?)null : int.Parse(magnetoZ),
                AccelerationX = (pressure == null) ? (int?)null : int.Parse(accelerationX),
                AccelerationY = (pressure == null) ? (int?)null : int.Parse(accelerationY),
                AccelerationZ = (pressure == null) ? (int?)null : int.Parse(accelerationZ),
                GyroX = (pressure == null) ? (int?)null : int.Parse(gyroX),
                GyroY = (pressure == null) ? (int?)null : int.Parse(gyroY),
                GyroZ = (pressure == null) ? (int?)null : int.Parse(gyroZ)
            };
            var insertEntity = InsertAsync(entity);
            return insertEntity;
        }

    }
}
