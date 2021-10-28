using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PersonellerUoW.Models.Entities;
using PersonellerUoW.Services.Interfaces;

namespace PersonellerUoW.Services
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly Context context;
        public DbSet<TEntity> table;
        public Repository(Context _context)
        {
            context = _context;
            table = _context.Set<TEntity>();
            context.ChangeTracker.LazyLoadingEnabled = false;

        }

        public void Add(TEntity entity)
        {
            table.Add(entity);  
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            table.AddRange(entities);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includes)
        {
            if (includes != null)
            {
                IQueryable<TEntity> query = table;
                query = includes.Aggregate(query,
                (current, include) => current.Include(include));
                return query.Where(filter);
            }
            else
            {
                return table.Where(filter);
            }
            
        }

        public TEntity Get(int id)
        {
            return table.Find(id);
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes)
        {
            if (includes != null)
            {
                IQueryable<TEntity> query = table;
                query = includes.Aggregate(query,
                (current, include) => current.Include(include));
                return query.ToList();
            }
            else
            {
                return table.ToList();
            }
            
        }

        public void Remove(TEntity entity)
        {
            table.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            table.RemoveRange(entities);
        }
    }
}
