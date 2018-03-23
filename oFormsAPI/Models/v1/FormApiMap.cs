using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace oFormsAPI.Models.v1
{
    public class FormApiMap
    {
        [Required]
        [JsonProperty(PropertyName = "id")]
        public string ApiKey { get; set; }
        [Required]
        public string ClientId { get; set; }
        [Required]
        public string FormId { get; set; }
        public EmailTemplateInfo EmailInfo { get; set; }
        public int NumberOfRequests { get; set; }
    }
}
