using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Blog.BLL.Services.Abstract
{
    public interface IMailMessage
    {
        bool SendMessage(string subject, string message);
        Task<bool> SendMessageAsync(string subject, string message);
    }
}
