using System.ComponentModel.DataAnnotations;

namespace KnowCloud.Utility
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file != null)
            {

                if (file.Length > (_maxFileSize * 2048 * 2048))
                {
                    return new ValidationResult($"El tamaño maximo permitido del archivo es {_maxFileSize} MB.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
