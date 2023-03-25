using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiPractica.Models;

namespace WebApiPractica.Controllers
{
    [Route("api/Controller")]
    [ApiController]
    public class facultadesController : ControllerBase
    {
        private readonly equiposContext _facultadesContexto;

        public facultadesController(equiposContext facultadescontext)
        {
           _facultadesContexto = facultadescontext;
        }

        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<facultades> facultad= (from e in _facultadesContexto.facultades
                                      select e).ToList();

            if (facultad.Count == 0)
            {
                return NotFound();
            }
            return Ok(facultad);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult Add(facultades facultad)
        {
            try
            {
                _facultadesContexto.facultades.Add(facultad);
                _facultadesContexto.SaveChanges();
                return Ok();

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Update")]

        public IActionResult update(int id, facultades updatefacultad)
        {
            facultades? facultad = (from e in _facultadesContexto.facultades
                                 where e.facultad_id == id
                                 select e).FirstOrDefault();

            if (facultad == null)
            {
                return NotFound();
            }
            facultad.nombre_facultda= updatefacultad.nombre_facultda;
            


            _facultadesContexto.Entry(facultad).State = EntityState.Modified;
            _facultadesContexto.SaveChanges();
            return Ok(facultad);
        }

        [HttpDelete]
        [Route("Delete")]

        public IActionResult Delete(int id)
        {
            facultades? facultad = (from e in _facultadesContexto.facultades
                                 where e.facultad_id== id
                                 select e).FirstOrDefault();

            if (facultad== null)
            {
                return NotFound();
            }
            _facultadesContexto.facultades.Attach(facultad);
            _facultadesContexto.facultades.Remove(facultad);
            _facultadesContexto.SaveChanges(true);

            return Ok(facultad);
        }
    }
}
