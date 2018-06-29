using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using App.Core.Common;
using App.Core.Utilities;
using App.Domain.Interfaces.Repository;
using App.Infra.Data.DbFactory;

namespace App.Infra.Data.Common
{
    public abstract class RepositoryBaseAsync<T> : IRepositoryBaseAsync<T>
    where T : BaseEntity
    {
        private readonly IDbSet<T> _dbSet;

        private Context.AppContext _dataContext;

        protected Context.AppContext DbContext
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

        protected IDbFactory DbFactory
        {
            get;
        }

        protected RepositoryBaseAsync(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            _dbSet = DbContext.Set<T>();
        }

        public T Add(T entity)
        {
            return _dbSet.Add(entity);
        }

        public void Delete(Expression<Func<T, bool>> where)
        {
            foreach (var t in _dbSet.Where(where).AsEnumerable())
            {
                _dbSet.Remove(t);
            }
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public IEnumerable<T> Find<TKey>(Expression<Func<T, bool>> whereClause, Expression<Func<T, TKey>> orderByClause, Paging page)
        {
            page.TotalRecord = _dbSet.AsNoTracking().Where(whereClause).Count();
            IEnumerable<T> list = _dbSet.AsNoTracking().Where(whereClause).OrderBy(orderByClause)
                .Skip((page.PageNumber - 1) * page.PageSize).Take(page.PageSize).ToList();
            return list;
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> whereClause, Paging page)
        {
            page.TotalRecord = _dbSet.AsNoTracking().Where(whereClause).Count();
            IEnumerable<T> list = GetDefaultOrder(_dbSet.AsNoTracking()).Where(whereClause).Skip((page.PageNumber - 1) * page.PageSize).Take(page.PageSize).ToList();
            return list;
        }

        public virtual async Task<IEnumerable<T>> FindAndSort(Expression<Func<T, bool>> whereClause, SortBuilder sortBuilder, Paging page)
        {
            IEnumerable<T> list;
            List<T> listAsync;
            page.TotalRecord = _dbSet.AsNoTracking().Where(whereClause).Count();
            if (string.IsNullOrEmpty(sortBuilder.ColumnName))
            {
                list = GetDefaultOrder(_dbSet.AsNoTracking()).Where(whereClause).Skip((page.PageNumber - 1) * page.PageSize).Take(page.PageSize).ToList();
            }
            else if (sortBuilder.ColumnOrder != SortBuilder.SortOrder.Ascending)
            {
                listAsync = await _dbSet.OrderByDescending(sortBuilder.ColumnName).AsNoTracking().Where(whereClause)
                    .Skip((page.PageNumber - 1) * page.PageSize).Take(page.PageSize).ToListAsync();
                list = listAsync;
            }
            else
            {
                listAsync = await _dbSet.OrderByDescending(sortBuilder.ColumnName).AsNoTracking().Where(whereClause).Skip((page.PageNumber - 1) * page.PageSize).Take(page.PageSize).ToListAsync();
                list = listAsync;
            }
            return list;
        }

        public virtual async Task<IEnumerable<T>> FindAsync<TKey>(CancellationToken cancellationToken, Expression<Func<T, bool>> whereClause, Expression<Func<T, TKey>> orderByClause, Paging page)
        {
            page.TotalRecord = _dbSet.AsNoTracking().Where(whereClause).Count();

            var listAsync = await _dbSet.AsNoTracking().Where(whereClause).OrderBy(orderByClause)
                .Skip((page.PageNumber - 1) * page.PageSize).Take(page.PageSize).ToListAsync(cancellationToken);

            return listAsync;
        }

        public virtual async Task<IEnumerable<T>> FindAsync<TKey>(Expression<Func<T, bool>> whereClause, Expression<Func<T, TKey>> orderByClause, Paging page)
        {
            page.TotalRecord = _dbSet.Where(whereClause).Count();

            var listAsync = await _dbSet.AsNoTracking().Where(whereClause).OrderBy(orderByClause).Skip((page.PageNumber - 1) * page.PageSize).Take(page.PageSize).ToListAsync();

            return listAsync;
        }

        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> whereClause, Paging page)
        {
            page.TotalRecord = _dbSet.AsNoTracking().Where(whereClause).Count();

            var listAsync = await GetDefaultOrder(_dbSet.AsNoTracking()).Where(whereClause).Skip((page.PageNumber - 1) * page.PageSize).Take(page.PageSize).ToListAsync();

            return listAsync;
        }

        public virtual async Task<IEnumerable<T>> FindAsync(CancellationToken cancellationToken, Expression<Func<T, bool>> whereClause, Paging page)
        {
            page.TotalRecord = _dbSet.AsNoTracking().Where(whereClause).Count();

            var listAsync = await GetDefaultOrder(_dbSet.AsNoTracking()).Where(whereClause).Skip((page.PageNumber - 1) * page.PageSize)
                .Take(page.PageSize).ToListAsync(cancellationToken);

            return listAsync;
        }

        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate, bool @readonly = false)
        {
            return @readonly ? _dbSet.AsNoTracking().Where(predicate).ToList() : _dbSet.Where(predicate).ToList();
        }

        public virtual async Task<IEnumerable<T>> FindByAsync(CancellationToken cancellationToken, Expression<Func<T, bool>> predicate, bool @readonly = false)
        {
            var task = @readonly
                ? _dbSet.AsNoTracking().Where(predicate).ToListAsync(cancellationToken)
                : _dbSet.Where(predicate).ToListAsync(cancellationToken);

            return await task;
        }

        public virtual async Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate, bool @readonly = false)
        {
            var task = @readonly ? _dbSet.AsNoTracking().Where(predicate).ToListAsync() : _dbSet.Where(predicate).ToListAsync();

            return await task;
        }

        public T FindById(object id, bool @readonly = false)
        {
            return _dbSet.Find(id);
        }

        public T Get(Expression<Func<T, bool>> where, bool @readonly = false)
        {
            return @readonly ? _dbSet.AsNoTracking().Where(where).FirstOrDefault() : _dbSet.Where(where).FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public virtual Task<List<T>> GetAllAsync()
        {
            return _dbSet.AsNoTracking().ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAllPagedListAsync(Paging page)
        {
            page.TotalRecord = _dbSet.AsNoTracking().Count();

            var listAsync = await GetDefaultOrder(_dbSet.AsNoTracking()).Skip((page.PageNumber - 1) * page.PageSize)
                .Take(page.PageSize).ToListAsync();

            return listAsync;
        }

        public virtual async Task<IEnumerable<T>> GetAllPagedListAsync(CancellationToken cancellationToken, Paging page)
        {
            page.TotalRecord = _dbSet.AsNoTracking().Count();

            var listAsync = await GetDefaultOrder(_dbSet.AsNoTracking()).Skip((page.PageNumber - 1) * page.PageSize)
                .Take(page.PageSize).ToListAsync(cancellationToken);

            return listAsync;
        }

        public Task<T> GetAsync(CancellationToken cancellationToken, Expression<Func<T, bool>> where, bool @readonly = false)
        {
            return @readonly ? _dbSet.AsNoTracking().Where(where).FirstOrDefaultAsync(cancellationToken) : _dbSet.Where(where).FirstOrDefaultAsync(cancellationToken);
        }

        public Task<T> GetAsync(Expression<Func<T, bool>> where, bool @readonly = false)
        {
            return @readonly ? _dbSet.AsNoTracking().Where(where).FirstOrDefaultAsync() : _dbSet.Where(where).FirstOrDefaultAsync();
        }

        protected abstract IOrderedQueryable<T> GetDefaultOrder(IQueryable<T> query);

        public async Task<IEnumerable<T>> GetTop(int take)
        {
            var listAsync = await _dbSet.AsNoTracking().Take(take).ToListAsync();

            return listAsync;
        }

        public async Task<IEnumerable<T>> GettopBy<TKey>(Expression<Func<T, bool>> whereClause, Expression<Func<T, TKey>> orderByClause, int take)
        {
            var listAsync = await _dbSet.AsNoTracking().OrderBy(orderByClause).Where(whereClause).Take(take)
                .ToListAsync();

            return listAsync;
        }

        public Task<IEnumerable<T>> GetTopBy<TKey>(int take, Expression<Func<T, bool>> where, Expression<Func<T, TKey>> orderByClause)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetTopBy(int take, Expression<Func<T, bool>> where)
        {
            var listAsync = await _dbSet.AsNoTracking().Where(where).Take(take).ToListAsync();

            return listAsync;
        }

        public virtual async Task<int> SaveAsync()
        {
            return await _dataContext.SaveChangesAsync();
        }

        public virtual async Task<int> SaveAsync(CancellationToken cancellationToken)
        {
            return await _dataContext.SaveChangesAsync(cancellationToken);
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);

            _dataContext.Entry(entity).State = EntityState.Modified;
        }
    }
}