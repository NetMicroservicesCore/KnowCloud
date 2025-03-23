﻿using KnowCloud.Contract;
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
            _env = env;
            _httpContextAccessor = httpContextAccessor;

        }
        public Task Delete(string path, string container)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Este metodo se encarga de subir un archivo  a traves del objeto IFormfile
        /// </summary>
        /// <param name="container">el contenedor del archivo o visto desde otro punto de vista es el nombre de la carpeta</param>
        /// <param name="files">los archivos cargados</param>
        /// <returns>una proyeccion de todos los archivos</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<CloudFileResult[]> UpLoadFiles(string container, IEnumerable<IFormFile> files)
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
            });
        }
    }
}
