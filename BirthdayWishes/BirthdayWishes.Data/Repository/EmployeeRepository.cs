using EmployeeNofitication.Shared.Entities;
using System;
using System.Collections.Generic;

namespace EmployeeNofitication.Data.Respository
{
    public class EmployeeRepository
    {
        public IList<Employee> GetEmployees(DateTime active)
        {
            var employees = new List<Employee>();

            return employees;
        }
    }
}
