using EmployeeNofitication.Data.Proxy;
using EmployeeNofitication.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeNofitication.Data.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IEmployeeProxy _employeeProxy;
        public EmployeeRepository(IEmployeeProxy employeeProxy)
        {
            _employeeProxy = employeeProxy;
        }

        public async Task<IList<Employee>> GetEmployees(DateTime wishOn)
        {
            var employeeList = await _employeeProxy.GetEmployees();
            var employeeExcludeList = await _employeeProxy.GetEmployeeExclusions();

            var employeeWishList = from employee in employeeList
                                   where employeeExcludeList.All(pe => pe != employee.id)
                                   && employee.dateOfBirth.Day == wishOn.Day
                                   && employee.dateOfBirth.Month == wishOn.Month
                                   && employee.employmentStartDate <= wishOn
                                   && (employee.employmentEndDate == null
                                   || employee.employmentEndDate.Value > wishOn)
                                   && (employee.lastNotification == null
                                   || employee.lastNotification != wishOn)
                                   select employee;

            return employeeWishList.ToList();
        }
    }
}
