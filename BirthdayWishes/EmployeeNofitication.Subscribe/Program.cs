using EmployeeNofitication.Business.Manager;
using EmployeeNofitication.Data.Proxy;
using EmployeeNofitication.Data.Repository;
using EmployeeNofitication.Shared.Messages;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;

namespace EmployeeNofitication.Subscribe
{
    class Program
    {
        static void Main(string[] args)
        {
            //setup our DI
            var services = new ServiceCollection();
            services.AddScoped<IEmployeeProxy, EmployeeProxy>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IBirthdayWishManager, BirthdayWishManager>();
            services.AddHttpClient();
            var serviceProvider = services.BuildServiceProvider();

            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379");
            ISubscriber sub = redis.GetSubscriber();

            sub.Subscribe("EmployeeNotification").OnMessage(async channelMessage => {
                var message = JsonConvert.DeserializeObject<NotificationMessage>((string)channelMessage.Message);
                // Could use reflection or a strategy pattern for other messages
                var manager = serviceProvider.GetRequiredService<IBirthdayWishManager>();
                var birthdayData = JsonConvert.DeserializeObject<BirthdayWishMessageData>(message.Data);
                await manager.SendWishes(birthdayData);
            });
            Console.ReadLine();
        }
    }
}
