using System;
using System.Linq;
using System.Threading.Tasks;
using App.Domain.Entities.Account;
using App.Domain.Entities.Identity;
using App.Infra.Data.Repository.Account;
using App.Infra.Data.UOW.Interfaces;
using Microsoft.AspNet.Identity;

namespace App.Service.Account
{
    public class RoleStoreService : IQueryableRoleStore<IdentityRole, Guid>
	{
		private readonly IRoleRepository _roleRepository;

		private readonly IUnitOfWorkAsync _unitOfWork;

		public IQueryable<IdentityRole> Roles
		{
			get
			{
				var identityRoles = (
					from x in _roleRepository.GetAll()
					select GetIdentityRole(x)).AsQueryable();
				return identityRoles;
			}
		}

		public RoleStoreService(IUnitOfWorkAsync unitOfWork, IRoleRepository roleRepository)
		{
			_unitOfWork = unitOfWork;
			_roleRepository = roleRepository;
		}

		public Task CreateAsync(IdentityRole role)
		{
			if (role == null)
			{
				throw new ArgumentNullException("role");
			}
			var role1 = GetRole(role);
			_roleRepository.Add(role1);
			return _unitOfWork.CommitAsync();
		}

		public Task DeleteAsync(IdentityRole role)
		{
			if (role == null)
			{
				throw new ArgumentNullException("role");
			}
			var role1 = GetRole(role);
			_roleRepository.Delete(role1);
			return _unitOfWork.CommitAsync();
		}

		public void Dispose()
		{
		}

		public Task<IdentityRole> FindByIdAsync(Guid roleId)
		{
			var role = _roleRepository.FindById(roleId);
			return Task.FromResult(GetIdentityRole(role));
		}

		public Task<IdentityRole> FindByNameAsync(string roleName)
		{
			var role = _roleRepository.FindByName(roleName);
			return Task.FromResult(GetIdentityRole(role));
		}

		private IdentityRole GetIdentityRole(Role role)
		{
			IdentityRole identityRole;
			if (role != null)
			{
				identityRole = new IdentityRole
				{
					Id = role.Id,
					Name = role.Name,
					Description = role.Description
				};
			}
			else
			{
				identityRole = null;
			}
			return identityRole;
		}

		private Role GetRole(IdentityRole identityRole)
		{
			Role role;
			if (identityRole != null)
			{
				role = new Role
				{
					Id = identityRole.Id,
					Name = identityRole.Name,
					Description = identityRole.Description
				};
			}
			else
			{
				role = null;
			}
			return role;
		}

		public Task UpdateAsync(IdentityRole role)
		{
			if (role == null)
			{
				throw new ArgumentNullException("role");
			}
			var role1 = GetRole(role);
			_roleRepository.Update(role1);
			return _unitOfWork.CommitAsync();
		}
	}
}