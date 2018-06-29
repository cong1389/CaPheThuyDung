using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using App.Core.Common;
using App.Core.Utilities;
using App.Domain.Interfaces.Repository;
using App.Domain.Interfaces.Services;

namespace App.Infra.Data.Common
{
    public class BaseAsyncService<T> : IBaseAsyncService<T>
	where T : BaseEntity
	{
        private readonly IRepositoryBaseAsync<T> _repository;

	    public BaseAsyncService(IRepositoryBaseAsync<T> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> whereClause, Paging page)
        {
            return await _repository.FindAsync(whereClause, page);
        }

        public async Task<IEnumerable<T>> FindAsync(CancellationToken cancellationToken,
            Expression<Func<T, bool>> whereClause, Paging page)
        {
            return await _repository.FindAsync(cancellationToken, whereClause, page);
        }

        public async Task<IEnumerable<T>> FindAsync<TKey>(CancellationToken cancellationToken,
            Expression<Func<T, bool>> whereClause, Expression<Func<T, TKey>> orderByClause, Paging page)
        {
            var ts = await _repository.FindAsync(cancellationToken, whereClause, orderByClause, page);
            return ts;
        }

        public async Task<IEnumerable<T>> FindAsync<TKey>(Expression<Func<T, bool>> whereClause, Expression<Func<T, TKey>> orderByClause, Paging page)
        {
            return await _repository.FindAsync(whereClause, page);
        }

        public async Task<IEnumerable<T>> FindByAsync(CancellationToken cancellationToken, Expression<Func<T, bool>> predicate, bool @readonly = false)
        {
            return await _repository.FindByAsync(cancellationToken, predicate, @readonly);
        }

        public async Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate, bool @readonly = false)
        {
            return await _repository.FindByAsync(predicate, @readonly);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<IEnumerable<T>> GetAllPagedListAsync(Paging page)
        {
            return await _repository.GetAllPagedListAsync(page);
        }

        public async Task<IEnumerable<T>> GetAllPagedListAsync(CancellationToken cancellationToken, Paging page)
        {
            return await _repository.GetAllPagedListAsync(cancellationToken, page);
        }

        public async Task<T> GetAsync(CancellationToken cancellationToken, Expression<Func<T, bool>> where, bool @readonly = false)
        {
            return await _repository.GetAsync(cancellationToken, where, @readonly);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> where, bool @readonly = false)
        {
            return await _repository.GetAsync(where, @readonly);
        }

        public async Task<IEnumerable<T>> GetTop(int take)
        {
            return await _repository.GetTop(take);
        }

        public async Task<IEnumerable<T>> GetTop(int take, Expression<Func<T, bool>> whereClause)
        {
            return await _repository.GetTopBy(take, whereClause);
        }

        public async Task<IEnumerable<T>> GetTop<TKey>(int take, Expression<Func<T, bool>> whereClause, Expression<Func<T, TKey>> orderByClause)
        {
            return await _repository.GetTopBy(take, whereClause, orderByClause);
        }
    }
}