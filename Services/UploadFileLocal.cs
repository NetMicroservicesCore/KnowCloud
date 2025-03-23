using KnowCloud.Contract;
using KnowCloud.Models;

namespace KnowCloud.Services
{

    public class UploadFileLocal : IDataCloudAzure
    {
        //intectamos el elemento que se encargara de  obtener toda la informacion del host
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UploadFileLocal(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            _env = env ?? throw new ArgumentNullException(nameof(env));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor)); 

        }

        /// <summary>
        /// Se encarga de realizar la eliminacion del archivo
        /// </summary>
        /// <param name="path">el directorio del archivo</param>
        /// <param name="container">el nombre de la carpeta o folder.</param>
        /// <returns>una tarea completada</returns>
        public Task Delete(string path, string container)
        {
            if (string.IsNullOrWhiteSpace(path)) 
            {
                return Task.CompletedTask;
            }
            var fileName = Path.GetFileName(path);
            var directoryFile = Path.Combine(_env.WebRootPath,container,fileName);
            if (File.Exists(directoryFile)) 
            {
                File.Delete(directoryFile);
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Este metodo se encarga de subir un archivo  a traves del objeto IFormfile
        /// </summary>
        /// <param name="container">el contenedor del archivo o visto desde otro punto de vista es el nombre de la carpeta</param>
        /// <param name="files">los archivos cargados</param>
        /// <returns>una proyeccion de todos los archivos</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<CloudFileResult[]> UpLoadFiles(string container, IEnumerable<IFormFile> files)
        {
            var task = files.Select(async file =>
            {
                //obtenemos el nombre origial del archivo
                var fileOriginName = Path.GetFileName(file.Name);
                //obtenemos la extison del archivo
                var extension = Path.GetExtension(file.Name);
                //establecemos un nombre del arhivo para que no se repita con un guid
                var nombreNewFile = $"{Guid.NewGuid()}{extension}";
                //creamos la carpeta
                string folder = Path.Combine(_env.WebRootPath,container);

                if (!Directory.Exists(folder)) {
                    Directory.CreateDirectory(folder);
                }
                string pathFile = Path.Combine(folder,nombreNewFile);
                using (var memoryStream = new MemoryStream()) {
                    await file.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    await File.WriteAllBytesAsync(pathFile,contenido);
                }
                //armamos la url 
                var url = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
                var urlArchivo = Path.Combine(url,container,nombreNewFile).Replace("\\","/");
                return new CloudFileResult
                {
                    URL = urlArchivo,
                    Titulo = fileOriginName
                };
            });
            //cuando se termine todo el procesamiento
            var result = await Task.WhenAll(task);
            return result;
        }
    }
}
