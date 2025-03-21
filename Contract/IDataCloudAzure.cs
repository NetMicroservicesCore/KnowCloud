
using KnowCloud.Models;

namespace KnowCloud.Contract
{
    public interface IDataCloudAzure
    {
        Task Delete(string path, string container);
        Task<CloudFileResult[]> UpLoadFiles(string container, IEnumerable<IFormFile> files);
    }
}
