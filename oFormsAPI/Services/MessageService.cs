using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using oFormsAPI.Models.v1;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web;
using Microsoft.Extensions.Logging;
using oFormsAPI.Models;
using Microsoft.Extensions.Options;

namespace oFormsAPI.Services
{
    public interface IMessageService
    {
        Task SendEmailAsync(EmailTemplateInfo emailTemplateInfo, string formData);
        Task SendSmsAsync(string number, string message);
    }
    
    public class MessageService : IMessageService
    {
        ILogger<MessageService> _logger;
        private string emailApiKey;


        public MessageService(ILogger<MessageService> logger, IOptions<FormsConfiguration> _formsConfiguration)
        {
            _logger = logger;
            emailApiKey = _formsConfiguration.Value.SendGridAPIKey;
        }

        public async Task SendEmailAsync(EmailTemplateInfo emailTemplateInfo, string formData)
        {
            _logger.LogInformation("Sending form data through Sendgrid");
            //SendGrid example
            var apiKey = emailApiKey;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(emailTemplateInfo.FromEmail, emailTemplateInfo.FromName);
            //var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress(emailTemplateInfo.ToEmail, emailTemplateInfo.ToName);
            //var plainTextContent = message;
            var htmlContent = FormatEmailMessage(emailTemplateInfo, formData);
            var msg = MailHelper.CreateSingleEmail(from, to, emailTemplateInfo.Subject, null, htmlContent);
            var response = await client.SendEmailAsync(msg);
            _logger.LogInformation("Finished sending form data through Sendgrid");
        }

        private string FormatEmailMessage(EmailTemplateInfo emailTemplateInfo, string formData)
        {
            var result = emailTemplateInfo.MessageTemplate;

            var dynData = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(formData);
            //result = result.Replace("{ApplicationName}", form.ApplicationName);

            StringBuilder fields = new StringBuilder();
            foreach (var key in dynData.Keys)
            {
                // check if the value is not null or empty.
                if (!string.IsNullOrEmpty(dynData[key]))
                {
                    var fieldStr = emailTemplateInfo.FieldTemplate;
                    fieldStr = fieldStr.Replace("{Name}", key);
                    fieldStr = fieldStr.Replace("{Value}", HttpUtility.UrlEncode(dynData[key]));
                    fields.Append(fieldStr);
                }
            }

            result = result.Replace("{Fields}", fields.ToString());

            result = result.Replace("%40", "@"); // Decode '@'

            result = result.Replace("%27", "'"); // Decode apostrophe
            result = result.Replace("%91", "'"); // Decode apostrophe
            result = result.Replace("%92", "'"); // Decode apostrophe

            result = result.Replace("%24", "$"); // Decode '$'

            result = result.Replace("%20", " "); // Decode ' '
            result = result.Replace("+", " "); // Decode ' '

            result = result.Replace("%21", "!"); // Decode ' '

            result = result.Replace("%2E", "."); // Decode ' '

            result = result.Replace("%3F", "?"); // Decode ' '

            result = result.Replace("%2C", ","); // Decode ','

            result = result.Replace("%28", "("); // Decode ' '
            result = result.Replace("%29", ")"); // Decode ' '

            return result;
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
