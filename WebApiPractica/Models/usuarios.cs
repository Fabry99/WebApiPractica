using System.ComponentModel.DataAnnotations;

namespace WebApiPractica.Models
{
    public class usuarios
    {

        [Key]

        public int usuario_id { get; set; }

        public string nombre { get; set; }

        public string documentacion {get; set; } 

        public string tipo { get; set; }

        public string carnet { get; set; }

        public int carrera_id { get; set; }
    }
}
