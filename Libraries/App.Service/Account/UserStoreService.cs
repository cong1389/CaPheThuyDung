using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using App.Domain.Entities.Account;
using App.Domain.Entities.Identity;
using App.Infra.Data.Repository.Account;
using App.Infra.Data.UOW.Interfaces;
using Microsoft.AspNet.Identity;
using Claim = System.Security.Claims.Claim;

namespace App.Service.Account
{
    public class UserStoreService : IUserLoginStore<IdentityUser, Guid>, IUserClaimStore<IdentityUser, Guid>,
        IUserRoleStore<IdentityUser, Guid>, IUserPasswordStore<IdentityUser, Guid>,
        IUserSecurityStampStore<IdentityUser, Guid>, IUserEmailStore<IdentityUser, Guid>, IUserTokenProvider<IdentityUser, Guid>, IIdentityMessageService, IUserStoreService

    {
        private readonly IExternalLoginRepository _externalLoginRepository;

        private readonly IRoleRepository _roleRepository;

        private readonly IUnitOfWorkAsync _unitOfWork;

        private readonly IUserRepository _userRepository;

        public UserStoreService(IUnitOfWorkAsync unitOfWork, IUserRepository userRepository, IExternalLoginRepository externalLoginRepository, IRoleRepository roleRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _externalLoginRepository = externalLoginRepository;
            _roleRepository = roleRepository;
        }

        public Task AddClaimAsync(IdentityUser user, Claim claim)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (claim == null)
            {
                throw new ArgumentNullException("claim");
            }
            var user1 = _userRepository.FindById(user.Id);
            if (user1 == null)
            {
                throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");
            }
            var claim1 = new Domain.Entities.Account.Claim
            {
                ClaimType = claim.Type,
                ClaimValue = claim.Value,
                User = user1
            };
            user1.Claims.Add(claim1);
            _userRepository.Update(user1);
            return _unitOfWork.CommitAsync();
        }

        public Task AddLoginAsync(IdentityUser user, UserLoginInfo login)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (login == null)
            {
                throw new ArgumentNullException("login");
            }
            var user1 = _userRepository.FindById(user.Id);
            if (user1 == null)
            {
                throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");
            }
            var externalLogin = new ExternalLogin
            {
                LoginProvider = login.LoginProvider,
                ProviderKey = login.ProviderKey,
                User = user1
            };
            user1.Logins.Add(externalLogin);
            _userRepository.Update(user1);
            return _unitOfWork.CommitAsync();
        }

        public Task AddToRoleAsync(IdentityUser user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException("Argument cannot be null, empty, or whitespace: roleName.");
            }
            var user1 = _userRepository.FindById(user.Id);
            if (user1 == null)
            {
                throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");
            }
            var role = _roleRepository.FindByName(roleName);
            if (role == null)
            {
                throw new ArgumentException("roleName does not correspond to a Role entity.", "roleName");
            }
            user1.Roles.Add(role);
            _userRepository.Update(user1);
            return _unitOfWork.CommitAsync();
        }

        public Task CreateAsync(IdentityUser user)
        {
            var user1 = GetUser(user);
            _userRepository.Add(user1);
            return _unitOfWork.CommitAsync();
        }

        public Task DeleteAsync(IdentityUser user)
        {
            var user1 = GetUser(user);
            _userRepository.Delete(user1);
            return _unitOfWork.CommitAsync();
        }

        public void Dispose()
        {
        }

        public Task<IdentityUser> FindAsync(UserLoginInfo login)
        {
            if (login == null)
            {
                throw new ArgumentNullException("login");
            }
            IdentityUser identityUser = null;
            var byProviderAndKey = _externalLoginRepository.GetByProviderAndKey(login.LoginProvider, login.ProviderKey);
            if (byProviderAndKey != null)
            {
                identityUser = GetIdentityUser(byProviderAndKey.User);
            }
            return Task.FromResult(identityUser);
        }

        public Task<IdentityUser> FindByIdAsync(Guid userId)
        {
            var user = _userRepository.FindById(userId);
            return Task.FromResult(GetIdentityUser(user));
        }

        public Task<IdentityUser> FindByNameAsync(string userName)
        {
            var user = _userRepository.FindByUserName(userName);
            return Task.FromResult(GetIdentityUser(user));
        }

        public Task<IList<Claim>> GetClaimsAsync(IdentityUser user)
        {
            var user1 = _userRepository.FindById(user.Id);
            if (user1 == null)
            {
                throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");
            }
            var task = Task.FromResult<IList<Claim>>((
                from x in user1.Claims
                select new Claim(x.ClaimType, x.ClaimValue)).ToList());
            return task;
        }

        private IdentityUser GetIdentityUser(User user)
        {
            IdentityUser identityUser;
            if (user != null)
            {
                var identityUser1 = new IdentityUser();
                PopulateIdentityUser(identityUser1, user);
                identityUser = identityUser1;
            }
            else
            {
                identityUser = null;
            }
            return identityUser;
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(IdentityUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            var user1 = _userRepository.FindById(user.Id);
            if (user1 == null)
            {
                throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");
            }
            var task = Task.FromResult<IList<UserLoginInfo>>((
                from x in user1.Logins
                select new UserLoginInfo(x.LoginProvider, x.ProviderKey)).ToList());
            return task;
        }

        public Task<string> GetPasswordHashAsync(IdentityUser user)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<IList<string>> GetRolesAsync(IdentityUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            var user1 = _userRepository.FindById(user.Id);
            if (user1 == null)
            {
                throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");
            }
            var task = Task.FromResult<IList<string>>((
                from x in user1.Roles
                select x.Name).ToList());
            return task;
        }

        public Task<string> GetSecurityStampAsync(IdentityUser user)
        {
            return Task.FromResult(user.SecurityStamp);
        }

        private User GetUser(IdentityUser identityUser)
        {
            var user = new User();
            PopulateUser(user, identityUser);
            return user;
        }

        public Task<bool> HasPasswordAsync(IdentityUser user)
        {
            var task = Task.FromResult(!string.IsNullOrWhiteSpace(user.PasswordHash));
            return task;
        }

        public Task<bool> IsInRoleAsync(IdentityUser user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException("Argument cannot be null, empty, or whitespace: role.");
            }
            var user1 = _userRepository.FindById(user.Id);
            if (user1 == null)
            {
                throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");
            }
            var task = Task.FromResult(user1.Roles.Any(x => x.Name == roleName));
            return task;
        }

        private void PopulateIdentityUser(IdentityUser identityUser, User user)
        {
            identityUser.Id = user.Id;
            identityUser.UserName = user.UserName;
            identityUser.PasswordHash = user.PasswordHash;
            identityUser.SecurityStamp = user.SecurityStamp;
            identityUser.Email = user.Email;
            identityUser.EmailConfirmed = user.EmailConfirmed;
            identityUser.FirstName = user.FirstName;
            identityUser.MiddleName = user.MiddleName;
            identityUser.LastName = user.LastName;
            identityUser.State = user.State;
            identityUser.City = user.City;
            identityUser.Phone = user.Phone;
            identityUser.Address = user.Address;
            identityUser.IsSuperAdmin = user.IsSuperAdmin;
            identityUser.IsLockedOut = user.IsLockedOut;
            identityUser.Created = user.Created;
        }

        private void PopulateUser(User user, IdentityUser identityUser)
        {
            user.Id = identityUser.Id;
            user.UserName = identityUser.UserName;
            user.PasswordHash = identityUser.PasswordHash;
            user.SecurityStamp = identityUser.SecurityStamp;
            user.Email = identityUser.Email;
            user.EmailConfirmed = identityUser.EmailConfirmed;
            user.FirstName = identityUser.FirstName;
            user.MiddleName = identityUser.MiddleName;
            user.LastName = identityUser.LastName;
            user.State = identityUser.State;
            user.City = identityUser.City;
            user.Phone = identityUser.Phone;
            user.Address = identityUser.Address;
            user.IsSuperAdmin = identityUser.IsSuperAdmin;
            user.IsLockedOut = identityUser.IsLockedOut;
            user.Created = identityUser.Created;
        }

        public Task RemoveClaimAsync(IdentityUser user, Claim claim)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (claim == null)
            {
                throw new ArgumentNullException("claim");
            }
            var user1 = _userRepository.FindById(user.Id);
            if (user1 == null)
            {
                throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");
            }
            var claim1 = user1.Claims.FirstOrDefault(x => (x.ClaimType == claim.Type && x.ClaimValue == claim.Value));
            user1.Claims.Remove(claim1);
            _userRepository.Update(user1);
            return _unitOfWork.CommitAsync();
        }

        public Task RemoveFromRoleAsync(IdentityUser user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException("Argument cannot be null, empty, or whitespace: role.");
            }
            var user1 = _userRepository.FindById(user.Id);
            if (user1 == null)
            {
                throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");
            }
            var role = user1.Roles.FirstOrDefault(x => x.Name == roleName);
            user1.Roles.Remove(role);
            _userRepository.Update(user1);
            return _unitOfWork.CommitAsync();
        }

        public Task RemoveLoginAsync(IdentityUser user, UserLoginInfo login)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (login == null)
            {
                throw new ArgumentNullException("login");
            }
            var user1 = _userRepository.FindById(user.Id);
            if (user1 == null)
            {
                throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");
            }
            var externalLogin = user1.Logins.FirstOrDefault(x => (x.LoginProvider == login.LoginProvider && x.ProviderKey == login.ProviderKey));
            user1.Logins.Remove(externalLogin);
            _userRepository.Update(user1);
            return _unitOfWork.CommitAsync();
        }

        public Task SetPasswordHashAsync(IdentityUser user, string passwordHash)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task SetSecurityStampAsync(IdentityUser user, string stamp)
        {
            user.SecurityStamp = stamp;
            return Task.FromResult(0);
        }

        public Task UpdateAsync(IdentityUser user)
        {
            if (user == null)
            {
                throw new ArgumentException("user");
            }
            var user1 = _userRepository.FindById(user.Id);
            if (user1 == null)
            {
                throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");
            }
            PopulateUser(user1, user);
            _userRepository.Update(user1);
            return _unitOfWork.CommitAsync();
        }

        public Task<IdentityUser> FindByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailAsync(IdentityUser user, string email)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetEmailAsync(IdentityUser user)
        {
            return Task.Factory.StartNew(() => user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(IdentityUser user)
        {
            return Task.Factory.StartNew(() => user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(IdentityUser user, bool confirmed)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityUser> FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task SendAsync(IdentityMessage message)
        {
            SmtpClient client = new SmtpClient
            {
                Port = 587,
                Host = "smtp.gmail.com",
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("ddemo9698@gmail.com", "abc@12345")
            };
            //client.Timeout = 10000;

            return client.SendMailAsync("ddemo9698@gmail.com", message.Destination, message.Subject, message.Body);
        }

        public Task SendEmailAsync(Guid userId, string subject, string body)
        {
            SmtpClient client = new SmtpClient
            {
                Port = 587,
                Host = "smtp.gmail.com",
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("ddemo9698@gmail.com", "abc@12345")
            };
            //client.Timeout = 10000;

            return client.SendMailAsync("ddemo9698@gmail.com", "", "", "");
        }

        //public async Task<IdentityResult> ResetPasswordAsync(IdentityUser user,
        //    string token, string newPassword)
        //{
        //    if (user == null)
        //    {
        //        throw new ArgumentNullException("user");
        //    }

        //    // Make sure the token is valid and the stamp matches.
        //    if (!await ValidateAsync("ResetPassword", token, token, user))
        //    {
        //        return IdentityResult.Failed("Invalid token.");
        //    }

        //    // Make sure the new password is valid.
        //    var result = await PasswordValidator.ValidateAsync(newPassword)
        //        .ConfigureAwait(false);
        //    if (!result.Succeeded)
        //    {
        //        return result;
        //    }

        //    // Update the password hash and invalidate the current security stamp.
        //    user.PasswordHash = PasswordHasher.HashPassword(newPassword);
        //    user.SecurityStamp = Guid.NewGuid().ToString();

        //    // Save the user and return the outcome.
        //    return await UpdateAsync(user).ConfigureAwait(false);
        //}

        public Task<string> GenerateAsync(string purpose, UserManager<IdentityUser, Guid> manager, IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidateAsync(string purpose, string token, UserManager<IdentityUser, Guid> manager, IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task NotifyAsync(string token, UserManager<IdentityUser, Guid> manager, IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsValidProviderForUserAsync(UserManager<IdentityUser, Guid> manager, IdentityUser user)
        {
            throw new NotImplementedException();
        }
    }
}