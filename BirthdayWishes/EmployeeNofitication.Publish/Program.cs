using EmployeeNofitication.Shared.Messages;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;

namespace EmployeeNofitication.Publish
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
            ISubscriber sub = redis.GetSubscriber();
            var message = new NotificationMessage
            {
                Manager = "EmployeeNofitication.Business.Manager.BirthdayWishManager",
                Data = JsonConvert.SerializeObject(new BirthdayWishMessageData
                {
                    DateOfBirthday = new DateTime(2021, 08, 11)
                })
            };

            var msg = JsonConvert.SerializeObject(message);
            sub.Publish("EmployeeNotification", msg);
        }
    }
}
