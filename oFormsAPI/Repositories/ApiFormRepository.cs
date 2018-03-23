using oFormsAPI.Models.v1;
using oFormsAPI.Repositories.Helpers;
using oFormsAPI.Repositories.Models;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Table;
using System.Threading.Tasks;
using System;

namespace oFormsAPI.Repositories
{
    public interface IApiFormRepository
    {
        Task<FormApiMap> GetFormApiMap(string apiKey);
        Task<string> GetCORSDomain(string domain);
    }

    public class ApiFormRepository : BaseRepository, IApiFormRepository
    {
        ILogger<ApiFormRepository> _logger;
        
        public ApiFormRepository(ILogger<ApiFormRepository> logger)
        {
            _logger = logger;
        }

        public async Task<FormApiMap> GetFormApiMap(string apiKey)
        {
            var start = DateTime.Now;
            _logger.LogInformation("Started retrieval of FormApiMap: " + start.ToString());
            CloudTable apiFormTable = GetAPIKeyTable();
            TableOperation retrieveOperation = TableOperation.Retrieve<ApiFormTableEntity>(Consts.AZURE_API_PARTITION_NAME, apiKey);
            TableResult retrievedResult = await apiFormTable.ExecuteAsync(retrieveOperation);
            var end = DateTime.Now;
            _logger.LogInformation("Finished retrieval of FormApiMap: " + DateTime.Now.ToString() + " - (" + end.Subtract(start).TotalMilliseconds + "ms)");
            return ((ApiFormTableEntity)retrievedResult.Result).ToApiMap();
        }

        public async Task<string> GetCORSDomain(string domain)
        {
            var start = DateTime.Now;
            _logger.LogInformation("Started retrieval of CORS domain: " + start.ToString());
            CloudTable apiFormTable = GetAPIKeyTable();
            TableOperation retrieveOperation = TableOperation.Retrieve<TableEntity>(Consts.AZURE_CORS_PARTITION_NAME, domain);
            TableResult retrievedResult = await apiFormTable.ExecuteAsync(retrieveOperation);
            var end = DateTime.Now;
            _logger.LogInformation("Finished retrieval of CORS domainsZ: " + DateTime.Now.ToString() + " - (" + end.Subtract(start).TotalMilliseconds + "ms)");
            if (((TableEntity)retrievedResult.Result) != null) return domain;
            return null;
        }
    }
}
