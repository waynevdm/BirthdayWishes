using EmployeeNofitication.Data.Proxy;
using EmployeeNofitication.Data.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmployeeNofitication.Test.Integration
{
    [TestClass]
    public class EmployeeRepositoryTest
    {
        IServiceProvider serviceProvider;

        [TestInitialize()]
        public void Startup()
        {
            var services = new ServiceCollection();
            services.AddScoped<IEmployeeProxy, EmployeeProxy>();
            services.AddHttpClient();
            serviceProvider = services.BuildServiceProvider();
        }

        [TestCleanup()]
        public void Cleanup()
        {
        }

        [TestMethod]
        public async Task GetEmployees()
        {
            try
            {
                IEmployeeProxy proxy = serviceProvider.GetRequiredService<IEmployeeProxy>();
                var employeeRepo = new EmployeeRepository(proxy);
                var employeeList = await employeeRepo.GetEmployees(new DateTime(2021,08,11)).ConfigureAwait(false);
                Assert.IsNotNull(employeeList);
                Assert.AreEqual(employeeList.Count, 1);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
