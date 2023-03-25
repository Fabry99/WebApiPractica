using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiPractica.Models;

namespace WebApiPractica.Controllers
{
    [Route("api/Controller")]
    [ApiController]
    public class carrerasController : ControllerBase
    {
        private readonly equiposContext _carrerascontexto;

        public carrerasController(equiposContext carrerascontext)
        {
            _carrerascontexto = carrerascontext;
        }

        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<carreras> carrera = (from e in _carrerascontexto.carreras
                                  select e).ToList();

            if (carrera.Count == 0)
            {
                return NotFound();
            }
            return Ok(carrera);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult Add(carreras carrera)
        {
            try
            {
                _carrerascontexto.carreras.Add(carrera);
                _carrerascontexto.SaveChanges();
                return Ok();

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Update")]

        public IActionResult update(int id, carreras updatecarrera)
        {
            carreras? carrera= (from e in _carrerascontexto.carreras
                             where e.carrera_id == id
                             select e).FirstOrDefault();

            if (carrera == null)
            {
                return NotFound();
            }
            carrera.nombre_carrera= updatecarrera.nombre_carrera;
            carrera.facultad_id= updatecarrera.facultad_id;


            _carrerascontexto.Entry(carrera).State = EntityState.Modified;
            _carrerascontexto.SaveChanges();
            return Ok(updatecarrera);
        }

        [HttpDelete]
        [Route("Delete")]

        public IActionResult Delete(int id)
        {
            carreras? carrera= (from e in _carrerascontexto.carreras
                             where e.carrera_id== id
                             select e).FirstOrDefault();

            if (carrera== null)
            {
                return NotFound();
            }
            _carrerascontexto.carreras.Attach(carrera);
            _carrerascontexto.carreras.Remove(carrera);
            _carrerascontexto.SaveChanges(true);

            return Ok(carrera);
        }
    }
}
