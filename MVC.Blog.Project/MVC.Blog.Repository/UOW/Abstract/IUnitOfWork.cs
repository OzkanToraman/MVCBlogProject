using MVC.Blog.Core.Abstract;
using MVC.Blog.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Blog.Repository.UOW.Abstract
{
    public interface IUnitOfWork:IDisposable
    {
        int Commit();
        IRepository<T> GetRepo<T>() where T : EntityBase, new();
        void Dispose(bool disposing);
    }
}
