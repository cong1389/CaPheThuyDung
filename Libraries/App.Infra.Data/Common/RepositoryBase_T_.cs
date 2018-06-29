using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using App.Core.Common;
using App.Core.Utilities;
using App.Domain.Interfaces.Repository;
using App.Infra.Data.DbFactory;

namespace App.Infra.Data.Common
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T>
    where T : BaseEntity
    {
        private readonly IDbSet<T> _dbSet;

        private Context.AppContext _dataContext;

        private Context.AppContext DbContext
        {
            get
            {
                var appContext = _dataContext;
                if (appContext == null)
                {
                    var appContext1 = DbFactory.Init();
                    var appContext2 = appContext1;
                    _dataContext = appContext1;
                    appContext = appContext2;
                }
                return appContext;
            }
        }

        private IDbFactory DbFactory
        {
            get;
        }

        protected RepositoryBase(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            _dbSet = DbContext.Set<T>();
        }

        public virtual T Add(T entity)
        {
            return _dbSet.Add(entity);
        }

        public void BactchAdd(IEnumerable<T> entity)
        {
            foreach (var t in _dbSet.AsEnumerable())
            {
                _dbSet.Add(t);
            }
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            foreach (var t in _dbSet.Where(where).AsEnumerable())
            {
                _dataContext.Entry(t).State = EntityState.Deleted;
                _dbSet.Remove(t);
            }
        }

        public virtual void Delete(T entity)
        {
            _dataContext.Entry(entity).State = EntityState.Deleted;
            _dbSet.Remove(entity);
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> whereClause, Paging page)
        {
            page.TotalRecord = _dbSet.AsNoTracking().Where(whereClause).Count();
            IEnumerable<T> list = GetDefaultOrder(_dbSet.AsNoTracking()).Where(whereClause)
                .Skip((page.PageNumber - 1) * page.PageSize).Take(page.PageSize).ToList();

            return list;
        }

        public virtual IEnumerable<T> Find<TKey>(Expression<Func<T, bool>> whereClause, Expression<Func<T, TKey>> orderByClause, Paging page)
        {
            page.TotalRecord = _dbSet.AsNoTracking().Where(whereClause).Count();
            IEnumerable<T> list = _dbSet.AsNoTracking().Where(whereClause).OrderBy(orderByClause)
                .Skip((page.PageNumber - 1) * page.PageSize).Take(page.PageSize).ToList();
            return list;
        }

        public virtual IEnumerable<T> FindAndSort(Expression<Func<T, bool>> whereClause, SortBuilder sortBuilder, Paging page)
        {
            IEnumerable<T> list;
            page.TotalRecord = _dbSet.AsNoTracking().Where(whereClause).Count();
            if (!string.IsNullOrEmpty(sortBuilder.ColumnName))
            {
                list = sortBuilder.ColumnOrder != SortBuilder.SortOrder.Descending
                    ? _dbSet.OrderBy(sortBuilder.ColumnName).AsNoTracking().Where(whereClause)
                        .Skip((page.PageNumber - 1) * page.PageSize).Take(page.PageSize).ToList()
                    : _dbSet.OrderByDescending(sortBuilder.ColumnName).AsNoTracking().Where(whereClause)
                        .Skip((page.PageNumber - 1) * page.PageSize).Take(page.PageSize).ToList();
            }
            else
            {
                list = GetDefaultOrder(_dbSet.AsNoTracking()).Where(whereClause).Skip((page.PageNumber - 1) * page.PageSize).Take(page.PageSize).ToList();
            }
            return list;
        }

        public virtual IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate, bool @readonly = false)
        {
            return @readonly ? _dbSet.AsNoTracking().Where(predicate).ToList() : _dbSet.Where(predicate).ToList();
        }

        public virtual T Get(Expression<Func<T, bool>> where, bool @readonly = false)
        {
            return @readonly ? _dbSet.AsNoTracking().Where(where).FirstOrDefault() : _dbSet.Where(where).FirstOrDefault();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public virtual IEnumerable<T> GetAllPagedList(Paging page)
        {
            page.TotalRecord = _dbSet.Count();
            IEnumerable<T> list = GetDefaultOrder(_dbSet.AsNoTracking()).Skip((page.PageNumber - 1) * page.PageSize)
                .Take(page.PageSize).ToList();
            return list;
        }

        protected abstract IOrderedQueryable<T> GetDefaultOrder(IQueryable<T> query);

        public IEnumerable<T> GetTop(int take)
        {
            IEnumerable<T> ts = _dbSet.AsNoTracking().Take(take);
            return ts;
        }

        public IEnumerable<T> GetTopBy(int take, Expression<Func<T, bool>> where)
        {
            IEnumerable<T> ts = _dbSet.AsNoTracking().Where(where).Take(take);
            return ts;
        }

        public IEnumerable<T> GetTopBy<TKey>(int take, Expression<Func<T, bool>> where, Expression<Func<T, TKey>> orderByClause)
        {
            IEnumerable<T> ts = _dbSet.AsNoTracking().OrderByDescending(orderByClause).Where(where).Take(take);
            return ts;
        }

        public virtual void Update(T entity)
        {
            _dbSet.Attach(entity);
            _dataContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual IQueryable<T> Table => _dbSet;
    }
}