using Microsoft.Azure.ServiceBus;

namespace INFRA.CloudServices.Interface
{
    public interface IServiceBus
    {
        public Task<bool> SendAsync<T>(T obj, string queueName);
        public Task<bool> SendAsync<T>(string serializedObject, string queueName);
        public Task<bool> ScheduleMessageAsync<T>(T obj, string queueName, DateTime scheduledTime);
        public Task<bool> ScheduleMessageAsync<T>(string serializedObject, string queueName, DateTime scheduledTime);
        public QueueClient QueueClient(string queueName);
    }
}