using EmployeeNofitication.Data.Proxy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmployeeNofitication.Test.Integration
{
    [TestClass]
    public class EmployeeProxyTest
    {
        IServiceCollection services;

        [TestInitialize()]
        public void Startup()
        {
            services = new ServiceCollection();
            services.AddHttpClient();
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
                IHttpClientFactory factory = services.BuildServiceProvider().GetRequiredService<IHttpClientFactory>();
                var employeeProxy = new EmployeeProxy(factory);
                var employeeList = await employeeProxy.GetEmployees().ConfigureAwait(false);
                Assert.IsNotNull(employeeList);
                Assert.AreEqual(employeeList.Count, 130);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public async Task GetEmployeeExclusions()
        {
            try
            {
                IHttpClientFactory factory = services.BuildServiceProvider().GetRequiredService<IHttpClientFactory>();
                var employeeProxy = new EmployeeProxy(factory);
                var employeeExclusionList = await employeeProxy.GetEmployeeExclusions().ConfigureAwait(false);
                Assert.IsNotNull(employeeExclusionList);
                Assert.AreEqual(employeeExclusionList.Count, 3);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
