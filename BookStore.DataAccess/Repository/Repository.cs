﻿using BookStore.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
           //_db=db,asNotracking() - to avoid tracking
           // _db.Products.Include(u=>u.Category);
            this.dbSet = _db.Set<T>();
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }
        //include Properties - Category,Cover
        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter=null, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null) {
                query = query.Where(filter);
            }
            if(includeProperties != null)
            {
                foreach(var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                   query =query.Include(includeProp);
                }
            }
            return query.ToList();
        }

        public T GetSingleOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = true)
        {

            IQueryable<T> query = dbSet;
            if (tracked)
            {
                query = dbSet;
            }
            else { query = dbSet.AsNoTracking();}
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query= query.Include(includeProp);
                }
            }
            query = query.Where(filter);
            return query.SingleOrDefault();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }
    }
}
