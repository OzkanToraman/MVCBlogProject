using MVC.Blog.Core.Abstract;
using MVC.Blog.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data.Entity;

namespace MVC.Blog.Core.Concrete
{
    public class EFRepositoryBase<T> : IRepository<T>
        where T : EntityBase,new()
    {
        protected DbContext _dbContext;
        protected DbSet<T> _dbSet;

        public EFRepositoryBase(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }


        public void Add(T model)
        {
            _dbSet.Add(model);
        }

        public bool Any(Expression<Func<T, bool>> lambda)
        {
            return _dbSet.Any(lambda);
        }

        public void Delete(int id)
        {
            var model = _dbSet.Find(id);
            _dbSet.Remove(model);
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id); 
        }

        public List<T> GetList()
        {
            return _dbSet.ToList();
        }

        public void Update(T model)
        {
            _dbContext.Entry(model).State = EntityState.Modified;
        }

        public IEnumerable<T> Where(Expression<Func<T, bool>> lambda)
        {
            return _dbSet.Where(lambda).AsEnumerable();
        }

        public IQueryable<T> WhereByQuery(Expression<Func<T, bool>> lambda)
        {
            return _dbSet.Where(lambda).AsQueryable();
        }
    }
}
