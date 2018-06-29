using App.Core.Common;
using App.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace App.Domain.Interfaces.Repository
{
	public interface IRepositoryBase<T>
	where T : BaseEntity
	{
        T Add(T entity);

        void BactchAdd(IEnumerable<T> entity);

        void Delete(Expression<Func<T, bool>> where);

        void Delete(T entity);

        IEnumerable<T> Find(Expression<Func<T, bool>> whereClause, Paging page);

        IEnumerable<T> Find<TKey>(Expression<Func<T, bool>> whereClause, Expression<Func<T, TKey>> orderByClause, Paging page);

        IEnumerable<T> FindAndSort(Expression<Func<T, bool>> whereClause, SortBuilder sortBuilder, Paging page);

        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate, bool @readonly = false);

        T Get(Expression<Func<T, bool>> where, bool @readonly = false);

        IEnumerable<T> GetAll();

        IEnumerable<T> GetAllPagedList(Paging page);

        IEnumerable<T> GetTop(int take);

        IEnumerable<T> GetTopBy(int take, Expression<Func<T, bool>> where);

        IEnumerable<T> GetTopBy<TKey>(int take, Expression<Func<T, bool>> where, Expression<Func<T, TKey>> orderByClause);

        void Update(T entity);


        /// <summary>
        /// Returns the queryable entity set for the given type {T}.
        /// </summary>
        IQueryable<T> Table { get; }       
    }
}