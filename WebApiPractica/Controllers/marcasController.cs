using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WebApiPractica.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApiPractica.Controllers
{

    [Route("api/Controller")]
    [ApiController]
    public class marcasController : ControllerBase
    {
        private readonly equiposContext _marcasContexto;

        public marcasController(equiposContext marcascontexto)
        {
            _marcasContexto= marcascontexto;
        }

        [HttpGet]
        [Route("GetAll")]
        
        public IActionResult Get()
        {
            List<marcas> marca=(from e in _marcasContexto.marcas
                                select e).ToList();

            if (marca.Count==0)
            {
                return NotFound();
            }
            return Ok(marca);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult Add(marcas marca)
        {
            try
            {
                _marcasContexto.marcas.Add(marca);
                _marcasContexto.SaveChanges();
                return Ok();
    
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Update")]

        public IActionResult update(int id, marcas updatemarca)
        {
            marcas? marca = (from e in _marcasContexto.marcas
                             where e.id_marcas==id
                             select e).FirstOrDefault();

            if (marca==null)
            {
                return NotFound();
            }
            marca.nombre_marca = updatemarca.nombre_marca;
            marca.estados= updatemarca.estados;
            

            _marcasContexto.Entry(marca).State = EntityState.Modified;
            _marcasContexto.SaveChanges();
            return Ok(updatemarca);
        }

        [HttpDelete]
        [Route("Delete")]

        public IActionResult Delete(int id)
        {
            marcas? marca = (from e in _marcasContexto.marcas
                             where e.id_marcas == id
                             select e).FirstOrDefault();

            if (marca == null)
            {
                return NotFound();
            }
                _marcasContexto.marcas.Attach(marca);
                _marcasContexto.marcas.Remove(marca);
                _marcasContexto.SaveChanges(true);
            
            return Ok(marca);
        }
    }
}
