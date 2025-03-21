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

        [HttpPost("{Id:int}")]
        public async Task<ActionResult> Post(int Id, [FromForm] IEnumerable<IFormFile> archivos)
        {
            //verificar si existen archvios adjuntos

            //subimos los archivos a nuestra nube
            await cloudAzure.UpLoadFiles(container, archivos);
            return Ok();
        }

    }
}
