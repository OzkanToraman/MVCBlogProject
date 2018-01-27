using MVC.Blog.BLL.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Blog.BLL.Services.Concrete
{
    public class Encryptor : IEncryptor
    {
        public string Hash(string plainText)
        {
            MD5CryptoServiceProvider myprovider = new MD5CryptoServiceProvider();

            byte[] data = myprovider.ComputeHash(Encoding.UTF8.GetBytes(plainText));

            StringBuilder s = new StringBuilder();

            foreach (var item in data)
            {
                //hexadecimal 16lık sayı sistemi
                s.Append(item.ToString("X2"));
            }

            return s.ToString();
        }
    }
}
