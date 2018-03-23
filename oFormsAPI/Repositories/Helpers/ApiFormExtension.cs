using oFormsAPI.Models.v1;
using oFormsAPI.Repositories.Models;
using Newtonsoft.Json;

namespace oFormsAPI.Repositories.Helpers
{
    public static class ApiFormExtension
    {
        public static FormApiMap ToApiMap(this ApiFormTableEntity apiFormEntity)
        {
            if (apiFormEntity == null) return null;
            var result = new FormApiMap();
            result.EmailInfo = JsonConvert.DeserializeObject<EmailTemplateInfo>(apiFormEntity.EmailInfo);

            return result;
        }
    }
}
