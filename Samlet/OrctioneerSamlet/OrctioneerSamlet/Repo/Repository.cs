using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VareDatabase.Models;

namespace VareDatabase.Repo
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            Context = context;
        }
        public virtual TEntity Get(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }
        public virtual IEnumerable<TEntity> Get()
        {
            return Context.Set<TEntity>().ToList();
        }
        public virtual void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }
        public virtual void Delete(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }
        public virtual void Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
