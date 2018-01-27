using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Blog.BLL.Services.Abstract
{
    public interface IEncryptor
    {
        string Hash(string plainText);
    }
}
