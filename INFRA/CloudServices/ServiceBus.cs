using INFRA.CloudServices.Interface;
using Microsoft.Azure.ServiceBus;
using System.Text;

namespace INFRA.CloudServices
{
    public class BusService : IServiceBus
    {
        private string connectionString = string.Empty;
        private QueueClient _queueClient;

        public BusService(string connString)
        {
            connectionString = connString;
        }

        #region PRODUCER :::::::
        public Task<bool> SendAsync<T>(T obj, string queueName)
        {
            try
            {
                _queueClient = new QueueClient(connectionString, queueName);

                var jsonObj = Newtonsoft.Json.JsonConvert.SerializeObject(obj);

                var mBytes = Encoding.UTF8.GetBytes(jsonObj);

                var message = new Message(mBytes);

                _queueClient.SendAsync(message).Wait();

                return Task.FromResult(true);
            }
            catch
            {
                throw;
            }
        }


        public Task<bool> SendAsync<T>(string serializedObject, string queueName)
        {
            try
            {
                _queueClient = new QueueClient(connectionString, queueName);

                var mBytes = Encoding.UTF8.GetBytes(serializedObject);

                var message = new Message(mBytes);

                _queueClient.SendAsync(message).Wait();

                return Task.FromResult(true);
            }
            catch
            {
                throw;
            }
        }

        public Task<bool> ScheduleMessageAsync<T>(T obj, string queueName, DateTime scheduledTime)
        {
            try
            {
                _queueClient = new QueueClient(connectionString, queueName);

                var jsonObj = Newtonsoft.Json.JsonConvert.SerializeObject(obj);

                var mBytes = Encoding.UTF8.GetBytes(jsonObj);

                var message = new Message(mBytes);

                _queueClient.ScheduleMessageAsync(message, scheduledTime);

                return Task.FromResult(true);
            }
            catch
            {
                throw;
            }
        }

        public Task<bool> ScheduleMessageAsync<T>(string serializedObject, string queueName, DateTime scheduledTime)
        {
            try
            {
                _queueClient = new QueueClient(connectionString, queueName);

                var mBytes = Encoding.UTF8.GetBytes(serializedObject);

                var message = new Message(mBytes);

                _queueClient.ScheduleMessageAsync(message, scheduledTime);

                return Task.FromResult(true);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        public QueueClient QueueClient(string queueName)
        {
            var client = new QueueClient(connectionString, queueName,
                ReceiveMode.ReceiveAndDelete);
            return client;
        }


        #region PRIVATE :::::::       


        #endregion


    }
}
