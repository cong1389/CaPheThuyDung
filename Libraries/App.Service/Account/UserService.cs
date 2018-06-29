using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using App.Core.Utilities;
using App.Domain.Entities.Account;
using App.Infra.Data.Common;
using App.Infra.Data.Repository.Account;

namespace App.Service.Account
{
    public class UserService : BaseAsyncService<User>, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) : base(userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> PagedList(SortingPagingBuilder sortBuider, Paging page)
        {
            return await _userRepository.PagedSearchList(sortBuider, page);
        }

        public void BatchCreate(IEnumerable<User> entity)
        {
            throw new NotImplementedException();
        }

        public void BatchDelete(IEnumerable<User> entity)
        {
            throw new NotImplementedException();
        }

        public void Create(User entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> FindAndSort(Expression<Func<User, bool>> whereClause,
            SortBuilder sortBuilder, Paging page)
        {
            return await _userRepository.FindAndSort(whereClause, sortBuilder, page);
        }

        public IEnumerable<User> FindBy(Expression<Func<User, bool>> predicate, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public User Get(Expression<Func<User, bool>> whereClause, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public new IEnumerable<User> GetTop(int take)
        {
            throw new NotImplementedException();
        }

        public new IEnumerable<User> GetTop(int take, Expression<Func<User, bool>> whereClause)
        {
            throw new NotImplementedException();
        }

        public new IEnumerable<User> GetTop<TKey>(int take, Expression<Func<User, bool>> whereClause,
            Expression<Func<User, TKey>> orderByClause)
        {
            throw new NotImplementedException();
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}