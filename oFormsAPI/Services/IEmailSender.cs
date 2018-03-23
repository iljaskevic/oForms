using oFormsAPI.Models.v1;
using System.Threading.Tasks;

namespace oFormsAPI.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(EmailTemplateInfo emailTemplateInfo, string formData);
    }
}
