using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using WebApiPractica.Models;

namespace WebApiPractica.Controllers
{
    [Route("api/Controller")]
    [ApiController]
    public class reservasController : ControllerBase
    {
        private readonly equiposContext _reservascontexto;

        public reservasController(equiposContext reservascontext)
        {
            _reservascontexto = reservascontext;
        }

        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<reservas> reserva = (from e in _reservascontexto.reservas
                                      select e).ToList();

            if (reserva.Count == 0)
            {
                return NotFound();
            }
            return Ok(reserva);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult Add(reservas reserva)
        {
            try
            {
                _reservascontexto.reservas.Add(reserva);
                _reservascontexto.SaveChanges();
                return Ok();

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Update")]

        public IActionResult update (int id, reservas updatereserva)
        {
            reservas? reserva = (from e in _reservascontexto.reservas
                                 where e.reserva_id == id
                                 select e).FirstOrDefault();

            if (reserva == null)
            {
                return NotFound();
            }
            reserva.equipo_id= updatereserva.equipo_id;
            reserva.usuario_id= updatereserva.usuario_id;
            reserva.fecha_salida = updatereserva.fecha_salida;
            reserva.hora_salida = updatereserva.hora_salida;
            reserva.tiempo_reserva = updatereserva.tiempo_reserva;
            reserva.fecha_retorno = updatereserva.fecha_retorno;
            reserva.hora_retorno=updatereserva.hora_retorno;


            _reservascontexto.Entry(reserva).State = EntityState.Modified;
            _reservascontexto.SaveChanges();
            return Ok(updatereserva);
        }

        [HttpDelete]
        [Route("Delete")]

        public IActionResult Delete(int id)
        {
            reservas? reserva = (from e in _reservascontexto.reservas
                                 where e.reserva_id == id
                                 select e).FirstOrDefault();

            if (reserva== null)
            {
                return NotFound();
            }
            _reservascontexto.reservas.Attach(reserva);
            _reservascontexto.reservas.Remove(reserva);
            _reservascontexto.SaveChanges(true);

            return Ok(reserva);
        }
    }
}
