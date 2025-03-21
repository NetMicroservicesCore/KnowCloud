using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

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
            client.SetAccessPolicy(Azure.Storage.Blobs.Models.PublicAccessType.Blob);
            var task = files.Select(async file =>
            {
                //obtenemos el nombre del archivo
                var filaNameOrigin = Path.GetFileName(file.FileName);
                var extension = Path.GetExtension(file.FileName);
                var nameFile = $"{Guid.NewGuid()}{extension}";
                var blob = client.GetBlobClient(nameFile);
                var blobHttpHeaders = new BlobHttpHeaders();
                blobHttpHeaders.ContentType = file.ContentType;
                await blob.UploadAsync(file.OpenReadStream(),blobHttpHeaders);

            });
        }

        public Task Delete(string path, string container)
        {
            throw new NotImplementedException(); ;
        }
    }
}
