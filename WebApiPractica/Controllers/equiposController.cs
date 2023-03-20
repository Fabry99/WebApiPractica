using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiPractica.Models;
using Microsoft.EntityFrameworkCore;


namespace WebApiPractica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class equiposController : ControllerBase
    {
        private readonly equiposContext _equiposContexto;

        public equiposController(equiposContext equiposContexto)
        {
            _equiposContexto = equiposContexto; 
        }

        [HttpGet]
        [Route("GetAll")]

        public IActionResult GetAll()
        {
            //var marca= (from m in _equiposContexto.marcas
            //            where m.estados == "A"
            //            select m).ToList();



            var ListaEquipos = (from e in _equiposContexto.equipos
                                join m in _equiposContexto.marcas on e.marca_id equals m.id_marcas
                                join te in _equiposContexto.tipo_equipo on e.tipo_equipo_id equals te.id_tipo_equipo
                                where e.estado == "A"
                                select new
                                {
                                    e.id_equipos,
                                    e.nombre,
                                    e.descripcion,
                                    e.tipo_equipo_id,
                                    tipo_descripcion = te.descripcion,
                                    e.marca_id,
                                    m.nombre_marca,
                                }
                                ).ToList();

            if (ListaEquipos == null)
            {
                return NotFound();
            }
           

            return Ok(ListaEquipos);
        }

        [HttpGet]
        [Route("GetById/{id}")]

        public IActionResult Get(int id)
        {
            equipos? equipo = (from e in _equiposContexto.equipos
                               where e.id_equipos == id
                               select e).FirstOrDefault();

            if (equipo == null)
            {
                return NotFound();
            }
            return Ok(equipo);

        }
        [HttpGet]
        [Route("Find/{filtro}")]

        public IActionResult FindbyDescription(String filtro)
        {
            equipos? equipo = (from e in _equiposContexto.equipos
                               where e.descripcion.Contains(filtro)
                               select e).FirstOrDefault();

            if (equipo == null)
            {
                return NotFound();
            }
            return Ok(equipo);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarEquipo([FromBody] equipos equipo)
        {

            try
            {
                _equiposContexto.equipos.Add(equipo);
                _equiposContexto.SaveChanges();
                return Ok(equipo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }

        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult ActualizarEquipo(int id, [FromBody] equipos equipoModificar)
        {
            equipos? equiposActual = (from e in _equiposContexto.equipos
                                      where e.id_equipos == id
                                      select e).FirstOrDefault();
            if (equiposActual == null)
            {
                return NotFound(id);
            }

            equiposActual.nombre = equipoModificar.nombre;
            equiposActual.descripcion = equipoModificar.descripcion;
            equiposActual.marca_id = equipoModificar.marca_id;
            equiposActual.tipo_equipo_id = equipoModificar.tipo_equipo_id;
            equiposActual.anio_compra = equipoModificar.anio_compra;
            equiposActual.costo = equipoModificar.costo;

            _equiposContexto.Entry(equiposActual).State = EntityState.Modified;
            _equiposContexto.SaveChanges();
            return Ok(equipoModificar);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]

        public IActionResult InactividadEquipo(int id)
        {

            equipos? equipo = (from e in _equiposContexto.equipos
                               where e.id_equipos == id
                               select e).FirstOrDefault();

            if (equipo == null)
                return NotFound();

            equipo.estado = "I";

            _equiposContexto.Entry(equipo).State = EntityState.Modified;
            _equiposContexto.SaveChanges(true);

            return Ok(equipo);
        }

        [HttpDelete]
        [Route("Activar")]

        public IActionResult ActivarEquipo(int id)
        {

            equipos? equipo = (from e in _equiposContexto.equipos
                               where e.id_equipos == id
                               select e).FirstOrDefault();

            if (equipo == null)
                return NotFound();

            equipo.estado = "A";

            _equiposContexto.Entry(equipo).State = EntityState.Modified;
            _equiposContexto.SaveChanges(true);

            return Ok(equipo);
        }
    }

}
