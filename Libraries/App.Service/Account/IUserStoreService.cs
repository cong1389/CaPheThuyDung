using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using App.Domain.Entities.Identity;
using Microsoft.AspNet.Identity;

namespace App.Service.Account
{
    public interface IUserStoreService
    {
        Task AddClaimAsync(IdentityUser user, Claim claim);
        Task AddLoginAsync(IdentityUser user, UserLoginInfo login);
        Task AddToRoleAsync(IdentityUser user, string roleName);
        Task CreateAsync(IdentityUser user);
        Task DeleteAsync(IdentityUser user);
        void Dispose();
        Task<IdentityUser> FindAsync(UserLoginInfo login);
        Task<IdentityUser> FindByIdAsync(Guid userId);
        Task<IdentityUser> FindByNameAsync(string userName);
        Task<IList<Claim>> GetClaimsAsync(IdentityUser user);
        Task<IList<UserLoginInfo>> GetLoginsAsync(IdentityUser user);
        Task<string> GetPasswordHashAsync(IdentityUser user);
        Task<IList<string>> GetRolesAsync(IdentityUser user);
        Task<string> GetSecurityStampAsync(IdentityUser user);
        Task<bool> HasPasswordAsync(IdentityUser user);
        Task<bool> IsInRoleAsync(IdentityUser user, string roleName);
        Task RemoveClaimAsync(IdentityUser user, Claim claim);
        Task RemoveFromRoleAsync(IdentityUser user, string roleName);
        Task RemoveLoginAsync(IdentityUser user, UserLoginInfo login);
        Task SetPasswordHashAsync(IdentityUser user, string passwordHash);
        Task SetSecurityStampAsync(IdentityUser user, string stamp);
        Task UpdateAsync(IdentityUser user);
        Task<IdentityUser> FindByEmailAsync(string email);
        Task SetEmailAsync(IdentityUser user, string email);
        Task<string> GetEmailAsync(IdentityUser user);
        Task<bool> GetEmailConfirmedAsync(IdentityUser user);
        Task SetEmailConfirmedAsync(IdentityUser user, bool confirmed);
        Task<IdentityUser> FindByIdAsync(string userId);
        Task SendAsync(IdentityMessage message);
        Task SendEmailAsync(Guid userId, string subject, string body);
    }
}