using System.ComponentModel.DataAnnotations;

namespace KnowCloud.Utility
{
    /// <summary>
    /// Clase que se encarga de realizar una validacion perzonalizada
    /// </summary>
    public class AllowedExtensionsAttribute: ValidationAttribute
    {
        private readonly string[] _extensions;
        public AllowedExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;    
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //configuramos que la variable venga envuelta en un objeto IFormFile
            var file = value as IFormFile;
            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                if (!_extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult("Esta extension no esta permitida");
                }
            }

            return ValidationResult.Success;
        }
    }
}
