using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiPractica.Models;

namespace WebApiPractica.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class tipo_equipoController : ControllerBase
    {
       
        private readonly equiposContext _tipo_equipocontexto;

        public tipo_equipoController(equiposContext tipo_contexto)
        {
            _tipo_equipocontexto = tipo_contexto;
        }

        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<tipo_equipo> tipo = (from e in _tipo_equipocontexto.tipo_equipo
                                      select e).ToList();

            if (tipo.Count == 0)
            {
                return NotFound();
            }
            return Ok(tipo);
        }


        [HttpPost]
        [Route("Add")]

        public IActionResult add(tipo_equipo tipo)
        {

            try
            {
                _tipo_equipocontexto.Add(tipo);
                _tipo_equipocontexto.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost]
        [Route("Update")]

        public IActionResult update(int id, tipo_equipo updatetipo)
        {
            tipo_equipo? tipo_ = (from e in _tipo_equipocontexto.tipo_equipo
                                  where e.id_tipo_equipo == id
                                  select e).FirstOrDefault();

            if (tipo_ == null)
            {
                return NotFound(id);
            }

            tipo_.descripcion = updatetipo.descripcion;
            tipo_.estado = updatetipo.estado;

            _tipo_equipocontexto.Entry(tipo_).State = EntityState.Modified;
            _tipo_equipocontexto.SaveChanges();

            return Ok(updatetipo);
        }

        [HttpDelete]
        [Route("Delete")]

        public IActionResult delete(int id)
        {
            tipo_equipo? tipo_ = (from e in _tipo_equipocontexto.tipo_equipo
                                  where e.id_tipo_equipo == id
                                  select e).FirstOrDefault();

            if(tipo_ == null)
            {
                return NotFound();
            }
            _tipo_equipocontexto.Attach(tipo_);
            _tipo_equipocontexto.Remove(tipo_);
            _tipo_equipocontexto.SaveChanges(true);
            return Ok(tipo_);
        }

    }
}
