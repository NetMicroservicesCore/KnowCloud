using KnowCloud.Contract;
using KnowCloud.Filters;
using KnowCloud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KnowCloud.Controllers
{
    [ApiController]
    [Route("api/archivos")]
    public class FilesCloudController : ControllerBase
    {
        private readonly IDataCloudAzure cloudAzure;
        private readonly string container = "archivosadjuntos";
        public FilesCloudController(IDataCloudAzure _cloudAzure)
        {
            this.cloudAzure = _cloudAzure;
        }

        [CustomExceptionFilterAttribute]
        [HttpPost("{Id:int}")] 
        public async Task<ActionResult> Post(int Id, [FromForm] IEnumerable<IFormFile> archivos)
        {
            //verificar si existen archvios adjuntos
            if (archivos == null || !archivos.Any())
            {
                return BadRequest("No existen archivos cargados");
            }
            //subimos los archivos a nuestra nube
            var result =await cloudAzure.UpLoadFiles(container, archivos);
            var filesAtach = result.Select((result, indice) => new FileAttach
            {
                FechaCreacion = DateTime.UtcNow,
                URL= result.URL,
                Titulo= result.Titulo,
                Orden = indice +1  

            }).ToList();
            return Ok();
        }

    }
}
