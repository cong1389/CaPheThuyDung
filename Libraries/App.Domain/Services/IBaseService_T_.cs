using App.Core.Common;
using App.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace App.Domain.Interfaces.Services
{
	public interface IBaseService<T> : IService
	where T : BaseEntity
	{
		void BatchCreate(IEnumerable<T> entity);

		void BatchDelete(IEnumerable<T> entity);

		void Create(T entity);

		void Delete(T entity);

		IEnumerable<T> FindAndSort(Expression<Func<T, bool>> whereClause, SortBuilder sortBuilder, Paging page);

		IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate, bool @readonly = false);

		T Get(Expression<Func<T, bool>> whereClause, bool @readonly = false);

		IEnumerable<T> GetAll();

		IEnumerable<T> GetTop(int take);

		IEnumerable<T> GetTop(int take, Expression<Func<T, bool>> whereClause);

		IEnumerable<T> GetTop<TKey>(int take, Expression<Func<T, bool>> whereClause, Expression<Func<T, TKey>> orderByClause);

		void Update(T entity);
	}
}