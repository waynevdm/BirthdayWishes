using System;

namespace EmployeeNofitication.Shared.Entities
{
    public class Employee
    {
        public int id { get; set; }
        public string name { get; set; }
        public string lastname { get; set; }
        public DateTime dateOfBirth { get; set; }
        public DateTime employmentStartDate { get; set; }
        public DateTime? employmentEndDate { get; set; }
        public DateTime? lastNotification { get; set; }
    }
}
