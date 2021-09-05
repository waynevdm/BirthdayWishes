using EmployeeNofitication.Data.Repository;
using EmployeeNofitication.Shared.Messages;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeNofitication.Business.Manager
{
    public class BirthdayWishManager : IBirthdayWishManager
    {
        private readonly IEmployeeRepository _employeeRepository;
        public BirthdayWishManager(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task SendWishes(BirthdayWishMessageData data)
        {
            var employeeeWishList = await _employeeRepository.GetEmployees(data.DateOfBirthday);
            StringBuilder build = new StringBuilder();
            build.AppendLine("To the birthday people");
            build.AppendLine(" ");
            foreach (var employee in employeeeWishList)
            {
                build.AppendLine($"{employee.name} {employee.lastname}");
            }
            build.AppendLine(" ");
            build.AppendLine("Wishing you the best on your birthday and everything good in the year ahead. Hope your day is filled with happiness. Wishing you a happy birthday and a wonderful year. Our whole team is wishing you the happiest of birthdays.");
            SendEmail(build.ToString());
        }
        private void SendEmail(string body)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("email", "password"),
                EnableSsl = true,
            };

            smtpClient.Send("Management", "all@mycompany.com", "HappyBirthday!", body);
        }
    }
}
