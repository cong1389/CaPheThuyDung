using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using App.Domain.Entities.Identity;
using Microsoft.AspNet.Identity;

namespace App.Service.Account
{
    public interface IUserManagerService
    {
        void Dispose();
        Task<ClaimsIdentity> CreateIdentityAsync(IdentityUser user, string authenticationType);
        Task<IdentityResult> CreateAsync(IdentityUser user);
        Task<IdentityResult> UpdateAsync(IdentityUser user);
        Task<IdentityResult> DeleteAsync(IdentityUser user);
        Task<IdentityUser> FindByIdAsync(Guid userId);
        Task<IdentityUser> FindByNameAsync(string userName);
        Task<IdentityResult> CreateAsync(IdentityUser user, string password);
        Task<IdentityUser> FindAsync(string userName, string password);
        Task<bool> CheckPasswordAsync(IdentityUser user, string password);
        Task<bool> HasPasswordAsync(Guid userId);
        Task<IdentityResult> AddPasswordAsync(Guid userId, string password);
        Task<IdentityResult> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);
        Task<IdentityResult> RemovePasswordAsync(Guid userId);
        Task<string> GetSecurityStampAsync(Guid userId);
        Task<IdentityResult> UpdateSecurityStampAsync(Guid userId);
        Task<string> GeneratePasswordResetTokenAsync(Guid userId);
        Task<IdentityResult> ResetPasswordAsync(Guid userId, string token, string newPassword);
        Task<IdentityUser> FindAsync(UserLoginInfo login);
        Task<IdentityResult> RemoveLoginAsync(Guid userId, UserLoginInfo login);
        Task<IdentityResult> AddLoginAsync(Guid userId, UserLoginInfo login);
        Task<IList<UserLoginInfo>> GetLoginsAsync(Guid userId);
        Task<IdentityResult> AddClaimAsync(Guid userId, Claim claim);
        Task<IdentityResult> RemoveClaimAsync(Guid userId, Claim claim);
        Task<IList<Claim>> GetClaimsAsync(Guid userId);
        Task<IdentityResult> AddToRoleAsync(Guid userId, string role);
        Task<IdentityResult> AddToRolesAsync(Guid userId, params string[] roles);
        Task<IdentityResult> RemoveFromRolesAsync(Guid userId, params string[] roles);
        Task<IdentityResult> RemoveFromRoleAsync(Guid userId, string role);
        Task<IList<string>> GetRolesAsync(Guid userId);
        Task<bool> IsInRoleAsync(Guid userId, string role);
        Task<string> GetEmailAsync(Guid userId);
        Task<IdentityResult> SetEmailAsync(Guid userId, string email);
        Task<IdentityUser> FindByEmailAsync(string email);
        Task<string> GenerateEmailConfirmationTokenAsync(Guid userId);
        Task<IdentityResult> ConfirmEmailAsync(Guid userId, string token);
        Task<bool> IsEmailConfirmedAsync(Guid userId);
        Task<string> GetPhoneNumberAsync(Guid userId);
        Task<IdentityResult> SetPhoneNumberAsync(Guid userId, string phoneNumber);
        Task<IdentityResult> ChangePhoneNumberAsync(Guid userId, string phoneNumber, string token);
        Task<bool> IsPhoneNumberConfirmedAsync(Guid userId);
        Task<string> GenerateChangePhoneNumberTokenAsync(Guid userId, string phoneNumber);
        Task<bool> VerifyChangePhoneNumberTokenAsync(Guid userId, string token, string phoneNumber);
        Task<bool> VerifyUserTokenAsync(Guid userId, string purpose, string token);
        Task<string> GenerateUserTokenAsync(string purpose, Guid userId);
        void RegisterTwoFactorProvider(string twoFactorProvider, IUserTokenProvider<IdentityUser, Guid> provider);
        Task<IList<string>> GetValidTwoFactorProvidersAsync(Guid userId);
        Task<bool> VerifyTwoFactorTokenAsync(Guid userId, string twoFactorProvider, string token);
        Task<string> GenerateTwoFactorTokenAsync(Guid userId, string twoFactorProvider);
        Task<IdentityResult> NotifyTwoFactorTokenAsync(Guid userId, string twoFactorProvider, string token);
        Task<bool> GetTwoFactorEnabledAsync(Guid userId);
        Task<IdentityResult> SetTwoFactorEnabledAsync(Guid userId, bool enabled);
        Task SendEmailAsync(Guid userId, string subject, string body);
        Task SendSmsAsync(Guid userId, string message);
        Task<bool> IsLockedOutAsync(Guid userId);
        Task<IdentityResult> SetLockoutEnabledAsync(Guid userId, bool enabled);
        Task<bool> GetLockoutEnabledAsync(Guid userId);
        Task<DateTimeOffset> GetLockoutEndDateAsync(Guid userId);
        Task<IdentityResult> SetLockoutEndDateAsync(Guid userId, DateTimeOffset lockoutEnd);
        Task<IdentityResult> AccessFailedAsync(Guid userId);
        Task<IdentityResult> ResetAccessFailedCountAsync(Guid userId);
        Task<int> GetAccessFailedCountAsync(Guid userId);
        IPasswordHasher PasswordHasher { get; set; }
        IIdentityValidator<IdentityUser> UserValidator { get; set; }
        IIdentityValidator<string> PasswordValidator { get; set; }
        IClaimsIdentityFactory<IdentityUser, Guid> ClaimsIdentityFactory { get; set; }
        IIdentityMessageService EmailService { get; set; }
        IIdentityMessageService SmsService { get; set; }
        IUserTokenProvider<IdentityUser, Guid> UserTokenProvider { get; set; }
        bool UserLockoutEnabledByDefault { get; set; }
        int MaxFailedAccessAttemptsBeforeLockout { get; set; }
        TimeSpan DefaultAccountLockoutTimeSpan { get; set; }
        bool SupportsUserTwoFactor { get; }
        bool SupportsUserPassword { get; }
        bool SupportsUserSecurityStamp { get; }
        bool SupportsUserRole { get; }
        bool SupportsUserLogin { get; }
        bool SupportsUserEmail { get; }
        bool SupportsUserPhoneNumber { get; }
        bool SupportsUserClaim { get; }
        bool SupportsUserLockout { get; }
        bool SupportsQueryableUsers { get; }
        IQueryable<IdentityUser> Users { get; }
        IDictionary<string, IUserTokenProvider<IdentityUser, Guid>> TwoFactorProviders { get; }
    }
}