using KnowCloud.Contract;
using Microsoft.AspNetCore.Mvc;

namespace KnowCloud.Controllers
{
    [Route("api/archivos")]
    public class FilesCloudController : ControllerBase
    {
        private readonly IDataCloudAzure cloudAzure;
        private readonly string container = "archivosadjuntos";
        public FilesCloudController(IDataCloudAzure _cloudAzure)
        {
            this.cloudAzure = _cloudAzure;
        }
    }
}
