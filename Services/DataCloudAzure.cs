using Azure.Storage.Blobs;

namespace KnowCloud.Services
{
    public class DataCloudAzure
    {
        private string connectionString;

        public DataCloudAzure(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("AzureStorage");
        }


        public async Task<string> UpLoadFiles(string container, IEnumerable<IFormFile> files)
        {
            var client = new BlobContainerClient(connectionString,container);
            await client.CreateIfNotExistsAsync();
            return "text1";
        }

        public Task Delete(string path, string container)
        {
            throw new NotImplementedException(); ;
        }
    }
}
