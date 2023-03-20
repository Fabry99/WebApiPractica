using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebApiPractica.Models;

namespace WebApiPractica.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class estados_equipoController : ControllerBase
    {

        private readonly equiposContext _estadosContext;
        public estados_equipoController(equiposContext _estadoscontexto)
        {
            _estadosContext = _estadoscontexto;
        }

        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<estados_equipos> estados = (from e in _estadosContext.estados_equipo
                                             where e.estado == "A"
                                             select e).ToList();

            if (estados == null)
            {
                return NotFound();
            }
            return Ok(estados);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult Add(estados_equipos estados)
        {
            try
            {
                _estadosContext.Add(estados);
                _estadosContext.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

         
        }
        [HttpPut]
        [Route("update")]

        public IActionResult update (int id, estados_equipos estadosupdate)

        {
           estados_equipos? estados=(from e in _estadosContext.estados_equipo
                                     where e.id_estados_equipo==id
                                     select e).FirstOrDefault();
            if (estados==null)
            {
                return NotFound(id);
            }
            estados.descripcion=estadosupdate.descripcion;
            estados.estado=estadosupdate.estado;
            _estadosContext.Entry(estados).State = EntityState.Modified;
            _estadosContext.SaveChanges(true);
            return Ok(estadosupdate);
        }

        [HttpDelete]
        [Route("desactivate")]

        public IActionResult desactivate(int id)
        {
            estados_equipos? estados = (from e in _estadosContext.estados_equipo
                                        where e.id_estados_equipo == id
                                        select e).FirstOrDefault();

            if (estados==null)
            {
                return NotFound();
            }

            estados.estado = "I";

            _estadosContext.Entry(estados).State = EntityState.Modified;
            _estadosContext.SaveChanges();
            return Ok(estados);
        }

        [HttpDelete]
        [Route("Activate")]

        public IActionResult dActivate(int id)
        {
            estados_equipos? estados = (from e in _estadosContext.estados_equipo
                                        where e.id_estados_equipo == id
                                        select e).FirstOrDefault();

            if (estados == null)
            {
                return NotFound();
            }

            estados.estado = "A";

            _estadosContext.Entry(estados).State = EntityState.Modified;
            _estadosContext.SaveChanges();
            return Ok(estados);
        }
    }
}
