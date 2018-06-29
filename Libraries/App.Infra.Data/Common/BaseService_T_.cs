using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using App.Core.Common;
using App.Core.Utilities;
using App.Domain.Interfaces.Repository;
using App.Domain.Interfaces.Services;
using App.Infra.Data.UOW.Interfaces;

namespace App.Infra.Data.Common
{
    public abstract class BaseService<T> : IBaseService<T>
    where T : BaseEntity
    {
        private readonly IRepositoryBase<T> _repository;

        private readonly IUnitOfWork _unitOfWork;

        protected BaseService(IUnitOfWork unitOfWork, IRepositoryBase<T> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public void BatchCreate(IEnumerable<T> entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _repository.BactchAdd(entity);
            _unitOfWork.Commit();
        }

        public void BatchDelete(IEnumerable<T> entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            foreach (var t in entity)
            {
                _repository.Delete(t);
            }
            _unitOfWork.Commit();
        }

        public virtual void Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _repository.Add(entity);
            _unitOfWork.Commit();
        }

        public virtual void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _repository.Delete(entity);
            _unitOfWork.Commit();
        }

        public IEnumerable<T> FindAndSort(Expression<Func<T, bool>> whereClause, SortBuilder sortBuilder, Paging page)
        {
            return _repository.FindAndSort(whereClause, sortBuilder, page);
        }

        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate, bool @readonly = false)
        {
            return _repository.FindBy(predicate, @readonly);
        }

        public T Get(Expression<Func<T, bool>> whereClause, bool @readonly = false)
        {
            return _repository.Get(whereClause, @readonly);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public IEnumerable<T> GetTop(int take)
        {
            return _repository.GetTop(take);
        }

        public IEnumerable<T> GetTop(int take, Expression<Func<T, bool>> whereClause)
        {
            return _repository.GetTopBy(take, whereClause);
        }

        public IEnumerable<T> GetTop<TKey>(int take, Expression<Func<T, bool>> whereClause, Expression<Func<T, TKey>> orderByClause)
        {
            return _repository.GetTopBy(take, whereClause, orderByClause);
        }

        public virtual void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _repository.Update(entity);
            _unitOfWork.Commit();
        }
    }
}