namespace KnowCloud.Models
{
    public class FileAttach
    {
        //este id lo puedo asociar a cualquier otro identificador de proceso.
        public int Id { get; set; }
        public DateTime FechaCreacion { get; set; }

        public string URL { get; set; }

        public string Titulo { get; set; }

        public int Orden { get; set; }
    }
}
