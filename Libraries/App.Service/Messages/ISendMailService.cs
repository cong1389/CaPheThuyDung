using System.Threading.Tasks;
using App.Domain.GlobalSetting;
using Microsoft.AspNet.Identity;

namespace App.Service.Messages
{
    public interface ISendMailService
    {
        Task SendAsync(IdentityMessage message);
        Task SendMailSmtp(SendMail message);
        Task SendMailTrap(IdentityMessage message);
    }
}