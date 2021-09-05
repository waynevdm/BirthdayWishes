using EmployeeNofitication.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeNofitication.Data.Proxy
{
    public interface IEmployeeProxy
    {
        Task<IList<Employee>> GetEmployees();
        Task<IList<int>> GetEmployeeExclusions();
    }
}
