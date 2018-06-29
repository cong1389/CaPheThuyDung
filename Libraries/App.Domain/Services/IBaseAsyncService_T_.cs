using App.Core.Common;
using App.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace App.Domain.Interfaces.Services
{
	public interface IBaseAsyncService<T> : IService
	where T : BaseEntity
	{
		Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> whereClause, Paging page);

		Task<IEnumerable<T>> FindAsync(CancellationToken cancellationToken, Expression<Func<T, bool>> whereClause, Paging page);

		Task<IEnumerable<T>> FindAsync<TKey>(CancellationToken cancellationToken, Expression<Func<T, bool>> whereClause, Expression<Func<T, TKey>> orderByClause, Paging page);

		Task<IEnumerable<T>> FindAsync<TKey>(Expression<Func<T, bool>> whereClause, Expression<Func<T, TKey>> orderByClause, Paging page);

		Task<IEnumerable<T>> FindByAsync(CancellationToken cancellationToken, Expression<Func<T, bool>> predicate, bool @readonly = false);

		Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate, bool @readonly = false);

		Task<List<T>> GetAllAsync();

		Task<IEnumerable<T>> GetAllPagedListAsync(Paging page);

		Task<IEnumerable<T>> GetAllPagedListAsync(CancellationToken cancellationToken, Paging page);

		Task<T> GetAsync(CancellationToken cancellationToken, Expression<Func<T, bool>> where, bool @readonly = false);

		Task<T> GetAsync(Expression<Func<T, bool>> where, bool @readonly = false);

		Task<IEnumerable<T>> GetTop(int take);

		Task<IEnumerable<T>> GetTop(int take, Expression<Func<T, bool>> whereClause);

		Task<IEnumerable<T>> GetTop<TKey>(int take, Expression<Func<T, bool>> whereClause, Expression<Func<T, TKey>> orderByClause);
	}
}