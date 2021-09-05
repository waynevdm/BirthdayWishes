using EmployeeNofitication.Shared.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeNofitication.Data.Proxy
{
    public class EmployeeProxy
    {
        private readonly IHttpClientFactory _clientFactory;
        public EmployeeProxy(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IList<Employee>> GetEmployees()
        {
            using (var httpClient = _clientFactory.CreateClient())
            {

                using (var request = new HttpRequestMessage
                            {
                                Method = HttpMethod.Get,
                                RequestUri = new Uri("https://interview-assessment-1.realmdigital.co.za/employees"),
                                Content = new StringContent("", Encoding.UTF8, ContentType.json),
                            })
                {

                    var response = await httpClient.SendAsync(request).ConfigureAwait(false);
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var employeeList = JsonConvert.DeserializeObject<IList<Employee>>(responseBody);

                    return employeeList;
                }
            }
        }

        public async Task<IList<int>> GetEmployeeExclusions()
        {
            using (var httpClient = _clientFactory.CreateClient())
            {

                using (var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://interview-assessment-1.realmdigital.co.za/do-not-send-birthday-wishes"),
                    Content = new StringContent("", Encoding.UTF8, ContentType.json),
                })
                {

                    var response = await httpClient.SendAsync(request).ConfigureAwait(false);
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var employeeExclusionList = JsonConvert.DeserializeObject<IList<int>>(responseBody);

                    return employeeExclusionList;
                }
            }
        }
    }
}
