using MVC.Blog.Repository.UOW.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC.Blog.Core.Abstract;
using MVC.Blog.DAL.Data;
using System.Data.Entity;
using MVC.Blog.Core.Concrete;

namespace MVC.Blog.Repository.UOW.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {

        protected DbContext _dbContext;
        protected bool _disposed = false; 


        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int Commit()
        {
            int resultSet = 0;

            using (_dbContext.Database.BeginTransaction())
            {
                try
                {
                    resultSet = _dbContext.SaveChanges();
                    _dbContext.Database.CurrentTransaction.Commit();
                }
                catch (Exception)
                {
                    _dbContext.Database.CurrentTransaction.Rollback();
                    resultSet = 0;
                    throw;
                }
            }
            return resultSet;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (_disposed==false)
            {
                Dispose();
                _disposed = true;
                _dbContext = null;
            }
        }

        public IRepository<T> GetRepo<T>() where T : EntityBase, new()
        {
            return new EFRepositoryBase<T>(_dbContext);
        }
    }
}
