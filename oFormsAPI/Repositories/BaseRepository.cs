using oFormsAPI.Models.v1;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace oFormsAPI.Repositories
{
    public interface IBaseRepository
    {
        CloudTable GetAPIKeyTable();
        CloudTable GetCORSDomainTable();
    }

    public abstract class BaseRepository : IBaseRepository
    {
        private readonly string mainStorageConnString = "DefaultEndpointsProtocol=https;AccountName=oformsstorage;AccountKey=6pJFKAnakvNoQldG+Pchd9tFgWp2XjUgxzQ6P+M19RhVkEI6mHL3zBlyTAG99QsP3+/I8nt5Cv6E1GfoIl9jHg==;EndpointSuffix=core.windows.net";
        
        public CloudTable GetAPIKeyTable()
        {
            return GetTable(Consts.AZURE_API_KEY_TABLE_NAME, mainStorageConnString);
        }

        public CloudTable GetCORSDomainTable()
        {
            return GetTable(Consts.AZURE_CORS_TABLE_NAME, mainStorageConnString);
        }

        private CloudTable GetTable(string tableName, string connString)
        {
            // Retrieve the storage account from the connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connString);

            ServicePoint tableServicePoint = ServicePointManager.FindServicePoint(storageAccount.TableEndpoint);
            tableServicePoint.UseNagleAlgorithm = false;
            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Retrieve a reference to the table.
            CloudTable table = tableClient.GetTableReference(tableName);

            return table;
        }
    }
}
