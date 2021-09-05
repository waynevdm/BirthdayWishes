using EmployeeNofitication.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeNofitication.Data.Repository
{
    public interface IEmployeeRepository
    {
        Task<IList<Employee>> GetEmployees(DateTime wishOn);
    }
}
