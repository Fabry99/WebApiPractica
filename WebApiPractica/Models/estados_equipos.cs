using System.ComponentModel.DataAnnotations;

namespace WebApiPractica.Models
{
    public class estados_equipos
    {

        [Key]
        public int id_estados_equipo { get; set; }

        public string descripcion {get; set;}

        public string estado {get; set;}
    }
}
