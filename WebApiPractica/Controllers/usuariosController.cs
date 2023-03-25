using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using WebApiPractica.Models;

namespace WebApiPractica.Controllers
{
    [Route("api/Controller")]
    [ApiController]
    public class usuariosController : ControllerBase
    {
        private readonly equiposContext _usuarioscontexto;

        public usuariosController(equiposContext usuariosContext)
        {
            _usuarioscontexto = usuariosContext;
        }

        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<usuarios> usuario= (from e in _usuarioscontexto.usuarios
                                      select e).ToList();

            if (usuario.Count == 0)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult Add(usuarios usuario)
        {
            try
            {
                _usuarioscontexto.usuarios.Add(usuario);
                _usuarioscontexto.SaveChanges();
                return Ok();

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Update")]

        public IActionResult update(int id, usuarios updateusuario)
        {
            usuarios? usuario = (from e in _usuarioscontexto.usuarios
                                 where e.carrera_id == id
                                 select e).FirstOrDefault();

            if (usuario == null)
            {
                return NotFound();
            }
            usuario.nombre = updateusuario.nombre;
            usuario.documentacion = updateusuario.documentacion;
            usuario.tipo = updateusuario.tipo;
            usuario.carnet = updateusuario.carnet;
            usuario.carrera_id = updateusuario.carrera_id;


            _usuarioscontexto.Entry(usuario).State = EntityState.Modified;
            _usuarioscontexto.SaveChanges();
            return Ok(updateusuario);
        }

        [HttpDelete]
        [Route("Delete")]

        public IActionResult Delete(int id)
        {
            usuarios? usuario = (from e in _usuarioscontexto.usuarios
                                 where e.usuario_id== id
                                 select e).FirstOrDefault();

            if (usuario == null)
            {
                return NotFound();
            }
            _usuarioscontexto.usuarios.Attach(usuario);
            _usuarioscontexto.usuarios.Remove(usuario);
            _usuarioscontexto.SaveChanges(true);

            return Ok(usuarios);
        }
    }
}
