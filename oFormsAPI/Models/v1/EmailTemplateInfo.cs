using System.ComponentModel.DataAnnotations;

namespace oFormsAPI.Models.v1
{
    public class EmailTemplateInfo
    {
        [Required]
        public string ToEmail { get; set; }
        public string ToName { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string Subject { get; set; }
        public string ReCaptchaPrivateKey { get; set; }

        public string MessageTemplate { get; set; }
        public string FieldTemplate { get; set; }
    }
}