using App.Core.Common;
using App.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace App.Domain.Interfaces.Repository
{
	public interface IRepositoryBaseAsync<T>
	where T : BaseEntity
	{
		T Add(T entity);

		void Delete(Expression<Func<T, bool>> where);

		void Delete(T entity);

		IEnumerable<T> Find(Expression<Func<T, bool>> whereClause, Paging page);

		IEnumerable<T> Find<TKey>(Expression<Func<T, bool>> whereClause, Expression<Func<T, TKey>> orderByClause, Paging page);

		Task<IEnumerable<T>> FindAndSort(Expression<Func<T, bool>> whereClause, SortBuilder sortBuilder, Paging page);

		Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> whereClause, Paging page);

		Task<IEnumerable<T>> FindAsync(CancellationToken cancellationToken, Expression<Func<T, bool>> whereClause, Paging page);

		Task<IEnumerable<T>> FindAsync<TKey>(CancellationToken cancellationToken, Expression<Func<T, bool>> whereClause, Expression<Func<T, TKey>> orderByClause, Paging page);

		Task<IEnumerable<T>> FindAsync<TKey>(Expression<Func<T, bool>> whereClause, Expression<Func<T, TKey>> orderByClause, Paging page);

		IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate, bool @readonly = false);

		Task<IEnumerable<T>> FindByAsync(CancellationToken cancellationToken, Expression<Func<T, bool>> predicate, bool @readonly = false);

		Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate, bool @readonly = false);

		T FindById(object id, bool @readonly = false);

		T Get(Expression<Func<T, bool>> where, bool @readonly = false);

		IEnumerable<T> GetAll();

		Task<List<T>> GetAllAsync();

		Task<IEnumerable<T>> GetAllPagedListAsync(Paging page);

		Task<IEnumerable<T>> GetAllPagedListAsync(CancellationToken cancellationToken, Paging page);

		Task<T> GetAsync(CancellationToken cancellationToken, Expression<Func<T, bool>> where, bool @readonly = false);

		Task<T> GetAsync(Expression<Func<T, bool>> where, bool @readonly = false);

		Task<IEnumerable<T>> GetTop(int take);

		Task<IEnumerable<T>> GettopBy<TKey>(Expression<Func<T, bool>> whereClause, Expression<Func<T, TKey>> orderByClause, int take);

		Task<IEnumerable<T>> GetTopBy(int take, Expression<Func<T, bool>> where);

		Task<IEnumerable<T>> GetTopBy<TKey>(int take, Expression<Func<T, bool>> where, Expression<Func<T, TKey>> orderByClause);

		Task<int> SaveAsync(CancellationToken cancellationToken);

		Task<int> SaveAsync();

		void Update(T entity);
	}
}