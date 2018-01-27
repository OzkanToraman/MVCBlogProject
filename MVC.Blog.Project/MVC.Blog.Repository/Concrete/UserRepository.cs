using MVC.Blog.Core.Concrete;
using MVC.Blog.DAL.Data;
using MVC.Blog.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MVC.Blog.Repository.Concrete
{
    public class UserRepository : EFRepositoryBase<Kullanici>, IUserRepository
    {
        public UserRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
