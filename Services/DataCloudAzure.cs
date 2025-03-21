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
        /// <summary>
        /// Este metodo se encarga de almacenar un archivo en la nube de Azure
        /// </summary>
        /// <param name="container"></param>
        /// <param name="files">los archivos a subir en la nube</param>
        /// <returns>una tarea que almacena  archivos</returns>
        public async Task<AlmacenarArchivoResult> UpLoadFiles(string container, IEnumerable<IFormFile> files)
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
                return new AlmacenarArchivoResult
                {
                    URL = blob.Uri.ToString(),
                    Titulo = filaNameOrigin
                };
                
            });
            var result = await Task.WhenAll(task);
            return result;
        }

        public async Task Delete(string path, string container)
        {
            if (string.IsNullOrEmpty(path)) 
            {
                return;
            }
            var client = new BlobContainerClient(connectionString,container);
            await client.CreateIfNotExistsAsync();
            var fileName = Path.GetFileName(path);
            var blob = client.GetBlobClient(fileName);
            await blob.DeleteIfExistsAsync();

        }
    }
}
