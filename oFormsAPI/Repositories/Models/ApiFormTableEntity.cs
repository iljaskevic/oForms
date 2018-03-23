using Microsoft.WindowsAzure.Storage.Table;
using oFormsAPI.Models.v1;

namespace oFormsAPI.Repositories.Models
{
    public class ApiFormTableEntity : TableEntity
    {
        public ApiFormTableEntity() { }
        public ApiFormTableEntity(string apiKey)
        {
            this.PartitionKey = Consts.AZURE_API_PARTITION_NAME;
            this.RowKey = apiKey;
        }
        public string EmailInfo { get; set; }
    }
}
