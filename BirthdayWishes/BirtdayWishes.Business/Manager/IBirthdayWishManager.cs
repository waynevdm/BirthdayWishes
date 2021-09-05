using EmployeeNofitication.Shared.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeNofitication.Business.Manager
{
    public interface IBirthdayWishManager
    {
        Task SendWishes(BirthdayWishMessageData data);
    }
}
